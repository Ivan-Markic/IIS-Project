
using LibraryForIISProject.Models;

namespace ClientApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbSurname = new System.Windows.Forms.TextBox();
            this.tbSubject = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCreateStudent = new System.Windows.Forms.Button();
            this.numbericGrade = new System.Windows.Forms.NumericUpDown();
            this.cbValidate = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbNameOfNode = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbCity = new System.Windows.Forms.TextBox();
            this.tbTemperature = new System.Windows.Forms.TextBox();
            this.btnGetTemperature = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.btnValidate = new System.Windows.Forms.Button();
            this.btnGetData = new System.Windows.Forms.Button();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numbericGrade)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Surname:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Grade:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Subject:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Create new Student";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(86, 32);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(125, 27);
            this.tbName.TabIndex = 5;
            // 
            // tbSurname
            // 
            this.tbSurname.Location = new System.Drawing.Point(86, 65);
            this.tbSurname.Name = "tbSurname";
            this.tbSurname.Size = new System.Drawing.Size(125, 27);
            this.tbSurname.TabIndex = 6;
            // 
            // tbSubject
            // 
            this.tbSubject.Location = new System.Drawing.Point(86, 97);
            this.tbSubject.Name = "tbSubject";
            this.tbSubject.Size = new System.Drawing.Size(125, 27);
            this.tbSubject.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Validate:";
            // 
            // btnCreateStudent
            // 
            this.btnCreateStudent.Location = new System.Drawing.Point(100, 212);
            this.btnCreateStudent.Name = "btnCreateStudent";
            this.btnCreateStudent.Size = new System.Drawing.Size(94, 29);
            this.btnCreateStudent.TabIndex = 11;
            this.btnCreateStudent.Text = "Create";
            this.btnCreateStudent.UseVisualStyleBackColor = true;
            this.btnCreateStudent.Click += new System.EventHandler(this.btnCreateStudent_Click);
            // 
            // numbericGrade
            // 
            this.numbericGrade.Location = new System.Drawing.Point(86, 130);
            this.numbericGrade.Name = "numbericGrade";
            this.numbericGrade.Size = new System.Drawing.Size(125, 27);
            this.numbericGrade.TabIndex = 12;
            // 
            // cbValidate
            // 
            this.cbValidate.FormattingEnabled = true;
            this.cbValidate.Location = new System.Drawing.Point(86, 166);
            this.cbValidate.Name = "cbValidate";
            this.cbValidate.Size = new System.Drawing.Size(125, 28);
            this.cbValidate.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(86, 262);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Search student";
            // 
            // cbNameOfNode
            // 
            this.cbNameOfNode.FormattingEnabled = true;
            this.cbNameOfNode.Location = new System.Drawing.Point(86, 301);
            this.cbNameOfNode.Name = "cbNameOfNode";
            this.cbNameOfNode.Size = new System.Drawing.Size(125, 28);
            this.cbNameOfNode.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 304);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 20);
            this.label8.TabIndex = 15;
            this.label8.Text = "By node:";
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(86, 354);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(125, 27);
            this.tbSearch.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 357);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 20);
            this.label9.TabIndex = 17;
            this.label9.Text = "Search:";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(98, 396);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(94, 29);
            this.btnSearch.TabIndex = 19;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(285, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(196, 20);
            this.label10.TabIndex = 20;
            this.label10.Text = "Get temperature for you city";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(285, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 20);
            this.label11.TabIndex = 21;
            this.label11.Text = "City:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(226, 97);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 20);
            this.label12.TabIndex = 22;
            this.label12.Text = "Temperature:";
            // 
            // tbCity
            // 
            this.tbCity.Location = new System.Drawing.Point(328, 62);
            this.tbCity.Name = "tbCity";
            this.tbCity.Size = new System.Drawing.Size(125, 27);
            this.tbCity.TabIndex = 23;
            // 
            // tbTemperature
            // 
            this.tbTemperature.Location = new System.Drawing.Point(328, 97);
            this.tbTemperature.Name = "tbTemperature";
            this.tbTemperature.ReadOnly = true;
            this.tbTemperature.Size = new System.Drawing.Size(125, 27);
            this.tbTemperature.TabIndex = 24;
            // 
            // btnGetTemperature
            // 
            this.btnGetTemperature.Location = new System.Drawing.Point(339, 130);
            this.btnGetTemperature.Name = "btnGetTemperature";
            this.btnGetTemperature.Size = new System.Drawing.Size(94, 29);
            this.btnGetTemperature.TabIndex = 25;
            this.btnGetTemperature.Text = "Get";
            this.btnGetTemperature.UseVisualStyleBackColor = true;
            this.btnGetTemperature.Click += new System.EventHandler(this.btnGetTemperature_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(328, 193);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(136, 20);
            this.label13.TabIndex = 26;
            this.label13.Text = "Test Authentication";
            // 
            // btnValidate
            // 
            this.btnValidate.Location = new System.Drawing.Point(339, 229);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(94, 29);
            this.btnValidate.TabIndex = 27;
            this.btnValidate.Text = "Validate";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(339, 348);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(94, 29);
            this.btnGetData.TabIndex = 28;
            this.btnGetData.Text = "Get Data";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(328, 264);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(125, 27);
            this.tbUsername.TabIndex = 29;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(328, 297);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(125, 27);
            this.tbPassword.TabIndex = 30;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(249, 301);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(73, 20);
            this.label14.TabIndex = 32;
            this.label14.Text = "Password:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(244, 267);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(78, 20);
            this.label15.TabIndex = 31;
            this.label15.Text = "Username:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.btnGetData);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnGetTemperature);
            this.Controls.Add(this.tbTemperature);
            this.Controls.Add(this.tbCity);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbNameOfNode);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbValidate);
            this.Controls.Add(this.numbericGrade);
            this.Controls.Add(this.btnCreateStudent);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbSubject);
            this.Controls.Add(this.tbSurname);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Application for Students";
            ((System.ComponentModel.ISupportInitialize)(this.numbericGrade)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbSurname;
        private System.Windows.Forms.TextBox tbSubject;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCreateStudent;
        private System.Windows.Forms.NumericUpDown numbericGrade;
        private System.Windows.Forms.ComboBox cbValidate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbNameOfNode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbCity;
        private System.Windows.Forms.TextBox tbTemperature;
        private System.Windows.Forms.Button btnGetTemperature;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
    }
}

