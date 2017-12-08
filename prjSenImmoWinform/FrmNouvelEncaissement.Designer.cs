namespace prjSenImmoWinform
{
    partial class FrmNouvelEncaissement
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pVersement = new System.Windows.Forms.Panel();
            this.txtDateLivraisonPrevue = new System.Windows.Forms.MaskedTextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.txtMontantReliquat = new System.Windows.Forms.TextBox();
            this.txtMontantVerse = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.pVersement.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.pVersement);
            this.groupBox3.Location = new System.Drawing.Point(6, 74);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(389, 98);
            this.groupBox3.TabIndex = 70;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Nouvel Encaissement";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(286, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 32);
            this.button1.TabIndex = 85;
            this.button1.Text = "Valider";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // pVersement
            // 
            this.pVersement.Controls.Add(this.txtDateLivraisonPrevue);
            this.pVersement.Controls.Add(this.label38);
            this.pVersement.Controls.Add(this.txtMontantReliquat);
            this.pVersement.Controls.Add(this.txtMontantVerse);
            this.pVersement.Controls.Add(this.label32);
            this.pVersement.Location = new System.Drawing.Point(16, 19);
            this.pVersement.Name = "pVersement";
            this.pVersement.Size = new System.Drawing.Size(264, 69);
            this.pVersement.TabIndex = 84;
            this.pVersement.Visible = false;
            // 
            // txtDateLivraisonPrevue
            // 
            this.txtDateLivraisonPrevue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDateLivraisonPrevue.Location = new System.Drawing.Point(144, 35);
            this.txtDateLivraisonPrevue.Mask = "00/00/0000";
            this.txtDateLivraisonPrevue.Name = "txtDateLivraisonPrevue";
            this.txtDateLivraisonPrevue.Size = new System.Drawing.Size(96, 20);
            this.txtDateLivraisonPrevue.TabIndex = 4;
            this.txtDateLivraisonPrevue.ValidatingType = typeof(System.DateTime);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(16, 38);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(128, 13);
            this.label38.TabIndex = 83;
            this.label38.Text = "Date livraison prévue";
            // 
            // txtMontantReliquat
            // 
            this.txtMontantReliquat.Location = new System.Drawing.Point(348, 198);
            this.txtMontantReliquat.Name = "txtMontantReliquat";
            this.txtMontantReliquat.ReadOnly = true;
            this.txtMontantReliquat.Size = new System.Drawing.Size(98, 20);
            this.txtMontantReliquat.TabIndex = 37;
            this.txtMontantReliquat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMontantVerse
            // 
            this.txtMontantVerse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontantVerse.Location = new System.Drawing.Point(145, 9);
            this.txtMontantVerse.Name = "txtMontantVerse";
            this.txtMontantVerse.Size = new System.Drawing.Size(98, 20);
            this.txtMontantVerse.TabIndex = 4;
            this.txtMontantVerse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(56, 12);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(88, 13);
            this.label32.TabIndex = 71;
            this.label32.Text = "Montant versé";
            // 
            // FrmNouvelEncaissement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 216);
            this.Controls.Add(this.groupBox3);
            this.Name = "FrmNouvelEncaissement";
            this.Text = "FrmNouvelEncaissement";
            this.groupBox3.ResumeLayout(false);
            this.pVersement.ResumeLayout(false);
            this.pVersement.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel pVersement;
        private System.Windows.Forms.MaskedTextBox txtDateLivraisonPrevue;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox txtMontantReliquat;
        private System.Windows.Forms.TextBox txtMontantVerse;
        private System.Windows.Forms.Label label32;
    }
}