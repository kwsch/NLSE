namespace NLSE.Subforms
{
    partial class DreamPatcher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DreamPatcher));
            this.TB_FilePath = new System.Windows.Forms.TextBox();
            this.B_PatchFile = new System.Windows.Forms.Button();
            this.B_OpenFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TB_FilePath
            // 
            this.TB_FilePath.Location = new System.Drawing.Point(12, 24);
            this.TB_FilePath.Name = "TB_FilePath";
            this.TB_FilePath.Size = new System.Drawing.Size(461, 20);
            this.TB_FilePath.TabIndex = 0;
            // 
            // B_PatchFile
            // 
            this.B_PatchFile.Location = new System.Drawing.Point(249, 50);
            this.B_PatchFile.Name = "B_PatchFile";
            this.B_PatchFile.Size = new System.Drawing.Size(75, 23);
            this.B_PatchFile.TabIndex = 2;
            this.B_PatchFile.Text = "Patch";
            this.B_PatchFile.UseVisualStyleBackColor = true;
            this.B_PatchFile.Click += new System.EventHandler(this.B_PatchFile_Click);
            // 
            // B_OpenFile
            // 
            this.B_OpenFile.Location = new System.Drawing.Point(168, 50);
            this.B_OpenFile.Name = "B_OpenFile";
            this.B_OpenFile.Size = new System.Drawing.Size(75, 23);
            this.B_OpenFile.TabIndex = 3;
            this.B_OpenFile.Text = "Open";
            this.B_OpenFile.UseVisualStyleBackColor = true;
            this.B_OpenFile.Click += new System.EventHandler(this.B_OpenFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "garden_plus.dat input:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(453, 91);
            this.label2.TabIndex = 5;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // DreamPatcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 187);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.B_OpenFile);
            this.Controls.Add(this.B_PatchFile);
            this.Controls.Add(this.TB_FilePath);
            this.Name = "DreamPatcher";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dream Patcher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TB_FilePath;
        private System.Windows.Forms.Button B_PatchFile;
        private System.Windows.Forms.Button B_OpenFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}