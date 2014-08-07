namespace StuInCourse
{
    partial class frm_printsetup
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btn_Yes = new DevComponents.DotNetBar.ButtonX();
            this.btn_No = new DevComponents.DotNetBar.ButtonX();
            this.checkBoxOne = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.checkBoxTwelve = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(28, 58);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(160, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "請選擇您需要的列數";
            // 
            // btn_Yes
            // 
            this.btn_Yes.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Yes.BackColor = System.Drawing.Color.Transparent;
            this.btn_Yes.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_Yes.Location = new System.Drawing.Point(212, 102);
            this.btn_Yes.Name = "btn_Yes";
            this.btn_Yes.Size = new System.Drawing.Size(75, 23);
            this.btn_Yes.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Yes.TabIndex = 1;
            this.btn_Yes.Text = "確定";
            this.btn_Yes.Click += new System.EventHandler(this.btn_Yes_Click);
            // 
            // btn_No
            // 
            this.btn_No.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_No.BackColor = System.Drawing.Color.Transparent;
            this.btn_No.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_No.Location = new System.Drawing.Point(293, 102);
            this.btn_No.Name = "btn_No";
            this.btn_No.Size = new System.Drawing.Size(75, 23);
            this.btn_No.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_No.TabIndex = 2;
            this.btn_No.Text = "取消";
            this.btn_No.Click += new System.EventHandler(this.btn_No_Click);
            // 
            // checkBoxOne
            // 
            this.checkBoxOne.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.checkBoxOne.BackgroundStyle.Class = "";
            this.checkBoxOne.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkBoxOne.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.checkBoxOne.Location = new System.Drawing.Point(212, 58);
            this.checkBoxOne.Name = "checkBoxOne";
            this.checkBoxOne.Size = new System.Drawing.Size(75, 23);
            this.checkBoxOne.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxOne.TabIndex = 3;
            this.checkBoxOne.Text = "1列";
            // 
            // checkBoxTwelve
            // 
            this.checkBoxTwelve.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.checkBoxTwelve.BackgroundStyle.Class = "";
            this.checkBoxTwelve.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkBoxTwelve.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.checkBoxTwelve.Location = new System.Drawing.Point(293, 58);
            this.checkBoxTwelve.Name = "checkBoxTwelve";
            this.checkBoxTwelve.Size = new System.Drawing.Size(75, 23);
            this.checkBoxTwelve.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxTwelve.TabIndex = 3;
            this.checkBoxTwelve.Text = "12列";
            // 
            // frm_printsetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 137);
            this.Controls.Add(this.checkBoxTwelve);
            this.Controls.Add(this.checkBoxOne);
            this.Controls.Add(this.btn_No);
            this.Controls.Add(this.btn_Yes);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Name = "frm_printsetup";
            this.Text = "學生修課清單";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btn_Yes;
        private DevComponents.DotNetBar.ButtonX btn_No;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxOne;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxTwelve;
    }
}