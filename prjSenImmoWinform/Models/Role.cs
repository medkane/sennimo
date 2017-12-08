using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class Role
    {
        public Role()
        {
            Menus = new HashSet<Menu>();
            SousMenus = new HashSet<SousMenu>();
            Agents = new HashSet<Agent>();
        }
        public int ID { get; set; }
        public string CodeRole { get; set; }
        public string LibelleRole { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<SousMenu> SousMenus { get; set; }
        public virtual ICollection<Agent> Agents { get; set; }
    }
}
