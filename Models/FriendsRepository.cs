using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;


namespace DapperBasicCrud.Models
{
    public class FriendsRepository
    {
        private string connectionstring;

        public FriendsRepository()
        {
            connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
            
        }
      

        public List<Friend> GetAll()
        {
            using(IDbConnection db = new SqlConnection(connectionstring))
            {                             
                return db.Query<Friend>("sp_Friends_GetAll",commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public Friend Get(int Id)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return db.Query<Friend>("sp_Friends_Get",new { Id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public Friend Create(Friend friend)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return db.Query<Friend>("sp_Friends_Create", friend, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public Friend Update(Friend friend)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return db.Query<Friend>("sp_Friends_Update", friend, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public Friend Delete(int Id)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return db.Query<Friend>("sp_Friends_Delete", new { Id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

    }
}