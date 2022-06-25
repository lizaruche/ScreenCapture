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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.customToggleSwitch1 = new kursach.CustomToggleSwitch();
            this.customButton3 = new kursach.CustomButton();
            this.customButton2 = new kursach.CustomButton();
            this.customButton1 = new kursach.CustomButton();
            this.form3 = new kursach.Form3();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(33, 109);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(330, 27);
            this.textBox1.TabIndex = 13;
            this.textBox1.Text = "http://127.0.0.1:5000";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(30, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Введите адрес сервера";
            // 
            // customToggleSwitch1
            // 
            this.customToggleSwitch1.BackColor = System.Drawing.Color.White;
            this.customToggleSwitch1.BackColorOFF = System.Drawing.Color.Red;
            this.customToggleSwitch1.BackColorON = System.Drawing.Color.DarkGreen;
            this.customToggleSwitch1.Checked = false;
            this.customToggleSwitch1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customToggleSwitch1.Font = new System.Drawing.Font("Verdana", 9F);
            this.customToggleSwitch1.Location = new System.Drawing.Point(225, 352);
            this.customToggleSwitch1.Name = "customToggleSwitch1";
            this.customToggleSwitch1.Size = new System.Drawing.Size(176, 15);
            this.customToggleSwitch1.TabIndex = 12;
            this.customToggleSwitch1.Text = "Захватить всё окно";
            this.customToggleSwitch1.TextOnChecked = "";
            this.customToggleSwitch1.CheckedChanged += new kursach.CustomToggleSwitch.OnCheckedChangedHandler(this.customToggleSwitch1_CheckedChanged);
            // 
            // customButton3
            // 
            this.customButton3.AnimColor = System.Drawing.Color.White;
            this.customButton3.BackColor = System.Drawing.Color.Transparent;
            this.customButton3.BorderColor = System.Drawing.Color.Black;
            this.customButton3.BorderColorEnabled = true;
            this.customButton3.Font = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
            this.customButton3.ForeColor = System.Drawing.Color.White;
            this.customButton3.Location = new System.Drawing.Point(12, 406);
            this.customButton3.Name = "customButton3";
            this.customButton3.NewBackColor = System.Drawing.Color.Red;
            this.customButton3.NewFont = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
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
            this.customButton2.Location = new System.Drawing.Point(198, 383);
            this.customButton2.Name = "customButton2";
            this.customButton2.NewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(147)))), ((int)(((byte)(43)))));
            this.customButton2.NewFont = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
            this.customButton2.Rounding = 75;
            this.customButton2.RoundingEnable = true;
            this.customButton2.Size = new System.Drawing.Size(217, 66);
            this.customButton2.TabIndex = 10;
            this.customButton2.Text = "Выбрать окно";
            this.customButton2.Click += new System.EventHandler(this.customButton2_Click_1);
            // 
            // customButton1
            // 
            this.customButton1.AnimColor = System.Drawing.Color.White;
            this.customButton1.BorderColor = System.Drawing.Color.Black;
            this.customButton1.BorderColorEnabled = true;
            this.customButton1.Font = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
            this.customButton1.ForeColor = System.Drawing.Color.White;
            this.customButton1.Location = new System.Drawing.Point(198, 383);
            this.customButton1.Name = "customButton1";
            this.customButton1.NewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(147)))), ((int)(((byte)(43)))));
            this.customButton1.NewFont = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
            this.customButton1.Rounding = 75;
            this.customButton1.RoundingEnable = true;
            this.customButton1.Size = new System.Drawing.Size(217, 63);
            this.customButton1.TabIndex = 9;
            this.customButton1.Text = "Остановить поток";
            this.customButton1.Visible = false;
            this.customButton1.Click += new System.EventHandler(this.customButton1_Click_1);
            // 
            // form3
            // 
            this.form3.BackColor = System.Drawing.Color.Gray;
            this.form3.ClientSize = new System.Drawing.Size(634, 529);
            this.form3.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.form3.Location = new System.Drawing.Point(130, 130);
            this.form3.Name = "form3";
            this.form3.Padding = new System.Windows.Forms.Padding(25, 25, 0, 0);
            this.form3.Text = "Выбор приложения";
            this.form3.Visible = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.customToggleSwitch1);
            this.Controls.Add(this.customButton3);
            this.Controls.Add(this.customButton2);
            this.Controls.Add(this.customButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form2";
            this.Text = "ScreenCapture";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Form3 form3;
        private CustomButton customButton1;
        private CustomButton customButton2;
        private CustomButton customButton3;
        private CustomToggleSwitch customToggleSwitch1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}