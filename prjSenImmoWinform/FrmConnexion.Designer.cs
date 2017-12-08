namespace prjSenImmoWinform
{
    partial class FrmConnexion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConnexion));
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.cmdConnection = new System.Windows.Forms.Button();
            this.cmdAnnuler = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.pEnvironnement = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pEnvironnement.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLogin
            // 
            this.txtLogin.BackColor = System.Drawing.Color.White;
            this.txtLogin.Location = new System.Drawing.Point(148, 16);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(178, 20);
            this.txtLogin.TabIndex = 11;
            this.txtLogin.Tag = "";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(92, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Utilisateur";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(74, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Mot de passe";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.Location = new System.Drawing.Point(148, 41);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(178, 20);
            this.txtPassword.TabIndex = 13;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // cmdConnection
            // 
            this.cmdConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConnection.Location = new System.Drawing.Point(239, 91);
            this.cmdConnection.Name = "cmdConnection";
            this.cmdConnection.Size = new System.Drawing.Size(86, 31);
            this.cmdConnection.TabIndex = 209;
            this.cmdConnection.Text = "Connecter";
            this.cmdConnection.UseVisualStyleBackColor = true;
            this.cmdConnection.Click += new System.EventHandler(this.cmdConnection_Click);
            // 
            // cmdAnnuler
            // 
            this.cmdAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAnnuler.Location = new System.Drawing.Point(12, 91);
            this.cmdAnnuler.Name = "cmdAnnuler";
            this.cmdAnnuler.Size = new System.Drawing.Size(86, 31);
            this.cmdAnnuler.TabIndex = 210;
            this.cmdAnnuler.Text = "Annuler";
            this.cmdAnnuler.UseVisualStyleBackColor = true;
            this.cmdAnnuler.Click += new System.EventHandler(this.cmdAnnuler_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::prjSenImmoWinform.Properties.Resources.application_pgp_signature2;
            this.pictureBox1.Location = new System.Drawing.Point(15, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 45);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 211;
            this.pictureBox1.TabStop = false;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(122, 3);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(76, 17);
            this.radioButton1.TabIndex = 212;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Production";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(204, 3);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(46, 17);
            this.radioButton2.TabIndex = 213;
            this.radioButton2.Text = "Test";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // pEnvironnement
            // 
            this.pEnvironnement.Controls.Add(this.label1);
            this.pEnvironnement.Controls.Add(this.radioButton2);
            this.pEnvironnement.Controls.Add(this.radioButton1);
            this.pEnvironnement.Location = new System.Drawing.Point(25, 66);
            this.pEnvironnement.Name = "pEnvironnement";
            this.pEnvironnement.Size = new System.Drawing.Size(305, 25);
            this.pEnvironnement.TabIndex = 214;
            this.pEnvironnement.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 214;
            this.label1.Text = "Environnement";
            // 
            // FrmConnexion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(338, 134);
            this.Controls.Add(this.pEnvironnement);
            this.Controls.Add(this.cmdAnnuler);
            this.Controls.Add(this.cmdConnection);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmConnexion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connexion à Prosopis";
            this.Load += new System.EventHandler(this.FrmConnexion_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmConnexion_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pEnvironnement.ResumeLayout(false);
            this.pEnvironnement.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button cmdConnection;
        private System.Windows.Forms.Button cmdAnnuler;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Panel pEnvironnement;
        private System.Windows.Forms.Label label1;
    }
}