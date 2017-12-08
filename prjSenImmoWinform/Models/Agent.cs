using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class Agent:Entity
    {
        public Agent()
        {
            Clients = new HashSet<Client>();
        }
        //public int ID { get; set; }
        public string Matricule { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NomComplet { get { return Prenom + " " + Nom; } }
        public string Adresse { get; set; }
        public string Mobile1 { get; set; }
        public string Fixe { get; set; }
        public string Email { get; set; }

        public bool RecouvrementResa { get; set; }
        public bool RecouvrementDepot { get; set; }
        public string UserLogin { get; set; }
        public string MotDePasse { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int? ChefEquipeId { get; set; }
        public virtual Agent ChefEquipe { get; set; }

        public bool IsChefEquipe { get; set; }

        public int? ProjetId { get; set; }
        public virtual Projet Projet { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
