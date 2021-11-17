using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;


namespace DapperBasicCrud.Models
{
    public class FriendsRepository
    {
        
        private string connectionstring;

        public FriendsRepository()
        {
            connectionstring = System.Configuration.
                ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
            
        }       
        
        public List<Friend> GetAll(RequestModel request)
        {
            using(IDbConnection db = new SqlConnection(connectionstring))
            {                             
                return db
                    .Query<Friend>("usp_Friends_GetAll",
                    request,
                    commandType: CommandType.StoredProcedure)
                    .ToList();
            }
        }
        public Friend Get(int Id)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return db
                    .Query<Friend>("usp_Friends_Get",
                    new { Id }, 
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }
        }
        public int Create(Friend friend)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                int lastInsertedId=
                 db.Query<int>("usp_Friends_Create", friend, 
                    commandType: CommandType.StoredProcedure)               
                    .FirstOrDefault();

                return lastInsertedId;
                
            }
        }
        public int Update(Friend friend)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return db
                    .Execute("usp_Friends_Update",
                       friend,
                       commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete(int Id)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return db
                    .Execute(
                        "usp_Friends_Delete",
                        new { Id },
                        commandType: CommandType.StoredProcedure);
            }
        }

    }
}