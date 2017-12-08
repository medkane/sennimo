using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class ObjectifAnnuel
    {
        public int ID { get; set; }
        public int Annee { get; set; }
        public decimal objectifVente { get; set; }
        public string Commentaire { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }

        public int? CommercialId { get; set; }
        public virtual Agent Commercial { get; set; }
    }

    public class ObjectifTrimestriel
    {
        public int ID { get; set; }
        public int Annee { get; set; }
        public int Trimestre { get; set; }
        public decimal objectifVente { get; set; }
        public string Commentaire { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }

        public int? CommercialId { get; set; }
        public virtual Agent Commercial { get; set; }

        public int? ObjectifAnnuelId { get; set; }
        public virtual ObjectifAnnuel ObjectifAnnuel { get; set; }
    }

    public class ObjectifMensuel
    {
        public int ID { get; set; }
        public int Annee { get; set; }
        public int Mois { get; set; }
        public decimal objectifVente { get; set; }
        public string Commentaire { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }

        public int? CommercialId { get; set; }
        public virtual Agent Commercial { get; set; }

        public int? ObjectifAnnuelId { get; set; }
        public virtual ObjectifAnnuel ObjectifAnnuel { get; set; }
    }

    public class TauxAtteinte
    {
        public int ID { get; set; }
        public decimal TauxMinimun { get; set; }
        public decimal TauxMaximun { get; set; }
        public decimal Taux { get; set; }
    }
}
