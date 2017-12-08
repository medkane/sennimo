using prjSenImmoWinform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.DAL
{

    public class AgentRepository
    {
        SenImmoDataContext db;
        public AgentRepository()
        {
            db = new SenImmoDataContext();
        }
        public IEnumerable<Agent> GetAllAgents()
        {
            return db.Agents;
        }

        public IEnumerable<Menu> GetRoleMenus(int RoleId)
        {
            return db.Roles.Find(RoleId).Menus;
        }

        public IEnumerable<SousMenu> GetRoleSousMenus(int RoleId)
        {
            return db.Roles.Find(RoleId).SousMenus;
        }

        public IEnumerable<SousMenu> GetMeuSousMenus(int menuId)
        {
            return db.SousMenus.Where(sMenu => sMenu.MenuId == menuId);
        }

        public IEnumerable<SousMenu> GetRoleMenuSousMenus(int RoleId, int menuId)
        {
            var roleSousMenus = db.Roles.Find(RoleId).SousMenus;
            return roleSousMenus.Where(sMenu => sMenu.MenuId == menuId);
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return db.Roles;
        }

        public IEnumerable<ObjectifAnnuel> GetObjectifsAgents(int annee)
        {
            return db.ObjectifAnnuels.Where(ob => ob.Annee==annee);
        }

        public IEnumerable<Menu> GetAllMenus()
        {
            return db.Menus;
        }

        public IEnumerable<SousMenu> GetAllSousMenu()
        {
            return db.SousMenus;
        }

        internal Agent FindById(int agentId)
        {
            return db.Agents.Find(agentId);
        }

        internal void Add(Agent agent)
        {
            try
            {
                db.Agents.Add(agent);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal void Delete(Agent agent)
        {
            try
            {
                db.Agents.Remove(agent);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal void SaveChanges()
        {
            db.SaveChanges();
        }

        public Agent FindByLogin(string login)
        {
            return db.Agents.Where(log => log.UserLogin == login).SingleOrDefault();
        }
        internal void AddRoleMenu(int RoldeId, int menuId)
        {
            try
            {
                db.Roles.Find(RoldeId).Menus.Add(db.Menus.Find(menuId));
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal void DeleteRoleMenu(int RoldeId, int menuId)
        {
            try
            {
                db.Roles.Find(RoldeId).Menus.Remove(db.Menus.Find(menuId));
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddRoleSousMenus(int RoleId, int sousMenuId)
        {
            try
            {
                db.Roles.Find(RoleId).SousMenus.Add(db.SousMenus.Find(sousMenuId));
                db.SaveChanges();
            }
	        catch (Exception)
	        {
		        throw;
	        }
        }

        internal void DeleteRoleSousMenu(int RoldeId, int sousMenuId)
        {
            try
            {
                db.Roles.Find(RoldeId).SousMenus.Remove(db.SousMenus.Find(sousMenuId));
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
