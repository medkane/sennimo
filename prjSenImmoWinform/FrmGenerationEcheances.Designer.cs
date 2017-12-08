namespace prjSenImmoWinform
{
    partial class FrmGenerationEcheances
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
            this.Au = new System.Windows.Forms.Label();
            this.cmdGenerer = new System.Windows.Forms.Button();
            this.cmdAnnuler = new System.Windows.Forms.Button();
            this.dtpDateReference = new System.Windows.Forms.DateTimePicker();
            this.cmdRAZ = new System.Windows.Forms.Button();
            this.cmbMois = new System.Windows.Forms.ComboBox();
            this.dtpAnnee = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // Au
            // 
            this.Au.AutoSize = true;
            this.Au.Location = new System.Drawing.Point(25, 24);
            this.Au.Name = "Au";
            this.Au.Size = new System.Drawing.Size(68, 13);
            this.Au.TabIndex = 243;
            this.Au.Text = "Période cible";
            // 
            // cmdGenerer
            // 
            this.cmdGenerer.Location = new System.Drawing.Point(260, 80);
            this.cmdGenerer.Name = "cmdGenerer";
            this.cmdGenerer.Size = new System.Drawing.Size(95, 29);
            this.cmdGenerer.TabIndex = 244;
            this.cmdGenerer.Text = "Générer";
            this.cmdGenerer.UseVisualStyleBackColor = true;
            this.cmdGenerer.Click += new System.EventHandler(this.cmdGenerer_Click);
            // 
            // cmdAnnuler
            // 
            this.cmdAnnuler.Location = new System.Drawing.Point(12, 80);
            this.cmdAnnuler.Name = "cmdAnnuler";
            this.cmdAnnuler.Size = new System.Drawing.Size(95, 29);
            this.cmdAnnuler.TabIndex = 245;
            this.cmdAnnuler.Text = "Annuler";
            this.cmdAnnuler.UseVisualStyleBackColor = true;
            // 
            // dtpDateReference
            // 
            this.dtpDateReference.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateReference.Location = new System.Drawing.Point(337, 12);
            this.dtpDateReference.Name = "dtpDateReference";
            this.dtpDateReference.Size = new System.Drawing.Size(101, 20);
            this.dtpDateReference.TabIndex = 246;
            this.dtpDateReference.Visible = false;
            // 
            // cmdRAZ
            // 
            this.cmdRAZ.Location = new System.Drawing.Point(137, 80);
            this.cmdRAZ.Name = "cmdRAZ";
            this.cmdRAZ.Size = new System.Drawing.Size(95, 29);
            this.cmdRAZ.TabIndex = 247;
            this.cmdRAZ.Text = "RAZ";
            this.cmdRAZ.UseVisualStyleBackColor = true;
            this.cmdRAZ.Visible = false;
            this.cmdRAZ.Click += new System.EventHandler(this.cmdRAZ_Click);
            // 
            // cmbMois
            // 
            this.cmbMois.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMois.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMois.FormattingEnabled = true;
            this.cmbMois.Items.AddRange(new object[] {
            "Janvier",
            "Février",
            "Mars",
            "Avril",
            "Mai",
            "Juin",
            "Juillet",
            "Août",
            "Septembre",
            "Octobre",
            "Novembre",
            "Décembre"});
            this.cmbMois.Location = new System.Drawing.Point(99, 21);
            this.cmbMois.Name = "cmbMois";
            this.cmbMois.Size = new System.Drawing.Size(94, 21);
            this.cmbMois.TabIndex = 248;
            this.cmbMois.SelectedIndexChanged += new System.EventHandler(this.cmbMois_SelectedIndexChanged);
            // 
            // dtpAnnee
            // 
            this.dtpAnnee.CustomFormat = "yyyy";
            this.dtpAnnee.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAnnee.Location = new System.Drawing.Point(199, 21);
            this.dtpAnnee.Name = "dtpAnnee";
            this.dtpAnnee.Size = new System.Drawing.Size(52, 20);
            this.dtpAnnee.TabIndex = 249;
            this.dtpAnnee.ValueChanged += new System.EventHandler(this.dtpAnnee_ValueChanged);
            // 
            // FrmGenerationEcheances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 121);
            this.Controls.Add(this.dtpAnnee);
            this.Controls.Add(this.cmbMois);
            this.Controls.Add(this.cmdRAZ);
            this.Controls.Add(this.dtpDateReference);
            this.Controls.Add(this.cmdAnnuler);
            this.Controls.Add(this.cmdGenerer);
            this.Controls.Add(this.Au);
            this.Name = "FrmGenerationEcheances";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmGenerationEcheances";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Au;
        private System.Windows.Forms.Button cmdGenerer;
        private System.Windows.Forms.Button cmdAnnuler;
        private System.Windows.Forms.DateTimePicker dtpDateReference;
        private System.Windows.Forms.Button cmdRAZ;
        private System.Windows.Forms.ComboBox cmbMois;
        private System.Windows.Forms.DateTimePicker dtpAnnee;
    }
}