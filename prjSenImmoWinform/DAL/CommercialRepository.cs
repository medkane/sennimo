using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prjSenImmoWinform.Models;

namespace prjSenImmoWinform.DAL
{
    public class CommercialRepository : IRepository<Commercial>
    {
        SenImmoDataContext DB;

        //public Commercial leCommercialEnCours { get; set; }

        public CommercialRepository()
        {
            DB = new SenImmoDataContext();
            //leCommercialEnCours = FindById(4);

        }
        public IEnumerable<Commercial> List { get
                                       {
                                           return DB.Commercials;
                                        }
                                     }

        public IEnumerable<Agent> GetCommerciaux()
        {
            var RoleCOmmerciale = DB.Roles.Where(c => c.CodeRole == "CMC").SingleOrDefault();
            return DB.Agents.Where(c => c.RoleId == RoleCOmmerciale.ID && c.IsChefEquipe==false).ToList();
        }

        public IEnumerable<Agent> GetAllCommerciaux()
        {
            return DB.Agents.Where(c => c.Role.CodeRole == "CMC").ToList();
        }

        public void Add(Commercial entity){}
        public void Delete(Commercial entity) { }
        public void Update(Commercial entity) { }

        public Commercial FindById(int Id) 
        {
            var result = (from r in DB.Commercials where r.Id == Id select r).FirstOrDefault();
            return result; 
        }

        internal void AffecterCommercial(int prospectId, int commercialId)
        {
            var leProspect = DB.Clients.Find(prospectId);
            var leCommercial = DB.Agents.Find(commercialId);
            leCommercial.Clients.Add(leProspect);
            leProspect.CommercialID = commercialId;
            DB.SaveChanges();
        }
    }
}
