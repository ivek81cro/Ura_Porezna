namespace Ura_Porezna
{
    partial class Form0
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.uRAPoreznaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iRAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pDVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uRAPoreznaToolStripMenuItem,
            this.iRAToolStripMenuItem,
            this.pDVToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // uRAPoreznaToolStripMenuItem
            // 
            this.uRAPoreznaToolStripMenuItem.Name = "uRAPoreznaToolStripMenuItem";
            this.uRAPoreznaToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.uRAPoreznaToolStripMenuItem.Text = "URA - porezna";
            this.uRAPoreznaToolStripMenuItem.Click += new System.EventHandler(this.URAPoreznaToolStripMenuItem_Click);
            // 
            // iRAToolStripMenuItem
            // 
            this.iRAToolStripMenuItem.Name = "iRAToolStripMenuItem";
            this.iRAToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.iRAToolStripMenuItem.Text = "IRA";
            this.iRAToolStripMenuItem.Click += new System.EventHandler(this.iRAToolStripMenuItem_Click);
            // 
            // pDVToolStripMenuItem
            // 
            this.pDVToolStripMenuItem.Name = "pDVToolStripMenuItem";
            this.pDVToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.pDVToolStripMenuItem.Text = "PDV";
            // 
            // Form0
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 658);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form0";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form0";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem uRAPoreznaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iRAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pDVToolStripMenuItem;
    }
}