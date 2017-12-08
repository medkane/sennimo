namespace prjSenImmoWinform
{
    partial class FrmLotEnOptions
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
            this.label31 = new System.Windows.Forms.Label();
            this.txtNbLotsTrouves = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmdRAZ = new System.Windows.Forms.Button();
            this.txtNumeroLotRecherche = new System.Windows.Forms.TextBox();
            this.cmdRechercher = new System.Windows.Forms.Button();
            this.cmbStatuts = new System.Windows.Forms.ComboBox();
            this.cmbTypeVillas = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lbStatut = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.cmbPositions = new System.Windows.Forms.ComboBox();
            this.dgLots = new System.Windows.Forms.DataGridView();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLots)).BeginInit();
            this.SuspendLayout();
            // 
            // label31
            // 
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(224)))), ((int)(((byte)(214)))));
            this.label31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.Black;
            this.label31.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label31.Location = new System.Drawing.Point(12, 123);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(854, 30);
            this.label31.TabIndex = 235;
            this.label31.Text = "Liste des lots";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNbLotsTrouves
            // 
            this.txtNbLotsTrouves.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNbLotsTrouves.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNbLotsTrouves.Location = new System.Drawing.Point(792, 497);
            this.txtNbLotsTrouves.Name = "txtNbLotsTrouves";
            this.txtNbLotsTrouves.ReadOnly = true;
            this.txtNbLotsTrouves.Size = new System.Drawing.Size(74, 20);
            this.txtNbLotsTrouves.TabIndex = 234;
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(651, 499);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(139, 17);
            this.label21.TabIndex = 233;
            this.label21.Text = "Nombre de lots trouvés";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.cmdRAZ);
            this.panel3.Controls.Add(this.txtNumeroLotRecherche);
            this.panel3.Controls.Add(this.cmdRechercher);
            this.panel3.Controls.Add(this.cmbStatuts);
            this.panel3.Controls.Add(this.cmbTypeVillas);
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.label22);
            this.panel3.Controls.Add(this.lbStatut);
            this.panel3.Controls.Add(this.label24);
            this.panel3.Controls.Add(this.cmbPositions);
            this.panel3.Location = new System.Drawing.Point(12, 523);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(854, 40);
            this.panel3.TabIndex = 232;
            // 
            // cmdRAZ
            // 
            this.cmdRAZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRAZ.Location = new System.Drawing.Point(761, 3);
            this.cmdRAZ.Name = "cmdRAZ";
            this.cmdRAZ.Size = new System.Drawing.Size(81, 29);
            this.cmdRAZ.TabIndex = 228;
            this.cmdRAZ.Text = "Réinitialiser";
            this.cmdRAZ.UseVisualStyleBackColor = true;
            // 
            // txtNumeroLotRecherche
            // 
            this.txtNumeroLotRecherche.Location = new System.Drawing.Point(24, 9);
            this.txtNumeroLotRecherche.Name = "txtNumeroLotRecherche";
            this.txtNumeroLotRecherche.Size = new System.Drawing.Size(74, 20);
            this.txtNumeroLotRecherche.TabIndex = 227;
            // 
            // cmdRechercher
            // 
            this.cmdRechercher.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRechercher.Location = new System.Drawing.Point(674, 3);
            this.cmdRechercher.Name = "cmdRechercher";
            this.cmdRechercher.Size = new System.Drawing.Size(81, 29);
            this.cmdRechercher.TabIndex = 220;
            this.cmdRechercher.Text = "Rechercher";
            this.cmdRechercher.UseVisualStyleBackColor = true;
            // 
            // cmbStatuts
            // 
            this.cmbStatuts.FormattingEnabled = true;
            this.cmbStatuts.Location = new System.Drawing.Point(442, 9);
            this.cmbStatuts.Name = "cmbStatuts";
            this.cmbStatuts.Size = new System.Drawing.Size(105, 21);
            this.cmbStatuts.TabIndex = 218;
            // 
            // cmbTypeVillas
            // 
            this.cmbTypeVillas.FormattingEnabled = true;
            this.cmbTypeVillas.Location = new System.Drawing.Point(160, 9);
            this.cmbTypeVillas.Name = "cmbTypeVillas";
            this.cmbTypeVillas.Size = new System.Drawing.Size(74, 21);
            this.cmbTypeVillas.TabIndex = 214;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(106, 12);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(52, 13);
            this.label20.TabIndex = 213;
            this.label20.Text = "Type villa";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(0, 12);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(22, 13);
            this.label22.TabIndex = 212;
            this.label22.Text = "Lot";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbStatut
            // 
            this.lbStatut.AutoSize = true;
            this.lbStatut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatut.Location = new System.Drawing.Point(403, 13);
            this.lbStatut.Name = "lbStatut";
            this.lbStatut.Size = new System.Drawing.Size(35, 13);
            this.lbStatut.TabIndex = 217;
            this.lbStatut.Text = "Statut";
            this.lbStatut.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(242, 13);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(44, 13);
            this.label24.TabIndex = 215;
            this.label24.Text = "Position";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPositions
            // 
            this.cmbPositions.FormattingEnabled = true;
            this.cmbPositions.Location = new System.Drawing.Point(290, 9);
            this.cmbPositions.Name = "cmbPositions";
            this.cmbPositions.Size = new System.Drawing.Size(105, 21);
            this.cmbPositions.TabIndex = 216;
            // 
            // dgLots
            // 
            this.dgLots.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgLots.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgLots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLots.Location = new System.Drawing.Point(12, 156);
            this.dgLots.Name = "dgLots";
            this.dgLots.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgLots.Size = new System.Drawing.Size(854, 335);
            this.dgLots.TabIndex = 231;
            // 
            // FrmLotEnOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 608);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.txtNbLotsTrouves);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.dgLots);
            this.Name = "FrmLotEnOptions";
            this.Text = "FrmLotEnOptions";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLots)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txtNbLotsTrouves;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button cmdRAZ;
        private System.Windows.Forms.TextBox txtNumeroLotRecherche;
        private System.Windows.Forms.Button cmdRechercher;
        private System.Windows.Forms.ComboBox cmbStatuts;
        private System.Windows.Forms.ComboBox cmbTypeVillas;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lbStatut;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cmbPositions;
        private System.Windows.Forms.DataGridView dgLots;
    }
}