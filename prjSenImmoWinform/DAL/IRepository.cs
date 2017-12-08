using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.Models
{
    public interface IRepository<T> where T: Entity
    {
       
        IEnumerable<T> List { get; }
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T FindById(int Id);
       
    }
}
