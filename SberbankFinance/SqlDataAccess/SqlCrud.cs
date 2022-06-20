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
        public List<SqlDataModel> GetCategory(bool category)
        {
            string sql = "SELECT CategoryName AS Categories FROM Categories WHERE Type=@category";
            return _dataAccess.LoadData<SqlDataModel, dynamic>(sql, new { category }, _connectionString);
        }
        public void AddCategory(string name,bool type)
        {
            string sql = "INSERT INTO Categories(CategoryName,Type) Values(@name,@type)";
            _dataAccess.SaveData(sql, new { name, type }, _connectionString);
        }
        public void UpdatePassword(string password, int id)
        {
            string sql = "UPDATE UserInformation SET password = @password WHERE id = @id";
            _dataAccess.SaveData(sql, new { password, id }, _connectionString);
        }
        public void UpdateLogin(string username, int id)
        {
            string sql = "UPDATE UserInformation SET username = @username WHERE id = @id";
            _dataAccess.SaveData(sql, new { username, id }, _connectionString);
        }

        public void AddOutcome(BalanceModel balance,int id)
        {
            string sql = "INSERT INTO Outcome(Amount,Date,Category_id,User_id) Values(@amount,@date,(Select Id from Categories WHERE CategoryName=@type),@id)";
            
            _dataAccess.SaveData(sql, new { balance.Amount, balance.Date,balance.Type,id }, _connectionString);
        }
        public List<BalanceModel> GetBalanceByMonth(int id,DateTime selectedDate,bool type )
        {
            DateTime startDate = new DateTime(selectedDate.Year,selectedDate.Month,1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            string sql = "SELECT sum(Outcome.Amount) AS Amount,Categories.CategoryName AS Type " +
                "From Outcome  " +
                "JOIN  Categories On Categories.Id=Outcome.Category_Id " +
                "WHERE Outcome.User_id=@id and Date BETWEEN @startDate AND @endDate AND Categories.Type=@type " +
                "GROUP BY Categories.CategoryName";

            return _dataAccess.LoadData<BalanceModel, dynamic>(sql, new {id,startDate,endDate,type }, _connectionString);
        }
        public List<BalanceModel> GetBalanceByDays(int id, DateTime selectedDate, bool type)
        {
            DateTime startDate = new DateTime(selectedDate.Year, selectedDate.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            string sql = "SELECT Outcome.Amount AS Amount,Categories.CategoryName AS Type,Outcome.Date " +
                "From Outcome  " +
                "JOIN  Categories On Categories.Id=Outcome.Category_Id " +
                "WHERE Outcome.User_id=@id and Date BETWEEN @startDate AND @endDate AND Categories.Type=@type  Order by Outcome.Date";
                

            return _dataAccess.LoadData<BalanceModel, dynamic>(sql, new { id, startDate, endDate, type }, _connectionString);
        }

    }
}
