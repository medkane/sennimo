using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace prjGeScout
{
    public partial class FrmSplash : Form
    {
        private TransparentLabel lbTitre;
        public FrmSplash()
        {
            InitializeComponent();        

        }

        private void rectangleShape1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FrmSplash_Load(object sender, EventArgs e)
        {
            //label1.BackColor = Color.Transparent;
            this.Show();
            this.RePaint();
            Application.DoEvents(); // Finish Paint 
            label1.Refresh();
            transparentLabel1.Refresh();
            transparentLabel2.Refresh();
            transparentLabel4.Refresh();
            transparentLabel5.Refresh();

            Cursor.Current = Cursors.WaitCursor;

            // Simulate some activity (e.g. connect to database, caching data, retrieving defaults) 
            //this.labelStatus.Text = "1: Connexion au serveur central... .";
            //this.labelStatus.Refresh();
            System.Threading.Thread.Sleep(3000);
            //this.RePaint();
            // Simulate some activity 
            //this.labelStatus.Text = "2: Gestion du versionning ...";
            //this.labelStatus.Refresh();
            System.Threading.Thread.Sleep(1000);
            //this.RePaint();
            // Simulate some activity 
            //this.labelStatus.Text = "3: Connexion à la base de données ...";
            //this.labelStatus.Refresh();
            System.Threading.Thread.Sleep(1000);
            //this.RePaint();
            //Close Form 
            this.Close();

        }
        protected void RePaint()
        {

            GraphicsPath graphicpath = new GraphicsPath();

            graphicpath.StartFigure();

            graphicpath.AddArc(0, 0, 25, 25, 180, 90);

            graphicpath.AddLine(25, 0, this.Width - 25, 0);

            graphicpath.AddArc(this.Width - 25, 0, 25, 25, 270, 90);

            graphicpath.AddLine(this.Width, 25, this.Width, this.Height - 25);

            graphicpath.AddArc(this.Width - 25, this.Height - 25, 25, 25, 0, 90);

            graphicpath.AddLine(this.Width - 25, this.Height, 25, this.Height);

            graphicpath.AddArc(0, this.Height - 25, 25, 25, 90, 90);

            graphicpath.CloseFigure();

            this.Region = new Region(graphicpath);

        }

        private void transparentLabel1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void transparentLabel2_Click(object sender, EventArgs e)
        {

        }

        private void transparentLabel5_Click(object sender, EventArgs e)
        {

        }

        private void transparentLabel4_Click(object sender, EventArgs e)
        {

        }
    }
}
