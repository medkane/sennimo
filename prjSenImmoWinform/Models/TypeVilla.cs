using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class TypeVilla
    { 
        public int TypeVillaId { get; set; }
        public TypeConstruction TypeConstruction { get; set; }

        public String NomType { get; set; } //Acajou; Acacia

        public String CodeType { get; set; }//F2A, F3A, etc

        public string NomComplet { get { return  CodeType +" (" + NomType + ")"; } }
        public ClasseVilla ClasseVilla { get; set; } //F2/F3.F4 etc

        public string Description { get; set; }

        public decimal SurfaceDeBase { get; set; }

        public decimal PrixStandard { get; set; }

        public int Chambre { get; set; }

        public int ChambreAvecSalleDeBain { get; set; }

        public int Salon { get; set; }

        public int Cuisine { get; set; }

        public int Toilette { get; set; }

        public int Patio { get; set; }

        public int CourArriere { get; set; }
        
        public string ImageVilla { get; set; }

        public int? TypeImmeubleId { get; set; }
        public virtual TypeImmeuble TypeImmeuble { get; set; }

        public int? ProjetId { get; set; }
        public virtual Projet Projet { get; set; }

    }

    public enum CategorieTypeVilla
    {

    }

    public enum ClasseVilla
    {
        F2, F3, F4, F5
    }
}


