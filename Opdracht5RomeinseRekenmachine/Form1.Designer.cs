namespace Opdracht5RomeinseRekenmachine
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
            this.LabelSum = new System.Windows.Forms.Label();
            this.LabelResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LabelSum
            // 
            this.LabelSum.AutoSize = true;
            this.LabelSum.Location = new System.Drawing.Point(13, 13);
            this.LabelSum.Name = "LabelSum";
            this.LabelSum.Size = new System.Drawing.Size(35, 13);
            this.LabelSum.TabIndex = 0;
            this.LabelSum.Text = "label1";
            // 
            // LabelResult
            // 
            this.LabelResult.AutoSize = true;
            this.LabelResult.Location = new System.Drawing.Point(13, 30);
            this.LabelResult.Name = "LabelResult";
            this.LabelResult.Size = new System.Drawing.Size(35, 13);
            this.LabelResult.TabIndex = 1;
            this.LabelResult.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LabelResult);
            this.Controls.Add(this.LabelSum);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelSum;
        private System.Windows.Forms.Label LabelResult;
    }
}

