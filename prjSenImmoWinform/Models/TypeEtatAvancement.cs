using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class TypeEtatAvancement
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string LibelleTechnique { get; set; }
        public string LibelleCommercial { get; set; }
        public string TypeAvancement { get; set; }
        public int ordre  { get; set; }
        public bool StatutTermine { get; set; }
        //public TypeEncaissement? TypeEncaissement { get; set; }
        public bool AppelFonds { get; set; } //précise si l'avancement nécessiement un décaissement
        public decimal TauxDecaissement { get; set; } //si AppelFonds Ok, alors Taux à renseigner, sinon Zero
        public bool NiveauTechnique { get; set; }

        public TypeConstruction TypeConstruction { get; set; }

        public int? ProjetId { get; set; }
        public virtual Projet Projet { get; set; }

        public int? TypeContratId { get; set; }
        public virtual TypeContrat TypeContrat { get; set; }

    }

   
}
