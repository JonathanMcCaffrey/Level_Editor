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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.itNew = new System.Windows.Forms.ToolStripButton();
            this.itOpen = new System.Windows.Forms.ToolStripButton();
            this.itSave = new System.Windows.Forms.ToolStripButton();
            this.itUndo = new System.Windows.Forms.ToolStripButton();
            this.itRedo = new System.Windows.Forms.ToolStripButton();
            this.itArrow = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.itAddSolid = new System.Windows.Forms.ToolStripButton();
            this.itAddDiffuse = new System.Windows.Forms.ToolStripButton();
            this.itSubtractSolid = new System.Windows.Forms.ToolStripButton();
            this.itSubtractDiffuse = new System.Windows.Forms.ToolStripButton();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.it0 = new System.Windows.Forms.ToolStripButton();
            this.it1 = new System.Windows.Forms.ToolStripButton();
            this.it2 = new System.Windows.Forms.ToolStripButton();
            this.it3 = new System.Windows.Forms.ToolStripButton();
            this.it4 = new System.Windows.Forms.ToolStripButton();
            this.it5 = new System.Windows.Forms.ToolStripButton();
            this.it6 = new System.Windows.Forms.ToolStripButton();
            this.it7 = new System.Windows.Forms.ToolStripButton();
            this.it8 = new System.Windows.Forms.ToolStripButton();
            this.it9 = new System.Windows.Forms.ToolStripButton();
            this.iTextureGraphic = new System.Windows.Forms.PictureBox();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.toolStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iTextureGraphic)).BeginInit();
            this.SuspendLayout();
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
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(16, 0, 1, 0);
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
            this.itNew.Click += new System.EventHandler(this.itNew_Click);
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
            this.itUndo.Size = new System.Drawing.Size(23, 22);
            this.itUndo.Text = "toolStripButton4";
            // 
            // itRedo
            // 
            this.itRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.itRedo.Image = ((System.Drawing.Image)(resources.GetObject("itRedo.Image")));
            this.itRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.itRedo.Name = "itRedo";
            this.itRedo.Size = new System.Drawing.Size(23, 22);
            this.itRedo.Text = "toolStripButton5";
            // 
            // itArrow
            // 
            this.itArrow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.itArrow.Image = ((System.Drawing.Image)(resources.GetObject("itArrow.Image")));
            this.itArrow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.itArrow.Name = "itArrow";
            this.itArrow.Size = new System.Drawing.Size(23, 22);
            this.itArrow.Text = "toolStripButton6";
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itAddSolid,
            this.itAddDiffuse,
            this.itSubtractSolid,
            this.itSubtractDiffuse});
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.toolStrip2.Size = new System.Drawing.Size(337, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // itAddSolid
            // 
            this.itAddSolid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.itAddSolid.Image = ((System.Drawing.Image)(resources.GetObject("itAddSolid.Image")));
            this.itAddSolid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.itAddSolid.Name = "itAddSolid";
            this.itAddSolid.Size = new System.Drawing.Size(23, 22);
            this.itAddSolid.Text = "toolStripButton1";
            // 
            // itAddDiffuse
            // 
            this.itAddDiffuse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.itAddDiffuse.Image = ((System.Drawing.Image)(resources.GetObject("itAddDiffuse.Image")));
            this.itAddDiffuse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.itAddDiffuse.Name = "itAddDiffuse";
            this.itAddDiffuse.Size = new System.Drawing.Size(23, 22);
            this.itAddDiffuse.Text = "toolStripButton2";
            // 
            // itSubtractSolid
            // 
            this.itSubtractSolid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.itSubtractSolid.Image = ((System.Drawing.Image)(resources.GetObject("itSubtractSolid.Image")));
            this.itSubtractSolid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.itSubtractSolid.Name = "itSubtractSolid";
            this.itSubtractSolid.Size = new System.Drawing.Size(23, 22);
            this.itSubtractSolid.Text = "toolStripButton3";
            // 
            // itSubtractDiffuse
            // 
            this.itSubtractDiffuse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.itSubtractDiffuse.Image = ((System.Drawing.Image)(resources.GetObject("itSubtractDiffuse.Image")));
            this.itSubtractDiffuse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.itSubtractDiffuse.Name = "itSubtractDiffuse";
            this.itSubtractDiffuse.Size = new System.Drawing.Size(23, 22);
            this.itSubtractDiffuse.Text = "toolStripButton4";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(163, 29);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown1.TabIndex = 3;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.it0,
            this.it1,
            this.it2,
            this.it3,
            this.it4,
            this.it5,
            this.it6,
            this.it7,
            this.it8,
            this.it9});
            this.toolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip3.Location = new System.Drawing.Point(11, 62);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip3.Size = new System.Drawing.Size(23, 232);
            this.toolStrip3.TabIndex = 4;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // it0
            // 
            this.it0.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.it0.Image = ((System.Drawing.Image)(resources.GetObject("it0.Image")));
            this.it0.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.it0.Name = "it0";
            this.it0.Size = new System.Drawing.Size(22, 20);
            this.it0.Text = "toolStripButton7";
            // 
            // it1
            // 
            this.it1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.it1.Image = ((System.Drawing.Image)(resources.GetObject("it1.Image")));
            this.it1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.it1.Name = "it1";
            this.it1.Size = new System.Drawing.Size(22, 20);
            this.it1.Text = "toolStripButton8";
            // 
            // it2
            // 
            this.it2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.it2.Image = ((System.Drawing.Image)(resources.GetObject("it2.Image")));
            this.it2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.it2.Name = "it2";
            this.it2.Size = new System.Drawing.Size(22, 20);
            this.it2.Text = "toolStripButton9";
            // 
            // it3
            // 
            this.it3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.it3.Image = ((System.Drawing.Image)(resources.GetObject("it3.Image")));
            this.it3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.it3.Name = "it3";
            this.it3.Size = new System.Drawing.Size(22, 20);
            this.it3.Text = "toolStripButton10";
            // 
            // it4
            // 
            this.it4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.it4.Image = ((System.Drawing.Image)(resources.GetObject("it4.Image")));
            this.it4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.it4.Name = "it4";
            this.it4.Size = new System.Drawing.Size(22, 20);
            this.it4.Text = "toolStripButton11";
            // 
            // it5
            // 
            this.it5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.it5.Image = ((System.Drawing.Image)(resources.GetObject("it5.Image")));
            this.it5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.it5.Name = "it5";
            this.it5.Size = new System.Drawing.Size(22, 20);
            this.it5.Text = "toolStripButton12";
            // 
            // it6
            // 
            this.it6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.it6.Image = ((System.Drawing.Image)(resources.GetObject("it6.Image")));
            this.it6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.it6.Name = "it6";
            this.it6.Size = new System.Drawing.Size(22, 20);
            this.it6.Text = "toolStripButton13";
            // 
            // it7
            // 
            this.it7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.it7.Image = ((System.Drawing.Image)(resources.GetObject("it7.Image")));
            this.it7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.it7.Name = "it7";
            this.it7.Size = new System.Drawing.Size(22, 20);
            this.it7.Text = "toolStripButton14";
            // 
            // it8
            // 
            this.it8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.it8.Image = ((System.Drawing.Image)(resources.GetObject("it8.Image")));
            this.it8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.it8.Name = "it8";
            this.it8.Size = new System.Drawing.Size(22, 20);
            this.it8.Text = "toolStripButton15";
            // 
            // it9
            // 
            this.it9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.it9.Image = ((System.Drawing.Image)(resources.GetObject("it9.Image")));
            this.it9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.it9.Name = "it9";
            this.it9.Size = new System.Drawing.Size(22, 20);
            this.it9.Text = "toolStripButton16";
            // 
            // iTextureGraphic
            // 
            this.iTextureGraphic.Location = new System.Drawing.Point(56, 62);
            this.iTextureGraphic.Name = "iTextureGraphic";
            this.iTextureGraphic.Size = new System.Drawing.Size(256, 256);
            this.iTextureGraphic.TabIndex = 0;
            this.iTextureGraphic.TabStop = false;
            this.iTextureGraphic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.iTextureGraphic_Click);
            this.iTextureGraphic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.iTextureGraphic_MouseDown);
            this.iTextureGraphic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.iTextureGraphic_MouseUp);
            // 
            // TextureEditorInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 352);
            this.Controls.Add(this.iTextureGraphic);
            this.Controls.Add(this.toolStrip3);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TextureEditorInterface";
            this.Text = "TextureEditor";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iTextureGraphic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton itNew;
        private System.Windows.Forms.ToolStripButton itOpen;
        private System.Windows.Forms.ToolStripButton itSave;
        private System.Windows.Forms.ToolStripButton itUndo;
        private System.Windows.Forms.ToolStripButton itRedo;
        private System.Windows.Forms.ToolStripButton itArrow;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton itAddSolid;
        private System.Windows.Forms.ToolStripButton itAddDiffuse;
        private System.Windows.Forms.ToolStripButton itSubtractSolid;
        private System.Windows.Forms.ToolStripButton itSubtractDiffuse;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton it0;
        private System.Windows.Forms.ToolStripButton it1;
        private System.Windows.Forms.ToolStripButton it2;
        private System.Windows.Forms.ToolStripButton it3;
        private System.Windows.Forms.ToolStripButton it4;
        private System.Windows.Forms.ToolStripButton it5;
        private System.Windows.Forms.ToolStripButton it6;
        private System.Windows.Forms.ToolStripButton it7;
        private System.Windows.Forms.ToolStripButton it8;
        private System.Windows.Forms.ToolStripButton it9;
        private System.Windows.Forms.PictureBox iTextureGraphic;

    }
}