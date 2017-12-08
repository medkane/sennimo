using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class Remboursement
    {
        public int ID { get; set; }
        public string NumeroRemboursement { get; set; }
        public decimal MontantRembourse{ get; set; }
        public DateTime? DateRemboursement { get; set; }
        public string Commentaire { get; set; }
        public string ReferencePaiement { get; set; }
        public ModePaiement ModePaiement { get; set; }

        public int SoldeDeToutCompteId { get; set; }
        public virtual SoldeDeToutCompte SoldeDeToutCompte { get; set; }


        public int ContratId { get; set; }
        public virtual Contrat Contrat { get; set; }

    }
}
