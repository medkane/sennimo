using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class EtatAvancement
    {
        public int ID { get; set; }
        public DateTime? DateSaisieAvancement { get; set; }
        public string Commentaire { get; set; }
        public bool Actif { get; set; }
        public bool Encours { get; set; }
        public StatutEtatAvancement Statut { get; set; }

        public int LotId { get; set; }
        public virtual Lot Lot { get; set; }


        public int? TypeEtatAvancementID { get; set; }
        public virtual TypeEtatAvancement TypeEtatAvancement { get; set; }

        public virtual ICollection<Facture> Factures { get; set; }
    }

    public enum StatutEtatAvancement
    {
        EnCours = 1, Terminé = 2
    }

    public class ImportEtatAvancement
    {
        public int ID { get; set; }
        public string NumeroLot { get; set; }
        public string Avancement { get; set; }
    }
}
