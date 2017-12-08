using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class AppelDeFond
    {
        public int ID { get; set; }
        public string Motif { get; set; }
        public decimal Montant { get; set; }
        public DateTime? Date { get; set; }

        public bool FactureGenere { get; set; }
        public bool CourrierEnvoye { get; set; }

        public int EtatAvancementId { get; set; }
        public virtual EtatAvancement EtatAvancement { get; set; }
    }
}
