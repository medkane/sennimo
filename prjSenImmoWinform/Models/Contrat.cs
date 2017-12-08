    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class Contrat:Entity
    {
        //public int ID { get; set; }
        public Contrat()
        {
            Factures = new HashSet<Facture>();
            FactureCommissions = new HashSet<FactureCommission>();
        }
       

        public string NumeroContrat { get; set; }
        public StatutContrat Statut { get; set; }

        public DateTime? DateSouscription { get; set; } 

        public DateTime? DateReservation { get; set; }//Date ou les 30% sont payés ou 50% en fonction du type de contrat

        public DateTime DateCreationSysteme { get; set; }

        public decimal PrixRevise { get; set; }

        public decimal TauxRemiseAccordee { get; set; }

        public decimal RemiseAccordee { get; set; }

        public decimal PrixFinal { get; set; }

        public decimal CommissionApporteur { get; set; }

        public decimal MontantPremierVersement { get; set; }

        public decimal MontantVerse { get; set; }

        public DateTime? DateLivraisonLot { get; set; }

        public bool Souscrit { get; set; }

        public bool AReserve { get; set; }

        public bool AttribuerLot { get; set; }

        public bool LotAttribue { get; set; }

        public bool ContratDepotValide { get; set; }

        public bool ContratValide { get; set; }

        public bool ContratSolde { get; set; }

       

        public int DureeDepot { get; set; }

        public int ClientID { get; set; }
        public virtual Client Client { get; set; }

        public int TypeContratID { get; set; }
        public virtual TypeContrat TypeContrat { get; set; }
        
        public TypeEcheancier? TypeEcheancier { get; set; } //Mensuel, Annuel, etc si echeancier sélectionné comme type de contrat

        public int? NbEcheances { get; set; }
        public decimal? MontantEcheance { get; set; }

        public int? LotId { get; set; }
        public virtual Lot Lot { get; set; }

        public int? CommercialID { get; set; }
        public virtual Agent Commercial { get; set; }

        public int? ApporteurID { get; set; }
        public virtual ApporteurAffaire Apporteur { get; set; }

        public bool? AvecAvenant { get; set; }


        public int? ProjetId { get; set; }
        public virtual Projet Projet { get; set; }

        public int? ContratPrecedentID { get; set; }
        public virtual Contrat ContratPrecedent { get; set; }


        public virtual ICollection<Facture> Factures { get; set; }
        public virtual ICollection<FactureCommission> FactureCommissions { get; set; }
        public virtual ICollection<EncaissementGlobal> EncaissementGlobals { get; set; }
        public virtual ICollection<NoteContrat> NoteContrats { get; set; }
    
    }

    public enum TypeEcheancier
    {
        Mensuel=1, Trimestriel=2,Semestriel=3,Annuel=4
    }

    public enum StatutContrat
    {
        Actif=1, Résilié=2,Cloturé=3,Désisté=4
    }

    public class NoteContrat
    {
        public int ID { get; set; }
        public DateTime? DateNote { get; set; }
        public string Note { get; set; }

        public int? ContratId { get; set; }
        public virtual Contrat Contrat { get; set; }
    }
}
