using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class Echeance
    {
        public int ID { get; set; }
        public DateTime DateEcheance { get; set; }
        public decimal MontantEcheance { get; set; }
        public DateTime DelaiEcheance { get; set; }

        public int? ClientId { get; set; }
        public virtual Client Client { get; set; }

        public int? ContratId { get; set; }
        public virtual Contrat Contrat { get; set; }
    }

    public class GenerationEcheance
    {
        public int ID { get; set; }
        public int Mois { get; set; }
        public int Annee { get; set; }
        public string Comentaire { get; set; }


        public int? AgentId { get; set; }
        public virtual Agent Agent { get; set; }
    }
}
