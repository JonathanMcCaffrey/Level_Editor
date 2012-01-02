namespace Button
{
    partial class AssetsWindow
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
            this.iAssetList = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // iAssetList
            // 
            this.iAssetList.BackColor = System.Drawing.SystemColors.Window;
            this.iAssetList.Location = new System.Drawing.Point(8, 25);
            this.iAssetList.Margin = new System.Windows.Forms.Padding(12);
            this.iAssetList.MultiSelect = false;
            this.iAssetList.Name = "iAssetList";
            this.iAssetList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.iAssetList.Size = new System.Drawing.Size(360, 542);
            this.iAssetList.TabIndex = 0;
            this.iAssetList.TabStop = false;
            this.iAssetList.TileSize = new System.Drawing.Size(30, 30);
            this.iAssetList.UseCompatibleStateImageBehavior = false;
            this.iAssetList.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // AssetsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 571);
            this.Controls.Add(this.iAssetList);
            this.Name = "AssetsWindow";
            this.Text = "AssetsWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView iAssetList;
    }
}