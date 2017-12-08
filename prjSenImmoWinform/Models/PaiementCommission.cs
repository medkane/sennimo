using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class PaiementCommission
    {
        public int ID { get; set; }
        public decimal MontantPaye { get; set; }
        public DateTime? Date { get; set; }
        public ModePaiement ModePaiement { get; set; }
        public string Commentaire { get; set; }
        public string ReferencePaiement { get; set; }

        public int FactureCommissionId { get; set; }
        public virtual FactureCommission FactureCommission { get; set; }

      
    }


    public class PaiementCommissionGlobal
    {
        public PaiementCommissionGlobal()
        {
            FactureCommissionGlobales = new HashSet<FactureCommissionGlobale>();
        }
        public int ID { get; set; }
        public decimal MontantPaye { get; set; }
        public DateTime? Date { get; set; }
        public ModePaiement ModePaiement { get; set; }
        public string Commentaire { get; set; }
        public string ReferencePaiement { get; set; }

       

        public int ApporteurAffaireId { get; set; }
        public virtual ApporteurAffaire ApporteurAffaire { get; set; }


        
        public virtual ICollection<FactureCommissionGlobale> FactureCommissionGlobales { get; set; }

    }
}
