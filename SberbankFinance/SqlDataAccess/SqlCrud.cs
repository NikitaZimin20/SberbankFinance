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
        public List<SqlDataModel> CheckExistance(string username,string password)
        {
            string sql = "SELECT Exists(select * from UserInformation where username=@username and password=@password) as IsCorrect ";

            return _dataAccess.LoadData<SqlDataModel, dynamic>(sql, new { username,password }, _connectionString);
        }
        public List<SqlDataModel> ShowId(string username)
        {
            string sql = "select id as Id from UserInformation  where username=@username  ";

            return _dataAccess.LoadData<SqlDataModel, dynamic>(sql, new { username }, _connectionString);
        }

        public void Register(string username,string password)
        {
           
            string sql = "INSERT OR IGNORE INTO UserInformation(username,password) VALUES(@username,@password)";
            _dataAccess.SaveData(sql, new { username, password }, _connectionString);

        }
        public List<SqlDataModel> GetOutcomeCategory()
        {
            string sql = "SELECT CategoryName AS Categories FROM OutcomeCategories";
            return _dataAccess.LoadData<SqlDataModel, dynamic>(sql, new {  }, _connectionString);
        }
        public List<SqlDataModel> GetIncomeCategory()
        {
            string sql = "Select CategoryName AS Categories FROM IncomeCategories";
            return _dataAccess.LoadData<SqlDataModel, dynamic>(sql, new { }, _connectionString);
        }
        public void AddOutcome(string amount,string category,string userId,string categoryId)
        {
            string sql = "INSERT INTO Outcome(Amount,Category_id,User_id) Values(@amount,@category,@userId,@categoryId)";
        }

    }
}
