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
        public List<SqlDataModel> GetColors(string category,int id)
        {
            string sql = "SELECT UserColors.Color AS Color FROM UserColors  " +
                "JOIN Categories ON  Categories.Id=UserColors.Category_id  " +
                "JOIN UserInformation ON UserInformation.Id=UserColors.UserId " +
                "WHERE EXISTS(SELECT * FROM UserColors WHERE CategoryName=@category AND UserInformation.id=@id ) ";

            return _dataAccess.LoadData<SqlDataModel, dynamic>(sql, new { category,id }, _connectionString);
        }
        public void AddColor(int userId,string category,string color)
        {
            string sql = "Delete from UserColors " +
                "WHERE EXISTS" +
                "(SELECT * from UserColors Where UserColors.UserId=@userId And UserColors.Category_id=(Select Id from Categories WHERE CategoryName=@category) );";
            _dataAccess.SaveData(sql, new { userId, category }, _connectionString);
            sql = "Insert Into UserColors VALUES(@userId,(Select Id from Categories WHERE CategoryName=@category),@color)";
            _dataAccess.SaveData(sql, new { userId, category, color }, _connectionString);
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
        public List<BalanceModel> GetCategory()
        {
            string sql = "SELECT CategoryName AS Category,Type FROM Categories";
            return _dataAccess.LoadData<BalanceModel, dynamic>(sql, new {  }, _connectionString);
        }
        public void AddCategory(string name,bool type)
        {
            string sql = "INSERT INTO Categories(CategoryName,Type) Values(@name,@type)";
            _dataAccess.SaveData(sql, new { name, type }, _connectionString);
        }
        
        public void AddOutcome(BalanceModel balance,int id)
        {
            string sql = "INSERT INTO Outcome(Amount,Date,Category_id,User_id) Values(@amount,@date,(Select Id from Categories WHERE CategoryName=@type),@id)";
            
            _dataAccess.SaveData(sql, new { balance.Amount, balance.Date,balance.Category,id }, _connectionString);
        }
        public List<BalanceModel> GetBalanceByMonth(int id,DateTime selectedDate,bool type )
        {
            DateTime startDate = new DateTime(selectedDate.Year,selectedDate.Month,1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            string sql = "SELECT sum(Outcome.Amount) AS Amount,Categories.CategoryName AS Category " +
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
            string sql = "SELECT Outcome.Amount AS Amount,Categories.CategoryName AS Category,Outcome.Date " +
                "From Outcome  " +
                "JOIN  Categories On Categories.Id=Outcome.Category_Id " +
                "WHERE Outcome.User_id=@id and Date BETWEEN @startDate AND @endDate AND Categories.Type=@type  Order by Outcome.Date";
                

            return _dataAccess.LoadData<BalanceModel, dynamic>(sql, new { id, startDate, endDate, type }, _connectionString);
        }

        public void DeleteAccount(int id)
        {
            string sql = "DELETE FROM UserInformation Where Id=@id";
            
             _dataAccess.SaveData(sql, new { id},_connectionString);
        }

    }
}
