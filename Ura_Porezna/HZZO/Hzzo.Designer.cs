namespace Ura_Porezna
{
    partial class Hzzo
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
            this.btn_ucitaj = new System.Windows.Forms.Button();
            this.dataGridView1 = new Ura_Porezna.CustomDataGridView();
            this.comboGodine = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnIspis = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.datumPick = new System.Windows.Forms.DateTimePicker();
            this.btnFilter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ucitaj
            // 
            this.btn_ucitaj.Location = new System.Drawing.Point(10, 11);
            this.btn_ucitaj.Margin = new System.Windows.Forms.Padding(2);
            this.btn_ucitaj.Name = "btn_ucitaj";
            this.btn_ucitaj.Size = new System.Drawing.Size(97, 29);
            this.btn_ucitaj.TabIndex = 0;
            this.btn_ucitaj.Text = "Učitaj spec.";
            this.btn_ucitaj.UseVisualStyleBackColor = true;
            this.btn_ucitaj.Click += new System.EventHandler(this.btn_ucitaj_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(10, 97);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(622, 302);
            this.dataGridView1.TabIndex = 1;
            // 
            // comboGodine
            // 
            this.comboGodine.FormattingEnabled = true;
            this.comboGodine.Location = new System.Drawing.Point(113, 11);
            this.comboGodine.Name = "comboGodine";
            this.comboGodine.Size = new System.Drawing.Size(121, 21);
            this.comboGodine.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(241, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Odaberi godinu koja se odnosi na izlazne račune";
            // 
            // btnIspis
            // 
            this.btnIspis.Location = new System.Drawing.Point(541, 11);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(75, 29);
            this.btnIspis.TabIndex = 4;
            this.btnIspis.Text = "Ispis baze";
            this.btnIspis.UseVisualStyleBackColor = true;
            this.btnIspis.Click += new System.EventHandler(this.btnIspis_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 55);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Datum placanja:";
            // 
            // datumPick
            // 
            this.datumPick.CustomFormat = "dd-MM-yyyy";
            this.datumPick.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datumPick.Location = new System.Drawing.Point(100, 52);
            this.datumPick.Margin = new System.Windows.Forms.Padding(2);
            this.datumPick.Name = "datumPick";
            this.datumPick.Size = new System.Drawing.Size(103, 20);
            this.datumPick.TabIndex = 17;
            this.datumPick.Value = new System.DateTime(2019, 2, 13, 21, 5, 21, 0);
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(208, 51);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 19;
            this.btnFilter.Text = "Filtriraj";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // Hzzo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 411);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.datumPick);
            this.Controls.Add(this.btnIspis);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboGodine);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_ucitaj);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Hzzo";
            this.Text = "Hzzo";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ucitaj;
        private CustomDataGridView dataGridView1;
        private System.Windows.Forms.ComboBox comboGodine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnIspis;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker datumPick;
        private System.Windows.Forms.Button btnFilter;
    }
}