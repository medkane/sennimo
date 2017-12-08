namespace prjSenImmoWinform
{
    partial class FrmReportingObjectifVentes
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label22 = new System.Windows.Forms.Label();
            this.cmbChefsEquipes = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(8, 89);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(662, 457);
            this.dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 240;
            this.label1.Text = "Exercice";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(84, 18);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(62, 20);
            this.dateTimePicker1.TabIndex = 239;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(20, 48);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(61, 13);
            this.label22.TabIndex = 238;
            this.label22.Text = "Commercial";
            // 
            // cmbChefsEquipes
            // 
            this.cmbChefsEquipes.BackColor = System.Drawing.SystemColors.Info;
            this.cmbChefsEquipes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChefsEquipes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbChefsEquipes.FormattingEnabled = true;
            this.cmbChefsEquipes.Location = new System.Drawing.Point(84, 44);
            this.cmbChefsEquipes.Name = "cmbChefsEquipes";
            this.cmbChefsEquipes.Size = new System.Drawing.Size(252, 21);
            this.cmbChefsEquipes.TabIndex = 237;
            // 
            // FrmReportingObjectifVentes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 613);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.cmbChefsEquipes);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FrmReportingObjectifVentes";
            this.Text = "FrmReportingObjectifVentes";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cmbChefsEquipes;
    }
}