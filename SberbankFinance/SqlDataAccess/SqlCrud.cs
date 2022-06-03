using SberbankFinance.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SberbankFinance.SqlDataAccess
{
    internal class SqlCrud
    {
        private readonly string _connectionString;
        private DataAccess _dataAccess = new DataAccess();
        public SqlCrud(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<SqlDataModel> CheckExistance(string username)
        {
            string sql = "SELECT username  FROM UserInformation WHERE @username" ;

            return _dataAccess.LoadData<SqlDataModel, dynamic>(sql, new { username }, _connectionString);
        }


        public void AddNewUser(string username)
        {
           
            string sql = "INSERT INTO Usename Values(@username) ";
            _dataAccess.SaveData(sql, new { username,  }, _connectionString);
        }

    }
}
