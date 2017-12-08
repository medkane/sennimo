using prjSenImmoWinform.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace prjSenImmoWinform.Models
{
    public partial class FrmRepriseDD : Form
    {
        private IlotRepository ilotRepository;

        public FrmRepriseDD()
        {
            InitializeComponent();
            ilotRepository = new IlotRepository();
        }

        private void cmdImporterProspects_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int i = 0;
                using (var db = new SenImmoDataContext())
                {
                    var AllClientsResa = db.ClientResas.Where(cl => cl.Importe == false && cl.APrendre.ToUpper() != "NON").ToList();
                    foreach (var clientResa in AllClientsResa)
                    {
                        var sexe = clientResa.Sexe.Trim().ToUpper() == "F" ? Genre.Féminin : Genre.Masculin;
                        DateTime DateEntree = DateTime.Parse("01/01/1900");
                        Client client = new Client
                        {
                            NumeroClient = clientResa.Numero,
                            Prenom = Tools.Tools.UppercaseWords(clientResa.Prenom.Trim().ToLower()),
                            Nom = clientResa.Nom.Trim().ToUpper(),
                            CompteTiers = clientResa.CompteTiers.Trim(),
                            Importe = true,
                            Genre = sexe,
                            DateCreation = DateTime.Now,
                            DateSouscription = DateEntree.Date,
                            DateDeDelivrancePiece = DateTime.Now,
                            Type = TypeClient.ProspectSansOption,
                            Actif = true,
                            CommentaireProspect = "Importé depuis la reprise de données." + (clientResa.Comentaires.Trim() != string.Empty ? "\n " + clientResa.Comentaires : "")
                        };
                        var lOrigine = db.TypeOrigines.Find(9);
                        if (clientResa.MatriculeCommercial.Trim() != string.Empty)
                        {
                            var commercial = db.Agents.Where(ag => ag.Matricule.ToLower() == clientResa.MatriculeCommercial.ToLower()).FirstOrDefault();
                            if (commercial != null)
                            {
                                client.Commercial = commercial;
                                client.ProspectAffecte = true;
                            }
                        }
                        client.Origine = lOrigine;
                        db.Clients.Add(client);
                        clientResa.Importe = true;
                        db.SaveChanges();
                        i++;
                    }
                }
                //    scope.Complete();
                MessageBox.Show("Importations des prospects terminés");
                cmdImporterProspects.Enabled = false;
                lbImportProspects.Text = i.ToString() + " clients importés";
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Prosopis - Reprise de données", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdGenererOptions_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int i = 0;
                using (var db = new SenImmoDataContext())
                {
                    foreach (var clientResa in db.ClientResas.Where(cl => cl.OptionGenere == false && cl.APrendre.ToUpper() != "NON").ToList())

                    {
                        var client = db.Clients.Where(cl => cl.NumeroClient == clientResa.Numero).FirstOrDefault();
                        var lot = db.Lots.Where(lt => lt.NumeroLot == clientResa.Lot).FirstOrDefault();
                        //Vérifier que le lot existe dans la base et qu'il est au statut réservé 
                        if (lot == null)
                        {
                            client.CommentaireProspect = client.CommentaireProspect + "\n" + "Le lot " + clientResa.Lot + " est introuvable dans Prosopis";
                            db.SaveChanges();
                            continue;
                        }
                        //Au cas où le lot est déja réservé dans Prosopis
                        if (lot != null && lot.StatutLot == StatutLot.Reserve)
                        {
                            var contrat = db.Contrats.Where(c => c.Lot.ID == lot.ID).FirstOrDefault();
                            if (contrat != null)
                            {
                                client.CommentaireProspect = client.CommentaireProspect + "\n" + "Le lot " + lot.NumeroLot + " est déjà réservé dans Prosopis au client " + contrat.Client.NomComplet;
                                db.SaveChanges();
                                continue;
                            }
                        }
                        //Au cas où le lot est déja pris en option dans Prosopis
                        if (lot != null && lot.StatutLot == StatutLot.Option)
                        {
                            var option = db.Options.Where(o => o.Lot.ID == lot.ID).FirstOrDefault();
                            if (option != null)
                            {
                                client.CommentaireProspect = client.CommentaireProspect + "\n" + "Le lot " + lot.NumeroLot + " est pris en option dans Prosopis par le prospect " + option.Client.NomComplet;
                                db.SaveChanges();
                                continue;
                            }
                        }

                        if (lot != null)
                        {
                            decimal prixDeVente = decimal.Parse(clientResa.PrixDeVente);
                            decimal MontantRemise = lot.PrixRevise - prixDeVente;
                            decimal tauxRemise = MontantRemise / lot.PrixRevise * 100;
                            var newOption = new Option()
                            {

                                ClientID = client.ID,
                                CommercialID = client.CommercialID.Value,
                                LotId = lot.ID,
                                PositionLot = lot.PositionLot,
                                PrixDeVente = prixDeVente,
                                MontantRemiseAccordee = MontantRemise,
                                TauxRemiseAccordee = tauxRemise,
                                TypeContratId = 1,
                                TypeVillaId = lot.TypeVillaID,
                                Active = true,
                                SeuilContratAtteint = false,
                                ContratGenere = false,
                                DatePriseOption = DateTime.Now,

                            };
                            db.Options.Add(newOption);
                            clientResa.OptionGenere = true;
                            client.Type = TypeClient.ProspectAvecOptionResa;
                            lot.StatutLot = StatutLot.Option;
                            db.SaveChanges();
                            i++;
                        }

                    }

                }
                //    scope.Complete();
                MessageBox.Show("Génération des options prospects terminés");
                cmdGenererOptions.Enabled = false;
                lbOprions.Text = i.ToString() + " options prises";
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Prosopis - Reprise de données", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdImporterEncaissementsSaari_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                this.Cursor = Cursors.WaitCursor;

                using (var db = new SenImmoDataContext())
                {
                    foreach (var encSaari in db.ImportEncaissementSaaris.Where(enc => enc.Importe == false && enc.APrendre != "NON").ToList())

                    {
                        var client = db.Clients.Where(cl => cl.CompteTiers == encSaari.CompteTiers).FirstOrDefault();
                        var clientResa = db.ClientResas.Where(cl => cl.CompteTiers == encSaari.CompteTiers).FirstOrDefault();

                        if (client != null)
                        {
                            EncaissementProspect versement = null;
                            decimal montantDebit = 0;
                            decimal montantCredit = 0;
                            DateTime dateEnc = DateTime.Parse(encSaari.DateEncaissement);
                            if (decimal.TryParse(encSaari.MontantDebit, out montantDebit))
                            {
                                versement = new EncaissementProspect()
                                {
                                    NumeroEncaissement = encSaari.Libelle,
                                    DateEncaissement = dateEnc,
                                    MontantGlobal = -montantDebit,
                                    ProspectId = client.ID,
                                    ReferencePaiement = encSaari.Libelle,
                                    Commentaire = "Encaissement importés à partir du compte: " + encSaari.CompteTiers + ((encSaari.Commentaire != string.Empty) ? ": " + encSaari.Commentaire : "")
                                };

                            }
                            else
                            if (decimal.TryParse(encSaari.MontantCredit, out montantCredit))
                            {
                                versement = new EncaissementProspect()
                                {
                                    NumeroEncaissement = encSaari.Libelle,
                                    DateEncaissement = dateEnc,
                                    MontantGlobal = montantCredit,
                                    ProspectId = client.ID,
                                    ReferencePaiement = encSaari.Libelle,
                                    Commentaire = "Encaissement importés à partir du compte: " + encSaari.CompteTiers + ((encSaari.Commentaire != string.Empty) ? ": " + encSaari.Commentaire : "")
                                };
                            }

                            if (versement != null)
                            {
                                db.EncaissementProspects.Add(versement);
                                encSaari.Importe = true;
                                clientResa.EncaissementsImporte = true;
                                db.SaveChanges();
                                i++;
                            }
                        }

                    }

                }
                //    scope.Complete();
                MessageBox.Show("Importation des encaissements saari terminés");
                cmdImporterEncaissementsSaari.Enabled = false;
                lbEncaissements.Text = i.ToString() + " encaissements importés";
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Prosopis - Reprise de données", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdGenererContrat_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;

            //    using (var db = new SenImmoDataContext())
            //    {
            //        var Clients = db.Clients.Where(cl => cl.Importe == true && cl.Type== TypeClient.ProspectAvecOptionResa).ToList();
            //        foreach (var client in Clients)
            //        {

            //            try
            //            {
            //                var option = db.Options.Where(opt => opt.ClientID == client.ID && opt.Active == true).FirstOrDefault();
            //                if (option != null)
            //                {
            //                    var contratRep = new ContratRepository();
            //                    var clientResa = db.ClientResas.Where(cl => cl.Numero == client.NumeroClient).FirstOrDefault();
            //                    var leTypeContratEnCours = db.TypeContrats.Where(tc => tc.CategorieContrat == CategorieContrat.Réservation).FirstOrDefault();
            //                    var lot = db.Lots.Where(l => l.NumeroLot == clientResa.Lot).FirstOrDefault();

            //                    if (lot != null)
            //                    {
            //                        var encaissements = db.EncaissementProspects.Where(enc => enc.ProspectId == client.ID && enc.MontantGlobal > 0).Sum(enc => (decimal?)enc.MontantGlobal) ?? 0;

            //                        if ((encaissements / option.PrixDeVente) * 100 >= 30)
            //                        {
            //                            var dateLivraison = DateTime.Parse("31/12/" + clientResa.DateLivraison);
            //                            DateTime dateContrat= DateTime.Parse("01/01/1900");
            //                            if (DateTime.TryParse(clientResa.DateContrat, out dateContrat))
            //                                Console.Write("Date OK");

            //                            int contratId = contratRep.AjouterContratReservationBis(client.ID, client.CommercialID.Value, 0, lot.ID,
            //                                                                               lot.PrixRevise, option.PrixDeVente, option.TauxRemiseAccordee, option.MontantRemiseAccordee, leTypeContratEnCours,
            //                                                                              dateContrat, dateLivraison, 100);
            //                            clientResa.ContratGenere = true;
            //                            lot.StatutLot = StatutLot.Reserve;
            //                        }
            //                    }
            //                }
            //                db.SaveChanges();
            //            }
            //            catch (Exception)
            //            {

            //                continue;
            //            }
            //        }

            //        MessageBox.Show("Génération des contrats Résa terminée");
            //        cmdGenererContrat.Enabled = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, "Erreur:..." + ex.Message,
            //                     "Prosopis - Reprise de données", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    this.Cursor = Cursors.Default;
            //}
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int i = 0;
                using (var db = new SenImmoDataContext())
                {
                    var Clients = db.ClientResas.Where(cl => cl.Importe == true && cl.OptionGenere == true && cl.ContratGenere == false).ToList();
                    foreach (var clientResa in Clients)
                    {

                        try
                        {
                            var clientProsopis = db.Clients.Where(cl => cl.Importe == true && cl.NumeroClient == clientResa.Numero).FirstOrDefault();
                            var option = db.Options.Where(opt => opt.ClientID == clientProsopis.ID && opt.Active == true).FirstOrDefault();
                            if (option != null)
                            {
                                var contratRep = new ContratRepository();
                                //var clientResa = db.ClientResas.Where(cl => cl.Numero == clientProsopis.NumeroClient).FirstOrDefault();
                                var leTypeContratEnCours = db.TypeContrats.Where(tc => tc.CategorieContrat == CategorieContrat.Réservation).FirstOrDefault();
                                var lot = db.Lots.Where(l => l.NumeroLot == clientResa.Lot).FirstOrDefault();

                                if (lot != null)
                                {
                                    var encaissements = db.EncaissementProspects.Where(enc => enc.ProspectId == clientProsopis.ID && enc.MontantGlobal > 0).Sum(enc => (decimal?)enc.MontantGlobal) ?? 0;

                                    if ((encaissements / option.PrixDeVente) * 100 >= 30)
                                    {
                                        //var dateLivraison = DateTime.Parse("31/12/" + clientResa.DateLivraison);
                                        //DateTime dateContrat = DateTime.Parse("01/01/1900");
                                        //if (DateTime.TryParse(clientResa.DateContrat, out dateContrat))
                                        //    Console.Write("Date OK");

                                        //int contratId = contratRep.AjouterContratReservationBis(clientProsopis.ID, clientProsopis.CommercialID.Value, 0, lot.ID,
                                        //                                                   lot.PrixRevise, option.PrixDeVente, option.TauxRemiseAccordee, option.MontantRemiseAccordee, leTypeContratEnCours,
                                        //                                                  dateContrat, dateLivraison, 100, false,0);
                                        //clientResa.ContratGenere = true;
                                        //lot.StatutLot = StatutLot.Reserve;
                                        //i++;
                                    }
                                }
                            }
                            db.SaveChanges();

                        }
                        catch (Exception)
                        {

                            continue;
                        }
                    }

                    MessageBox.Show("Génération des contrats Résa terminée");
                    cmdGenererContrat.Enabled = false;
                    lbContrats.Text = i.ToString() + " contrats générés";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                 "Prosopis - Reprise de données", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdGenererAppelsDeFond_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int j = 0;
                using (var db = new SenImmoDataContext())
                {
                    var Clients = db.ClientResas.Where(cl => cl.ContratGenere == true && cl.OrdreEtatAvancement != 1).ToList();
                    foreach (var client in Clients)
                    {

                        try
                        {
                            var lot = db.Lots.Where(lt => lt.NumeroLot == client.Lot).FirstOrDefault();
                            if (lot != null)
                            {
                                var ordreDernierEA = int.Parse(client.DernierEtatAvancement);
                                TypeEtatAvancement niveauAvancement = null;//(TypeEtatAvancement)cmbNiveauxAvancements.SelectedItem;
                                for (int i = ordreDernierEA; i > 0; i--)
                                {
                                    niveauAvancement = db.TypeEtatAvancements.Where(tea => tea.ordre == i).FirstOrDefault();
                                    if (niveauAvancement.AppelFonds == true)
                                    {
                                        ilotRepository.AddEtatAvancement(lot, niveauAvancement, DateTime.Now.Date, "REPRISE DE DONNEES");
                                        j++;
                                    }
                                }

                            }
                            client.OrdreEtatAvancement = 1;
                            db.SaveChanges();

                        }
                        catch (Exception)
                        {

                            continue;
                        }
                    }

                    MessageBox.Show("Génération des Appels de fonds terminée");
                    cmdGenererAppelsDeFond.Enabled = false;
                    lbAppelsDeFonds.Text = j.ToString() + " appels de fonds générés";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                 "Prosopis - Reprise de données", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void FrmRepriseDD_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmdImporterProspectsDepot_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int i = 0;
                using (var db = new SenImmoDataContext())
                {
                    var AllClientsDepot = db.ClientDepots.Where(cl => cl.Importe == false && cl.Numero.Trim() != string.Empty && cl.CompteTiers.Trim() != string.Empty && cl.APrendre == "").ToList();
                    foreach (var clientDepot in AllClientsDepot)
                    {
                        Client client = null;

                        //var clientDejaPresent= db.ClientResas.Where(cl =>cl.Importe==false && cl.CompteTiers.Trim() == clientDepot.CompteTiers.Trim()
                        //                                            && cl.APrendre.Trim().ToUpper() !="NON").FirstOrDefault();
                        //// var sexe = clientDepot.Sexe.Trim().ToUpper() == "F" ? Genre.Féminin : Genre.Masculin;
                        //if (clientDejaPresent == null)
                        //    client = new Client();
                        //else
                        //{
                        Client clientCorrespondant = db.Clients.Where(cl => cl.Importe == true
                                                    && cl.CompteTiers.Trim() == clientDepot.CompteTiers.Trim()).FirstOrDefault();
                        if (clientCorrespondant != null)
                        {
                            client = clientCorrespondant;
                        }
                        else
                        {
                            client = new Client();
                            db.Clients.Add(client);
                        }
                        //}
                        DateTime DateEntree = DateTime.Parse("01/01/1900");
                        if (client.NumeroClient == null || client.NumeroClient == string.Empty)
                        {
                            client.NumeroClient = clientDepot.Numero;
                        }
                        if (client.Prenom == null || client.Prenom == string.Empty)
                        {
                            client.Prenom = clientDepot.Prenom;
                        }
                        if (client.Nom == null || client.Nom == string.Empty)
                        {
                            client.Nom = string.Empty;
                        }
                        if (client.CompteTiers == null || client.CompteTiers == string.Empty)
                        {
                            client.CompteTiers = clientDepot.CompteTiers;
                        }
                        if (client.Mobile1 == null || client.Mobile1 == string.Empty)
                        {
                            client.Mobile1 = clientDepot.Telephone; ;
                        }
                        if (client.Email == null || client.Email == string.Empty)
                        {
                            client.Email = clientDepot.Mail; ;
                        }
                        if (client.Adresse == null || client.Adresse == string.Empty)
                        {
                            client.Adresse = clientDepot.Adresse; ;
                        }
                        if (client.Pays == null || client.Pays == string.Empty)
                        {
                            client.Pays = clientDepot.Pays; ;
                        }
                        if (client.Genre == 0)
                        {
                            client.Genre = Genre.Féminin;
                        }
                        //client.NumeroClient = client.NumeroClient != string.Empty ? client.NumeroClient:clientDepot.Numero ;
                        //client.Prenom = client.Prenom != string.Empty ? client.Prenom : Tools.Tools.UppercaseWords(clientDepot.Prenom.Trim().ToLower());
                        //client.Nom = client.Nom != string.Empty ? "":"";
                        //client.CompteTiers = client.CompteTiers != string.Empty ? client.CompteTiers:clientDepot.CompteTiers.Trim();
                        client.Importe = true;
                        //client.Mobile1 = clientDepot.Telephone;
                        //client.Email = clientDepot.Mail;
                        //client.Adresse = clientDepot.Adresse;
                        //client.Pays = clientDepot.Pays;
                        //client.Genre = Genre.Féminin;
                        client.DateCreation = DateTime.Now;
                        client.DateSouscription = DateEntree.Date;
                        client.DateDeNaissance = DateTime.Parse("01/01/1900");
                        client.DateDeDelivrancePiece = DateTime.Parse("01/01/1900");

                        client.Type = TypeClient.ProspectSansOption;
                        client.Actif = true;
                        client.CommentaireProspect = "Importé depuis la reprise de données." + (clientDepot.Comentaires.Trim() != string.Empty ? "\n " + clientDepot.Comentaires : "");
                        var lOrigine = db.TypeOrigines.Find(9);
                        if (clientDepot.MatriculeCommercial.Trim() != string.Empty)
                        {
                            var commercial = db.Agents.Where(ag => ag.Matricule.Trim().ToLower() == clientDepot.MatriculeCommercial.Trim().ToLower()).FirstOrDefault();
                            if (commercial != null)
                            {
                                client.Commercial = commercial;
                                client.ProspectAffecte = true;
                                client.ProspectEdite = true;
                            }
                        }
                        client.Origine = lOrigine;
                        clientDepot.Importe = true;
                        db.SaveChanges();
                        i++;
                    }
                }
                //    scope.Complete();
                MessageBox.Show("Importations des prospects terminés");
                cmdImporterProspectsDepot.Enabled = false;
                lbImportProspects.Text = i.ToString() + " clients importés";
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Prosopis - Reprise de données", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdGenererOptionsDepot_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int i = 0;
                using (var db = new SenImmoDataContext())
                {
                    var clientsDepots = db.ClientDepots.Where(cl => cl.OptionGenere == false && cl.CompteTiers.Trim() != string.Empty
                                                                          && cl.APrendre.Trim() == "").ToList();
                    foreach (var clientDepot in clientsDepots)
                    {
                        try
                        {
                            var client = db.Clients.Where(cl => cl.CompteTiers.Trim() == clientDepot.CompteTiers.Trim()).FirstOrDefault();
                            if (client == null)
                                continue;
                            var option = db.Options.Where(opt => opt.ClientID == client.ID && opt.Active == true).FirstOrDefault();
                            if (option != null)
                            {
                                clientDepot.Comentaires = clientDepot.Comentaires + " ce client dépot a une option déjà prise lors de la reprise de données Résa";
                                db.SaveChanges();
                                continue;
                            }

                            var TypeVilla = db.TypeVillas.Where(tv => tv.CodeType == clientDepot.TypeVilla.Trim()).FirstOrDefault();

                            //Vérifier que le lot existe dans la base et qu'il est au statut réservé 
                            if (TypeVilla == null)
                            {
                                client.CommentaireProspect = client.CommentaireProspect + "\n" + "Le type de villa " + clientDepot.TypeVilla + " est introuvable dans Prosopis";
                                db.SaveChanges();
                                continue;
                            }

                            PositionLot positionLot = 0;
                            if (clientDepot.FraisDossier == "angle")
                                positionLot = PositionLot.Angle;
                            else
                                positionLot = PositionLot.Standard;

                            if (TypeVilla != null)
                            {
                                var lotVirtuel = db.Lots.Where(lt => lt.TypeVillaID == TypeVilla.TypeVillaId && lt.LotVirtuel == true && lt.PositionLot == positionLot).FirstOrDefault();
                                if (lotVirtuel == null)
                                {
                                    client.CommentaireProspect = client.CommentaireProspect + "\n" + "Le lot virtuel n'est pas configuré pour le type de villa " + clientDepot.TypeVilla + " dans Prosopis";
                                    db.SaveChanges();
                                    continue;
                                }
                                decimal prixDeVente = decimal.Parse(clientDepot.PrixDeVente);

                                decimal MontantRemise = lotVirtuel.PrixRevise - prixDeVente;
                                decimal tauxRemise = MontantRemise / lotVirtuel.PrixRevise * 100;
                                //DateTime dtpDateDebutVersement = DateTime.Parse(clientDepot.DateDebutVersement);
                                DateTime dtpDateDebutVersement = DateTime.Parse(clientDepot.DateDebutVersement != string.Empty ? clientDepot.DateDebutVersement : "01/01/1900").Date;
                                TypeContrat typeDepot = null;
                                if (clientDepot.TypeDepot.Trim() == "5%")
                                {
                                    typeDepot = db.TypeContrats.Find(2);
                                }
                                else
                                    if (clientDepot.TypeDepot.Trim() == "15%")
                                {
                                    typeDepot = db.TypeContrats.Find(3);
                                }

                                ContratRepository contratRep = new ContratRepository();
                                contratRep.AddOptionProspectDepot(client.ID, typeDepot.ID, lotVirtuel.TypeVillaID, lotVirtuel.TypeVilla.PrixStandard,
                                            lotVirtuel.PrixRevise, lotVirtuel.PositionLot, lotVirtuel.TypeVilla.SurfaceDeBase, client.CommercialID.Value, prixDeVente, false,
                                            dtpDateDebutVersement, dtpDateDebutVersement, lotVirtuel.ID, MontantRemise, tauxRemise);
                                clientDepot.OptionGenere = true;
                                db.SaveChanges();
                                i++;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            continue;
                        }
                    }
                }
                MessageBox.Show("Génération des options prospects terminés");
                cmdGenererOptionsDepot.Enabled = false;
                lbOprions.Text = i.ToString() + " options prises";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Prosopis - Reprise de données", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdImporterEncaissementsSaariDepot_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                this.Cursor = Cursors.WaitCursor;

                using (var db = new SenImmoDataContext())
                {
                    foreach (var encSaari in db.ImportEncaissementSaaris.Where(enc => enc.Importe == false && enc.APrendre != "NON").ToList())

                    {
                        var clientDepot = db.ClientDepots.Where(cl => cl.APrendre.Trim() == "" && cl.CompteTiers.Trim() == encSaari.CompteTiers.Trim()).FirstOrDefault();
                        if (clientDepot == null) continue;
                        var client = db.Clients.Where(cl => cl.CompteTiers.Trim() == encSaari.CompteTiers.Trim()).FirstOrDefault();
                        

                        if (client != null && clientDepot != null)
                        {
                            EncaissementProspect versement = null;
                            decimal montantDebit = 0;
                            decimal montantCredit = 0;
                            DateTime dateEnc = DateTime.Parse(encSaari.DateEncaissement);
                            if (decimal.TryParse(encSaari.MontantDebit, out montantDebit))
                            {
                                versement = new EncaissementProspect()
                                {
                                    NumeroEncaissement = encSaari.Libelle,
                                    DateEncaissement = dateEnc,
                                    MontantGlobal = -montantDebit,
                                    ProspectId = client.ID,
                                    ReferencePaiement = encSaari.Libelle,
                                    Commentaire = "Encaissement importés à partir du compte: " + encSaari.CompteTiers + ((encSaari.Commentaire != string.Empty) ? ": " + encSaari.Commentaire : "")
                                };

                            }
                            else
                            if (decimal.TryParse(encSaari.MontantCredit, out montantCredit))
                            {
                                versement = new EncaissementProspect()
                                {
                                    NumeroEncaissement = encSaari.Libelle,
                                    DateEncaissement = dateEnc,
                                    MontantGlobal = montantCredit,
                                    ProspectId = client.ID,
                                    ReferencePaiement = encSaari.Libelle,
                                    Commentaire = "Encaissement importés à partir du compte: " + encSaari.CompteTiers + ((encSaari.Commentaire != string.Empty) ? ": " + encSaari.Commentaire : "")
                                };
                            }

                            if (versement != null)
                            {
                                db.EncaissementProspects.Add(versement);
                                encSaari.Importe = true;
                                clientDepot.EncaissementsImporte = true;
                                db.SaveChanges();
                                i++;
                            }
                            var option = client.Options.Where(opt => opt.Active == true).FirstOrDefault();
                            var encaissementsProspect = client.EncaissementProspects.Where(enc => enc.FraisDeDossier == false).Sum(mvt => mvt.MontantGlobal);
                            var tauxEncaissement = encaissementsProspect / option.PrixDeVente * 100;

                            if (db.TypeContrats.Find(option.TypeContratId).CategorieContrat == CategorieContrat.Dépôt && tauxEncaissement >= option.TypeContrat.SeuilSouscription)
                            {
                                option.SeuilContratAtteint = true;
                                option.ContratGenere = false;
                            }

                            db.SaveChanges();
                        }

                    }

                }
                //    scope.Complete();
                MessageBox.Show("Importation des encaissements saari terminés");
                cmdImporterEncaissementsSaariDepot.Enabled = false;
                lbEncaissements.Text = i.ToString() + " encaissements importés";
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                       "Prosopis - Prosopis - Reprise de données", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdGenererContratDepot_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int i = 0;
                using (var db = new SenImmoDataContext())
                {
                    var Clients = db.ClientDepots.Where(cl => cl.Importe == true && cl.OptionGenere == true && cl.ContratGenere == false && cl.APrendre.Trim() == "");
                    foreach (var clientDepot in Clients.ToList())
                    {

                        try
                        {
                            //if (clientDepot.CompteTiers.ToUpper().Trim()== "4111MAURICEDIOUF")
                            //{
                            //    Console.Write("STOP");
                            //}
                            var clientProsopis = db.Clients.Where(cl => cl.Importe == true && cl.CompteTiers.Trim() == clientDepot.CompteTiers.Trim()).FirstOrDefault();
                            if (clientProsopis == null)
                                continue;
                            var option = db.Options.Where(opt => opt.ClientID == clientProsopis.ID && opt.Active == true).FirstOrDefault();
                            if (option != null)
                            {

                                var contratRep = new ContratRepository();
                                //var clientDepot = db.ClientDepots.Where(cl => cl.Numero == clientProsopis.NumeroClient).FirstOrDefault();
                                TypeEcheancier typeEch = TypeEcheancier.Mensuel;
                                if (clientDepot.Periodicite.Trim().ToLower() == "mensuelle")
                                {
                                    typeEch = TypeEcheancier.Mensuel;
                                }
                                else
                                    if (clientDepot.Periodicite.Trim().ToLower() == "trimestrielle")
                                {
                                    typeEch = TypeEcheancier.Trimestriel;
                                }
                                else
                                    if (clientDepot.Periodicite.Trim().ToLower() == "semestrielle")
                                {
                                    typeEch = TypeEcheancier.Trimestriel;
                                }
                                //var lot = db.Lots.Where(l => l.NumeroLot == clientDepot.Lot).FirstOrDefault();

                                //if (lot != null)
                                //{
                               
                                var encaissements = db.EncaissementProspects.Where(enc => enc.ProspectId == clientProsopis.ID && enc.MontantGlobal > 0).Sum(enc => (decimal?)enc.MontantGlobal) ?? 0;
                                var tauxEncaissement = (encaissements / option.PrixDeVente) * 100;
                                if (tauxEncaissement >= option.TypeContrat.SeuilSouscription)
                                {
                                    DateTime datePremiereEcheance = DateTime.Parse(clientDepot.DateDebutVersement != string.Empty ? clientDepot.DateDebutVersement : "01/01/1900");
                                    DateTime dateDerniereEcheance = DateTime.Parse(clientDepot.DateFinVersement != string.Empty ? clientDepot.DateFinVersement : "01/01/1900");
                                    var dateLivraison = DateTime.Parse(clientDepot.DateFinVersement != string.Empty ? clientDepot.DateFinVersement : "01/01/1900");
                                    DateTime dateContrat = DateTime.Parse(clientDepot.DateSignatureContratDepot != string.Empty ? clientDepot.DateSignatureContratDepot : "01/01/1900");
                                    decimal depotMinimum = decimal.Parse(clientDepot.DepotInitial);
                                    decimal montantEcheance = decimal.Parse(clientDepot.MontantVersement);
                                    int dureeDepot = int.Parse(clientDepot.DureeDepot);
                                    int nbEcheances = int.Parse(clientDepot.NbEcheances);

                                    var MontantTotal = montantEcheance * nbEcheances + depotMinimum;

                                    //if (MontantTotal > option.PrixDeVente)
                                    //{
                                    //    clientDepot.Comentaires = clientDepot.Comentaires + "Montant total à recouvrer différent du prix de vente";
                                    //    db.SaveChanges();
                                    //    continue;
                                    //}


                                    var montantRestantADispacher = option.PrixDeVente - depotMinimum;
                                    var montantDerniereEcheance = (int)(montantRestantADispacher - montantEcheance * nbEcheances);
                                    if (clientDepot.CompteTiers.Trim() == "4111NDEYEFALLYMED" )
                                    {
                                        montantDerniereEcheance = -15;
                                    }

                                    if (clientDepot.CompteTiers.Trim() == "4111MAMEAISSATOUG")
                                    {
                                        montantDerniereEcheance = -8;
                                    }
                                    //if (montantDerniereEcheance > 0)

                                    //if (DateTime.TryParse(clientDepot.DateSignatureContratDepot, out dateContrat))
                                    //    Console.Write("Date OK");
                                    /////// DUREE DEPOT///////
                                    //int contratId = contratRep.AjouterContratReservationBis(clientProsopis.ID, clientProsopis.CommercialID.Value, 0, lot.ID,
                                    //                                                   lot.PrixRevise, option.PrixDeVente, option.TauxRemiseAccordee, option.MontantRemiseAccordee, leTypeContratEnCours,
                                    //                                                  dateContrat, dateLivraison, 100);
                                    int contratId = contratRep.AjouterContratDepotTer(clientProsopis.ID, clientProsopis.CommercialID.Value, 0
                                                  , option.TypeVilla, option.PositionLot, option.PrixDeVente, option.TauxRemiseAccordee, option.MontantRemiseAccordee, option.TypeContrat,
                                                  typeEch, datePremiereEcheance, dateDerniereEcheance,
                                                  dateContrat, dateLivraison, depotMinimum, montantEcheance, nbEcheances, montantDerniereEcheance, dureeDepot);
                                    clientDepot.ContratGenere = true;

                                    i++;
                                }
                                db.SaveChanges();

                            }


                        }
                        catch (Exception)
                        {

                            continue;
                        }
                    }

                    MessageBox.Show("Génération des contrats Résa terminée");
                    cmdGenererContratDepot.Enabled = false;
                    lbContrats.Text = i.ToString() + " contrats générés";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                 "Prosopis - Reprise de données", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdGenererEcheances_Click(object sender, EventArgs e)
        {
            //var dateReference = dtpDateReference.Value;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SenImmoDataContext"].ConnectionString;
                using (SqlConnection cnx = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"
                                UPDATE [AKYSDATABASE].[dbo].Factures 
                                SET [AKYSDATABASE].[dbo].Factures.Echue=1, 
                                    [AKYSDATABASE].[dbo].Factures.Active=1
                                WHERE [AKYSDATABASE].[dbo].Factures.TypeFacture=4 
                                AND [AKYSDATABASE].[dbo].Factures.DateEcheanceFacture  <= CAST('" + DateTime.Now.Date + @"'  AS DATE);

                                
                        ");
                    cmd.Connection = cnx;
                    cnx.Open();

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Les échéances depuis le début du projets AKYS ont été générées avec succes.", "Prosopis - Génération des factures d'échéances", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmdGenererEcheances.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la génération des échéances: " + ex.Message, "Prosopis - Génération des factures d'échéances", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdGenererFraisDossier_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int j = 0;
                using (var db = new SenImmoDataContext())
                {
                    var Clients = db.ClientDepots.Where(cl => cl.APrendre.Trim() == "OUI1" || cl.APrendre.Trim() == "").ToList();

                    foreach (var clientResa in Clients)
                    {
                        if(clientResa.CompteTiers.Trim()== "4111MAURICEDIOUF")
                        {
                            Console.Write("OK");
                        }
                        var clientProsopis = db.Clients.Where(cl => cl.Importe == true && cl.NumeroClient == clientResa.Numero).FirstOrDefault();
                        DateTime dateEncaissement = DateTime.Parse("01/01/1900");
                        if (clientProsopis != null)
                        {
                            var encFraisDeDossier = db.EncaissementProspects.Any(enc => enc.ProspectId == clientProsopis.ID && enc.FraisDeDossier == true);
                            if (encFraisDeDossier == true)
                                continue;
                            try
                            {
                                //var prospect = db.Clients.Find(prospectId);

                                var newFacture = new FactureProspect()
                                {
                                    Motif = "Frais de dossier",
                                    Date = dateEncaissement,
                                    Montant = 200000,
                                    TypeFacture = TypeFacture.FraisDossier,
                                    ProspectId = clientProsopis.ID,

                                };
                                var newEncaissement = new EncaissementProspect()
                                {
                                    NumeroEncaissement = "ENFD0000000",
                                    DateEncaissement = dateEncaissement,
                                    MontantGlobal = 200000,
                                    ProspectId = clientProsopis.ID,
                                    ReferencePaiement = "REPRISE DE DONNEES",
                                    Commentaire = "Encaissement frais de dossier(reprise de données) ",
                                    FraisDeDossier = true
                                };
                                newFacture.EncaissementsProspects.Add(newEncaissement);
                                db.FactureProspects.Add(newFacture);

                                db.SaveChanges();
                            }
                            catch (Exception)
                            {

                                MessageBox.Show("Erreur sur " + clientProsopis.NumeroClient);
                            }
                        }
                        //    var clientProsopis = db.Clients.Where(cl => cl.Importe == true && cl.NumeroClient == clientResa.Numero).FirstOrDefault();
                        //    var contrat = db.Contrats.Where(ct => ct.ClientID == clientProsopis.ID).FirstOrDefault();
                        //    var newFacture = new Facture
                        //    {
                        //        TypeFacture = TypeFacture.FraisDossier,
                        //        ContratId = contrat.Id,
                        //        ClientId = contrat.ClientID,
                        //        Date = dateEncaissement,
                        //        DateEcheanceFacture = dateEncaissement,
                        //        Active = true,
                        //        Echue = true,
                        //        FacturePayee = true,
                        //        NumeroFacture = "FCFD00000000"  ,
                        //        Montant = 200000,
                        //        Motif = "Frais de dossier"
                        //    };

                        //    contrat.Factures.Add(newFacture);


                        //    var versement = new EncaissementGlobal()
                        //    {
                        //        NumeroEncaissement = "ENFD0000000",
                        //        DateEncaissement = dateEncaissement,
                        //        MontantGlobal = 200000,
                        //        ContratId = contrat.Id,
                        //        ModePaiement = ModePaiement.Chèque,
                        //        ReferencePaiement = "Reprise de données",
                        //        Commentaire = "Frais de dossier",

                        //    };
                        //    db.EncaissementGlobals.Add(versement);

                        //    ////Mis à jour des éléments contractuels
                        //    contrat.MontantVerse += versement.MontantGlobal;

                        //    Encaissement nouvelEncaissement = new Encaissement
                        //    {
                        //        Date = dateEncaissement,
                        //        ModePaiement = ModePaiement.Chèque,
                        //        Montant = 200000,
                        //        Commentaire = "Frais de dossier",
                        //        ReferencePaiement = "Reprise de données",
                        //    };
                        //    newFacture.Encaissements.Add(nouvelEncaissement);

                        //    versement.Encaissements.Add(nouvelEncaissement);

                        //    db.SaveChanges();

                        //}
                        //catch (Exception)
                        //{

                        //    continue;
                        //}
                    }

                    MessageBox.Show("Génération des Appels de fonds terminée");
                    cmdGenererFraisDossier.Enabled = false;
                    lbAppelsDeFonds.Text = j.ToString() + " appels de fonds générés";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                 "Prosopis - Reprise de données", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdSupprimerAnnulation_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int j = 0;
                using (var db = new SenImmoDataContext())
                {
                    var ListeAnnulations = db.EncaissementProspects.Where(enc =>enc.MontantGlobal <0 ).ToList();
                    using (var scope = new TransactionScope())
                    {
                        foreach (var annulation in ListeAnnulations)
                        {
                            var encaissement = db.EncaissementProspects.Where(enc => enc.ProspectId == annulation.ProspectId 
                            && enc.MontantGlobal == -annulation.MontantGlobal 
                            && annulation.NumeroEncaissement.Trim().Contains(enc.NumeroEncaissement.Trim().Substring(0,15))).FirstOrDefault();

                            //encaissement.Commentaire = "A supprimer concerne "+ annulation.NumeroEncaissement;
                            //annulation.Commentaire = "A supprimer, concerne " + encaissement.NumeroEncaissement;
                            db.EncaissementProspects.Remove(encaissement);
                            db.EncaissementProspects.Remove(annulation);
                            db.SaveChanges();
                        }
                        scope.Complete();
                    }
                }
                MessageBox.Show("Suppression des extournes terminée");
                cmdSupprimerAnnulation.Enabled = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                 "Prosopis - Reprise de données", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdRecalculSeuil_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int i = 0;
                using (var db = new SenImmoDataContext())
                {
                    var Clients = db.ClientResas.Where(cl => cl.Importe == true && (cl.APrendre.ToUpper().Trim() != "NON" || cl.APrendre.ToUpper().Trim()=="OUI1"));
                    foreach (var client in Clients.ToList())
                    {

                        try
                        {
                            if (client.CompteTiers.ToUpper().Trim() == "4111AMADOUAWF4AMI")
                            {
                                Console.Write("STOP");
                            }
                            var clientProsopis = db.Clients.Where(cl => cl.Importe == true && cl.CompteTiers.Trim() == client.CompteTiers.Trim()).FirstOrDefault();
                            if (clientProsopis == null)
                                continue;
                            var option = db.Options.Where(opt => opt.ClientID == clientProsopis.ID && opt.Active == true).FirstOrDefault();
                            if (option != null)
                            {
                                var encaissements = db.EncaissementProspects.Where(enc => enc.ProspectId == clientProsopis.ID && enc.MontantGlobal > 0).Sum(enc => (decimal?)enc.MontantGlobal) ?? 0;
                                var tauxEncaissement = (encaissements / option.PrixDeVente) * 100;
                                if (tauxEncaissement >= option.TypeContrat.SeuilSouscription)
                                {
                                    option.SeuilContratAtteint = true;
                                    db.SaveChanges();
                                }
                            }
                        }
                        catch (Exception)
                        {

                            continue;
                        }
                    }

                    MessageBox.Show("Génération des contrats Résa terminée");
                    cmdGenererContratDepot.Enabled = false;
                    lbContrats.Text = i.ToString() + " contrats générés";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur:..." + ex.Message,
                                 "Prosopis - Reprise de données", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }
    }
}
