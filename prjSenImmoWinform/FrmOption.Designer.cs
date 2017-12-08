namespace prjSenImmoWinform
{
    partial class FrmOption
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAdresse = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDateNaissance = new System.Windows.Forms.MaskedTextBox();
            this.txtLieuNaissance = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPrenom = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPrixRevise = new System.Windows.Forms.TextBox();
            this.txtPrixStandard = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSuperficieDeBase = new System.Windows.Forms.TextBox();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSuperficieReelle = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTypeVilla = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNumeroLot = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pDetailsOptions = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCommentaires = new System.Windows.Forms.TextBox();
            this.txtDureeOption = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDateFinPriseOption = new System.Windows.Forms.TextBox();
            this.dtpDatePriseOption = new System.Windows.Forms.DateTimePicker();
            this.label23 = new System.Windows.Forms.Label();
            this.cmdFermer = new System.Windows.Forms.Button();
            this.cmdEnregistrer = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pDetailsOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.groupBox1.Controls.Add(this.txtAdresse);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDateNaissance);
            this.groupBox1.Controls.Add(this.txtLieuNaissance);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtNom);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtPrenom);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(11, 151);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 140);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Prospect";
            // 
            // txtAdresse
            // 
            this.txtAdresse.Location = new System.Drawing.Point(60, 75);
            this.txtAdresse.Multiline = true;
            this.txtAdresse.Name = "txtAdresse";
            this.txtAdresse.ReadOnly = true;
            this.txtAdresse.Size = new System.Drawing.Size(353, 54);
            this.txtAdresse.TabIndex = 43;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Adresse";
            // 
            // txtDateNaissance
            // 
            this.txtDateNaissance.Location = new System.Drawing.Point(60, 49);
            this.txtDateNaissance.Mask = "00/00/0000";
            this.txtDateNaissance.Name = "txtDateNaissance";
            this.txtDateNaissance.ReadOnly = true;
            this.txtDateNaissance.Size = new System.Drawing.Size(172, 20);
            this.txtDateNaissance.TabIndex = 37;
            this.txtDateNaissance.ValidatingType = typeof(System.DateTime);
            // 
            // txtLieuNaissance
            // 
            this.txtLieuNaissance.Location = new System.Drawing.Point(289, 49);
            this.txtLieuNaissance.Name = "txtLieuNaissance";
            this.txtLieuNaissance.ReadOnly = true;
            this.txtLieuNaissance.Size = new System.Drawing.Size(124, 20);
            this.txtLieuNaissance.TabIndex = 38;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(270, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(13, 13);
            this.label13.TabIndex = 42;
            this.label13.Text = "à";
            // 
            // txtNom
            // 
            this.txtNom.Location = new System.Drawing.Point(289, 19);
            this.txtNom.Name = "txtNom";
            this.txtNom.ReadOnly = true;
            this.txtNom.Size = new System.Drawing.Size(124, 20);
            this.txtNom.TabIndex = 36;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(254, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 13);
            this.label12.TabIndex = 41;
            this.label12.Text = "Nom";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 40;
            this.label10.Text = "Né(e) le";
            // 
            // txtPrenom
            // 
            this.txtPrenom.Location = new System.Drawing.Point(60, 19);
            this.txtPrenom.Name = "txtPrenom";
            this.txtPrenom.ReadOnly = true;
            this.txtPrenom.Size = new System.Drawing.Size(172, 20);
            this.txtPrenom.TabIndex = 35;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 39;
            this.label9.Text = "Prénom";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.groupBox2.Controls.Add(this.txtPrixRevise);
            this.groupBox2.Controls.Add(this.txtPrixStandard);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtSuperficieDeBase);
            this.groupBox2.Controls.Add(this.txtPosition);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtSuperficieReelle);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtTypeVilla);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtNumeroLot);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(11, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(434, 137);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lot";
            // 
            // txtPrixRevise
            // 
            this.txtPrixRevise.Location = new System.Drawing.Point(306, 74);
            this.txtPrixRevise.Name = "txtPrixRevise";
            this.txtPrixRevise.ReadOnly = true;
            this.txtPrixRevise.Size = new System.Drawing.Size(119, 20);
            this.txtPrixRevise.TabIndex = 276;
            // 
            // txtPrixStandard
            // 
            this.txtPrixStandard.Location = new System.Drawing.Point(306, 48);
            this.txtPrixStandard.Name = "txtPrixStandard";
            this.txtPrixStandard.ReadOnly = true;
            this.txtPrixStandard.Size = new System.Drawing.Size(119, 20);
            this.txtPrixStandard.TabIndex = 275;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(245, 77);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(55, 13);
            this.label15.TabIndex = 274;
            this.label15.Text = "Prix révisé";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(232, 51);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 13);
            this.label14.TabIndex = 273;
            this.label14.Text = "Prix standard";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(256, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 272;
            this.label2.Text = "Position";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSuperficieDeBase
            // 
            this.txtSuperficieDeBase.Location = new System.Drawing.Point(106, 71);
            this.txtSuperficieDeBase.Name = "txtSuperficieDeBase";
            this.txtSuperficieDeBase.ReadOnly = true;
            this.txtSuperficieDeBase.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSuperficieDeBase.Size = new System.Drawing.Size(119, 20);
            this.txtSuperficieDeBase.TabIndex = 271;
            // 
            // txtPosition
            // 
            this.txtPosition.Location = new System.Drawing.Point(306, 21);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.ReadOnly = true;
            this.txtPosition.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPosition.Size = new System.Drawing.Size(119, 20);
            this.txtPosition.TabIndex = 270;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 269;
            this.label3.Text = "Superficie réelle";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSuperficieReelle
            // 
            this.txtSuperficieReelle.Location = new System.Drawing.Point(106, 97);
            this.txtSuperficieReelle.Name = "txtSuperficieReelle";
            this.txtSuperficieReelle.ReadOnly = true;
            this.txtSuperficieReelle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSuperficieReelle.Size = new System.Drawing.Size(119, 20);
            this.txtSuperficieReelle.TabIndex = 268;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(8, 74);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(95, 13);
            this.label11.TabIndex = 267;
            this.label11.Text = "Superficie de base";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTypeVilla
            // 
            this.txtTypeVilla.Location = new System.Drawing.Point(106, 46);
            this.txtTypeVilla.Name = "txtTypeVilla";
            this.txtTypeVilla.ReadOnly = true;
            this.txtTypeVilla.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTypeVilla.Size = new System.Drawing.Size(119, 20);
            this.txtTypeVilla.TabIndex = 266;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(51, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 265;
            this.label4.Text = "Type villa";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumeroLot
            // 
            this.txtNumeroLot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroLot.Location = new System.Drawing.Point(106, 21);
            this.txtNumeroLot.Name = "txtNumeroLot";
            this.txtNumeroLot.ReadOnly = true;
            this.txtNumeroLot.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtNumeroLot.Size = new System.Drawing.Size(119, 20);
            this.txtNumeroLot.TabIndex = 264;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(36, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 263;
            this.label5.Text = "Numéro lot";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pDetailsOptions
            // 
            this.pDetailsOptions.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pDetailsOptions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pDetailsOptions.Controls.Add(this.label8);
            this.pDetailsOptions.Controls.Add(this.txtCommentaires);
            this.pDetailsOptions.Controls.Add(this.txtDureeOption);
            this.pDetailsOptions.Controls.Add(this.label7);
            this.pDetailsOptions.Controls.Add(this.label6);
            this.pDetailsOptions.Controls.Add(this.txtDateFinPriseOption);
            this.pDetailsOptions.Controls.Add(this.dtpDatePriseOption);
            this.pDetailsOptions.Controls.Add(this.label23);
            this.pDetailsOptions.Location = new System.Drawing.Point(11, 299);
            this.pDetailsOptions.Name = "pDetailsOptions";
            this.pDetailsOptions.Size = new System.Drawing.Size(434, 162);
            this.pDetailsOptions.TabIndex = 264;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label8.Location = new System.Drawing.Point(21, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 278;
            this.label8.Text = "Commentaires";
            // 
            // txtCommentaires
            // 
            this.txtCommentaires.Location = new System.Drawing.Point(97, 64);
            this.txtCommentaires.Multiline = true;
            this.txtCommentaires.Name = "txtCommentaires";
            this.txtCommentaires.Size = new System.Drawing.Size(328, 83);
            this.txtCommentaires.TabIndex = 277;
            // 
            // txtDureeOption
            // 
            this.txtDureeOption.Location = new System.Drawing.Point(97, 38);
            this.txtDureeOption.Name = "txtDureeOption";
            this.txtDureeOption.Size = new System.Drawing.Size(40, 20);
            this.txtDureeOption.TabIndex = 276;
            this.txtDureeOption.Validated += new System.EventHandler(this.txtDureeOption_Validated);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label7.Location = new System.Drawing.Point(58, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 275;
            this.label7.Text = "Durée";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(201, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 274;
            this.label6.Text = "au";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDateFinPriseOption
            // 
            this.txtDateFinPriseOption.Location = new System.Drawing.Point(226, 10);
            this.txtDateFinPriseOption.Name = "txtDateFinPriseOption";
            this.txtDateFinPriseOption.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtDateFinPriseOption.Size = new System.Drawing.Size(98, 20);
            this.txtDateFinPriseOption.TabIndex = 273;
            // 
            // dtpDatePriseOption
            // 
            this.dtpDatePriseOption.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDatePriseOption.Location = new System.Drawing.Point(97, 10);
            this.dtpDatePriseOption.Name = "dtpDatePriseOption";
            this.dtpDatePriseOption.Size = new System.Drawing.Size(98, 20);
            this.dtpDatePriseOption.TabIndex = 257;
            this.dtpDatePriseOption.ValueChanged += new System.EventHandler(this.dtpDatePriseOption_ValueChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label23.Location = new System.Drawing.Point(6, 13);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(88, 13);
            this.label23.TabIndex = 256;
            this.label23.Text = "Option à partir du";
            // 
            // cmdFermer
            // 
            this.cmdFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFermer.Location = new System.Drawing.Point(361, 474);
            this.cmdFermer.Name = "cmdFermer";
            this.cmdFermer.Size = new System.Drawing.Size(86, 34);
            this.cmdFermer.TabIndex = 265;
            this.cmdFermer.Text = "Fermer";
            this.cmdFermer.UseVisualStyleBackColor = true;
            // 
            // cmdEnregistrer
            // 
            this.cmdEnregistrer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdEnregistrer.Location = new System.Drawing.Point(7, 474);
            this.cmdEnregistrer.Name = "cmdEnregistrer";
            this.cmdEnregistrer.Size = new System.Drawing.Size(86, 34);
            this.cmdEnregistrer.TabIndex = 266;
            this.cmdEnregistrer.Text = "Enregistrer";
            this.cmdEnregistrer.UseVisualStyleBackColor = true;
            this.cmdEnregistrer.Click += new System.EventHandler(this.cmdEnregistrer_Click);
            // 
            // FrmOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(458, 518);
            this.Controls.Add(this.cmdEnregistrer);
            this.Controls.Add(this.cmdFermer);
            this.Controls.Add(this.pDetailsOptions);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmOption";
            this.Text = "FrmOption";
            this.Load += new System.EventHandler(this.FrmOption_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pDetailsOptions.ResumeLayout(false);
            this.pDetailsOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel pDetailsOptions;
        private System.Windows.Forms.DateTimePicker dtpDatePriseOption;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.MaskedTextBox txtDateNaissance;
        private System.Windows.Forms.TextBox txtLieuNaissance;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPrenom;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAdresse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPrixRevise;
        private System.Windows.Forms.TextBox txtPrixStandard;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSuperficieDeBase;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSuperficieReelle;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTypeVilla;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNumeroLot;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDureeOption;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDateFinPriseOption;
        private System.Windows.Forms.Button cmdFermer;
        private System.Windows.Forms.Button cmdEnregistrer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCommentaires;
    }
}