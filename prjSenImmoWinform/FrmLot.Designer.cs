namespace prjSenImmoWinform
{
    partial class FrmLot
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
            this.components = new System.ComponentModel.Container();
            this.cmLots = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.choisirCeLotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbTypeVillas = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbIlots = new System.Windows.Forms.ComboBox();
            this.cmbPositions = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdEnregistrer = new System.Windows.Forms.Button();
            this.txtPrixStandard = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSuperficieStandard = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtSuperficieReelle = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPrixRevise = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNomCommercial = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtProjet = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pPosition = new System.Windows.Forms.Panel();
            this.pEtage = new System.Windows.Forms.Panel();
            this.cmbEtages = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lb = new System.Windows.Forms.Label();
            this.txtNumeroLot = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmdSupprimer = new System.Windows.Forms.Button();
            this.cmLots.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pPosition.SuspendLayout();
            this.pEtage.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmLots
            // 
            this.cmLots.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.choisirCeLotToolStripMenuItem});
            this.cmLots.Name = "cmLots";
            this.cmLots.Size = new System.Drawing.Size(144, 26);
            // 
            // choisirCeLotToolStripMenuItem
            // 
            this.choisirCeLotToolStripMenuItem.Name = "choisirCeLotToolStripMenuItem";
            this.choisirCeLotToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.choisirCeLotToolStripMenuItem.Text = "Choisir ce lot";
            // 
            // cmbTypeVillas
            // 
            this.cmbTypeVillas.BackColor = System.Drawing.Color.White;
            this.cmbTypeVillas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeVillas.FormattingEnabled = true;
            this.cmbTypeVillas.Location = new System.Drawing.Point(98, 100);
            this.cmbTypeVillas.Name = "cmbTypeVillas";
            this.cmbTypeVillas.Size = new System.Drawing.Size(200, 21);
            this.cmbTypeVillas.TabIndex = 3;
            this.cmbTypeVillas.SelectedIndexChanged += new System.EventHandler(this.cmbTypeVillas_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(38, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 17);
            this.label4.TabIndex = 197;
            this.label4.Text = "Type villa";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbIlots
            // 
            this.cmbIlots.BackColor = System.Drawing.Color.White;
            this.cmbIlots.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIlots.FormattingEnabled = true;
            this.cmbIlots.Location = new System.Drawing.Point(98, 47);
            this.cmbIlots.Name = "cmbIlots";
            this.cmbIlots.Size = new System.Drawing.Size(200, 21);
            this.cmbIlots.TabIndex = 0;
            this.cmbIlots.SelectedIndexChanged += new System.EventHandler(this.cmbIlots_SelectedIndexChanged);
            this.cmbIlots.Validated += new System.EventHandler(this.cmbIlots_Validated);
            // 
            // cmbPositions
            // 
            this.cmbPositions.BackColor = System.Drawing.Color.White;
            this.cmbPositions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPositions.FormattingEnabled = true;
            this.cmbPositions.Location = new System.Drawing.Point(57, 3);
            this.cmbPositions.Name = "cmbPositions";
            this.cmbPositions.Size = new System.Drawing.Size(200, 21);
            this.cmbPositions.TabIndex = 5;
            this.cmbPositions.SelectedIndexChanged += new System.EventHandler(this.cmbPositions_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 17);
            this.label5.TabIndex = 199;
            this.label5.Text = "Position";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(29, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 17);
            this.label3.TabIndex = 196;
            this.label3.Text = "Numéro ilôt";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdEnregistrer
            // 
            this.cmdEnregistrer.Location = new System.Drawing.Point(274, 344);
            this.cmdEnregistrer.Name = "cmdEnregistrer";
            this.cmdEnregistrer.Size = new System.Drawing.Size(89, 33);
            this.cmdEnregistrer.TabIndex = 204;
            this.cmdEnregistrer.Text = "Enregistrer";
            this.cmdEnregistrer.UseVisualStyleBackColor = true;
            this.cmdEnregistrer.Click += new System.EventHandler(this.cmdEnregistrer_Click);
            // 
            // txtPrixStandard
            // 
            this.txtPrixStandard.Location = new System.Drawing.Point(124, 69);
            this.txtPrixStandard.Name = "txtPrixStandard";
            this.txtPrixStandard.ReadOnly = true;
            this.txtPrixStandard.Size = new System.Drawing.Size(126, 20);
            this.txtPrixStandard.TabIndex = 209;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(42, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 17);
            this.label9.TabIndex = 208;
            this.label9.Text = "Prix standard";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSuperficieStandard
            // 
            this.txtSuperficieStandard.Location = new System.Drawing.Point(124, 15);
            this.txtSuperficieStandard.Name = "txtSuperficieStandard";
            this.txtSuperficieStandard.ReadOnly = true;
            this.txtSuperficieStandard.Size = new System.Drawing.Size(126, 20);
            this.txtSuperficieStandard.TabIndex = 205;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(11, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 17);
            this.label7.TabIndex = 204;
            this.label7.Text = "Superficie standard";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(799, 562);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 33);
            this.button1.TabIndex = 206;
            this.button1.Text = "Fermer";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtSuperficieReelle
            // 
            this.txtSuperficieReelle.BackColor = System.Drawing.Color.White;
            this.txtSuperficieReelle.Location = new System.Drawing.Point(98, 219);
            this.txtSuperficieReelle.Name = "txtSuperficieReelle";
            this.txtSuperficieReelle.Size = new System.Drawing.Size(200, 20);
            this.txtSuperficieReelle.TabIndex = 4;
            this.txtSuperficieReelle.TextChanged += new System.EventHandler(this.txtSuperficieReelle_TextChanged);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 221);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 17);
            this.label10.TabIndex = 210;
            this.label10.Text = "Surperficie réelle";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPrixRevise
            // 
            this.txtPrixRevise.Location = new System.Drawing.Point(98, 282);
            this.txtPrixRevise.Name = "txtPrixRevise";
            this.txtPrixRevise.ReadOnly = true;
            this.txtPrixRevise.Size = new System.Drawing.Size(124, 20);
            this.txtPrixRevise.TabIndex = 213;
            this.txtPrixRevise.TextChanged += new System.EventHandler(this.txtPrixRevise_TextChanged);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(15, 283);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 17);
            this.label11.TabIndex = 212;
            this.label11.Text = "Prix révisé";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // txtNomCommercial
            // 
            this.txtNomCommercial.Location = new System.Drawing.Point(124, 42);
            this.txtNomCommercial.Name = "txtNomCommercial";
            this.txtNomCommercial.ReadOnly = true;
            this.txtNomCommercial.Size = new System.Drawing.Size(126, 20);
            this.txtNomCommercial.TabIndex = 215;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 17);
            this.label2.TabIndex = 214;
            this.label2.Text = "Nom commercial";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(4, 344);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 33);
            this.button2.TabIndex = 217;
            this.button2.Text = "Annuler";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.txtProjet);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.pPosition);
            this.panel2.Controls.Add(this.pEtage);
            this.panel2.Controls.Add(this.txtPrixRevise);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.lb);
            this.panel2.Controls.Add(this.txtNumeroLot);
            this.panel2.Controls.Add(this.cmbTypeVillas);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cmbIlots);
            this.panel2.Controls.Add(this.txtSuperficieReelle);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(359, 334);
            this.panel2.TabIndex = 219;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // txtProjet
            // 
            this.txtProjet.BackColor = System.Drawing.Color.White;
            this.txtProjet.Location = new System.Drawing.Point(84, 5);
            this.txtProjet.Name = "txtProjet";
            this.txtProjet.Size = new System.Drawing.Size(138, 20);
            this.txtProjet.TabIndex = 229;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(-4, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 9);
            this.groupBox1.TabIndex = 228;
            this.groupBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 17);
            this.label6.TabIndex = 227;
            this.label6.Text = "Projet";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pPosition
            // 
            this.pPosition.Controls.Add(this.cmbPositions);
            this.pPosition.Controls.Add(this.label5);
            this.pPosition.Location = new System.Drawing.Point(41, 246);
            this.pPosition.Name = "pPosition";
            this.pPosition.Size = new System.Drawing.Size(278, 26);
            this.pPosition.TabIndex = 225;
            // 
            // pEtage
            // 
            this.pEtage.Controls.Add(this.cmbEtages);
            this.pEtage.Controls.Add(this.label1);
            this.pEtage.Location = new System.Drawing.Point(366, 198);
            this.pEtage.Name = "pEtage";
            this.pEtage.Size = new System.Drawing.Size(278, 26);
            this.pEtage.TabIndex = 224;
            // 
            // cmbEtages
            // 
            this.cmbEtages.BackColor = System.Drawing.Color.White;
            this.cmbEtages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEtages.FormattingEnabled = true;
            this.cmbEtages.Location = new System.Drawing.Point(55, 3);
            this.cmbEtages.Name = "cmbEtages";
            this.cmbEtages.Size = new System.Drawing.Size(200, 21);
            this.cmbEtages.TabIndex = 222;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 223;
            this.label1.Text = "Etage";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb
            // 
            this.lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb.Location = new System.Drawing.Point(3, 73);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(91, 17);
            this.lb.TabIndex = 221;
            this.lb.Text = "Numéro lot";
            this.lb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumeroLot
            // 
            this.txtNumeroLot.BackColor = System.Drawing.Color.White;
            this.txtNumeroLot.Location = new System.Drawing.Point(98, 74);
            this.txtNumeroLot.Name = "txtNumeroLot";
            this.txtNumeroLot.Size = new System.Drawing.Size(84, 20);
            this.txtNumeroLot.TabIndex = 220;
            this.txtNumeroLot.TextChanged += new System.EventHandler(this.txtNumeroLot_TextChanged);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtSuperficieStandard);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.txtPrixStandard);
            this.panel3.Controls.Add(this.txtNomCommercial);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(13, 110);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(306, 100);
            this.panel3.TabIndex = 219;
            // 
            // cmdSupprimer
            // 
            this.cmdSupprimer.Location = new System.Drawing.Point(99, 344);
            this.cmdSupprimer.Name = "cmdSupprimer";
            this.cmdSupprimer.Size = new System.Drawing.Size(89, 33);
            this.cmdSupprimer.TabIndex = 220;
            this.cmdSupprimer.Text = "Supprimer";
            this.cmdSupprimer.UseVisualStyleBackColor = true;
            this.cmdSupprimer.Click += new System.EventHandler(this.cmdSupprimer_Click);
            // 
            // FrmLot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.ClientSize = new System.Drawing.Size(368, 389);
            this.Controls.Add(this.cmdSupprimer);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.cmdEnregistrer);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "FrmLot";
            this.Text = "Ajout d\'un lot";
            this.Load += new System.EventHandler(this.FrmLot_Load);
            this.cmLots.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pPosition.ResumeLayout(false);
            this.pEtage.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbTypeVillas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbIlots;
        private System.Windows.Forms.ComboBox cmbPositions;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdEnregistrer;
        private System.Windows.Forms.TextBox txtSuperficieStandard;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPrixStandard;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ContextMenuStrip cmLots;
        private System.Windows.Forms.ToolStripMenuItem choisirCeLotToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSuperficieReelle;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPrixRevise;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNomCommercial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lb;
        private System.Windows.Forms.TextBox txtNumeroLot;
        private System.Windows.Forms.Button cmdSupprimer;
        private System.Windows.Forms.ComboBox cmbEtages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pPosition;
        private System.Windows.Forms.Panel pEtage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtProjet;
    }
}