using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class Cooperative
    {
        public int Id { get; set; }
        public string Denomination { get; set; }

        public DateTime? DateSouscription { get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Fixe { get; set; }

        public int? AgentID { get; set; }
        public virtual Agent Agent { get; set; }


        public decimal TauxRemise { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }
}
