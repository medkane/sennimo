using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prjSenImmoWinform.Models;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Net.Mail;

namespace prjSenImmoWinform.Tools
{
    public class Tools
    {
        public static SenImmoDataContext db = new SenImmoDataContext();
        public static Commercial Commercial = db.Commercials.Where(c => c.Id == 1).SingleOrDefault();
        public static string DossierTemplates = db.Parametres.Where(c => c.Nom == "DossierModeles").FirstOrDefault().valeurString;
        //public static string Projet = db.Parametres.Where(c => c.Nom == "Projet").FirstOrDefault().valeurString;
        public static Agent AgentEnCours { get; set; }

        public static Projet ProjetEnCours { get; set; }
        public static Form MDIForm;


        public static string Encrypt(string strToEncrypt, string strKey)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto =
                    new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = ASCIIEncoding.ASCII.GetBytes(strToEncrypt);
                return Convert.ToBase64String(objDESCrypto.CreateEncryptor().
                    TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }

        /// <summary>
        /// Decrypt the given string using the specified key.
        /// </summary>
        /// <param name="strEncrypted">The string to be decrypted.</param>
        /// <param name="strKey">The decryption key.</param>
        /// <returns>The decrypted string.</returns>
        public static string Decrypt(string strEncrypted, string strKey)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto =
                    new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = Convert.FromBase64String(strEncrypted);
                string strDecrypted = ASCIIEncoding.ASCII.GetString
                (objDESCrypto.CreateDecryptor().TransformFinalBlock
                (byteBuff, 0, byteBuff.Length));
                objDESCrypto = null;
                return strDecrypted;
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }

        public static string GetSemestre(int month)
        {
            string strSemestre = string.Empty;

            switch (month)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    strSemestre = "premier semestre";
                    break;
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                    strSemestre = "deuxieme semestre";
                    break;
                default:
                    break;
            }
            return strSemestre;
        }
        public static void EmailSend(string destinataire, string FichierPath, string sujet, string body)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.sherweb2010.com";
                client.EnableSsl = true;
                client.Timeout = 700000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("prosopis-akys@teyliom.com", "Password@2016");

                // MailMessage mm = new MailMessage("prosopis-akys@teyliom.com", "fansou.fofana@teyliom.com", "test", "Bonjour Fansou \n Ceci est un mail envoyé à partir de Prosopis, merci de répondre si tu reçoies ce message");


                // Create instance of message
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.BodyEncoding = UTF8Encoding.UTF8;
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                // Add receiver
                message.To.Add(destinataire);

                // Set sender
                // In this case the same as the username
                message.From = new System.Net.Mail.MailAddress("prosopis-akys@teyliom.com");

                // Set subject
                message.Subject = sujet;

                // Set body of message
                message.Body = body;

                if (FichierPath != string.Empty)
                    message.Attachments.Add(new Attachment(FichierPath));

                // Send the message
                client.Send(message);

                // Clean up
                message = null;
            }
            catch (Exception)
	        {
                throw;
            }

           
        }

        //public static void EmailSend(string destinataire, string FichierPath, string sujet, string body)
        //{
        //    //string dossierTemplates = Tools.Tools.DossierTemplates;
        //    //string templateName = dossierTemplates + "FactureAppelDeFonds.dotx";

        //    var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
        //    client.EnableSsl = true;
        //    client.UseDefaultCredentials = false;
        //    client.Credentials = new System.Net.NetworkCredential("kmedtech@gmail.com", "JHzZ1LR2");

        //    try
        //    {

        //        // Create instance of message
        //        System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

        //        // Add receiver
        //        message.To.Add(destinataire);

        //        // Set sender
        //        // In this case the same as the username
        //        message.From = new System.Net.Mail.MailAddress("kmedtech@gmail.com");

        //        // Set subject
        //        message.Subject = sujet;

        //        // Set body of message
        //        message.Body = body;

        //        if(FichierPath!=string.Empty)
        //            message.Attachments.Add(new Attachment(FichierPath));

        //        // Send the message
        //        client.Send(message);

        //        // Clean up
        //        message = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}


        public static string UppercaseWords(string value)
        {
            char[] array = value.ToLower().ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

    }
}
