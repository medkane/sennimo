using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class ActiviteCommerciale
    {
        public int Id { get; set; }
        public DateTime DateActivite { get; set; }
        public TimeSpan HeureActivite { get; set; }

        public int? MyProperty { get; set; }
        public DateTime? DateFinActivite { get; set; }
        public TypeActivite TypeActivite { get; set; }
        public Priorite Priorite { get; set; }
        public StatutActiviteCommerciale StatutActiviteCommerciale { get; set; }
        public string AutreTypeActivite { get; set; }
        public string Commentaire { get; set; }
        public bool BRappel { get; set; }


        public DateTime? DateRappel { get; set; }
        public TimeSpan? HeureRappel { get; set; }

        public int ClientID { get; set; }
        public virtual Client Client { get; set; }

        public int CommercialID { get; set; }
        public virtual Agent Commercial { get; set; }
    }

    public enum TypeActivite
    {
        RendezVous = 1, Appel = 2, Visite = 3, Autre=4
    }
    public enum Priorite
    {
        Faible = 1, Moyenne = 2, Haute = 3
    }

    public enum StatutActiviteCommerciale
    {
        NonEchue=1,  Exécutée = 2, Renvoyée = 3, Annulée = 4, EchueNonExecutée=5
    }



}
