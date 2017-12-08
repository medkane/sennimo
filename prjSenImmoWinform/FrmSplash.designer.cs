namespace prjGeScout
{
    partial class FrmSplash
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
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.transparentLabel5 = new prjGeScout.TransparentLabel();
            this.transparentLabel4 = new prjGeScout.TransparentLabel();
            this.transparentLabel2 = new prjGeScout.TransparentLabel();
            this.labelStatus = new prjGeScout.TransparentLabel();
            this.transparentLabel1 = new prjGeScout.TransparentLabel();
            this.label1 = new prjGeScout.TransparentLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(435, 280);
            this.shapeContainer1.TabIndex = 1;
            this.shapeContainer1.TabStop = false;
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.BackColor = System.Drawing.Color.Transparent;
            this.rectangleShape1.BackgroundImage = global::prjSenImmoWinform.Properties.Resources.bg1;
            this.rectangleShape1.BorderColor = System.Drawing.Color.Transparent;
            this.rectangleShape1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.rectangleShape1.CornerRadius = 15;
            this.rectangleShape1.FillColor = System.Drawing.Color.Transparent;
            this.rectangleShape1.FillGradientColor = System.Drawing.Color.Indigo;
            this.rectangleShape1.FillGradientStyle = Microsoft.VisualBasic.PowerPacks.FillGradientStyle.Horizontal;
            this.rectangleShape1.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.rectangleShape1.Location = new System.Drawing.Point(73, 0);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(620, 282);
            this.rectangleShape1.Click += new System.EventHandler(this.rectangleShape1_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(19, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.TabIndex = 12;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::prjSenImmoWinform.Properties.Resources.Sy9Jd3lq1;
            this.pictureBox1.Location = new System.Drawing.Point(12, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(104, 102);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // transparentLabel5
            // 
            this.transparentLabel5.BackColor = System.Drawing.SystemColors.ControlDark;
            this.transparentLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transparentLabel5.ForeColor = System.Drawing.Color.IndianRed;
            this.transparentLabel5.Location = new System.Drawing.Point(298, 194);
            this.transparentLabel5.Name = "transparentLabel5";
            this.transparentLabel5.Size = new System.Drawing.Size(116, 21);
            this.transparentLabel5.TabIndex = 11;
            this.transparentLabel5.TabStop = false;
            this.transparentLabel5.Text = "by Teyliom Properties";
            this.transparentLabel5.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.transparentLabel5.Click += new System.EventHandler(this.transparentLabel5_Click);
            // 
            // transparentLabel4
            // 
            this.transparentLabel4.BackColor = System.Drawing.SystemColors.ControlDark;
            this.transparentLabel4.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transparentLabel4.ForeColor = System.Drawing.Color.Black;
            this.transparentLabel4.Location = new System.Drawing.Point(260, 109);
            this.transparentLabel4.Name = "transparentLabel4";
            this.transparentLabel4.Size = new System.Drawing.Size(79, 26);
            this.transparentLabel4.TabIndex = 8;
            this.transparentLabel4.TabStop = false;
            this.transparentLabel4.Text = "Version 4.2.11";
            this.transparentLabel4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.transparentLabel4.Click += new System.EventHandler(this.transparentLabel4_Click);
            // 
            // transparentLabel2
            // 
            this.transparentLabel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.transparentLabel2.Font = new System.Drawing.Font("Segoe Print", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transparentLabel2.ForeColor = System.Drawing.Color.Black;
            this.transparentLabel2.Location = new System.Drawing.Point(82, 74);
            this.transparentLabel2.Name = "transparentLabel2";
            this.transparentLabel2.Size = new System.Drawing.Size(287, 42);
            this.transparentLabel2.TabIndex = 6;
            this.transparentLabel2.TabStop = false;
            this.transparentLabel2.Text = "Prosopis";
            this.transparentLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.transparentLabel2.Click += new System.EventHandler(this.transparentLabel2_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.BackColor = System.Drawing.SystemColors.ControlDark;
            this.labelStatus.ForeColor = System.Drawing.Color.Black;
            this.labelStatus.Location = new System.Drawing.Point(12, 219);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(394, 15);
            this.labelStatus.TabIndex = 4;
            this.labelStatus.TabStop = false;
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // transparentLabel1
            // 
            this.transparentLabel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.transparentLabel1.Font = new System.Drawing.Font("Segoe Print", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transparentLabel1.ForeColor = System.Drawing.Color.Black;
            this.transparentLabel1.Location = new System.Drawing.Point(60, 148);
            this.transparentLabel1.Name = "transparentLabel1";
            this.transparentLabel1.Size = new System.Drawing.Size(346, 42);
            this.transparentLabel1.TabIndex = 3;
            this.transparentLabel1.TabStop = false;
            this.transparentLabel1.Text = "Logiciel de gestion commerciale ";
            this.transparentLabel1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.transparentLabel1.Click += new System.EventHandler(this.transparentLabel1_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Font = new System.Drawing.Font("Mistral", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(235, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 26);
            this.label1.TabIndex = 2;
            this.label1.TabStop = false;
            this.label1.Text = "Copyrigth @2017 Teyliom Properties";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // FrmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(435, 280);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.transparentLabel5);
            this.Controls.Add(this.transparentLabel4);
            this.Controls.Add(this.transparentLabel2);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.transparentLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSplash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSplash";
            this.TransparencyKey = System.Drawing.Color.Navy;
            this.Load += new System.EventHandler(this.FrmSplash_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
        private TransparentLabel label1;
        private TransparentLabel transparentLabel1;
        private TransparentLabel labelStatus;
        private TransparentLabel transparentLabel2;
        private TransparentLabel transparentLabel4;
        private TransparentLabel transparentLabel5;
        private System.Windows.Forms.Label label2;
    }
}