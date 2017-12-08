using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class Encaissement
    {
        public int ID { get; set; }
        public decimal Montant { get; set; }
        public DateTime? Date { get; set; }
        public ModePaiement? ModePaiement { get; set; }
        public string Commentaire { get; set; }
        public string ReferencePaiement { get; set; }
        public int FactureId { get; set; }
        public virtual Facture Facture { get; set; }

        public int? EncaissementGlobalId { get; set; }
        public virtual EncaissementGlobal EncaissementGlobal { get; set; }
    }

    public class EncaissementGlobal
    {
        public EncaissementGlobal()
        {
            Encaissements = new HashSet<Encaissement>();
            FactureCommissions = new HashSet<FactureCommission>();
        }
        public int ID { get; set; }
        public string NumeroEncaissement { get; set; }
        public decimal MontantGlobal { get; set; }
        public DateTime? DateEncaissement { get; set; }
        public string Commentaire { get; set; }
        public string ReferencePaiement { get; set; }
        public ModePaiement? ModePaiement { get; set; }
        public bool EncaissementLettre { get; set; }

        public int ContratId { get; set; }
        public virtual Contrat Contrat { get; set; }

        public virtual ICollection<Encaissement> Encaissements { get; set; }
        public virtual ICollection<FactureCommission> FactureCommissions { get; set; }
    }

    public class EncaissementProspect
    {
        public int ID { get; set; }
        public string NumeroEncaissement { get; set; }
        public decimal MontantGlobal { get; set; }
        public DateTime? DateEncaissement { get; set; }
        public string Commentaire { get; set; }
        public string ReferencePaiement { get; set; }
        public ModePaiement? ModePaiement { get; set; }

        public bool FraisDeDossier { get; set; }
        public bool Deverse { get; set; }
        public bool AtteinteSeuil { get; set; }

        public int? ContratId { get; set; }
        public virtual Contrat Contrat { get; set; }

        public int ProspectId { get; set; }
        public virtual Client Prospect { get; set; }

        public int? FactureId { get; set; }
        public virtual FactureProspect Facture { get; set; }
    }

    public enum ModePaiement
    {
        Virement, Chèque, Carte  , Espèces,Compense, Transfert
    }
}
