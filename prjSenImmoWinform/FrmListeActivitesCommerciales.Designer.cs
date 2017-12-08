namespace prjSenImmoWinform
{
    partial class FrmListeActivitesCommerciales
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
            this.dgActivitesCommerciales = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgActivitesCommerciales)).BeginInit();
            this.SuspendLayout();
            // 
            // dgActivitesCommerciales
            // 
            this.dgActivitesCommerciales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgActivitesCommerciales.Location = new System.Drawing.Point(12, 30);
            this.dgActivitesCommerciales.Name = "dgActivitesCommerciales";
            this.dgActivitesCommerciales.Size = new System.Drawing.Size(423, 376);
            this.dgActivitesCommerciales.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(349, 426);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 26);
            this.button1.TabIndex = 1;
            this.button1.Text = "Fermer";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // FrmListeActivitesCommerciales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 461);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgActivitesCommerciales);
            this.Name = "FrmListeActivitesCommerciales";
            this.Text = "FrmListeActivitesCommerciales";
            ((System.ComponentModel.ISupportInitialize)(this.dgActivitesCommerciales)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgActivitesCommerciales;
        private System.Windows.Forms.Button button1;
    }
}