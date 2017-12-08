using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class SoldeDeToutCompte
    {

        public SoldeDeToutCompte()
        {
            Remboursements = new HashSet<Remboursement>();
        }
        public int Id { get; set; }
        public DateTime DateResiliation { get; set; }
        public string NumeroFacture { get; set; }
        public decimal PrixDeVente { get; set; }
        public decimal MontantTotalEncaisse { get; set; }
        public decimal MontantDepotDeGarantie { get; set; }
        public decimal MontantARembourser { get; set; }

        public decimal MontantTotalCommissionsVersees { get; set; }

        public bool Remboursé { get; set; }

        public int ContratId { get; set; }
        public virtual Contrat Contrat { get; set; }


        public virtual ICollection<Remboursement> Remboursements { get; set; }
    }
}
