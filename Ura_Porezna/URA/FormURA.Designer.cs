namespace Ura_Porezna
{
    partial class FormURA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormURA));
            this.button1 = new System.Windows.Forms.Button();
            this.datumDo = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.txtDob = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.datumOd = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 145);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "Kreiraj XML za Poreznu";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // datumDo
            // 
            this.datumDo.CustomFormat = "dd-MM-yyyy";
            this.datumDo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datumDo.Location = new System.Drawing.Point(100, 95);
            this.datumDo.Name = "datumDo";
            this.datumDo.Size = new System.Drawing.Size(122, 22);
            this.datumDo.TabIndex = 2;
            this.datumDo.Value = new System.DateTime(2019, 2, 10, 22, 55, 5, 0);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 361);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(878, 192);
            this.dataGridView1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(304, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = ".";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(304, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = ".";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(304, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = ".";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(304, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = ".";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(304, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = ".";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(133, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 47);
            this.button2.TabIndex = 9;
            this.button2.Text = "Otvori csv";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 7);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 47);
            this.button3.TabIndex = 10;
            this.button3.Text = "Briši bazu";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Datum Do:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "Datum Od:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(453, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(12, 17);
            this.label8.TabIndex = 13;
            this.label8.Text = ".";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(453, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(12, 17);
            this.label9.TabIndex = 14;
            this.label9.Text = ".";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(453, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(12, 17);
            this.label10.TabIndex = 15;
            this.label10.Text = ".";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(453, 125);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(12, 17);
            this.label11.TabIndex = 16;
            this.label11.Text = ".";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 188);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(530, 170);
            this.label12.TabIndex = 17;
            this.label12.Text = resources.GetString("label12.Text");
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(731, 7);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(118, 47);
            this.button4.TabIndex = 18;
            this.button4.Text = "Ispis iz baze";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(642, 57);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(240, 51);
            this.label13.TabIndex = 19;
            this.label13.Text = "Provjera da li postoji zapis u bazi \r\nili nakon otvaranja csv- datoteke \r\nmože se" +
    " vidjeti postojeći zapis u bazi";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(731, 152);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(118, 47);
            this.button5.TabIndex = 20;
            this.button5.Text = "Odobrenja zbirno";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(666, 202);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(195, 34);
            this.label14.TabIndex = 21;
            this.label14.Text = "Izvlači samo financijska \r\nodobrenja za odabrani period";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(258, 13);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(291, 17);
            this.label15.TabIndex = 22;
            this.label15.Text = "Baza se briše prije učitavanja novog csv fajla";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(731, 240);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(118, 45);
            this.button6.TabIndex = 23;
            this.button6.Text = "Odobrenja pojedinacno";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(731, 306);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(118, 28);
            this.button7.TabIndex = 24;
            this.button7.Text = "Troškovi";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // txtDob
            // 
            this.txtDob.Location = new System.Drawing.Point(575, 262);
            this.txtDob.Name = "txtDob";
            this.txtDob.Size = new System.Drawing.Size(141, 22);
            this.txtDob.TabIndex = 25;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(575, 240);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(102, 17);
            this.label16.TabIndex = 26;
            this.label16.Text = "Dobavljač-filter";
            // 
            // datumOd
            // 
            this.datumOd.CustomFormat = "dd-MM-yyyy";
            this.datumOd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datumOd.Location = new System.Drawing.Point(100, 67);
            this.datumOd.Name = "datumOd";
            this.datumOd.Size = new System.Drawing.Size(122, 22);
            this.datumOd.TabIndex = 1;
            this.datumOd.Value = new System.DateTime(2019, 2, 13, 21, 5, 21, 0);
            // 
            // FormURA
            // 
            this.ClientSize = new System.Drawing.Size(902, 557);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtDob);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.datumDo);
            this.Controls.Add(this.datumOd);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormURA";
            this.Text = "URA";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker datumDo;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox txtDob;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker datumOd;
    }
}

