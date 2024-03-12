
namespace WindowsFormsApp1
{
    partial class Form1
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
            this.TestButton = new System.Windows.Forms.Button();
            this.TestLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.UploadedFileLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TestButton
            // 
            this.TestButton.BackColor = System.Drawing.Color.Silver;
            this.TestButton.Location = new System.Drawing.Point(105, 88);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(190, 59);
            this.TestButton.TabIndex = 0;
            this.TestButton.Text = "Download Template";
            this.TestButton.UseVisualStyleBackColor = false;
            this.TestButton.Click += new System.EventHandler(this.Download_Template);
            // 
            // TestLabel
            // 
            this.TestLabel.AutoSize = true;
            this.TestLabel.Location = new System.Drawing.Point(520, 215);
            this.TestLabel.Name = "TestLabel";
            this.TestLabel.Size = new System.Drawing.Size(0, 20);
            this.TestLabel.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Silver;
            this.button1.Location = new System.Drawing.Point(105, 215);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 59);
            this.button1.TabIndex = 2;
            this.button1.Text = "Browse File";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Browse_File);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Silver;
            this.button2.Location = new System.Drawing.Point(426, 331);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(233, 59);
            this.button2.TabIndex = 3;
            this.button2.Text = "Upload Document";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.Upload_Document);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(101, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Download Template DaaS Employee";
            // 
            // UploadedFileLabel
            // 
            this.UploadedFileLabel.AutoSize = true;
            this.UploadedFileLabel.Location = new System.Drawing.Point(105, 189);
            this.UploadedFileLabel.Name = "UploadedFileLabel";
            this.UploadedFileLabel.Size = new System.Drawing.Size(79, 20);
            this.UploadedFileLabel.TabIndex = 5;
            this.UploadedFileLabel.Text = "File Path :";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1022, 450);
            this.Controls.Add(this.UploadedFileLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TestLabel);
            this.Controls.Add(this.TestButton);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Daas Employee";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button TestButton;
        private System.Windows.Forms.Label TestLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label UploadedFileLabel;
    }
}

