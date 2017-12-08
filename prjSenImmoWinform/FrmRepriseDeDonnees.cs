using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using prjSenImmoWinform.Models;
using prjSenImmoWinform.DAL;

namespace prjSenImmoWinform
{
    public partial class FrmRepriseDeDonnees : Form
    {
        private SenImmoDataContext db;
        private IlotRepository iloRep;

        private ContratRepository contratRep;

        public FrmRepriseDeDonnees()
        {
            InitializeComponent();
            db = new SenImmoDataContext();
            iloRep = new IlotRepository();
            contratRep = new ContratRepository();
        }

        private void txtDemarrer_Click(object sender, EventArgs e)
        {
            Lot newLot = null;
            SqlConnection upDateCnx = new SqlConnection(@"Data Source= SRV-SC\;Initial Catalog=AKYSDATABASE;Integrated Security=false;User Id = sa; Password = JHzZ3LR4");
            upDateCnx.Open();
            SqlCommand UpdateCmd = null;
            try
            {
                using (SqlConnection cnx = new SqlConnection(@"Data Source=  SRV-SC;Initial Catalog=AKYSDATABASE;Integrated Security=false;User Id = sa; Password = JHzZ3LR4"))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM ImportLotsKerria WHERE Importe <> 1 AND LTRIM(RTRIM([ PRIX en FCFA ]))<> '' ");
                    cmd.Connection = cnx;
                    cnx.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var ilot = (reader.GetValue(1).ToString());
                        var lot = (reader.GetValue(0)).ToString();
                        var typeVilla = (reader.GetValue(2).ToString());
                        var superficie = (reader.GetValue(5).ToString());
                        var position = (reader.GetValue(3).ToString());
                        var niveau = (reader.GetValue(4).ToString());
                        var prix = (reader.GetValue(6).ToString());
                        //var nom = (reader.GetValue(6).ToString());
                        //txtLog.AppendText(lot);

                        newLot = new Lot();
                        newLot.NumeroLot = lot;
                        newLot.Superficie = decimal.Parse(superficie);
                        if (position.Trim().ToLower() == "0")
                            newLot.PositionLot = PositionLot.Angle;
                        else
                            newLot.PositionLot = PositionLot.Standard;

                        newLot.IlotID = int.Parse(ilot);
                        newLot.TypeVillaID = int.Parse(typeVilla);
                        newLot.PrixRevise = decimal.Parse(prix);
                        switch (niveau)
                        {
                            case "1":
                                newLot.NiveauAppartement = NiveauAppartement.RDC;
                                break;
                            case "2":
                                newLot.NiveauAppartement = NiveauAppartement.Premier;
                                break;
                            case "3":
                                newLot.NiveauAppartement = NiveauAppartement.Deuxième;
                                break;
                            case "4":
                                newLot.NiveauAppartement = NiveauAppartement.Troisième;
                                break;
                            case "5":
                                newLot.NiveauAppartement = NiveauAppartement.Quatrième;
                                break;
                            case "6":
                                newLot.NiveauAppartement = NiveauAppartement.Cinquième;
                                break;
                            case "7":
                                newLot.NiveauAppartement = NiveauAppartement.Sixième;
                                break;
                            default:
                                newLot.NiveauAppartement = 0;
                                break;
                        }
                        //if (prenom != string.Empty || nom != string.Empty)
                        //    newLot.StatutLot = StatutLot.Reserve;


                        //var differenceSurface = superficieReelle - superficieStandard;
                        //var differencePrix = differenceSurface * 40000;
                        //prixRevise = prixStandard + differencePrix;

                        //if (position == PositionLot.Angle)
                        //    prixRevise += prixRevise * 10 / 100;

                        iloRep.AddLot(newLot,2);
                        db.SaveChanges();

                        UpdateCmd = new SqlCommand("UPDATE ImportLotsKerria SET Importe=1 WHERE [N° lot]='" + newLot.NumeroLot+"'");
                        UpdateCmd.Connection = upDateCnx;

                        UpdateCmd.ExecuteNonQuery();


                    }

                    MessageBox.Show("OK");
                }
            }
            catch (Exception ex)
	        {
                if(newLot!=null)
                    richTextBox1.AppendText(newLot.NumeroLot+"\n");
                MessageBox.Show("Erreur: "+ ex.Message);
            }

        }

        private void cmdClients_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection upDateCnx = new SqlConnection(@"Data Source= SRV-SC\;Initial Catalog=AKYSDATABASE;Integrated Security=false;User Id = sa; Password = JHzZ3LR4");
                upDateCnx.Open();
                SqlCommand UpdateCmd = null;

                using (SqlConnection cnx = new SqlConnection(@"Data Source= SRV-SC;Initial Catalog=AKYSDATABASE;Integrated Security=false;User Id = sa; Password = JHzZ3LR4"))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM ClientsKerria R WHERE Client IS NOT NULL AND Importe='false';");
                    cmd.Connection = cnx;
                    cnx.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var client = (reader.GetValue(0)).ToString();
                        var lot = (reader.GetValue(1).ToString());
                        var compteTiers = (reader.GetValue(2).ToString());
                       
                        var loginCommercial = (reader.GetValue(4).ToString());
                        //var adresse = (reader.GetValue(6).ToString());
                        //var telephone = (reader.GetValue(7).ToString());
                        //var mail = (reader.GetValue(8).ToString());
                        //var titre = (reader.GetValue(3).ToString());


                        var commercial = db.Agents.Where(c =>c.UserLogin.Trim().ToLower()==loginCommercial.Trim().ToLower()).FirstOrDefault();
                        var newProspect = new Client();
                        newProspect.Prenom = client;
                        newProspect.DateCreation = DateTime.Now;
                        newProspect.DateDeDelivrancePiece = DateTime.Now.AddDays(-1);
                        newProspect.CompteTiers = compteTiers;
                        if(commercial!=null)
                            newProspect.CommercialID = commercial.Id;
                        newProspect.Mobile2 = lot;


                        //var differenceSurface = superficieReelle - superficieStandard;
                        //var differencePrix = differenceSurface * 40000;
                        //prixRevise = prixStandard + differencePrix;

                        //if (position == PositionLot.Angle)
                        //    prixRevise += prixRevise * 10 / 100;

                        db.Clients.Add(newProspect);
                        db.SaveChanges();

                        UpdateCmd = new SqlCommand("UPDATE ClientsKerria SET Importe=1 WHERE [N° lot]='" + lot + "'");
                        UpdateCmd.Connection = upDateCnx;

                        UpdateCmd.ExecuteNonQuery();

                    }

                    MessageBox.Show("OK");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void cmdClientsDepot_Click(object sender, EventArgs e)
        {

            try
            {
                using (SqlConnection cnx = new SqlConnection(@"Data Source= MEDKANE-PC\SQLSERVER;Initial Catalog=AKYSDATABASE;Integrated Security=false;User Id = sa; Password = JHzZ3LR4"))
                {
                    SqlCommand cmd = new SqlCommand(@"SELECT*
                                                      FROM[AKYSDATABASE].[dbo].[CLIENTSDEPOT] D
                                                      WHERE D.[N°] IS NOT NULL ; ");
                    cmd.Connection = cnx;
                    cnx.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var Numero = (reader.GetValue(0).ToString());
                        var type = (reader.GetValue(1).ToString());
                        var prenom = (reader.GetValue(2)).ToString();
                        // var nom = (reader.GetValue(5).ToString());
                        var adresse = (reader.GetValue(5).ToString());
                        var telephone = (reader.GetValue(3).ToString());
                        var mail = (reader.GetValue(4).ToString());
                        //var titre = (reader.GetValue(3).ToString());



                        var newProspect = new Client();
                        newProspect.NumeroClient = "D" + Numero;
                        newProspect.Prenom = prenom;
                        //newProspect.Nom = nom;
                        //if (titre.ToLower() == "monsieur")
                        //    newProspect.Genre = Genre.Masculin;
                        //else if (titre.ToLower() == "madame")
                        //    newProspect.Genre = Genre.Féminin;
                        newProspect.DateCreation = DateTime.Now.AddDays(-1);
                        newProspect.DateDeDelivrancePiece = DateTime.Now.AddDays(-1);
                        newProspect.Adresse = adresse;
                        newProspect.Mobile1 = telephone;
                        newProspect.Email = mail;

                        newProspect.Fax = type;
                        //var differenceSurface = superficieReelle - superficieStandard;
                        //var differencePrix = differenceSurface * 40000;
                        //prixRevise = prixStandard + differencePrix;

                        //if (position == PositionLot.Angle)
                        //    prixRevise += prixRevise * 10 / 100;

                        db.Clients.Add(newProspect);
                        db.SaveChanges();


                    }

                    MessageBox.Show("OK");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void cmdMAJNumeroLot_Click(object sender, EventArgs e)
        {
            var lots = db.Lots.ToList();
            foreach (var lot in lots)
            {
                lot.NumeroLot = lot.Ilot.NomIlot + lot.NumeroLot;
            }
            db.SaveChanges();
        }

        private void cmdEtatAvancement_Click(object sender, EventArgs e)
        {
            try
            {
                var lots = db.Lots.ToList();
                foreach (var lot in lots)
                {
                    var ea = db.ImportEtatAvancements.Where(eav => eav.NumeroLot == lot.NumeroLot).FirstOrDefault();
                    if (ea != null)
                    {
                        var TypeEtatAvancement = db.TypeEtatAvancements.Where(tea => tea.LibelleTechnique == ea.Avancement).FirstOrDefault();
                        var newEtatAvancement = new EtatAvancement()
                        {
                            DateSaisieAvancement = DateTime.Now,
                            LotId = lot.ID,
                            TypeEtatAvancementID = TypeEtatAvancement.ID,
                            Actif = true,
                            Statut = StatutEtatAvancement.Terminé
                        };
                        db.EtatAvancements.Add(newEtatAvancement);
                    }
                }
                db.SaveChanges();
                MessageBox.Show("OK");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cmdEncaissements_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                List<ImportCompteTiers> lesCompteTiersTrouves = new List<ImportCompteTiers>();
                List<ClientView> clientsViews = new List<ClientView>();
                var clients = db.Clients.Where(cl => cl.Importe==false).OrderBy(cl => cl.Prenom).ToList();
                foreach (var client in clients)
                {
                    if (client.NumeroClient =="R52" ||
                        client.NumeroClient == "R112" ||
                        client.NumeroClient == "R113" ||
                        client.NumeroClient == "R591" ||
                        client.NumeroClient == "R597")
                    {
                        bool trouvre = true;
                    }


                        ////V1 Correspondance sur NomComplet et IntituléCompteTiers
                        //var compteTiers = db.ImportCompteTiers
                        //    .Where(ct => ct.InituleComptetiers != null && ct.InituleComptetiers.Contains(client.NomComplet));

                        ////V2 Correspondance sur NomComplet et IntituléCompteTiers; TypeVilla ou Lot
                        //var compteTiers = db.ImportCompteTiers
                        //    .Where(ct => ct.InituleComptetiers != null 
                        //                 && ct.InituleComptetiers.Contains(client.NomComplet)
                        //                 && (ct.InituleComptetiers.Contains(client.Fax) ||
                        //                 ct.InituleComptetiers.Contains(client.Mobile2))
                        //    );

                        //V3 Correspondance exact sur NomComplet et IntituléCompteTiers; TypeVilla ou Lot
                        var compteTiers = db.ImportCompteTiers
                        .Where(ct => ct.InituleComptetiers != null
                                     && ct.InituleComptetiers.Contains(client.NomComplet)
                                     && (client.Mobile2 == null ? ct.InituleComptetiers.Contains(client.Fax) :
                                     ct.InituleComptetiers.Contains(client.Mobile2))
                        ).FirstOrDefault();
                    

                    if (compteTiers != null)
                    {
                        clientsViews.Add(new ClientView()
                        {
                            NomComplet = client.NomComplet,
                            Téléphone = client.Mobile1,
                            Adresse = client.Adresse,
                            Email = client.Email,
                            TypeContrat = client.Mobile2 != null ? "Résa" : "Dépot",
                            Lot = client.Mobile2,
                            TypeVilla = client.Fax,
                            CompteTiers = compteTiers.Compte,
                            IntituleCompteTiers = compteTiers.InituleComptetiers,

                        });

                        lesCompteTiersTrouves.Add(compteTiers);
                        client.CompteTiers = compteTiers.Compte;
                        client.IntituleCompteTiers = compteTiers.InituleComptetiers;
                        client.Importe = true;
                        db.SaveChanges();
                        var lesEncaissements = db.ImportEncaissements
                             .Where(enc => enc.Compte == compteTiers.Compte && enc.Importe==false)
                             .OrderBy(enc => enc.DateEncaissement).ToList();

                        //Pour chaque encaissement importé créer un encaissement prospect
                        foreach (var enc in lesEncaissements)
                        {

                            var versement = new EncaissementProspect()
                            {
                                NumeroEncaissement = enc.Reference,
                                DateEncaissement = enc.DateEncaissement.Date,
                                MontantGlobal = enc.Montant,
                                ProspectId = client.ID,
                                ReferencePaiement = enc.Libelle,
                                Commentaire = "Encaissement importés à partir du compte: "+enc.Compte +" de " + enc.InituleComptetiers
                            };

                        
                            db.EncaissementProspects.Add(versement);
                            enc.Importe = true;
                            enc.iDClient = client.ID;
                            enc.NomClient = client.NomComplet;
                        }
                        db.SaveChanges();
                        //dgEncaissements.DataSource = lesEncaissements.ToList();
                    }
                }

                dataGridView1.DataSource = clientsViews.ToList();
                //dataGridView1.Columns[7].Visible = false;
                //dataGridView1.Columns[8].Visible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                txtNbClients.Text = clientsViews.Count.ToString();
                //db.SaveChanges();
                //MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                       "Prosopis - Reprise de données encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    string compteTiers = (string)dataGridView1.SelectedRows[0].Cells[7].Value;

                    var lesEncaissements = db.ImportEncaissements
                         .Where(enc => enc.Compte == compteTiers)
                         .OrderBy(enc => enc.DateEncaissement);
                    dgEncaissements.DataSource = lesEncaissements.ToList();
                    dgEncaissements.Columns[6].DefaultCellStyle.Format = "### ### ###";
                    dgEncaissements.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgEncaissements.Columns[7].Visible = false;
                    dgEncaissements.Columns[8].Visible = false;
                    dgEncaissements.Columns[9].Visible = false;
                    dgEncaissements.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                    txtTotalEncaissements.Text= lesEncaissements.Sum(enc =>enc.Montant).ToString("### ### ###");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                  "Prosopis - Reprise de données encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdOptions_Click(object sender, EventArgs e)
        {
            try
            {
                var clients = db.Clients.ToList();
                foreach (var client in clients)
                {
                    var lot = db.Lots.Where(l => l.NumeroLot == client.Mobile2).FirstOrDefault();
                    if (lot != null)
                    {

                        var newOption = new Option()
                        {

                            ClientID = client.ID,
                            CommercialID = 4,
                            LotId = lot.ID,
                            PositionLot = lot.PositionLot,
                            PrixDeVente = lot.PrixRevise,
                            TypeContratId = 1,
                            TypeVillaId = lot.TypeVillaID,
                            Active = true,
                            SeuilContratAtteint = false,
                            ContratGenere = false,
                            DatePriseOption = DateTime.Now,

                        };
                        db.Options.Add(newOption);
                    }
                        
                }
                
                db.SaveChanges();
                MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                 "Prosopis - Reprise de données encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdMAJPrixRevise_Click(object sender, EventArgs e)
        {
            try
            {
                var lots = db.Lots.ToList();
                foreach (var lot in lots)
                {

                    var prixStandard = lot.TypeVilla.PrixStandard;
                    var superficieStandard = lot.TypeVilla.SurfaceDeBase;
                    var superficieReelle = lot.Superficie;
                    var position = lot.PositionLot;


                    var differenceSurface = superficieReelle - superficieStandard;
                    var differencePrix = differenceSurface * 40000;
                    var prixRevise = prixStandard + differencePrix;

                    if (position == PositionLot.Angle)
                        prixRevise += prixRevise * 10 / 100;

                    lot.PrixRevise = prixRevise;

                }

                db.SaveChanges();
                MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                 "Prosopis - Reprise de données encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdGenererContratResa_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    var Clients = db.Clients.Where(cl => cl.Mobile2=="BC80").ToList();
            //    foreach (var client in Clients)
            //    {
            //        var contratRep = new ContratRepository();
            //        var leTypeContratEnCours = db.TypeContrats.Where(tc => tc.CategorieContrat == CategorieContrat.Réservation).FirstOrDefault();
            //        var lot = db.Lots.Where(l => l.NumeroLot == client.Mobile2).FirstOrDefault();
            //        if(lot!=null)
            //        {
            //            var encaissements = db.EncaissementProspects.Where(enc => enc.ProspectId == client.ID).Sum(enc => (decimal?)enc.MontantGlobal) ?? 0;

            //            if ((encaissements / lot.PrixRevise)*100 >= 30)
            //            {
            //                int contratId = contratRep.AjouterContratReservationBis(client.ID, client.CommercialID.Value, 0, lot.ID,
            //                                                                   lot.PrixRevise, lot.PrixRevise, 0, 0, leTypeContratEnCours,
            //                                                                  DateTime.Now, DateTime.Now, 100,false,0);
            //            }
            //        }

            //    }

            //    db.SaveChanges();
            //    MessageBox.Show("OK");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, "Erreur:..." + ex.Message,
            //                     "Prosopis - Reprise de données encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
           
        }

        private void cmdNiveauEncResa_Click(object sender, EventArgs e)
        {
            try
            {
                var Clients = db.Clients.Where(cl => cl.Mobile2 !=null).ToList();
                dataGridView1.DataSource = Clients.Select
                    (cl => new
                    {
                        client = cl.NomComplet,
                        lot = cl.Mobile2,
                        PrixVente = db.Lots.Where(l => l.NumeroLot == cl.Mobile2).FirstOrDefault()!=null?db.Lots.Where(l => l.NumeroLot == cl.Mobile2).FirstOrDefault().PrixRevise:0,
                        Encaisse = db.EncaissementProspects.Where(enc => enc.ProspectId == cl.ID).Sum(enc =>(decimal?) enc.MontantGlobal)??0,
                     
                    }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                 "Prosopis - Reprise de données encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdImporterEncaissements_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;

                SqlConnection upDateCnx = new SqlConnection(@"Data Source= SRV-SC\;Initial Catalog=AKYSDATABASE;Integrated Security=false;User Id = sa; Password = JHzZ3LR4");
                upDateCnx.Open();
                SqlCommand UpdateCmd = null;

                using (SqlConnection cnx = new SqlConnection(@"Data Source= SRV-SC;Initial Catalog=AKYSDATABASE;Integrated Security=false;User Id = sa; Password = JHzZ3LR4"))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [AKYSDATABASE].[dbo].ImportEncaissementsKerria R WHERE Importe='false';");
                    //Pour chaque encaissement importé créer un encaissement prospect
                    cmd.Connection = cnx;
                    cnx.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var compteTiers = (reader.GetValue(0)).ToString();
                        if (compteTiers.Trim() != "41113LAMINEFALL") continue;
                        var piece = (reader.GetValue(3)).ToString();
                        var dateEncaissement = (reader.GetValue(1).ToString());
                        var references = (reader.GetValue(7).ToString());
                        var montant = (reader.GetValue(12).ToString());
                        if (montant == string.Empty) continue;
                            decimal dMontant = decimal.Parse(montant);
                        var id = (reader.GetValue(16)).ToString();
                        DateTime dDateEncaissement = DateTime.Parse(dateEncaissement);

                        //var loginCommercial = (reader.GetValue(4).ToString());
                       

                        var client = db.Clients.Where(c => c.CompteTiers.Trim().ToLower() == compteTiers.Trim().ToLower()).FirstOrDefault();
                        if (client != null)
                        {
                            var versement = new EncaissementProspect()
                            {
                                NumeroEncaissement = piece,
                                DateEncaissement = dDateEncaissement,
                                MontantGlobal = dMontant,
                                ProspectId = client.ID,
                                ReferencePaiement = references,
                                Commentaire = "Encaissement importés à partir du compte: " + compteTiers + " de " + client.Prenom
                            };


                            db.EncaissementProspects.Add(versement);

                            UpdateCmd = new SqlCommand("UPDATE ImportEncaissementsKerria SET Importe=1 WHERE id=" + id);
                            UpdateCmd.Connection = upDateCnx;
                            UpdateCmd.ExecuteNonQuery();
                        }
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                    //dgEncaissements.DataSource = lesEncaissements.ToList();
                }

                //dataGridView1.DataSource = clientsViews.ToList();
                ////dataGridView1.Columns[7].Visible = false;
                ////dataGridView1.Columns[8].Visible = false;
                //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                //txtNbClients.Text = clientsViews.Count.ToString();
                ////db.SaveChanges();
                ////MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                       "Prosopis - Reprise de données encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdFraisDeDossierKerria_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.AppStarting;

                using (SqlConnection cnx = new SqlConnection(@"Data Source= SRV-SC;Initial Catalog=AKYSDATABASE;Integrated Security=false;User Id = sa; Password = JHzZ3LR4"))
                {
                    SqlCommand cmd = new SqlCommand(@"SELECT [ID]
                                                      ,[Nom]
                                                      ,[Prenom]
                                                      ,K.[N° lot]
                                                      ,[CommercialID],
                                                      C.CompteTiers,
                                                      K.[compte tiers]
                                                  FROM [AKYSDATABASE].[dbo].[Clients] C,[AKYSDATABASE].[dbo].ClientsKerria K
                                                  WHERE C.CompteTiers=K.[compte tiers]
                                                  AND CommercialID IS NOT NULL
                                                  AND C.CompteTiers <> '#N/A'");
                    //Pour chaque encaissement importé créer un encaissement prospect
                    cmd.Connection = cnx;
                    cnx.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var id = (reader.GetValue(0)).ToString();
                        var prospectId = int.Parse(id);
                        contratRep.EnregistrerFraisDeDossierProspect(prospectId, new DateTime(2017,01,01),
                            200000, ModePaiement.Virement, "Frais de dossier reprise de données Kérria", "Frais de dossier reprise de données Kérria positionné par défaut");
                    }
                    //dgEncaissements.DataSource = lesEncaissements.ToList();
                }

            //dataGridView1.DataSource = clientsViews.ToList();
            ////dataGridView1.Columns[7].Visible = false;
            ////dataGridView1.Columns[8].Visible = false;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            //txtNbClients.Text = clientsViews.Count.ToString();
            ////db.SaveChanges();
            ////MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                       "Prosopis - Reprise de données encaissements", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
}
    }

    public class ClientView
    {
        public string NomComplet { get; set; }
        public string Téléphone { get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }

        public string TypeContrat { get; set; }
        public string TypeVilla { get; set; }
        public string Lot { get; set; }
        public string CompteTiers { get; set; }
        public string IntituleCompteTiers { get; set; }
       
       
        
    }
}
