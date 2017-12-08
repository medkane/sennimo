using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using System.Globalization;

namespace prjSenImmoWinform
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime first_date = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1);
            DateTime last_date = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, DateTime.DaysInMonth(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month));
            MessageBox.Show(last_date.ToString("MMMM", CultureInfo.CurrentCulture).ToUpper());
        }


        private void GenererAttestationVersementContratResa()
        {
            try
            {
                Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
                msWord.Visible = true; // mettez cette variable à true si vous souhaitez visualiser les opérations.
                object missing = System.Reflection.Missing.Value;


                Microsoft.Office.Interop.Word.Document doc;
                // Choisir le template
                string dossierTemplates = Tools.Tools.DossierTemplates;
                object templateName = dossierTemplates + "testBullet.dotx";


                // Créer le document
                doc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);

                Microsoft.Office.Interop.Word.Bookmarks bookmarks = null;
                bookmarks = doc.Bookmarks;
                Microsoft.Office.Interop.Word.Bookmark myBookmark = null;
                Microsoft.Office.Interop.Word.Range bookmarkRange = null;

               
                Paragraph assets = doc.Content.Paragraphs.Add();

                assets.Range.ListFormat.ApplyBulletDefault();
                string[] bulletItems = new string[] { "One", "Two", "Three" };

                for (int i = 0; i < bulletItems.Length; i++)
                {
                    string bulletItem = bulletItems[i];
                    if (i < bulletItems.Length - 1)
                        bulletItem = bulletItem + "\n";
                    assets.Range.InsertBefore(bulletItem);
                }

               
                //// Attribuer le nom
                //object fileName = @"Mon nouveau document.doc";
                ////// Sauver le document
                ////doc.SaveAs(ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing, ref missing, ref missing, ref missing, ref missing,
                ////            ref missing);
                //// Fermer le document
                //doc.Close(ref missing, ref missing, ref missing);


                //// Fermeture de word
                //msWord.Quit(ref missing, ref missing, ref missing);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var mfi = new DateTimeFormatInfo();
            MessageBox.Show(CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(1));
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text= textBox1.Text.Replace(" ", string.Empty);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
