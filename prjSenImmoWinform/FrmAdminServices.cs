using EAGetMail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using prjSenImmoWinform.Models;

namespace prjSenImmoWinform
{
    public partial class FrmAdminServices : Form
    {
        public FrmAdminServices()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void timerAdmin_Tick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ChargerLesMailsDuSiteWeb();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur: " + ex.Message, "Gestion des services d'administration", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private  void ChargerLesMailsDuSiteWeb()
        {

            string curpath = Directory.GetCurrentDirectory();
            string mailbox = String.Format("{0}\\inbox", curpath);

            // If the folder is not existed, create it.
            if (!Directory.Exists(mailbox))
            {
                Directory.CreateDirectory(mailbox);
            }

            // Gmail IMAP4 server is "imap.gmail.com"
            MailServer oServer = new MailServer("imap.gmail.com",
                        "prosopis.akys@gmail.com", "Password@2016", ServerProtocol.Imap4);
            MailClient oClient = new MailClient("TryIt");

            // Set SSL connection,
            oServer.SSLConnection = true;

            // Set 993 IMAP4 port
            oServer.Port = 993;

            try
            {
                oClient.Connect(oServer);
                List<MailInfo> infos = oClient.GetMailInfos().Where(mail => oClient.GetMail(mail).From.Address == "no-reply@akys.sn"
                                                                   && mail.Read == false).ToList();
                //listView1.Items.Clear();
                using (var db =new SenImmoDataContext())
                {
                    foreach (var info in infos)
                    {
                        Mail oMail = oClient.GetMail(info);
                        ListViewItem lviMail = new ListViewItem(oMail.ReceivedDate.ToShortDateString());
                        lviMail.SubItems.Add(oMail.TextBody);

                        var mailBodytext = oMail.TextBody.Split(':');

                        string nom = mailBodytext[1].ToString();
                        nom = nom.Substring(0, nom.Length - 7);
                        nom = nom.ToUpper().Trim();
                        lviMail.SubItems.Add(nom);

                        string prenom = mailBodytext[2].ToString();
                        prenom = prenom.Substring(0, prenom.Length - 7);
                        prenom = Tools.Tools.UppercaseWords(prenom.ToLower());
                        lviMail.SubItems.Add(prenom);

                        string email = mailBodytext[3].ToString();
                        email = email.Substring(0, email.Length - 11).Trim();
                        lviMail.SubItems.Add(email.Trim());

                        string telephone = mailBodytext[4].ToString();
                        telephone = telephone.Substring(0, telephone.Length - 14).Trim();
                        lviMail.SubItems.Add(telephone.Trim());

                        string typeVilla = mailBodytext[5].ToString();
                        typeVilla = typeVilla.Substring(0, typeVilla.Length - 5).Trim();
                        lviMail.SubItems.Add(typeVilla.Trim());

                        string pays = mailBodytext[6].ToString();
                        pays = pays.Trim().ToUpper();
                        lviMail.SubItems.Add(pays);

                        listView1.Items.Add(lviMail);

                        //// Mark email as deleted in GMail account.
                        oClient.MarkAsRead(info, true);
                        // Enregistrement du  prospect
                        var client = new Client();
                        client.DateSouscription = oMail.ReceivedDate;
                        client.Prenom =prenom;
                        client.Nom = nom;
                        client.Mobile1 = telephone;
                        client.Email = email;
                        client.Pays = pays;
                        client.CommentaireProspect = typeVilla;
                        client.DateCreation = DateTime.Now;
                        client.DateDeDelivrancePiece= DateTime.Parse("01/01/1900");
                        client.DateDeNaissance = DateTime.Parse("01/01/1900");
                        client.DateMariage = DateTime.Parse("01/01/1900");
                        client.DateContratMariage = DateTime.Parse("01/01/1900");
                        client.Actif = true;
                        client.Type = TypeClient.ProspectSansOption;
                        client.Origine = db.TypeOrigines.Where(to => to.LibelleTypeOrigine.ToLower() == "Site web").FirstOrDefault();
                        client.ProspectAffecte = false;
                        db.Clients.Add(client);
                        db.SaveChanges();
                    }

                }
                // Quit and purge emails marked as read from Gmail IMAP4 server.
                oClient.Quit();
            }
            catch (Exception ep)
            {
                MessageBox.Show("Erreur:..." + ep.Message);
            }

        }
    }
}
