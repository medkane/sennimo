using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class Projet
    {
        public int Id { get; set; }
        public string DenominationProjet { get; set; }
        public string Localisation { get; set; }
        public string Description { get; set; }
    }
}
