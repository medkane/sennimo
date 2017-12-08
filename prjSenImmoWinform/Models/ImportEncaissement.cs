using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class ImportEncaissement
    {
        public int Id { get; set; }
        public DateTime DateEncaissement { get; set; }
        public string Compte { get; set; }
        public string Reference { get; set; }
        public string Libelle { get; set; }
        public string InituleComptetiers { get; set; }
        public Decimal Montant { get; set; }

        public bool? Importe { get; set; }
        public int? iDClient { get; set; }
        public string NomClient { get; set; }
    }

    public class ImportCompteTiers
    {
        public int Id { get; set; }
      
        public string Compte { get; set; }
    
        public string InituleComptetiers { get; set; }
      
    }


}
