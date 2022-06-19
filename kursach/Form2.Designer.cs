namespace kursach
{
    partial class Form2
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
            this.customButton3 = new kursach.CustomButton();
            this.customButton2 = new kursach.CustomButton();
            this.customButton1 = new kursach.CustomButton();
            this.customComboBox1 = new kursach.CustomComboBox();
            this.customToggleSwitch1 = new kursach.CustomToggleSwitch();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(8, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Выберите окно:";
            // 
            // customButton3
            // 
            this.customButton3.AnimColor = System.Drawing.Color.White;
            this.customButton3.BackColor = System.Drawing.Color.Transparent;
            this.customButton3.BorderColor = System.Drawing.Color.Black;
            this.customButton3.BorderColorEnabled = true;
            this.customButton3.Font = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
            this.customButton3.ForeColor = System.Drawing.Color.White;
            this.customButton3.Location = new System.Drawing.Point(16, 399);
            this.customButton3.Name = "customButton3";
            this.customButton3.NewBackColor = System.Drawing.Color.Red;
            this.customButton3.Rounding = 75;
            this.customButton3.RoundingEnable = true;
            this.customButton3.Size = new System.Drawing.Size(107, 43);
            this.customButton3.TabIndex = 11;
            this.customButton3.Text = "Выход";
            this.customButton3.Click += new System.EventHandler(this.customButton3_Click);
            // 
            // customButton2
            // 
            this.customButton2.AnimColor = System.Drawing.Color.White;
            this.customButton2.BackColor = System.Drawing.Color.Transparent;
            this.customButton2.BorderColor = System.Drawing.Color.Black;
            this.customButton2.BorderColorEnabled = true;
            this.customButton2.Font = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
            this.customButton2.ForeColor = System.Drawing.Color.White;
            this.customButton2.Location = new System.Drawing.Point(285, 373);
            this.customButton2.Name = "customButton2";
            this.customButton2.NewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(147)))), ((int)(((byte)(43)))));
            this.customButton2.Rounding = 75;
            this.customButton2.RoundingEnable = true;
            this.customButton2.Size = new System.Drawing.Size(199, 66);
            this.customButton2.TabIndex = 10;
            this.customButton2.Text = "Старт стрим";
            this.customButton2.Click += new System.EventHandler(this.customButton2_Click_1);
            // 
            // customButton1
            // 
            this.customButton1.AnimColor = System.Drawing.Color.White;
            this.customButton1.BorderColor = System.Drawing.Color.Black;
            this.customButton1.BorderColorEnabled = true;
            this.customButton1.Font = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
            this.customButton1.ForeColor = System.Drawing.Color.White;
            this.customButton1.Location = new System.Drawing.Point(285, 376);
            this.customButton1.Name = "customButton1";
            this.customButton1.NewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(147)))), ((int)(((byte)(43)))));
            this.customButton1.Rounding = 75;
            this.customButton1.RoundingEnable = true;
            this.customButton1.Size = new System.Drawing.Size(199, 63);
            this.customButton1.TabIndex = 9;
            this.customButton1.Text = "Стоп стрим";
            this.customButton1.Visible = false;
            this.customButton1.Click += new System.EventHandler(this.customButton1_Click_1);
            // 
            // customComboBox1
            // 
            this.customComboBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.customComboBox1.BorderColor = System.Drawing.Color.Navy;
            this.customComboBox1.BorderSize = 1;
            this.customComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.customComboBox1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.customComboBox1.ForeColor = System.Drawing.Color.DimGray;
            this.customComboBox1.IconColor = System.Drawing.Color.Navy;
            this.customComboBox1.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(228)))), ((int)(((byte)(245)))));
            this.customComboBox1.ListTextColor = System.Drawing.Color.DimGray;
            this.customComboBox1.Location = new System.Drawing.Point(12, 71);
            this.customComboBox1.MinimumSize = new System.Drawing.Size(200, 30);
            this.customComboBox1.Name = "customComboBox1";
            this.customComboBox1.Padding = new System.Windows.Forms.Padding(1);
            this.customComboBox1.Size = new System.Drawing.Size(269, 30);
            this.customComboBox1.TabIndex = 8;
            this.customComboBox1.Texts = "";
            this.customComboBox1.OnSelectedIndexChanged += new System.EventHandler(this.customComboBox1_OnSelectedIndexChanged);
            // 
            // customToggleSwitch1
            // 
            this.customToggleSwitch1.BackColor = System.Drawing.Color.White;
            this.customToggleSwitch1.BackColorOFF = System.Drawing.Color.Red;
            this.customToggleSwitch1.BackColorON = System.Drawing.Color.DarkGreen;
            this.customToggleSwitch1.Checked = false;
            this.customToggleSwitch1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customToggleSwitch1.Font = new System.Drawing.Font("Verdana", 9F);
            this.customToggleSwitch1.Location = new System.Drawing.Point(308, 71);
            this.customToggleSwitch1.Name = "customToggleSwitch1";
            this.customToggleSwitch1.Size = new System.Drawing.Size(176, 15);
            this.customToggleSwitch1.TabIndex = 12;
            this.customToggleSwitch1.Text = "Захватить всё окно";
            this.customToggleSwitch1.TextOnChecked = "";
            this.customToggleSwitch1.CheckedChanged += new kursach.CustomToggleSwitch.OnCheckedChangedHandler(this.customToggleSwitch1_CheckedChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 474);
            this.Controls.Add(this.customToggleSwitch1);
            this.Controls.Add(this.customButton3);
            this.Controls.Add(this.customButton2);
            this.Controls.Add(this.customButton1);
            this.Controls.Add(this.customComboBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "ScreenCapture";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private CustomComboBox customComboBox1;
        private CustomButton customButton1;
        private CustomButton customButton2;
        private CustomButton customButton3;
        private CustomToggleSwitch customToggleSwitch1;
    }
}