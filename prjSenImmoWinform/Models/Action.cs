using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class Action
    {
        public int ID { get; set; }
        public string CodeAction { get; set; }
        public string LibelleAction { get; set; }

        public int SousMenuId { get; set; }
        public virtual SousMenu SousMenu { get; set; }
    }
}
