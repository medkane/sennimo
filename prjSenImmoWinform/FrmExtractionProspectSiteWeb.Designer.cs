namespace prjSenImmoWinform
{
    partial class FrmExtractionProspectSiteWeb
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdImporter = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNbProspects = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.panel1.Controls.Add(this.txtNbProspects);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmdImporter);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Location = new System.Drawing.Point(5, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 452);
            this.panel1.TabIndex = 4;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listView1.Location = new System.Drawing.Point(5, 36);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1041, 384);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Date";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Source";
            this.columnHeader2.Width = 277;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Nom";
            this.columnHeader3.Width = 112;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Prénom";
            this.columnHeader4.Width = 148;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Email";
            this.columnHeader5.Width = 154;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Téléphone";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 160;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Type villa";
            this.columnHeader7.Width = 95;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Pays";
            this.columnHeader8.Width = 120;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(954, 464);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "Fermer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(7, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1040, 31);
            this.label1.TabIndex = 206;
            this.label1.Text = "Liste des prospects souscrits à partir du site web";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdImporter
            // 
            this.cmdImporter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdImporter.Image = global::prjSenImmoWinform.Properties.Resources._012___cloud_20;
            this.cmdImporter.Location = new System.Drawing.Point(969, 3);
            this.cmdImporter.Name = "cmdImporter";
            this.cmdImporter.Size = new System.Drawing.Size(75, 28);
            this.cmdImporter.TabIndex = 207;
            this.cmdImporter.Text = "Extraire";
            this.cmdImporter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdImporter.UseVisualStyleBackColor = true;
            this.cmdImporter.Click += new System.EventHandler(this.cmdImporter_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(836, 429);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 208;
            this.label2.Text = "Nombre de prospects";
            // 
            // txtNbProspects
            // 
            this.txtNbProspects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNbProspects.Location = new System.Drawing.Point(946, 425);
            this.txtNbProspects.Name = "txtNbProspects";
            this.txtNbProspects.Size = new System.Drawing.Size(100, 20);
            this.txtNbProspects.TabIndex = 209;
            // 
            // FrmExtractionProspectSiteWeb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 497);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Name = "FrmExtractionProspectSiteWeb";
            this.Text = "FrmExtractionProspectSiteWeb";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cmdImporter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNbProspects;
        private System.Windows.Forms.Label label2;
    }
}