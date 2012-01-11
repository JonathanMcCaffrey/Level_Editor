using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace LevelEditor
{
    //<summary>
    // Gizmo controls. Code originally taken from codeplex.
    //</summary>
    class GizmoComponent
    {
        #region Constants
        private const float BOUNDINGBOX_THICKNESS = 0.05f;
        private const float LINE_LENGTH = 3.0f;
        private const float LINE_OFFSET = 0.5f;
        private const float LINE_OFFSET_DOUBLED = LINE_OFFSET * 2;
        private const float BOUNDINGSPHERE_RADIUS = 1.0f;
        private const float SCALE_FACTOR = 25.0f;
        #endregion

        #region Enums
        private enum GizmoAxis
        {
            X,
            Y,
            Z,
            XY,
            ZX,
            YZ,
            NONE
        }

        private enum GizmoMode
        {
            TRANSLATE,
            ROTATE,
            NON_LINEAR_SCALE,
            SCALE
        }

        private enum TransformSpace // Not implemeneted
        {
            LOCAL,
            WORLD
        }

        private enum PivotType
        {
            OBJECT_CENTER,
            SELECTION_CENTER,
            WORLD_CENTER
        }
        #endregion

        #region Fields
        private bool m_IsEnabled = false;

        private BasicEffect m_LineEffect = null;
        private BasicEffect m_QuadEffect = null;
        private Quad[] m_Quads = {  new Quad(new Vector3(LINE_OFFSET, LINE_OFFSET, 0), Vector3.Backward, Vector3.Up, LINE_OFFSET_DOUBLED, LINE_OFFSET_DOUBLED), //XY
                                    new Quad(new Vector3(LINE_OFFSET, 0, LINE_OFFSET), Vector3.Up, Vector3.Right, LINE_OFFSET_DOUBLED, LINE_OFFSET_DOUBLED),    //XZ
                                    new Quad(new Vector3(0, LINE_OFFSET, LINE_OFFSET), Vector3.Right, Vector3.Up, LINE_OFFSET_DOUBLED, LINE_OFFSET_DOUBLED) };  //ZY

        private VertexPositionColor[] m_TranslationLineVertices;

        private Color[] m_AxisColors = { Color.Red, Color.Blue, Color.Gray };
        private Color m_HighlightColor = Color.Yellow;

        private Matrix m_ScreenScaleMatrix = Matrix.Identity;
        private float m_ScreenScale = 0.0f;

        private Vector3 m_Position = Vector3.Zero;
        private Matrix m_RotationMatrix = Matrix.Identity;

        private Matrix m_ObjectOrientedWorld = Matrix.Identity;
        private Matrix m_AxisAlignedWorld = Matrix.Identity;
        private Matrix[] m_ModelLocalSpace = {  Matrix.CreateWorld(new Vector3(LINE_LENGTH, 0, 0), Vector3.Left, Vector3.Up),
                                                Matrix.CreateWorld(new Vector3(0, LINE_LENGTH, 0), Vector3.Down, Vector3.Left),
                                                Matrix.CreateWorld(new Vector3(0, 0, LINE_LENGTH), Vector3.Forward, Vector3.Up)};

        private Matrix m_GizmoWorld = Matrix.Identity;
        private Matrix m_SceneWorld = Matrix.Identity;

        private Model m_TranslationModel = GameFiles.LoadModel("gizmo_translate");
        private Model m_RotationModel = GameFiles.LoadModel("gizmo_rotate");
        private Model m_ScaleModel = GameFiles.LoadModel("gizmo_scale");

        private Vector3 m_LocalForward = Vector3.Forward;
        private Vector3 m_LocalUp = Vector3.Up;
        private Vector3 m_LocalRight = Vector3.Right;

        private GizmoAxis m_ActiveAxis = GizmoAxis.NONE;
        private GizmoMode m_ActiveMode = GizmoMode.TRANSLATE;
        private PivotType m_ActivePivot = PivotType.OBJECT_CENTER;

        private float m_TranslationDelta;

        public List<Tile> m_GizmoSelection = new List<Tile>();
        #endregion

        #region Construction
        public GizmoComponent()
        {
            List<VertexPositionColor> tempVertexData = new List<VertexPositionColor>(18);

            m_GizmoSelection = new List<Tile>();
            m_GizmoSelection.Add(new Tile());
            GameFiles.GizmoSelection = m_GizmoSelection;

            m_LineEffect = new BasicEffect(GameFiles.GraphicsDevice);
            m_QuadEffect = new BasicEffect(GameFiles.GraphicsDevice);

            // X
            tempVertexData.Add(new VertexPositionColor(new Vector3(LINE_OFFSET, 0, 0), m_AxisColors[0]));
            tempVertexData.Add(new VertexPositionColor(new Vector3(LINE_LENGTH, 0, 0), m_AxisColors[0]));

            tempVertexData.Add(new VertexPositionColor(new Vector3(LINE_OFFSET_DOUBLED, 0, 0), m_AxisColors[0]));
            tempVertexData.Add(new VertexPositionColor(new Vector3(LINE_OFFSET_DOUBLED, LINE_OFFSET_DOUBLED, 0), m_AxisColors[0]));

            tempVertexData.Add(new VertexPositionColor(new Vector3(LINE_OFFSET_DOUBLED, 0, 0), m_AxisColors[0]));
            tempVertexData.Add(new VertexPositionColor(new Vector3(LINE_OFFSET_DOUBLED, 0, LINE_OFFSET_DOUBLED), m_AxisColors[0]));

            // Y
            tempVertexData.Add(new VertexPositionColor(new Vector3(0, LINE_OFFSET, 0), m_AxisColors[1]));
            tempVertexData.Add(new VertexPositionColor(new Vector3(0, LINE_LENGTH, 0), m_AxisColors[1]));

            tempVertexData.Add(new VertexPositionColor(new Vector3(0, LINE_OFFSET_DOUBLED, 0), m_AxisColors[1]));
            tempVertexData.Add(new VertexPositionColor(new Vector3(LINE_OFFSET_DOUBLED, LINE_OFFSET_DOUBLED, 0), m_AxisColors[1]));

            tempVertexData.Add(new VertexPositionColor(new Vector3(0, LINE_OFFSET_DOUBLED, 0), m_AxisColors[1]));
            tempVertexData.Add(new VertexPositionColor(new Vector3(0, LINE_OFFSET_DOUBLED, LINE_OFFSET_DOUBLED), m_AxisColors[1]));

            // Z
            tempVertexData.Add(new VertexPositionColor(new Vector3(0, 0, LINE_OFFSET), m_AxisColors[2]));
            tempVertexData.Add(new VertexPositionColor(new Vector3(0, 0, LINE_LENGTH), m_AxisColors[2]));

            tempVertexData.Add(new VertexPositionColor(new Vector3(0, 0, LINE_OFFSET_DOUBLED), m_AxisColors[2]));
            tempVertexData.Add(new VertexPositionColor(new Vector3(LINE_OFFSET_DOUBLED, 0, LINE_OFFSET_DOUBLED), m_AxisColors[2]));

            tempVertexData.Add(new VertexPositionColor(new Vector3(0, 0, LINE_OFFSET_DOUBLED), m_AxisColors[0]));
            tempVertexData.Add(new VertexPositionColor(new Vector3(0, LINE_OFFSET_DOUBLED, LINE_OFFSET_DOUBLED), m_AxisColors[2]));

            m_TranslationLineVertices = tempVertexData.ToArray();
        }
        #endregion

        #region Methods
        #region Update
        public void Update(GameTime a_GameTime)
        {
            if (m_IsEnabled == false) 
            {
                if (m_GizmoSelection == null || m_GizmoSelection.Count < 1)
                {
                    m_IsEnabled = false;
                }
                else
                {
                    m_IsEnabled = true;
                }

                return;
            }

            m_TranslationDelta = (float)a_GameTime.ElapsedGameTime.TotalSeconds;

            SetPosition();

            Vector3 tempLength = GameFiles.CameraPosition - m_Position;
            m_ScreenScale = tempLength.Length() / SCALE_FACTOR;
            m_ScreenScaleMatrix = Matrix.CreateScale(new Vector3(m_ScreenScale));

            m_LocalForward = m_GizmoSelection[0].Forward;
            m_LocalUp = m_GizmoSelection[0].Forward;
            m_LocalRight = Vector3.Cross(m_LocalForward, m_LocalUp);

            m_ObjectOrientedWorld = m_ScreenScaleMatrix * Matrix.CreateWorld(m_Position, m_LocalForward, m_LocalUp);
            m_AxisAlignedWorld = m_ScreenScaleMatrix * Matrix.CreateWorld(m_Position, m_SceneWorld.Forward, m_SceneWorld.Up);


            m_GizmoWorld = m_AxisAlignedWorld;
            m_RotationMatrix.Forward = m_SceneWorld.Forward;
            m_RotationMatrix.Up = m_SceneWorld.Up;
            m_RotationMatrix.Right = m_SceneWorld.Right;

            /*  // If Local Translations get supported at a later date.
            m_GizmoWorld = m_ObjectOrientedWorld;
            m_RotationMatrix.Forward = m_LocalForward;
            m_RotationMatrix.Up = m_LocalUp;
            m_RotationMatrix.Right = m_LocalRight;
             */

            ApplyColor(GizmoAxis.X, m_AxisColors[0]);
            ApplyColor(GizmoAxis.X, m_AxisColors[1]);
            ApplyColor(GizmoAxis.X, m_AxisColors[2]);
            ApplyColor(m_ActiveAxis, m_HighlightColor);
        }
        #endregion

        #region Draw
        public void Draw3D()
        {
            return;

            if (m_IsEnabled == false) { return; }

            m_ActiveMode = GizmoMode.TRANSLATE;
            m_ActiveAxis = GizmoAxis.XY;
            m_ActivePivot = PivotType.OBJECT_CENTER;

            GameFiles.GraphicsDevice.DepthStencilState = DepthStencilState.None;

            m_LineEffect.World = m_GizmoWorld;
            m_LineEffect.View = GameFiles.ViewMatrix;
            m_LineEffect.Projection = GameFiles.ProjectionMatrix;

            m_LineEffect.CurrentTechnique.Passes[0].Apply();
            GameFiles.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, m_TranslationLineVertices, 0, m_TranslationLineVertices.Length / 2);

            if (m_ActiveMode == GizmoMode.TRANSLATE || m_ActiveMode == GizmoMode.NON_LINEAR_SCALE)
            {
                if (m_ActiveAxis == GizmoAxis.XY || m_ActiveAxis == GizmoAxis.YZ || m_ActiveAxis == GizmoAxis.ZX)
                {
                    GameFiles.GraphicsDevice.BlendState = BlendState.AlphaBlend;
                    GameFiles.GraphicsDevice.RasterizerState = RasterizerState.CullNone;

                    m_QuadEffect.World = m_GizmoWorld;
                    m_QuadEffect.View = GameFiles.ViewMatrix;
                    m_QuadEffect.Projection = GameFiles.ProjectionMatrix;

                    m_QuadEffect.CurrentTechnique.Passes[0].Apply();

                    Quad activeQuad = new Quad();
                    switch (m_ActiveAxis)
                    {
                        case GizmoAxis.XY:
                            activeQuad = m_Quads[0];
                            break;
                        case GizmoAxis.ZX:
                            activeQuad = m_Quads[1];
                            break;
                        case GizmoAxis.YZ:
                            activeQuad = m_Quads[2];
                            break;
                    }

                    GameFiles.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList,
                        activeQuad.Vertices, 0, 4,
                        activeQuad.Indexes, 0, 2);

                    GameFiles.GraphicsDevice.BlendState = BlendState.Opaque;
                    GameFiles.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
                }

                if (m_ActiveMode == GizmoMode.TRANSLATE)
                {
                    for (int loop = 0; loop < 3; loop++)
                    {
                        for (int meshLoop = 0; meshLoop < m_TranslationModel.Meshes.Count; meshLoop++)
                        {
                            ModelMesh tempModelMesh = m_TranslationModel.Meshes[meshLoop];
                            for (int meshPartLoop = 0; meshPartLoop < m_TranslationModel.Meshes.Count; meshPartLoop++)
                            {
                                ModelMeshPart tempModelMeshPart = tempModelMesh.MeshParts[meshPartLoop];

                                BasicEffect effect = (BasicEffect)tempModelMeshPart.Effect;
                                Vector3 color = m_AxisColors[loop].ToVector3();

                                effect.World = m_ModelLocalSpace[loop] * m_GizmoWorld;
                                effect.DiffuseColor = color;
                                effect.EmissiveColor = color;

                                effect.EnableDefaultLighting();

                                effect.View = GameFiles.ViewMatrix;
                                effect.Projection = GameFiles.ProjectionMatrix;
                            }

                            tempModelMesh.Draw();
                        }
                    }
                }
                else
                {
                    for (int loop = 0; loop < 3; loop++)
                    {
                        for (int meshLoop = 0; meshLoop < m_ScaleModel.Meshes.Count; meshLoop++)
                        {
                            ModelMesh tempModelMesh = m_ScaleModel.Meshes[meshLoop];
                            for (int meshPartLoop = 0; meshPartLoop < m_ScaleModel.Meshes.Count; meshPartLoop++)
                            {
                                ModelMeshPart tempModelMeshPart = tempModelMesh.MeshParts[meshPartLoop];

                                BasicEffect effect = (BasicEffect)tempModelMeshPart.Effect;
                                Vector3 color = m_AxisColors[meshPartLoop].ToVector3();

                                effect.World = m_ModelLocalSpace[meshPartLoop] * m_GizmoWorld;
                                effect.DiffuseColor = color;
                                effect.EmissiveColor = color;

                                effect.EnableDefaultLighting();

                                effect.View = GameFiles.ViewMatrix;
                                effect.Projection = GameFiles.ProjectionMatrix;
                            }

                            tempModelMesh.Draw();
                        }
                    }
                }
            }
            else if (m_ActiveMode == GizmoMode.ROTATE)
            {
                for (int loop = 0; loop < 3; loop++)
                {
                    for (int meshLoop = 0; meshLoop < m_RotationModel.Meshes.Count; meshLoop++)
                    {
                        ModelMesh tempModelMesh = m_RotationModel.Meshes[meshLoop];
                        for (int meshPartLoop = 0; meshPartLoop < m_RotationModel.Meshes.Count; meshPartLoop++)
                        {
                            ModelMeshPart tempModelMeshPart = tempModelMesh.MeshParts[meshPartLoop];

                            BasicEffect effect = (BasicEffect)tempModelMeshPart.Effect;
                            Vector3 color = m_AxisColors[meshPartLoop].ToVector3();

                            effect.World = m_ModelLocalSpace[meshPartLoop] * m_GizmoWorld;
                            effect.DiffuseColor = color;
                            effect.EmissiveColor = color;

                            effect.EnableDefaultLighting();

                            effect.View = GameFiles.ViewMatrix;
                            effect.Projection = GameFiles.ProjectionMatrix;
                        }

                        tempModelMesh.Draw();
                    }
                }
            }
            else if (m_ActiveMode == GizmoMode.SCALE)
            {
                for (int loop = 0; loop < 3; loop++)
                {
                    for (int meshLoop = 0; meshLoop < m_RotationModel.Meshes.Count; meshLoop++)
                    {
                        ModelMesh tempModelMesh = m_RotationModel.Meshes[meshLoop];
                        for (int meshPartLoop = 0; meshPartLoop < m_RotationModel.Meshes.Count; meshPartLoop++)
                        {
                            ModelMeshPart tempModelMeshPart = tempModelMesh.MeshParts[meshPartLoop];

                            BasicEffect effect = (BasicEffect)tempModelMeshPart.Effect;
                            Vector3 color = m_AxisColors[meshPartLoop].ToVector3();

                            effect.World = m_ModelLocalSpace[meshPartLoop] * m_GizmoWorld;
                            effect.DiffuseColor = color;
                            effect.EmissiveColor = color;

                            effect.EnableDefaultLighting();

                            effect.View = GameFiles.ViewMatrix;
                            effect.Projection = GameFiles.ProjectionMatrix;
                        }

                        tempModelMesh.Draw();
                    }
                }

                if (m_ActiveAxis != GizmoAxis.NONE)
                {
                    GameFiles.GraphicsDevice.BlendState = BlendState.AlphaBlend;
                    GameFiles.GraphicsDevice.RasterizerState = RasterizerState.CullNone;

                    m_QuadEffect.World = m_GizmoWorld;
                    m_QuadEffect.View = GameFiles.ViewMatrix;
                    m_QuadEffect.Projection = GameFiles.ProjectionMatrix;

                    m_QuadEffect.CurrentTechnique.Passes[0].Apply();

                    for (int loop = 0; loop < m_Quads.Length; loop++)
                    {
                        GameFiles.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList,
                            m_Quads[loop].Vertices, 0, 4,
                            m_Quads[loop].Indexes, 0, 2);
                    }

                    GameFiles.GraphicsDevice.BlendState = BlendState.Opaque;
                    GameFiles.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
                }
            }

            GameFiles.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        }
        #endregion

        #region Apply Color
        private void ApplyColor(GizmoAxis a_GizmoAxis, Color a_Color)
        {
            if (m_ActiveMode == GizmoMode.TRANSLATE || m_ActiveMode == GizmoMode.NON_LINEAR_SCALE)
            {
                switch (a_GizmoAxis)
                {
                    case GizmoAxis.X:
                        ApplyLineColor(0, 6, a_Color);
                        break;
                    case GizmoAxis.Y:
                        ApplyLineColor(6, 6, a_Color);
                        break;
                    case GizmoAxis.Z:
                        ApplyLineColor(12, 6, a_Color);
                        break;
                    case GizmoAxis.XY:
                        ApplyLineColor(0, 4, a_Color);
                        ApplyLineColor(6, 4, a_Color);
                        break;
                    case GizmoAxis.YZ:
                        ApplyLineColor(6, 2, a_Color);
                        ApplyLineColor(12, 2, a_Color);
                        ApplyLineColor(10, 2, a_Color);
                        ApplyLineColor(16, 2, a_Color);
                        break;
                    case GizmoAxis.ZX:
                        ApplyLineColor(0, 2, a_Color);
                        ApplyLineColor(4, 2, a_Color);
                        ApplyLineColor(12, 4, a_Color);
                        break;
                }
            }
            else if (m_ActiveMode == GizmoMode.ROTATE)
            {
                switch (a_GizmoAxis)
                {
                    case GizmoAxis.X:
                        ApplyLineColor(0, 6, a_Color);
                        break;
                    case GizmoAxis.Y:
                        ApplyLineColor(6, 6, a_Color);
                        break;
                    case GizmoAxis.Z:
                        ApplyLineColor(12, 6, a_Color);
                        break;
                }
            }
            else if (m_ActiveMode == GizmoMode.SCALE)
            {
                if (m_ActiveAxis == GizmoAxis.NONE)
                {
                    ApplyLineColor(0, m_TranslationLineVertices.Length, m_AxisColors[0]);
                }
                else
                {
                    ApplyLineColor(0, m_TranslationLineVertices.Length, m_HighlightColor);
                }
            }
        }

        private void ApplyLineColor(int a_StartIndex, int a_Count, Color a_Color)
        {
            for (int loop = a_StartIndex; loop < (a_StartIndex + a_Count); loop++)
            {
                m_TranslationLineVertices[loop].Color = a_Color;
            }
        }
        #endregion

        #region Set Position
        private void SetPosition()
        {

            switch (m_ActivePivot)
            {
                case PivotType.OBJECT_CENTER:
                    {
                        if (m_GizmoSelection.Count > 0)
                            m_Position = m_GizmoSelection[0].WorldPosition;
                    }
                    break;
                case PivotType.SELECTION_CENTER:
                    {
                        Vector3 tempCenter = Vector3.Zero;

                        for (int loop = 0; loop < m_GizmoSelection.Count; loop++)
                        {
                            tempCenter += m_GizmoSelection[loop].WorldPosition;
                        }
                        tempCenter /= m_GizmoSelection.Count;

                        m_Position = tempCenter;
                    }
                    break;
                case PivotType.WORLD_CENTER:
                    {
                        m_Position = m_SceneWorld.Translation;
                    }
                    break;
            }
        }
        #endregion

        #region Change Mode
        public void TranslateMode()
        {
            m_ActiveMode = GizmoMode.TRANSLATE;
        }
        public void RotateMode()
        {
            m_ActiveMode = GizmoMode.ROTATE;
        }
        public void ScaleMode()
        {
            m_ActiveMode = GizmoMode.SCALE;
        }
        public void NonLinearScaleMode()
        {
            m_ActiveMode = GizmoMode.NON_LINEAR_SCALE;
        }
        #endregion
        #endregion

        #region Private
        private Quaternion RotationQuaternion
        {
            get { return Quaternion.CreateFromRotationMatrix(m_RotationMatrix); }
        }

        private BoundingOrientedBox BoundingOrientedBoxX
        {
            get
            {
                return new BoundingOrientedBox(Vector3.Transform(new Vector3((LINE_LENGTH / 2) + (LINE_OFFSET * 2), 0, 0), m_GizmoWorld),
                    Vector3.Transform(new Vector3(LINE_LENGTH / 2, 0.2f, 0.2f), m_ScreenScaleMatrix), RotationQuaternion);
            }
        }
        private BoundingOrientedBox BoundingOrientedBoxY
        {
            get
            {
                return new BoundingOrientedBox(Vector3.Transform(new Vector3(0, (LINE_LENGTH / 2) + (LINE_OFFSET * 2), 0), m_GizmoWorld),
                    Vector3.Transform(new Vector3(0.2f, LINE_LENGTH / 2, 0.2f), m_ScreenScaleMatrix), RotationQuaternion);
            }
        }
        private BoundingOrientedBox BoundingOrientedBoxZ
        {
            get
            {
                return new BoundingOrientedBox(Vector3.Transform(new Vector3(0, 0, (LINE_LENGTH / 2) + (LINE_OFFSET * 2)), m_GizmoWorld),
                    Vector3.Transform(new Vector3(0.2f, 0.2f, LINE_LENGTH / 2), m_ScreenScaleMatrix), RotationQuaternion);
            }
        }
        private BoundingOrientedBox BoundingOrientedBoxZX
        {
            get
            {
                return new BoundingOrientedBox(Vector3.Transform(new Vector3(LINE_OFFSET, 0, LINE_OFFSET), m_GizmoWorld),
                    Vector3.Transform(new Vector3(LINE_OFFSET, BOUNDINGBOX_THICKNESS, LINE_OFFSET), m_ScreenScaleMatrix), RotationQuaternion);
            }
        }
        private BoundingOrientedBox BoundingOrientedBoxXY
        {
            get
            {
                return new BoundingOrientedBox(Vector3.Transform(new Vector3(LINE_OFFSET, LINE_OFFSET, 0), m_GizmoWorld),
                    Vector3.Transform(new Vector3(LINE_OFFSET, LINE_OFFSET, BOUNDINGBOX_THICKNESS), m_ScreenScaleMatrix), RotationQuaternion);
            }
        }
        private BoundingOrientedBox BoundingOrientedBoxYZ
        {
            get
            {
                return new BoundingOrientedBox(Vector3.Transform(new Vector3(0, LINE_OFFSET, LINE_OFFSET), m_GizmoWorld),
                    Vector3.Transform(new Vector3(BOUNDINGBOX_THICKNESS, LINE_OFFSET, LINE_OFFSET), m_ScreenScaleMatrix), RotationQuaternion);
            }
        }

        private BoundingSphere BoundingSphereX
        {
            get { return new BoundingSphere(Vector3.Transform(m_TranslationLineVertices[1].Position, m_GizmoWorld), BOUNDINGSPHERE_RADIUS * m_ScreenScale); }
        }

        private BoundingSphere BoundingSphereY
        {
            get { return new BoundingSphere(Vector3.Transform(m_TranslationLineVertices[7].Position, m_GizmoWorld), BOUNDINGSPHERE_RADIUS * m_ScreenScale); }
        }

        private BoundingSphere BoundingSphereZ
        {
            get { return new BoundingSphere(Vector3.Transform(m_TranslationLineVertices[13].Position, m_GizmoWorld), BOUNDINGSPHERE_RADIUS * m_ScreenScale); }
        }
        #endregion
    }
}
