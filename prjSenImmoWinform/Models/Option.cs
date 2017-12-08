using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class Option
    {
        public int Id { get; set; }
        public DateTime? DatePriseOption { get; set; }
        public DateTime? DateFinOption { get; set; }
        public bool Active { get; set; }

        public bool SeuilContratAtteint { get; set; }
        public DateTime? DateAtteinteSeuil { get; set; }

        public bool ContratGenere { get; set; }

        public int? ClientID { get; set; }
        public virtual Client Client { get; set; }

        public int? LotId { get; set; }
        public virtual Lot Lot { get; set; }

        public int TypeContratId { get; set; }
        public virtual TypeContrat TypeContrat { get; set; }

        public int TypeVillaId { get; set; }
        public virtual TypeVilla TypeVilla { get; set; }
        public PositionLot PositionLot { get; set; }
        public decimal Surface { get; set; }
        public decimal PrixStandard { get; set; }
        public decimal PrixRevise { get; set; }
        public decimal PrixDeVente { get; set; }
        public decimal TauxRemiseAccordee { get; set; }
        public decimal MontantRemiseAccordee { get; set; }

        public int CommercialID { get; set; }
        public virtual Agent Commercial { get; set; }

        public int? ApporteurID { get; set; }
        public virtual ApporteurAffaire Apporteur { get; set; }
    }
}
