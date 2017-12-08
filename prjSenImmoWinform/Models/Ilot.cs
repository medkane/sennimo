using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class Ilot:Entity
    {
        public Ilot()
        {
            Lots = new  HashSet<Lot>();
        }
        public string NomIlot { get; set; }
        public double Superficie { get; set; }
        public DateTime? DateOuverturePrevue { get; set; }
        public DateTime? DateOuvertureEffective { get; set; }

        public DateTime? DateDemarrageTravaux { get; set; }
        public DateTime? DateFinTravaux { get; set; }
        public DateTime? DateDebutLivraison { get; set; }
        public DateTime? DateFinLivraison { get; set; }
        public bool StatutOuverture { get; set; }
        public TypeConstruction TypeConstruction { get; set; }
        public string Commentaires { get; set; }

        public int? TypeImmeubleId { get; set; }
        public virtual TypeImmeuble TypeImmeuble { get; set; }

        public int? ProjetId { get; set; }
        public virtual Projet Projet { get; set; }

        public virtual ICollection<Lot> Lots { get; set; }

    }

    public enum TypeConstruction
    {
        Villa=1, Appartement=2
    }
}
