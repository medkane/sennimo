namespace prjSenImmoWinform
{
    partial class Form5
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
            this.cmdControlSeuil = new System.Windows.Forms.Button();
            this.dgResult = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdDeRelettrer = new System.Windows.Forms.Button();
            this.cmdPrixDeVente = new System.Windows.Forms.Button();
            this.cmdPrixRevise = new System.Windows.Forms.Button();
            this.cmdFormatterTel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdMAJDateContrat = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.cmdMAJDateEntree = new System.Windows.Forms.Button();
            this.cmdMAJDateFD = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgResult)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdControlSeuil
            // 
            this.cmdControlSeuil.Location = new System.Drawing.Point(9, 7);
            this.cmdControlSeuil.Name = "cmdControlSeuil";
            this.cmdControlSeuil.Size = new System.Drawing.Size(157, 26);
            this.cmdControlSeuil.TabIndex = 0;
            this.cmdControlSeuil.Text = "Control Seuil Contrat";
            this.cmdControlSeuil.UseVisualStyleBackColor = true;
            this.cmdControlSeuil.Visible = false;
            this.cmdControlSeuil.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgResult
            // 
            this.dgResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgResult.Location = new System.Drawing.Point(9, 85);
            this.dgResult.Name = "dgResult";
            this.dgResult.Size = new System.Drawing.Size(845, 346);
            this.dgResult.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(173, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 26);
            this.button1.TabIndex = 2;
            this.button1.Text = "Doubons Contrats";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // cmdDeRelettrer
            // 
            this.cmdDeRelettrer.Location = new System.Drawing.Point(335, 7);
            this.cmdDeRelettrer.Name = "cmdDeRelettrer";
            this.cmdDeRelettrer.Size = new System.Drawing.Size(157, 26);
            this.cmdDeRelettrer.TabIndex = 3;
            this.cmdDeRelettrer.Text = "Dé/Relettrage";
            this.cmdDeRelettrer.UseVisualStyleBackColor = true;
            this.cmdDeRelettrer.Click += new System.EventHandler(this.cmdDeRelettrer_Click);
            // 
            // cmdPrixDeVente
            // 
            this.cmdPrixDeVente.Location = new System.Drawing.Point(498, 7);
            this.cmdPrixDeVente.Name = "cmdPrixDeVente";
            this.cmdPrixDeVente.Size = new System.Drawing.Size(157, 26);
            this.cmdPrixDeVente.TabIndex = 4;
            this.cmdPrixDeVente.Text = "Renseigner Prix de vente";
            this.cmdPrixDeVente.UseVisualStyleBackColor = true;
            this.cmdPrixDeVente.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmdPrixRevise
            // 
            this.cmdPrixRevise.Location = new System.Drawing.Point(661, 7);
            this.cmdPrixRevise.Name = "cmdPrixRevise";
            this.cmdPrixRevise.Size = new System.Drawing.Size(135, 26);
            this.cmdPrixRevise.TabIndex = 5;
            this.cmdPrixRevise.Text = "Calculer Prix Révisé";
            this.cmdPrixRevise.UseVisualStyleBackColor = true;
            this.cmdPrixRevise.Click += new System.EventHandler(this.cmdPrixRevise_Click);
            // 
            // cmdFormatterTel
            // 
            this.cmdFormatterTel.Location = new System.Drawing.Point(22, 440);
            this.cmdFormatterTel.Name = "cmdFormatterTel";
            this.cmdFormatterTel.Size = new System.Drawing.Size(114, 23);
            this.cmdFormatterTel.TabIndex = 6;
            this.cmdFormatterTel.Text = "Formatter N° Tel";
            this.cmdFormatterTel.UseVisualStyleBackColor = true;
            this.cmdFormatterTel.Click += new System.EventHandler(this.cmdFormatterTel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(142, 445);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // cmdMAJDateContrat
            // 
            this.cmdMAJDateContrat.Location = new System.Drawing.Point(195, 438);
            this.cmdMAJDateContrat.Name = "cmdMAJDateContrat";
            this.cmdMAJDateContrat.Size = new System.Drawing.Size(157, 26);
            this.cmdMAJDateContrat.TabIndex = 8;
            this.cmdMAJDateContrat.Text = "MAJ Date Contrat";
            this.cmdMAJDateContrat.UseVisualStyleBackColor = true;
            this.cmdMAJDateContrat.Click += new System.EventHandler(this.cmdMAJDateContrat_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(358, 441);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "MAJ Etat avancement";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(9, 39);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(157, 26);
            this.button3.TabIndex = 10;
            this.button3.Text = "Client avec plusieurs contrats";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // cmdMAJDateEntree
            // 
            this.cmdMAJDateEntree.Location = new System.Drawing.Point(498, 440);
            this.cmdMAJDateEntree.Name = "cmdMAJDateEntree";
            this.cmdMAJDateEntree.Size = new System.Drawing.Size(157, 26);
            this.cmdMAJDateEntree.TabIndex = 11;
            this.cmdMAJDateEntree.Text = "MAJ Date Entree";
            this.cmdMAJDateEntree.UseVisualStyleBackColor = true;
            this.cmdMAJDateEntree.Click += new System.EventHandler(this.cmdMAJDateEntree_Click);
            // 
            // cmdMAJDateFD
            // 
            this.cmdMAJDateFD.Location = new System.Drawing.Point(661, 440);
            this.cmdMAJDateFD.Name = "cmdMAJDateFD";
            this.cmdMAJDateFD.Size = new System.Drawing.Size(188, 26);
            this.cmdMAJDateFD.TabIndex = 12;
            this.cmdMAJDateFD.Text = "MAJ Date Frais de dossier 1900";
            this.cmdMAJDateFD.UseVisualStyleBackColor = true;
            this.cmdMAJDateFD.Click += new System.EventHandler(this.cmdMAJDateFD_Click);
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 475);
            this.Controls.Add(this.cmdMAJDateFD);
            this.Controls.Add(this.cmdMAJDateEntree);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.cmdMAJDateContrat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdFormatterTel);
            this.Controls.Add(this.cmdPrixRevise);
            this.Controls.Add(this.cmdPrixDeVente);
            this.Controls.Add(this.cmdDeRelettrer);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgResult);
            this.Controls.Add(this.cmdControlSeuil);
            this.Name = "Form5";
            this.Text = "Form5";
            this.Load += new System.EventHandler(this.Form5_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdControlSeuil;
        private System.Windows.Forms.DataGridView dgResult;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cmdDeRelettrer;
        private System.Windows.Forms.Button cmdPrixDeVente;
        private System.Windows.Forms.Button cmdPrixRevise;
        private System.Windows.Forms.Button cmdFormatterTel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdMAJDateContrat;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button cmdMAJDateEntree;
        private System.Windows.Forms.Button cmdMAJDateFD;
    }
}