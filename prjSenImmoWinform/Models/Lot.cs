using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class Lot
    {
        public int ID { get; set; }
        public string NumeroLot { get; set; }

        public decimal Superficie { get; set; }
        public PositionLot PositionLot { get; set; }
        public NiveauAppartement NiveauAppartement { get; set; }
        public decimal PrixRevise { get; set; }
        public StatutLot StatutLot{ get; set; }

        public bool LotVirtuel { get; set; }
        public int TypeVillaID { get; set; }
        public virtual TypeVilla TypeVilla { get; set; }

        public int IlotID { get; set; }
        public virtual Ilot Ilot { get; set; }

        public virtual ICollection<EtatAvancement> EtatsAvancements { get; set; }
        public virtual ICollection<Option> Options { get; set; }
        public virtual ICollection<Contrat> Contrats { get; set; }
    }

    public enum StatutLot
    {
        Libre, Option, Reserve, Vendu,Desactive
    }

    public enum PositionLot
    {
        Angle, Standard
    }
        
    public enum NiveauAppartement
    {
       RDC=1, Premier=2, Deuxième=3, Troisième = 4, Quatrième = 5, Cinquième = 6, Sixième = 7
    }
}
