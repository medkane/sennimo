namespace prjSenImmoWinform
{
    partial class FrmListeClientsEtProspect
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.txtPrenom = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdRechercherClients = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgClients = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgClients)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtNom);
            this.panel2.Controls.Add(this.txtPrenom);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cmdRechercherClients);
            this.panel2.Location = new System.Drawing.Point(6, 535);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(782, 49);
            this.panel2.TabIndex = 210;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(255, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 210;
            this.label6.Text = "Nom";
            // 
            // txtNom
            // 
            this.txtNom.Location = new System.Drawing.Point(290, 14);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(124, 20);
            this.txtNom.TabIndex = 209;
            this.txtNom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNom_KeyDown);
            // 
            // txtPrenom
            // 
            this.txtPrenom.Location = new System.Drawing.Point(61, 14);
            this.txtPrenom.Name = "txtPrenom";
            this.txtPrenom.Size = new System.Drawing.Size(172, 20);
            this.txtPrenom.TabIndex = 208;
            this.txtPrenom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrenom_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 207;
            this.label5.Text = "Prénom";
            // 
            // cmdRechercherClients
            // 
            this.cmdRechercherClients.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRechercherClients.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdRechercherClients.Location = new System.Drawing.Point(684, 9);
            this.cmdRechercherClients.Name = "cmdRechercherClients";
            this.cmdRechercherClients.Size = new System.Drawing.Size(89, 28);
            this.cmdRechercherClients.TabIndex = 206;
            this.cmdRechercherClients.Text = "Rechercher";
            this.cmdRechercherClients.UseVisualStyleBackColor = true;
            this.cmdRechercherClients.Click += new System.EventHandler(this.cmdRechercherClients_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.dgClients);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(6, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(782, 526);
            this.panel1.TabIndex = 209;
            // 
            // dgClients
            // 
            this.dgClients.AllowUserToAddRows = false;
            this.dgClients.AllowUserToDeleteRows = false;
            this.dgClients.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgClients.Location = new System.Drawing.Point(0, 24);
            this.dgClients.Name = "dgClients";
            this.dgClients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgClients.Size = new System.Drawing.Size(778, 498);
            this.dgClients.TabIndex = 0;
            this.dgClients.SelectionChanged += new System.EventHandler(this.dgClients_SelectionChanged);
            this.dgClients.DoubleClick += new System.EventHandler(this.dgClients_DoubleClick);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SteelBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(778, 24);
            this.label1.TabIndex = 205;
            this.label1.Text = "Liste des clients d\'AKYS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmListeClientsEtProspect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 626);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmListeClientsEtProspect";
            this.Text = "FrmListeClientsEtProspect";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgClients)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.TextBox txtPrenom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdRechercherClients;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgClients;
        private System.Windows.Forms.Label label1;
    }
}