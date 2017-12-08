namespace prjSenImmoWinform
{
    partial class FrmRoleFormAccess
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
            this.cmbRoles = new System.Windows.Forms.ComboBox();
            this.dgMenu = new System.Windows.Forms.DataGridView();
            this.cmMenus = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.supprimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgSousMenu = new System.Windows.Forms.DataGridView();
            this.cmSousMenus = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.supprimerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dgActions = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdAjouterMenu = new System.Windows.Forms.Button();
            this.cmbMenus = new System.Windows.Forms.ComboBox();
            this.Menu = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSousMenus = new System.Windows.Forms.ComboBox();
            this.cmdAjouterSousMenu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgMenu)).BeginInit();
            this.cmMenus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSousMenu)).BeginInit();
            this.cmSousMenus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgActions)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbRoles
            // 
            this.cmbRoles.FormattingEnabled = true;
            this.cmbRoles.Location = new System.Drawing.Point(46, 13);
            this.cmbRoles.Name = "cmbRoles";
            this.cmbRoles.Size = new System.Drawing.Size(172, 21);
            this.cmbRoles.TabIndex = 0;
            this.cmbRoles.SelectedIndexChanged += new System.EventHandler(this.cmbRoles_SelectedIndexChanged);
            // 
            // dgMenu
            // 
            this.dgMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMenu.ContextMenuStrip = this.cmMenus;
            this.dgMenu.Location = new System.Drawing.Point(12, 50);
            this.dgMenu.MultiSelect = false;
            this.dgMenu.Name = "dgMenu";
            this.dgMenu.ReadOnly = true;
            this.dgMenu.RowHeadersVisible = false;
            this.dgMenu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMenu.Size = new System.Drawing.Size(445, 197);
            this.dgMenu.TabIndex = 1;
            this.dgMenu.SelectionChanged += new System.EventHandler(this.dgMenu_SelectionChanged);
            // 
            // cmMenus
            // 
            this.cmMenus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.supprimerToolStripMenuItem});
            this.cmMenus.Name = "cmMenus";
            this.cmMenus.Size = new System.Drawing.Size(130, 26);
            // 
            // supprimerToolStripMenuItem
            // 
            this.supprimerToolStripMenuItem.Name = "supprimerToolStripMenuItem";
            this.supprimerToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.supprimerToolStripMenuItem.Text = "Supprimer";
            this.supprimerToolStripMenuItem.Click += new System.EventHandler(this.supprimerToolStripMenuItem_Click);
            // 
            // dgSousMenu
            // 
            this.dgSousMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSousMenu.ContextMenuStrip = this.cmSousMenus;
            this.dgSousMenu.Location = new System.Drawing.Point(12, 282);
            this.dgSousMenu.Name = "dgSousMenu";
            this.dgSousMenu.ReadOnly = true;
            this.dgSousMenu.RowHeadersVisible = false;
            this.dgSousMenu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSousMenu.Size = new System.Drawing.Size(445, 280);
            this.dgSousMenu.TabIndex = 2;
            // 
            // cmSousMenus
            // 
            this.cmSousMenus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.supprimerToolStripMenuItem1});
            this.cmSousMenus.Name = "cmSousMenus";
            this.cmSousMenus.Size = new System.Drawing.Size(130, 26);
            // 
            // supprimerToolStripMenuItem1
            // 
            this.supprimerToolStripMenuItem1.Name = "supprimerToolStripMenuItem1";
            this.supprimerToolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.supprimerToolStripMenuItem1.Text = "Supprimer";
            this.supprimerToolStripMenuItem1.Click += new System.EventHandler(this.supprimerToolStripMenuItem1_Click);
            // 
            // dgActions
            // 
            this.dgActions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgActions.Location = new System.Drawing.Point(463, 50);
            this.dgActions.Name = "dgActions";
            this.dgActions.Size = new System.Drawing.Size(367, 512);
            this.dgActions.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Rôle";
            // 
            // cmdAjouterMenu
            // 
            this.cmdAjouterMenu.Location = new System.Drawing.Point(382, 253);
            this.cmdAjouterMenu.Name = "cmdAjouterMenu";
            this.cmdAjouterMenu.Size = new System.Drawing.Size(75, 23);
            this.cmdAjouterMenu.TabIndex = 5;
            this.cmdAjouterMenu.Text = "Ajouter";
            this.cmdAjouterMenu.UseVisualStyleBackColor = true;
            this.cmdAjouterMenu.Click += new System.EventHandler(this.cmdAjouterMenu_Click);
            // 
            // cmbMenus
            // 
            this.cmbMenus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMenus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMenus.FormattingEnabled = true;
            this.cmbMenus.Location = new System.Drawing.Point(107, 255);
            this.cmbMenus.Name = "cmbMenus";
            this.cmbMenus.Size = new System.Drawing.Size(269, 21);
            this.cmbMenus.TabIndex = 6;
            // 
            // Menu
            // 
            this.Menu.AutoSize = true;
            this.Menu.Location = new System.Drawing.Point(72, 258);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(34, 13);
            this.Menu.TabIndex = 7;
            this.Menu.Text = "Menu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 572);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Sous-menu";
            // 
            // cmbSousMenus
            // 
            this.cmbSousMenus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSousMenus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSousMenus.FormattingEnabled = true;
            this.cmbSousMenus.Location = new System.Drawing.Point(107, 570);
            this.cmbSousMenus.Name = "cmbSousMenus";
            this.cmbSousMenus.Size = new System.Drawing.Size(269, 21);
            this.cmbSousMenus.TabIndex = 9;
            // 
            // cmdAjouterSousMenu
            // 
            this.cmdAjouterSousMenu.Location = new System.Drawing.Point(382, 569);
            this.cmdAjouterSousMenu.Name = "cmdAjouterSousMenu";
            this.cmdAjouterSousMenu.Size = new System.Drawing.Size(75, 23);
            this.cmdAjouterSousMenu.TabIndex = 8;
            this.cmdAjouterSousMenu.Text = "Ajouter";
            this.cmdAjouterSousMenu.UseVisualStyleBackColor = true;
            this.cmdAjouterSousMenu.Click += new System.EventHandler(this.cmdAjouterSousMenu_Click);
            // 
            // FrmRoleFormAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 598);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbSousMenus);
            this.Controls.Add(this.cmdAjouterSousMenu);
            this.Controls.Add(this.Menu);
            this.Controls.Add(this.cmbMenus);
            this.Controls.Add(this.cmdAjouterMenu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgActions);
            this.Controls.Add(this.dgSousMenu);
            this.Controls.Add(this.dgMenu);
            this.Controls.Add(this.cmbRoles);
            this.Name = "FrmRoleFormAccess";
            this.Text = "FrmRoleFormAccess";
            ((System.ComponentModel.ISupportInitialize)(this.dgMenu)).EndInit();
            this.cmMenus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgSousMenu)).EndInit();
            this.cmSousMenus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgActions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbRoles;
        private System.Windows.Forms.DataGridView dgMenu;
        private System.Windows.Forms.DataGridView dgSousMenu;
        private System.Windows.Forms.DataGridView dgActions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdAjouterMenu;
        private System.Windows.Forms.ComboBox cmbMenus;
        private System.Windows.Forms.Label Menu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSousMenus;
        private System.Windows.Forms.Button cmdAjouterSousMenu;
        private System.Windows.Forms.ContextMenuStrip cmMenus;
        private System.Windows.Forms.ToolStripMenuItem supprimerToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmSousMenus;
        private System.Windows.Forms.ToolStripMenuItem supprimerToolStripMenuItem1;
    }
}