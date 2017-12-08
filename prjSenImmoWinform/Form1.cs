using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjSenImmoWinform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmdAfficher_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Veullez saisir votre age:");
            int iAge = int.Parse(Console.ReadLine());

            if (iAge <= 10)
                Console.WriteLine("Désolé t'es trop jeune pour ce jeu!");
            else
                if (iAge >= 10 && iAge <= 25)
                        Console.WriteLine("Bienvenue dans 'Monster Attack' votre jeu préféré!");
                else
                    Console.WriteLine("Dis, t'es pas un peu trop vieux pour ces c.....!");

        }
               
        





private void splitContainer1_Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}

namespace Mathematique
{
    class Calcul
    {
        static void AfficherCarrePaire(int iLimiteSup)
        {
            int iAffiche=0;
            for (int i = 0; i < iLimiteSup; i++)
            {
                if (i % 2 == 0)
                {
                    iAffiche = i;
                    iAffiche *= iAffiche;
                    Console.WriteLine(iAffiche);
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Saisir un nombre");
            int iLimite = int.Parse(Console.ReadLine());
            AfficherCarrePaire(iLimite);
            Console.ReadKey();
        }
    }
}
