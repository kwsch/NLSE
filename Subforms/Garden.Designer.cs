namespace NLSE
{
    partial class Garden
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Garden));
            this.PB_JPEG = new System.Windows.Forms.PictureBox();
            this.L_Info = new System.Windows.Forms.Label();
            this.B_Save = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PB_JPEG)).BeginInit();
            this.SuspendLayout();
            // 
            // PB_JPEG
            // 
            this.PB_JPEG.Location = new System.Drawing.Point(12, 12);
            this.PB_JPEG.Name = "PB_JPEG";
            this.PB_JPEG.Size = new System.Drawing.Size(64, 104);
            this.PB_JPEG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PB_JPEG.TabIndex = 0;
            this.PB_JPEG.TabStop = false;
            // 
            // L_Info
            // 
            this.L_Info.AutoSize = true;
            this.L_Info.Location = new System.Drawing.Point(82, 12);
            this.L_Info.Name = "L_Info";
            this.L_Info.Size = new System.Drawing.Size(25, 13);
            this.L_Info.TabIndex = 1;
            this.L_Info.Text = "Info";
            // 
            // B_Save
            // 
            this.B_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Save.Location = new System.Drawing.Point(207, 227);
            this.B_Save.Name = "B_Save";
            this.B_Save.Size = new System.Drawing.Size(65, 23);
            this.B_Save.TabIndex = 2;
            this.B_Save.Text = "Save";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // B_Cancel
            // 
            this.B_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Cancel.Location = new System.Drawing.Point(136, 227);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(65, 23);
            this.B_Cancel.TabIndex = 3;
            this.B_Cancel.Text = "Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // Garden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.B_Save);
            this.Controls.Add(this.L_Info);
            this.Controls.Add(this.PB_JPEG);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Garden";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Garden Editor";
            ((System.ComponentModel.ISupportInitialize)(this.PB_JPEG)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PB_JPEG;
        private System.Windows.Forms.Label L_Info;
        private System.Windows.Forms.Button B_Save;
        private System.Windows.Forms.Button B_Cancel;
    }
}