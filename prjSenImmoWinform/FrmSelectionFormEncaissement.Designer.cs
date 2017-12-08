namespace prjSenImmoWinform
{
    partial class FrmSelectionFormEncaissement
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label37 = new System.Windows.Forms.Label();
            this.cmbProjets = new System.Windows.Forms.ComboBox();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdAnnuler = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(205)))), ((int)(((byte)(193)))));
            this.panel1.Controls.Add(this.label37);
            this.panel1.Controls.Add(this.cmbProjets);
            this.panel1.Controls.Add(this.cmdOK);
            this.panel1.Controls.Add(this.cmdAnnuler);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(343, 112);
            this.panel1.TabIndex = 6;
            // 
            // label37
            // 
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(9, 31);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(61, 17);
            this.label37.TabIndex = 230;
            this.label37.Text = "Projet";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbProjets
            // 
            this.cmbProjets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProjets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProjets.FormattingEnabled = true;
            this.cmbProjets.Location = new System.Drawing.Point(73, 28);
            this.cmbProjets.Name = "cmbProjets";
            this.cmbProjets.Size = new System.Drawing.Size(224, 21);
            this.cmbProjets.TabIndex = 229;
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(228, 74);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(103, 29);
            this.cmdOK.TabIndex = 3;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdAnnuler
            // 
            this.cmdAnnuler.Location = new System.Drawing.Point(14, 74);
            this.cmdAnnuler.Name = "cmdAnnuler";
            this.cmdAnnuler.Size = new System.Drawing.Size(103, 29);
            this.cmdAnnuler.TabIndex = 2;
            this.cmdAnnuler.Text = "Annuler";
            this.cmdAnnuler.UseVisualStyleBackColor = true;
            this.cmdAnnuler.Click += new System.EventHandler(this.cmdAnnuler_Click);
            // 
            // FrmSelectionFormEncaissement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 115);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmSelectionFormEncaissement";
            this.Text = "Gestion des encaissements";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.ComboBox cmbProjets;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdAnnuler;
    }
}