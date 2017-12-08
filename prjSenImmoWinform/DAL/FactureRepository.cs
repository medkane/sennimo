using prjSenImmoWinform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.DAL
{
    public class FactureRepository : IRepository<FactureProspect>
    {
       
        SenImmoDataContext DB;

        public FactureRepository()
        {
            DB = new SenImmoDataContext();

        }
        public IEnumerable<FactureProspect> List
        {
            get
            {
                return DB.FactureProspects;
            }
        }

        public void Add(FactureProspect entity) 
        {
            DB.FactureProspects.Add(entity);
            DB.SaveChanges();
        }
        public void Delete(FactureProspect entity) { }
        public void Update(FactureProspect entity) { }

        public FactureProspect FindById(int Id)
        {
            var result = (from r in DB.FactureProspects where r.Id == Id select r).FirstOrDefault();
            return result;
        }
        
    }
}
