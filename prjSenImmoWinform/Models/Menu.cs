using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class Menu
    {
        public Menu()
        {
            Roles = new HashSet<Role>();
            SousMenus = new HashSet<SousMenu>();
           
        }

        public int ID { get; set; }
        public string CodeMenu { get; set; }
        public string LibelleMenu { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<SousMenu> SousMenus { get; set; }
    }

    public class SousMenu
    {
        public SousMenu()
        {
            Roles = new HashSet<Role>();
        }
        public int ID { get; set; }
        public string CodeSousMenu { get; set; }
        public string LibelleSousMenu { get; set; }

        public int MenuId { get; set; }
        public virtual Menu Menu { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
