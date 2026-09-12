namespace DVLD.Test
{
    partial class frmScheduleTest
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
            this.crlScheduleTest1 = new DVLD.Test.Controls.ctrlScheduleTest();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // crlScheduleTest1
            // 
            this.crlScheduleTest1.Location = new System.Drawing.Point(3, 3);
            this.crlScheduleTest1.Name = "crlScheduleTest1";
            this.crlScheduleTest1.Size = new System.Drawing.Size(538, 725);
            this.crlScheduleTest1.TabIndex = 0;
            this.crlScheduleTest1.TestTypeID = DVLD_Buisness.clsTestType.enTestType.VisionTest;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_322;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(198, 727);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(137, 38);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmScheduleTest
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 772);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.crlScheduleTest1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmScheduleTest";
            this.Text = "frmScheduleTest";
            this.Load += new System.EventHandler(this.frmScheduleTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlScheduleTest crlScheduleTest1;
        private System.Windows.Forms.Button btnClose;
    }
}