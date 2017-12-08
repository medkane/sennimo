using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class TypeContrat
    {
        public TypeContrat()
        {
            TypeEtatAvancements = new HashSet<TypeEtatAvancement>();
        }
        public int ID { get; set; }
        public string LibelleTypeContrat { get; set; }
        public int SeuilSouscription { get; set; }
        public int SeuilEntreeEnVigueur { get; set; }
        public DateTime? DateCreation { get; set; }
        public bool Actif { get; set; }
        public DateTime? DateDesactivation { get; set; }

        public CategorieContrat CategorieContrat { get; set; }

        public TypeConstruction TypeConstruction { get; set; }
        public int? ProjetId { get; set; }
        public virtual Projet Projet { get; set; }

        public virtual ICollection<TypeEtatAvancement> TypeEtatAvancements { get; set; }

    }

    public enum CategorieContrat
    {
        Réservation=1, Dépôt=2
    }
}
