namespace MiniTraktor
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtbMiniText = new System.Windows.Forms.RichTextBox();
            this.rtbFullText = new System.Windows.Forms.RichTextBox();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.tbKeywords = new System.Windows.Forms.TextBox();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbMiniText
            // 
            this.rtbMiniText.Location = new System.Drawing.Point(12, 12);
            this.rtbMiniText.Name = "rtbMiniText";
            this.rtbMiniText.Size = new System.Drawing.Size(583, 133);
            this.rtbMiniText.TabIndex = 0;
            this.rtbMiniText.Text = "";
            // 
            // rtbFullText
            // 
            this.rtbFullText.Location = new System.Drawing.Point(12, 151);
            this.rtbFullText.Name = "rtbFullText";
            this.rtbFullText.Size = new System.Drawing.Size(583, 133);
            this.rtbFullText.TabIndex = 1;
            this.rtbFullText.Text = "";
            // 
            // tbTitle
            // 
            this.tbTitle.Location = new System.Drawing.Point(12, 290);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(583, 20);
            this.tbTitle.TabIndex = 2;
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(12, 316);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(583, 20);
            this.tbDescription.TabIndex = 3;
            // 
            // tbKeywords
            // 
            this.tbKeywords.Location = new System.Drawing.Point(12, 342);
            this.tbKeywords.Name = "tbKeywords";
            this.tbKeywords.Size = new System.Drawing.Size(583, 20);
            this.tbKeywords.TabIndex = 4;
            // 
            // tbLogin
            // 
            this.tbLogin.Location = new System.Drawing.Point(601, 12);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(201, 20);
            this.tbLogin.TabIndex = 5;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(601, 38);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(201, 20);
            this.tbPassword.TabIndex = 6;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(601, 64);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(201, 23);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "Начать обработку";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 374);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbLogin);
            this.Controls.Add(this.tbKeywords);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.tbTitle);
            this.Controls.Add(this.rtbFullText);
            this.Controls.Add(this.rtbMiniText);
            this.Name = "Form1";
            this.Text = "Запчасти для минитракторов";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbMiniText;
        private System.Windows.Forms.RichTextBox rtbFullText;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.TextBox tbKeywords;
        private System.Windows.Forms.TextBox tbLogin;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btnStart;
    }
}

