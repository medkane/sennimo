using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class FactureCommission
    {
        public int ID { get; set; }
        public string Motif { get; set; }
        public decimal MontantAPayer { get; set; }
        public DateTime? Date { get; set; }
        public bool Payee { get; set; }
        public bool FactureGenere { get; set; }


        public int ContratId { get; set; }
        public virtual Contrat Contrat { get; set; }

        public int EncaissementGlobalId { get; set; }
        public virtual EncaissementGlobal EncaissementGlobal { get; set; }

        public int? FactureCommissionGlobaleId { get; set; }
        public virtual FactureCommissionGlobale FactureCommissionGlobale { get; set; }

        public virtual ICollection<PaiementCommission> PaiementCommissions { get; set; }
    }

    public class FactureCommissionGlobale
    {
        public FactureCommissionGlobale()
        {
            FactureCommissions = new HashSet<FactureCommission>();
        }
        public int ID { get; set; }
        public string Motif { get; set; }
        public decimal MontantAPayer { get; set; }
        public DateTime? Date { get; set; }
        public bool Payee { get; set; }


        public int ApporteurAffaireId { get; set; }
        public virtual ApporteurAffaire ApporteurAffaire { get; set; }


        public int? PaiementCommissionGlobalId { get; set; }
        public virtual PaiementCommissionGlobal PaiementCommissionGlobal { get; set; }

        public virtual ICollection<FactureCommission> FactureCommissions { get; set; }

       
    }
}
