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
            this.btnSaveTemplate = new System.Windows.Forms.Button();
            this.cbOther = new System.Windows.Forms.CheckBox();
            this.gpOther = new System.Windows.Forms.GroupBox();
            this.cbFullText = new System.Windows.Forms.CheckBox();
            this.cbMinitext = new System.Windows.Forms.CheckBox();
            this.cbSEO = new System.Windows.Forms.CheckBox();
            this.gpOther.SuspendLayout();
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
            this.tbLogin.Size = new System.Drawing.Size(217, 20);
            this.tbLogin.TabIndex = 5;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(601, 38);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(217, 20);
            this.tbPassword.TabIndex = 6;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(601, 64);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(217, 23);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "Начать обработку";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnSaveTemplate
            // 
            this.btnSaveTemplate.Location = new System.Drawing.Point(601, 93);
            this.btnSaveTemplate.Name = "btnSaveTemplate";
            this.btnSaveTemplate.Size = new System.Drawing.Size(217, 23);
            this.btnSaveTemplate.TabIndex = 8;
            this.btnSaveTemplate.Text = "Сохранить шаблон";
            this.btnSaveTemplate.UseVisualStyleBackColor = true;
            this.btnSaveTemplate.Click += new System.EventHandler(this.btnSaveTemplate_Click);
            // 
            // cbOther
            // 
            this.cbOther.AutoSize = true;
            this.cbOther.Location = new System.Drawing.Point(601, 122);
            this.cbOther.Name = "cbOther";
            this.cbOther.Size = new System.Drawing.Size(106, 17);
            this.cbOther.TabIndex = 25;
            this.cbOther.Text = "Дополнительно";
            this.cbOther.UseVisualStyleBackColor = true;
            this.cbOther.CheckedChanged += new System.EventHandler(this.cbOther_CheckedChanged_1);
            // 
            // gpOther
            // 
            this.gpOther.Controls.Add(this.cbFullText);
            this.gpOther.Controls.Add(this.cbMinitext);
            this.gpOther.Controls.Add(this.cbSEO);
            this.gpOther.Location = new System.Drawing.Point(602, 145);
            this.gpOther.Name = "gpOther";
            this.gpOther.Size = new System.Drawing.Size(216, 85);
            this.gpOther.TabIndex = 26;
            this.gpOther.TabStop = false;
            this.gpOther.Text = "Дополнительно";
            this.gpOther.Visible = false;
            // 
            // cbFullText
            // 
            this.cbFullText.AutoSize = true;
            this.cbFullText.Location = new System.Drawing.Point(6, 65);
            this.cbFullText.Name = "cbFullText";
            this.cbFullText.Size = new System.Drawing.Size(203, 17);
            this.cbFullText.TabIndex = 24;
            this.cbFullText.Text = "Обновить полное описание товара";
            this.cbFullText.UseVisualStyleBackColor = true;
            // 
            // cbMinitext
            // 
            this.cbMinitext.AutoSize = true;
            this.cbMinitext.Location = new System.Drawing.Point(6, 42);
            this.cbMinitext.Name = "cbMinitext";
            this.cbMinitext.Size = new System.Drawing.Size(208, 17);
            this.cbMinitext.TabIndex = 23;
            this.cbMinitext.Text = "Обновить краткое описание товара";
            this.cbMinitext.UseVisualStyleBackColor = true;
            // 
            // cbSEO
            // 
            this.cbSEO.AutoSize = true;
            this.cbSEO.Location = new System.Drawing.Point(6, 19);
            this.cbSEO.Name = "cbSEO";
            this.cbSEO.Size = new System.Drawing.Size(100, 17);
            this.cbSEO.TabIndex = 22;
            this.cbSEO.Text = "Обновить СЕО";
            this.cbSEO.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 374);
            this.Controls.Add(this.cbOther);
            this.Controls.Add(this.gpOther);
            this.Controls.Add(this.btnSaveTemplate);
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
            this.gpOther.ResumeLayout(false);
            this.gpOther.PerformLayout();
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
        private System.Windows.Forms.Button btnSaveTemplate;
        private System.Windows.Forms.CheckBox cbOther;
        private System.Windows.Forms.GroupBox gpOther;
        private System.Windows.Forms.CheckBox cbFullText;
        private System.Windows.Forms.CheckBox cbMinitext;
        private System.Windows.Forms.CheckBox cbSEO;
    }
}

