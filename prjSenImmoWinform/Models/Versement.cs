using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class Versement
    {
        public int ID { get; set; }
        public string Motif { get; set; }
        public decimal Montant { get; set; }
        public DateTime? Date { get; set; }

        public int CommissionId { get; set; }
        public virtual Commission Commission { get; set; }
    }
}
