using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class Facture:Entity
    {
        public Facture()
        {
            Encaissements = new HashSet<Encaissement>();
        }
        //public int ID { get; set; }
        public string NumeroFacture { get; set; }
        public string Motif { get; set; }
        public decimal Montant { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DateEcheanceFacture { get; set; }
        public TypeFacture TypeFacture { get; set; }
        public bool FactureGenere { get; set; }
        public bool CourrierEnvoye { get; set; }
        public bool FacturePayee { get; set; }
        public decimal MontantTotal { get; set; }
        public decimal MontantEncaisse
        {
            get { return Encaissements.Sum(u => u.Montant); }
        }

        public bool Active { get; set; }
        public bool Echue { get; set; }
        public int? ClientId { get; set; }
        public virtual Client Client { get; set; }

        public int? ContratId { get; set; }
        public virtual Contrat Contrat { get; set; }

        public int? EtatAvancementId { get; set; }
        public virtual EtatAvancement EtatAvancement { get; set; }


        public virtual ICollection<Encaissement> Encaissements { get; set; }
    }


    public class FactureProspect : Entity
    {
        public FactureProspect()
        {
            EncaissementsProspects = new HashSet<EncaissementProspect>();
        }
        //public int ID { get; set; }
        public string NumeroFacture { get; set; }
        public string Motif { get; set; }
        public decimal Montant { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DateEcheanceFacture { get; set; }
        public TypeFacture TypeFacture { get; set; }
        public bool FacturePayee { get; set; }
        public decimal MontantTotal { get; set; }
        public decimal MontantEncaisse
        {
            get { return EncaissementsProspects.Sum(enc => enc.MontantGlobal); }
        }

        //public bool Active { get; set; }
        //public bool Echue { get; set; }
        public int? ProspectId { get; set; }
        public virtual Client Prospect { get; set; }

     

        public virtual ICollection<EncaissementProspect> EncaissementsProspects { get; set; }
    }


    public enum TypeFacture
    {
        DepotMinimum=1,AvanceDemarrage=2,AppelDeFond=3,Echeance=4,Avance=5,FraisDossier=6
    }
}
