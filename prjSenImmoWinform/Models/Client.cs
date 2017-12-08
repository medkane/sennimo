using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class Client
    {
        public Client()
        {
            Options = new HashSet<Option>();
            EncaissementProspects = new HashSet<EncaissementProspect>();
        }
        public int ID { get; set; }
        public string NumeroClient { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NomComplet {
                                      get { return Prenom + " " + Nom; }
                                 }
        public Genre Genre { get; set; }
        public DateTime? DateDeNaissance { get; set; }
        public string LieuDeNaissance { get; set; }
        public string Nationalite { get; set; }
        public string Profession { get; set; }
        public TypePieceIdentite? TypePieceIdentite { get; set; }
        public string NumeroPieceIdentification { get; set; }
        public DateTime? DateDeDelivrancePiece { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }
        public string AdresseComplete { get { return Adresse + " , " + Ville + " - " + Pays; } }
        public string Email { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
       
        public string TelephoneFixe { get; set; }
        public string TelephoneBureau { get; set; }
        public string Fax { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateSouscription { get; set; }
        public bool Actif { get; set; }
        public string NotesClient { get; set; }
        public TypeClient Type { get; set; }
        public bool ProspectAffecte { get; set; }
        public DateTime? DateAffectationCommercial { get; set; }

        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }

        public int OrigineId { get; set; }
        public virtual TypeOrigine Origine { get; set; }

        public bool ProspectEdite { get; set; }
        public string AutreOrigine { get; set; }
        public string CommentaireProspect { get; set; }
        public bool Importe { get; set; }
        public string CompteTiers { get; set; }
        public string IntituleCompteTiers { get; set; }

        public DateTime? DateMariage { get; set; }
        public string LieuDeMariage { get; set; }
        public DateTime? DateContratMariage { get; set; }

        public SituationMatrimoniale SituationMatrimoniale { get; set; }
        public string NomConjoint { get; set; }
        public string PrenomConjoint { get; set; }
        public string NomCompletConjoint { get { return PrenomConjoint + " " + NomConjoint; } }
        public DateTime? DateDeNaissanceConjoint { get; set; }
        public string LieuDeNaissanceConjoint { get; set; }
        public string NationaliteConjoint { get; set; }
        public string ProfessionConjoint { get; set; }
        public RegimeMatrimoniale RegimeMatrimoniale { get; set; }
        public string PrenomNotaire { get; set; }
        public string NomNotaire { get; set; }
        public string AdresseNotaire { get; set; }

        public virtual ICollection<StatutProspect> StatutProspects { get; set; }
        public virtual ICollection<Contrat> Contrats { get; set; }
        public virtual ICollection<Option> Options { get; set; }
        public virtual ICollection<ActiviteCommerciale> ActiviteCommerciales { get; set; }
        public virtual ICollection<Facture> Factures { get; set; }
        public virtual ICollection<EncaissementProspect> EncaissementProspects { get; set; }
        public virtual ICollection<NoteProspect> Notes { get; set; }
        //public string ImageProfile { get; set; }

        public int? CommercialID { get; set; }
        public virtual Agent Commercial { get; set; }


        public int? CooperativeId { get; set; }
        public virtual Cooperative Cooperative { get; set; }

        public int? ProjetId { get; set; }
        public virtual Projet Projet { get; set; }
    }

    public enum Genre
    {
        Masculin=1, Féminin=2
    }

    public enum TypePieceIdentite
    {
        CNI=1, Passport=2
    }

    public enum TypeClient
    {
       ProspectSansOption=1,ProspectAvecOptionResa=2,ProspectAvecOptionDepot=3, ClientEnCours=4,Client=5,Résilié=6
    }



    //public enum TypeOrigine
    //{
    //    Desk = 1, Perso = 2, Salon = 3, Voyage = 4, Foire = 5, Autre = 6
    //}

    public enum SituationMatrimoniale
    {
        Célibataire = 1, Marié = 2, Veuf = 3, Divorcé= 4
    }

    public enum RegimeMatrimoniale
    {
        Séparation = 1, Communautaire = 2, Autre = 3
    }

    public class StatutProspect
    {
        public int ID { get; set; }
        public string Motif { get; set; }
        public DateTime DateStatut { get; set; }
        public string Commentaires { get; set; }

        public int ProspectId { get; set; }
        public virtual Client Prospect { get; set; }

        public int TypeStatutProspectId { get; set; }
        public virtual TypeStatutProspect TypeStatutProspect { get; set; }


    }

    public class TypeStatutProspect
    {
        public int ID { get; set; }
        public string Libelle { get; set; }

    }

    //public en

    public class TypeOrigine
    {
        public TypeOrigine()
        {
            Clients = new HashSet<Client>();
        }
        public int TypeOrigineId { get; set; }
        public ClassOrigine ClassOrigine { get; set; }
        public string LibelleTypeOrigine { get; set; }
        public string CommentaireTypeOrigine { get; set; }
        public bool BLimiteDansLeTemps { get; set; }
        public DateTime? DateDebutTypeOrigine { get; set; }
        public DateTime? DateFinTypeOrigine { get; set; }
        public bool BActif { get; set; }

        public int? ProjetId { get; set; }
        public virtual Projet Projet { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
    public enum ClassOrigine
    {
        Desk = 1, Perso = 2
    }

    public class NoteProspect
    {
        public int ID { get; set; }
        public DateTime? DateDebutTypeOrigine { get; set; }
        public string Comentaire { get; set; }

        public int ProspectId { get; set; }
        public virtual Client Prospect { get; set; }

        public int? ActivitecommercialId { get; set; }
        public virtual ActiviteCommerciale ActiviteCommerciale { get; set; }
    }
}
