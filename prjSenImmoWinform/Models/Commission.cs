using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class Commission
    {
        public int ID { get; set; }
        public string Motif { get; set; }
        public decimal MontantTotalCommission { get; set; }
        public DateTime? Date { get; set; }

        public bool Payee { get; set; }

        public int ContratId { get; set; }
        public virtual Contrat Contrat { get; set; }

        public int ApporteurAffaireId { get; set; }
        public virtual ApporteurAffaire ApporteurAffaire { get; set; }

      
    }
}
