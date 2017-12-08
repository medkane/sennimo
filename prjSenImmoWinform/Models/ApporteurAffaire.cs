using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public class ApporteurAffaire:Entity
    {
        public ApporteurAffaire()
        {
            Contrats = new HashSet<Contrat>();
        }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NomComplet
        {
            get
            {
                if (Type == TypeApporteur.Particulier)
                    return string.Format("{0} {1}",Prenom,Nom);
                else
                    return RaisonSociale;
            }
        }
        public string Adresse { get; set; }

        public string Email { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string TelephoneFixe { get; set; }
        public decimal TauxCommission { get; set; }
        public DateTime DateCreation { get; set; }
        public bool Actif { get; set; }
        public TypeApporteur Type { get; set; }
        public string RaisonSociale { get; set; }
        public string NINEA { get; set; }
        public string RCCM { get; set; }
        public string AdresseBureau { get; set; }
        public string TelephoneAgence { get; set; }
        public string NomGerant { get; set; }
        public string EmailAgence { get; set; }

        public virtual ICollection<Contrat> Contrats { get; set; }
        public virtual ICollection<Commission> Commissions { get; set; }
    }

    public enum TypeApporteur
    {
        Particulier = 1, Agence = 2
    }
}
