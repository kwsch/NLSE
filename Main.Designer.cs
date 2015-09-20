namespace NLSE
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.B_Friend = new System.Windows.Forms.Button();
            this.CB_Friend = new System.Windows.Forms.ComboBox();
            this.B_Exhibition = new System.Windows.Forms.Button();
            this.B_Garden = new System.Windows.Forms.Button();
            this.L_IO = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // B_Friend
            // 
            this.B_Friend.Location = new System.Drawing.Point(12, 66);
            this.B_Friend.Name = "B_Friend";
            this.B_Friend.Size = new System.Drawing.Size(90, 30);
            this.B_Friend.TabIndex = 1;
            this.B_Friend.Text = "Friend";
            this.B_Friend.UseVisualStyleBackColor = true;
            this.B_Friend.Click += new System.EventHandler(this.clickFriend);
            // 
            // CB_Friend
            // 
            this.CB_Friend.FormattingEnabled = true;
            this.CB_Friend.Location = new System.Drawing.Point(108, 75);
            this.CB_Friend.Name = "CB_Friend";
            this.CB_Friend.Size = new System.Drawing.Size(90, 21);
            this.CB_Friend.TabIndex = 2;
            // 
            // B_Exhibition
            // 
            this.B_Exhibition.Location = new System.Drawing.Point(12, 30);
            this.B_Exhibition.Name = "B_Exhibition";
            this.B_Exhibition.Size = new System.Drawing.Size(90, 30);
            this.B_Exhibition.TabIndex = 3;
            this.B_Exhibition.Text = "Exhibition";
            this.B_Exhibition.UseVisualStyleBackColor = true;
            this.B_Exhibition.Click += new System.EventHandler(this.clickExhibition);
            // 
            // B_Garden
            // 
            this.B_Garden.Location = new System.Drawing.Point(108, 30);
            this.B_Garden.Name = "B_Garden";
            this.B_Garden.Size = new System.Drawing.Size(90, 30);
            this.B_Garden.TabIndex = 4;
            this.B_Garden.Text = "Garden";
            this.B_Garden.UseVisualStyleBackColor = true;
            this.B_Garden.Click += new System.EventHandler(this.clickGarden);
            // 
            // L_IO
            // 
            this.L_IO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.L_IO.Location = new System.Drawing.Point(-233, 6);
            this.L_IO.Name = "L_IO";
            this.L_IO.Size = new System.Drawing.Size(431, 18);
            this.L_IO.TabIndex = 5;
            this.L_IO.Text = "Drop a file/folder to begin.";
            this.L_IO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.L_IO.TextChanged += new System.EventHandler(this.updatePath);
            this.L_IO.Click += new System.EventHandler(this.L_IO_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(209, 105);
            this.Controls.Add(this.L_IO);
            this.Controls.Add(this.B_Garden);
            this.Controls.Add(this.B_Exhibition);
            this.Controls.Add(this.CB_Friend);
            this.Controls.Add(this.B_Friend);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AC:NL Save Editor";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.clickHelp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button B_Friend;
        private System.Windows.Forms.ComboBox CB_Friend;
        private System.Windows.Forms.Button B_Exhibition;
        private System.Windows.Forms.Button B_Garden;
        private System.Windows.Forms.Label L_IO;

    }
}

