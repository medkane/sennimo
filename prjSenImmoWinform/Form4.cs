using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjSenImmoWinform
{
    public partial class Form4 : Form
    {
       

        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MailMessage msg = new MailMessage();
            //msg.To.Add(new MailAddress("kmedtech@gmail.com", "Médoune"));
            //msg.From = new MailAddress("prosopis-akys@teyliom.com", "Prosopis");
            //msg.Subject = "This is a Test Mail";
            //msg.Body = "This is a test message using Exchange OnLine";
            //msg.IsBodyHtml = true;

            //SmtpClient client = new SmtpClient();
            //client.UseDefaultCredentials = false;
            //client.Credentials = new System.Net.NetworkCredential("Prosopis", "Password@2016");
            //client.Port = 587; // You can use Port 25 if 587 is blocked (mine is!)
            //client.Host = "smtp.sherweb2010.com";
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = true;
            //try
            //{
            //    client.Send(msg);
            //    lblText.Text = "Message Sent Succesfully";
            //}
            //catch (Exception ex)
            //{
            //    lblText.Text = ex.ToString();
            //}
            //SmtpClient client = new SmtpClient();
            //client.Port = 587;
            //client.Host = "smtp.sherweb2010.com";
            //client.EnableSsl = true;
            //client.Timeout = 10000;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //client.Credentials = new System.Net.NetworkCredential("prosopis-akys@teyliom.com", "Password@2016");

            //MailMessage mm = new MailMessage("prosopis-akys@teyliom.com", "fansou.fofana@teyliom.com", "test", "Bonjour Fansou \n Ceci est un mail envoyé à partir de Prosopis, merci de répondre si tu reçoies ce message");
            //mm.BodyEncoding = UTF8Encoding.UTF8;
            //mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            //client.Send(mm);

            Tools.Tools.EmailSend("kmedtech@gmail.com", "", "Test", "Juste un test\n Via Prosopis");

        }

       
    }
}
