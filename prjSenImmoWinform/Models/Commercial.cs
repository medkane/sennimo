using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class Commercial:Agent
    {
       

        public virtual ICollection<Contrat> Contrats { get; set; }
        public virtual ICollection<Option> Options { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<ActiviteCommerciale> ActiviteCommerciales { get; set; }



    }
}
