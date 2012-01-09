using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework;


namespace LevelEditor
{
    //<summary>
    // Includes the serialization of all game assets.
    //</summary>
    [Serializable]
    class AssetSerializer : ISerializable
    {
        #region Test Code
        [NonSerialized] // Test value
        private AssetSerializer m_AssetSerializer = new AssetSerializer();

        List<AssetSerializer> m_List = new List<AssetSerializer>();  //Test list. Remove later
        public List<AssetSerializer> List
        {
            get { return m_List; }
            set { m_List = value; }
        }

        // This method proves that the serialization in this manner works, and it works very well.
        void Test()
        {
            m_AssetSerializer = new AssetSerializer();
            m_AssetSerializer.List.Add(new AssetSerializer());
            m_AssetSerializer.List.Add(new AssetSerializer());
            m_AssetSerializer.List.Add(new AssetSerializer());
            m_AssetSerializer.List.Add(new AssetSerializer());
            m_AssetSerializer.List.Add(new AssetSerializer());
            m_AssetSerializer.List.Add(new AssetSerializer());
            m_AssetSerializer.List.Add(new AssetSerializer());
            m_AssetSerializer.List[0].FilePathToModel = "UltTest";
            m_AssetSerializer.FilePathToModel = "Test";
            m_AssetSerializer.FilePathToTexture = "OtherTest";
            m_AssetSerializer.IsCollidable = true;
            m_AssetSerializer.Rotation = Vector3.Up;
            m_AssetSerializer.Translation = Vector3.Right;
            m_AssetSerializer.Tint = Vector3.One;

            Stream stream = File.Open("Test.osl", FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, m_AssetSerializer);
            stream.Close();

            m_AssetSerializer = null;

            stream = File.Open("Test.osl", FileMode.Open);
            binaryFormatter = new BinaryFormatter();
            m_AssetSerializer = (AssetSerializer)binaryFormatter.Deserialize(stream);
            stream.Close();

            Console.WriteLine(m_AssetSerializer.FilePathToTexture.ToString());
            Console.WriteLine(m_AssetSerializer.List[0].FilePathToTexture.ToString());
        }
        #endregion

        #region Fields
        private string m_FilePathToModel = "Blank";
        private string m_FilePathToTexture = "Blank";
        private Vector3 m_Translation = Vector3.Zero;
        private Vector3 m_Rotation = Vector3.Zero;
        private Vector3 m_Scale = Vector3.Zero;
        private Vector3 m_Tint = Vector3.Zero;
        private bool m_IsCollidable = false;
        #endregion

        #region Properties
        public string FilePathToModel
        {
            get { return m_FilePathToModel; }
            set { m_FilePathToModel = value; }
        }
        public string FilePathToTexture
        {
            get { return m_FilePathToTexture; }
            set { m_FilePathToTexture = value; }
        }
        public Vector3 Translation
        {
            get { return m_Translation; }
            set { m_Translation = value; }
        }
        public Vector3 Rotation
        {
            get { return m_Rotation; }
            set { m_Rotation = value; }
        }
        public Vector3 Scale
        {
            get { return m_Scale; }
            set { m_Scale = value; }
        }
        public Vector3 Tint
        {
            get { return m_Tint; }
            set { m_Tint = value; }
        }
        public bool IsCollidable
        {
            get { return m_IsCollidable; }
            set { m_IsCollidable = value; }
        }
        #endregion

        #region Construction
        public AssetSerializer()
        {

        }

        public AssetSerializer(SerializationInfo aSerializationInfo, StreamingContext aStreamingContext)
        {
            List = (List<AssetSerializer>)aSerializationInfo.GetValue("List", typeof(List<AssetSerializer>));
            FilePathToModel = (string)aSerializationInfo.GetValue("FilePathToModel", typeof(string));
            FilePathToTexture = (string)aSerializationInfo.GetValue("FilePathToTexture", typeof(string));
            Translation = (Vector3)aSerializationInfo.GetValue("Translation", typeof(Vector3));
            Rotation = (Vector3)aSerializationInfo.GetValue("Rotation", typeof(Vector3));
            Scale = (Vector3)aSerializationInfo.GetValue("Scale", typeof(Vector3));
            Tint = (Vector3)aSerializationInfo.GetValue("Tint", typeof(Vector3));
            IsCollidable = (bool)aSerializationInfo.GetValue("IsCollidable", typeof(bool));
        }

        public void GetObjectData(SerializationInfo aSerializationInfo, StreamingContext aStreamingContext)
        {
            aSerializationInfo.AddValue("List", List);
            aSerializationInfo.AddValue("FilePathToModel", FilePathToModel);
            aSerializationInfo.AddValue("FilePathToTexture", FilePathToTexture);
            aSerializationInfo.AddValue("Translation", Translation);
            aSerializationInfo.AddValue("Rotation", Rotation);
            aSerializationInfo.AddValue("Scale", Scale);
            aSerializationInfo.AddValue("Tint", Tint);
            aSerializationInfo.AddValue("IsCollidable", IsCollidable);
        }
        #endregion
    }
}
