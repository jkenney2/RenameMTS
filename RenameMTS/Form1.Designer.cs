namespace RenameMTS
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbxSuccess = new System.Windows.Forms.ListBox();
            this.lbxFailure = new System.Windows.Forms.ListBox();
            this.btnRename = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Successfully Renamed:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(446, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Not Renamed:";
            // 
            // lbxSuccess
            // 
            this.lbxSuccess.FormattingEnabled = true;
            this.lbxSuccess.HorizontalScrollbar = true;
            this.lbxSuccess.Location = new System.Drawing.Point(40, 135);
            this.lbxSuccess.Name = "lbxSuccess";
            this.lbxSuccess.Size = new System.Drawing.Size(329, 368);
            this.lbxSuccess.TabIndex = 2;
            // 
            // lbxFailure
            // 
            this.lbxFailure.FormattingEnabled = true;
            this.lbxFailure.HorizontalScrollbar = true;
            this.lbxFailure.Location = new System.Drawing.Point(449, 135);
            this.lbxFailure.Name = "lbxFailure";
            this.lbxFailure.Size = new System.Drawing.Size(314, 368);
            this.lbxFailure.TabIndex = 3;
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(40, 43);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(88, 23);
            this.btnRename.TabIndex = 4;
            this.btnRename.Text = "Rename Files";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 549);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.lbxFailure);
            this.Controls.Add(this.lbxSuccess);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "RenameMTS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbxSuccess;
        private System.Windows.Forms.ListBox lbxFailure;
        private System.Windows.Forms.Button btnRename;
    }
}

