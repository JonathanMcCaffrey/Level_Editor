using System;

namespace Button
{
    partial class TextureEditorInterface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextureEditorInterface));
            this.iTextureEditor = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.itNew = new System.Windows.Forms.ToolStripButton();
            this.itOpen = new System.Windows.Forms.ToolStripButton();
            this.itSave = new System.Windows.Forms.ToolStripButton();
            this.itUndo = new System.Windows.Forms.ToolStripButton();
            this.itRedo = new System.Windows.Forms.ToolStripButton();
            this.itArrow = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // iTextureEditor
            // 
            this.iTextureEditor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iTextureEditor.Location = new System.Drawing.Point(45, 62);
            this.iTextureEditor.Name = "iTextureEditor";
            this.iTextureEditor.Size = new System.Drawing.Size(256, 256);
            this.iTextureEditor.TabIndex = 0;
            this.iTextureEditor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.iTextureEditor_Paint);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itNew,
            this.itOpen,
            this.itSave,
            this.itUndo,
            this.itRedo,
            this.itArrow});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(337, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // itNew
            // 
            this.itNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.itNew.Image = ((System.Drawing.Image)(resources.GetObject("itNew.Image")));
            this.itNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.itNew.Name = "itNew";
            this.itNew.Size = new System.Drawing.Size(23, 22);
            this.itNew.Text = "toolStripButton1";
            // 
            // itOpen
            // 
            this.itOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.itOpen.Image = ((System.Drawing.Image)(resources.GetObject("itOpen.Image")));
            this.itOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.itOpen.Name = "itOpen";
            this.itOpen.Size = new System.Drawing.Size(23, 22);
            this.itOpen.Text = "toolStripButton2";
            this.itOpen.Click += new System.EventHandler(this.itOpen_Click);
            // 
            // itSave
            // 
            this.itSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.itSave.Image = ((System.Drawing.Image)(resources.GetObject("itSave.Image")));
            this.itSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.itSave.Name = "itSave";
            this.itSave.Size = new System.Drawing.Size(23, 22);
            this.itSave.Text = "toolStripButton3";
            this.itSave.Click += new System.EventHandler(this.itSave_Click);
            // 
            // itUndo
            // 
            this.itUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.itUndo.Image = ((System.Drawing.Image)(resources.GetObject("itUndo.Image")));
            this.itUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.itUndo.Name = "itUndo";
            this.itUndo.Size = new System.Drawing.Size(29, 20);
            this.itUndo.Text = "toolStripButton4";
            // 
            // itRedo
            // 
            this.itRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.itRedo.Image = ((System.Drawing.Image)(resources.GetObject("itRedo.Image")));
            this.itRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.itRedo.Name = "itRedo";
            this.itRedo.Size = new System.Drawing.Size(29, 20);
            this.itRedo.Text = "toolStripButton5";
            // 
            // itArrow
            // 
            this.itArrow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.itArrow.Image = ((System.Drawing.Image)(resources.GetObject("itArrow.Image")));
            this.itArrow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.itArrow.Name = "itArrow";
            this.itArrow.Size = new System.Drawing.Size(29, 20);
            this.itArrow.Text = "toolStripButton6";
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton6});
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(337, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "toolStripButton4";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "toolStripButton5";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton6.Text = "toolStripButton6";
            // 
            // TextureEditorInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 352);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.iTextureEditor);
            this.Name = "TextureEditorInterface";
            this.Text = "TextureEditor";
            this.Load += new System.EventHandler(this.TextureEditor_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public void UpdateEditor()
        {
            Console.WriteLine(InputManager.Get().mouseTranslation); 
        }

        private System.Windows.Forms.Panel iTextureEditor;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton itNew;
        private System.Windows.Forms.ToolStripButton itOpen;
        private System.Windows.Forms.ToolStripButton itSave;
        private System.Windows.Forms.ToolStripButton itUndo;
        private System.Windows.Forms.ToolStripButton itRedo;
        private System.Windows.Forms.ToolStripButton itArrow;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;

    }
}