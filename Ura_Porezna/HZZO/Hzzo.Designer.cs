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
            this.SuspendLayout();
            // 
            // btn_ucitaj
            // 
            this.btn_ucitaj.Location = new System.Drawing.Point(13, 13);
            this.btn_ucitaj.Name = "btn_ucitaj";
            this.btn_ucitaj.Size = new System.Drawing.Size(92, 36);
            this.btn_ucitaj.TabIndex = 0;
            this.btn_ucitaj.Text = "Učitaj spec.";
            this.btn_ucitaj.UseVisualStyleBackColor = true;
            this.btn_ucitaj.Click += new System.EventHandler(this.btn_ucitaj_Click);
            // 
            // Hzzo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 506);
            this.Controls.Add(this.btn_ucitaj);
            this.Name = "Hzzo";
            this.Text = "Hzzo";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ucitaj;
    }
}