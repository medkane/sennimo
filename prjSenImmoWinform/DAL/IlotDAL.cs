using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prjSenImmoWinform.Tools;
using prjSenImmoWinform.Models;

namespace prjSenImmoWinform.DAL
{
    class IlotDAL
    {
        SenImmoDataContext db = Tools.Tools.db;  

        public IlotDAL()
         {
            
         }


        public IEnumerable<Ilot> GetAllIlots()
        {
            try
            {
                return db.Ilots.Where(i => i.StatutOuverture== true);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public Ilot GetIlot(int idIlot)
        {
            try
            {
                return db.Ilots.Where(i => i.StatutOuverture == true
                                        && i.Id == idIlot).SingleOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Ilot GetIlotByName(string nomIlot)
        {
            try
            {
                return db.Ilots.Where(i => i.StatutOuverture == true
                                        && i.NomIlot == nomIlot).SingleOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }
       
        // public IEnumerable<Client> GetClients(string nomClient, string prenomClient, TypeClient typeClient)
        //{
        //    try
        //    {
        //        var lesClientTrouves= db.Clients.Where(c => c.Actif == true && c.Type == typeClient);
                
        //        if (nomClient != string.Empty)
        //            lesClientTrouves = lesClientTrouves.Where(c => c.Nom.StartsWith(nomClient));
        //        if (prenomClient != string.Empty)
        //            lesClientTrouves = lesClientTrouves.Where(c => c.Prenom.StartsWith(prenomClient));

        //        return lesClientTrouves;

        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
        //}
    }
}
