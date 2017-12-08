namespace prjSenImmoWinform
{
    partial class FrmRepriseDeDonnees
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
            this.txtDemarrer = new System.Windows.Forms.Button();
            this.cmdClients = new System.Windows.Forms.Button();
            this.cmdClientsDepot = new System.Windows.Forms.Button();
            this.cmdMAJNumeroLot = new System.Windows.Forms.Button();
            this.cmdEtatAvancement = new System.Windows.Forms.Button();
            this.cmdEncaissements = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dgEncaissements = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNbClients = new System.Windows.Forms.TextBox();
            this.txtTotalEncaissements = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdOptions = new System.Windows.Forms.Button();
            this.cmdMAJPrixRevise = new System.Windows.Forms.Button();
            this.cmdGenererContratResa = new System.Windows.Forms.Button();
            this.cmdNiveauEncResa = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.cmdImporterEncaissements = new System.Windows.Forms.Button();
            this.cmdFraisDeDossierKerria = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEncaissements)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDemarrer
            // 
            this.txtDemarrer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDemarrer.Location = new System.Drawing.Point(9, 527);
            this.txtDemarrer.Name = "txtDemarrer";
            this.txtDemarrer.Size = new System.Drawing.Size(63, 27);
            this.txtDemarrer.TabIndex = 0;
            this.txtDemarrer.Text = "Lots";
            this.txtDemarrer.UseVisualStyleBackColor = true;
            this.txtDemarrer.Click += new System.EventHandler(this.txtDemarrer_Click);
            // 
            // cmdClients
            // 
            this.cmdClients.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdClients.Location = new System.Drawing.Point(78, 527);
            this.cmdClients.Name = "cmdClients";
            this.cmdClients.Size = new System.Drawing.Size(94, 27);
            this.cmdClients.TabIndex = 2;
            this.cmdClients.Text = "Clients Résa";
            this.cmdClients.UseVisualStyleBackColor = true;
            this.cmdClients.Click += new System.EventHandler(this.cmdClients_Click);
            // 
            // cmdClientsDepot
            // 
            this.cmdClientsDepot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdClientsDepot.Location = new System.Drawing.Point(178, 527);
            this.cmdClientsDepot.Name = "cmdClientsDepot";
            this.cmdClientsDepot.Size = new System.Drawing.Size(94, 27);
            this.cmdClientsDepot.TabIndex = 3;
            this.cmdClientsDepot.Text = "Clients Dépôt";
            this.cmdClientsDepot.UseVisualStyleBackColor = true;
            this.cmdClientsDepot.Visible = false;
            this.cmdClientsDepot.Click += new System.EventHandler(this.cmdClientsDepot_Click);
            // 
            // cmdMAJNumeroLot
            // 
            this.cmdMAJNumeroLot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdMAJNumeroLot.Location = new System.Drawing.Point(278, 527);
            this.cmdMAJNumeroLot.Name = "cmdMAJNumeroLot";
            this.cmdMAJNumeroLot.Size = new System.Drawing.Size(104, 27);
            this.cmdMAJNumeroLot.TabIndex = 4;
            this.cmdMAJNumeroLot.Text = "MAJ Numéro Lot";
            this.cmdMAJNumeroLot.UseVisualStyleBackColor = true;
            this.cmdMAJNumeroLot.Visible = false;
            this.cmdMAJNumeroLot.Click += new System.EventHandler(this.cmdMAJNumeroLot_Click);
            // 
            // cmdEtatAvancement
            // 
            this.cmdEtatAvancement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdEtatAvancement.Location = new System.Drawing.Point(390, 527);
            this.cmdEtatAvancement.Name = "cmdEtatAvancement";
            this.cmdEtatAvancement.Size = new System.Drawing.Size(134, 27);
            this.cmdEtatAvancement.TabIndex = 5;
            this.cmdEtatAvancement.Text = "MAJ Etat Avancement";
            this.cmdEtatAvancement.UseVisualStyleBackColor = true;
            this.cmdEtatAvancement.Visible = false;
            this.cmdEtatAvancement.Click += new System.EventHandler(this.cmdEtatAvancement_Click);
            // 
            // cmdEncaissements
            // 
            this.cmdEncaissements.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEncaissements.Location = new System.Drawing.Point(806, 560);
            this.cmdEncaissements.Name = "cmdEncaissements";
            this.cmdEncaissements.Size = new System.Drawing.Size(98, 27);
            this.cmdEncaissements.TabIndex = 6;
            this.cmdEncaissements.Text = "Encaissements";
            this.cmdEncaissements.UseVisualStyleBackColor = true;
            this.cmdEncaissements.Click += new System.EventHandler(this.cmdEncaissements_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 154);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(994, 181);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // dgEncaissements
            // 
            this.dgEncaissements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgEncaissements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEncaissements.Location = new System.Drawing.Point(12, 386);
            this.dgEncaissements.Name = "dgEncaissements";
            this.dgEncaissements.Size = new System.Drawing.Size(994, 123);
            this.dgEncaissements.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(812, 346);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Nombre de clients";
            // 
            // txtNbClients
            // 
            this.txtNbClients.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNbClients.Location = new System.Drawing.Point(906, 343);
            this.txtNbClients.Name = "txtNbClients";
            this.txtNbClients.Size = new System.Drawing.Size(100, 20);
            this.txtNbClients.TabIndex = 10;
            // 
            // txtTotalEncaissements
            // 
            this.txtTotalEncaissements.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalEncaissements.Location = new System.Drawing.Point(906, 535);
            this.txtTotalEncaissements.Name = "txtTotalEncaissements";
            this.txtTotalEncaissements.Size = new System.Drawing.Size(100, 20);
            this.txtTotalEncaissements.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(799, 538);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Total Encaissements";
            // 
            // cmdOptions
            // 
            this.cmdOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdOptions.Location = new System.Drawing.Point(530, 528);
            this.cmdOptions.Name = "cmdOptions";
            this.cmdOptions.Size = new System.Drawing.Size(134, 27);
            this.cmdOptions.TabIndex = 13;
            this.cmdOptions.Text = "Générer les options";
            this.cmdOptions.UseVisualStyleBackColor = true;
            this.cmdOptions.Click += new System.EventHandler(this.cmdOptions_Click);
            // 
            // cmdMAJPrixRevise
            // 
            this.cmdMAJPrixRevise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdMAJPrixRevise.Location = new System.Drawing.Point(9, 560);
            this.cmdMAJPrixRevise.Name = "cmdMAJPrixRevise";
            this.cmdMAJPrixRevise.Size = new System.Drawing.Size(104, 29);
            this.cmdMAJPrixRevise.TabIndex = 14;
            this.cmdMAJPrixRevise.Text = "MAJ Prix révisé Lot";
            this.cmdMAJPrixRevise.UseVisualStyleBackColor = true;
            this.cmdMAJPrixRevise.Click += new System.EventHandler(this.cmdMAJPrixRevise_Click);
            // 
            // cmdGenererContratResa
            // 
            this.cmdGenererContratResa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdGenererContratResa.Location = new System.Drawing.Point(136, 560);
            this.cmdGenererContratResa.Name = "cmdGenererContratResa";
            this.cmdGenererContratResa.Size = new System.Drawing.Size(136, 29);
            this.cmdGenererContratResa.TabIndex = 15;
            this.cmdGenererContratResa.Text = "Générer Contrat Résa";
            this.cmdGenererContratResa.UseVisualStyleBackColor = true;
            this.cmdGenererContratResa.Click += new System.EventHandler(this.cmdGenererContratResa_Click);
            // 
            // cmdNiveauEncResa
            // 
            this.cmdNiveauEncResa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdNiveauEncResa.Location = new System.Drawing.Point(292, 560);
            this.cmdNiveauEncResa.Name = "cmdNiveauEncResa";
            this.cmdNiveauEncResa.Size = new System.Drawing.Size(141, 29);
            this.cmdNiveauEncResa.TabIndex = 16;
            this.cmdNiveauEncResa.Text = "Niveau Encaissement Résa";
            this.cmdNiveauEncResa.UseVisualStyleBackColor = true;
            this.cmdNiveauEncResa.Click += new System.EventHandler(this.cmdNiveauEncResa_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(13, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(993, 136);
            this.richTextBox1.TabIndex = 17;
            this.richTextBox1.Text = "";
            // 
            // cmdImporterEncaissements
            // 
            this.cmdImporterEncaissements.Location = new System.Drawing.Point(449, 562);
            this.cmdImporterEncaissements.Name = "cmdImporterEncaissements";
            this.cmdImporterEncaissements.Size = new System.Drawing.Size(134, 23);
            this.cmdImporterEncaissements.TabIndex = 18;
            this.cmdImporterEncaissements.Text = "Importer Encaissements";
            this.cmdImporterEncaissements.UseVisualStyleBackColor = true;
            this.cmdImporterEncaissements.Click += new System.EventHandler(this.cmdImporterEncaissements_Click);
            // 
            // cmdFraisDeDossierKerria
            // 
            this.cmdFraisDeDossierKerria.Location = new System.Drawing.Point(600, 563);
            this.cmdFraisDeDossierKerria.Name = "cmdFraisDeDossierKerria";
            this.cmdFraisDeDossierKerria.Size = new System.Drawing.Size(121, 23);
            this.cmdFraisDeDossierKerria.TabIndex = 19;
            this.cmdFraisDeDossierKerria.Text = "Frais de dossier Kerria";
            this.cmdFraisDeDossierKerria.UseVisualStyleBackColor = true;
            this.cmdFraisDeDossierKerria.Click += new System.EventHandler(this.cmdFraisDeDossierKerria_Click);
            // 
            // FrmRepriseDeDonnees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 600);
            this.Controls.Add(this.cmdFraisDeDossierKerria);
            this.Controls.Add(this.cmdImporterEncaissements);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.cmdNiveauEncResa);
            this.Controls.Add(this.cmdGenererContratResa);
            this.Controls.Add(this.cmdMAJPrixRevise);
            this.Controls.Add(this.cmdOptions);
            this.Controls.Add(this.txtTotalEncaissements);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNbClients);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgEncaissements);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmdEncaissements);
            this.Controls.Add(this.cmdEtatAvancement);
            this.Controls.Add(this.cmdMAJNumeroLot);
            this.Controls.Add(this.cmdClientsDepot);
            this.Controls.Add(this.cmdClients);
            this.Controls.Add(this.txtDemarrer);
            this.Name = "FrmRepriseDeDonnees";
            this.Text = "FrmRepriseDeDonnees";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEncaissements)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button txtDemarrer;
        private System.Windows.Forms.Button cmdClients;
        private System.Windows.Forms.Button cmdClientsDepot;
        private System.Windows.Forms.Button cmdMAJNumeroLot;
        private System.Windows.Forms.Button cmdEtatAvancement;
        private System.Windows.Forms.Button cmdEncaissements;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dgEncaissements;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNbClients;
        private System.Windows.Forms.TextBox txtTotalEncaissements;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdOptions;
        private System.Windows.Forms.Button cmdMAJPrixRevise;
        private System.Windows.Forms.Button cmdGenererContratResa;
        private System.Windows.Forms.Button cmdNiveauEncResa;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button cmdImporterEncaissements;
        private System.Windows.Forms.Button cmdFraisDeDossierKerria;
    }
}