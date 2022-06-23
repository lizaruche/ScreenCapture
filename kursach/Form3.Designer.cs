namespace kursach
{
    partial class Form3
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
            this.customButton1 = new kursach.CustomButton();
            this.customButton2 = new kursach.CustomButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // customButton1
            // 
            this.customButton1.AnimColor = System.Drawing.Color.White;
            this.customButton1.BorderColor = System.Drawing.Color.Black;
            this.customButton1.BorderColorEnabled = true;
            this.customButton1.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.customButton1.ForeColor = System.Drawing.Color.White;
            this.customButton1.Location = new System.Drawing.Point(477, 458);
            this.customButton1.Name = "customButton1";
            this.customButton1.NewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(147)))), ((int)(((byte)(43)))));
            this.customButton1.NewFont = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.customButton1.RoundingEnable = true;
            this.customButton1.Size = new System.Drawing.Size(140, 59);
            this.customButton1.TabIndex = 0;
            this.customButton1.Text = "Начать";
            this.customButton1.Click += new System.EventHandler(this.customButton1_Click);
            // 
            // customButton2
            // 
            this.customButton2.AnimColor = System.Drawing.Color.White;
            this.customButton2.BorderColor = System.Drawing.Color.Black;
            this.customButton2.BorderColorEnabled = true;
            this.customButton2.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.customButton2.ForeColor = System.Drawing.Color.White;
            this.customButton2.Location = new System.Drawing.Point(371, 475);
            this.customButton2.Name = "customButton2";
            this.customButton2.NewBackColor = System.Drawing.Color.Red;
            this.customButton2.NewFont = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.customButton2.RoundingEnable = true;
            this.customButton2.Size = new System.Drawing.Size(100, 30);
            this.customButton2.TabIndex = 1;
            this.customButton2.Text = "Отмена";
            this.customButton2.Click += new System.EventHandler(this.customButton2_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(17, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 400);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Gray;
            this.label1.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(57, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(523, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Выберите приложение для демонстрации";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(634, 529);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.customButton2);
            this.Controls.Add(this.customButton1);
            this.Name = "Form3";
            this.Padding = new System.Windows.Forms.Padding(25, 25, 0, 0);
            this.Text = "Выбор приложения";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomButton customButton1;
        private CustomButton customButton2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}