using ISpan.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CRUD
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var dbHelper = new SqlDbHelper("default");
			string table = "Users";

			// 新增資料
			SQLInsert(dbHelper,table);

			// 修改資料
			SQLUpdate(dbHelper, table);
 
			// 刪除資料
			SQLDelete(dbHelper, table);

			// 選擇資料
			SQLSelect(dbHelper, table);

		}
		public static void SQLInsert(SqlDbHelper dbHelper,string table)
		{
			string sql = $@"INSERT INTO {table}
							(Name, Account,Password,dateofBirthd,Height)
							VALUES
							(@Name, @Account,@Password,@dateofBirthd,@Height)";
			var parameters = new SqlParameterBuilder()
								   .AddNVarchar("Name", 50, "Amy")
								   .AddNVarchar("Account", 50, "Amy666")
								   .AddNVarchar("Password", 50, "666Amy")
								   .AddDateTime("dateOfBirthd", Convert.ToDateTime("1999 - 10 - 11"))
								   .AddInt("Height", 166)
								   .Build();

			dbHelper.ExecuteNonQuery(sql, parameters);

			Console.WriteLine("紀錄已新增");
			
		}

		public static void SQLDelete(SqlDbHelper dbHelper, string table)
		{
			string sql = $@"DELETE FROM {table} WHERE Id=@Id";

			var parameters = new SqlParameterBuilder()
									.AddInt("id", 2)
									.Build();

			dbHelper.ExecuteNonQuery(sql, parameters);

			Console.WriteLine("記錄已刪除");

		}
		
		public static void SQLUpdate(SqlDbHelper dbHelper, string table)
		{
			string sql = $@"UPDATE {table} 
							SET Name=@Name, Account=@Account,Password=@Password
							,dateofBirthd=@dateofBirthd,Height=@Height 
							WHERE Id=@Id";

			var parameters = new SqlParameterBuilder()
								   .AddInt("Id", 2)
								   .AddNVarchar("Name", 50, "Amy.H ")
								   .AddNVarchar("Account", 50, "AmyH666")
								   .AddNVarchar("Password", 50, "666AmyH")
								   .AddDateTime("dateOfBirthd", Convert.ToDateTime("1999 - 12 - 11"))
								   .AddInt("Height", 162)
								   .Build();

			dbHelper.ExecuteNonQuery(sql, parameters);

			Console.WriteLine("記錄已修改");
		}
		
		public static void SQLSelect(SqlDbHelper dbHelper, string table)
		{
			string sql = $@"SELECT Name, Account,Password,dateofBirthd,Height FROM {table} 
								WHERE Id> @Id  ORDER BY Id ASC";

			var parameters = new SqlParameterBuilder().AddInt("id", 0).Build();

			DataTable news = dbHelper.Select(sql, parameters);
			foreach (DataRow row in news.Rows)
			{
				int id = row.Field<int>("id");
				string name = row.Field<string>("Name");
				string account = row.Field<string>("Account");
				string password = row.Field<string>("Password");
				DateTime dateofbirth = row.Field<DateTime>("dateofBirthd");
				int height = row.Field<int>("height");

				Console.WriteLine($@"id={id} Name={name} Account={account} Password={password} Birthday={dateofbirth.ToString("yyyy-MM-dd")} Height={height}");
			}
		}

	}
	
}
