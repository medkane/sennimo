using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class ClientResa
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string CompteTiers { get; set; }
        public string Type { get; set; }
        public string Ilot { get; set; }
        public string Lot { get; set; }
        public string Surface { get; set; }
        public string PrixDeVente { get; set; }
        public string MatriculeCommercial { get; set; }
        public bool Importe { get; set; }
        public bool OptionGenere { get; set; }
        public bool EncaissementsImporte { get; set; }
        public bool ContratGenere { get; set; }
        public string FraisDossier { get; set; }
        public string DateLivraison { get; set; }
        public string DateContrat { get; set; }
        public string DernierEtatAvancement  { get; set; }
        public int OrdreEtatAvancement { get; set; }
        public string  Sexe { get; set; }
        public string Comentaires { get; set; }
        public string APrendre { get; set; }
    }


    public class ClientDepot
    {
        public int Id { get; set; }
        public string MatriculeCommercial { get; set; }
        public string Numero { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string CompteTiers { get; set; }
        public string Telephone { get; set; }
        public string Mail { get; set; }
        public string Adresse { get; set; }
        public string Pays { get; set; }
        public string Sexe { get; set; }

        public string TypeVilla { get; set; }
        public string Lot { get; set; }
        public string PrixDeVente { get; set; }
        
      
        public string FraisDossier { get; set; }

        //public string Ilot { get; set; }
        //public string Lot { get; set; }
        //public string Surface { get; set; }
        public string TypeDepot { get; set; }
        public string DureeDepot { get; set; }
        public string Periodicite { get; set; }
        public string NbEcheances { get; set; }
        public string DateDebutVersement { get; set; }
        public string DateFinVersement { get; set; }
        public string MontantVersement { get; set; }
        public string DepotInitial { get; set; }
        public string DateSignatureContratDepot { get; set; }

        public string ContratResaSigne { get; set; }
        public string DateSignatureContratResa { get; set; }

        public string Comentaires { get; set; }
        public string APrendre { get; set; }

        public bool Importe { get; set; }
        public bool OptionGenere { get; set; }
        public bool EncaissementsImporte { get; set; }
        public bool ContratGenere { get; set; }
    }


    public class ImportEncaissementSaari
    {
        public int Id { get; set; }
        public string DateEncaissement { get; set; }
        public string Libelle { get; set; }
        public string CompteTiers { get; set; }
        public string MontantDebit { get; set; }
        public string MontantCredit { get; set; }
        public bool Importe { get; set; }
        public string Commentaire { get; set; }
        public string APrendre { get; set; }

    }
}
