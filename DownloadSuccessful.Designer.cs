
namespace WindowsFormsApp1
{
    partial class DownloadSuccessful
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
            this.label1 = new System.Windows.Forms.Label();
            this.downloadPath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(140, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(295, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Download Successful!";
            // 
            // downloadPath
            // 
            this.downloadPath.AutoSize = true;
            this.downloadPath.Location = new System.Drawing.Point(140, 150);
            this.downloadPath.Margin = new System.Windows.Forms.Padding(10);
            this.downloadPath.Name = "downloadPath";
            this.downloadPath.Padding = new System.Windows.Forms.Padding(0, 0, 50, 0);
            this.downloadPath.Size = new System.Drawing.Size(168, 20);
            this.downloadPath.TabIndex = 1;
            this.downloadPath.Text = "download_path";
            // 
            // DownloadSuccessful
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(578, 244);
            this.Controls.Add(this.downloadPath);
            this.Controls.Add(this.label1);
            this.Name = "DownloadSuccessful";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DownloadSuccessful";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label downloadPath;
    }
}