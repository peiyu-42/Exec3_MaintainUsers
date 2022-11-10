using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISpan.Utility
{	
	public class SqlDbHelper
	{
		private string connString;
		/// <summary>
		/// sql的連接字串
		/// </summary>
		/// <param name="keyOfConnstring">app.config裡的值，例如'default'</param>
		public SqlDbHelper(string keyOfConnstring)
		{
			connString = System.Configuration.ConfigurationManager
								.ConnectionStrings[keyOfConnstring]
								.ConnectionString;
		}
		public void ExecuteNonQuery(string sql, SqlParameter[] parameters)
		{
			// ExecuteNonQuery() 執行語法不傳回值，無法用在"select"
			using (var conn = new SqlConnection(connString))
			{
				SqlCommand command = new SqlCommand(sql, conn);
				conn.Open();

				command.Parameters.AddRange(parameters);
				command.ExecuteNonQuery();
			}
		}
		public DataTable Select(string sql, SqlParameter[] parameters)
		{
			// SQL，select 語法
			using (var conn = new SqlConnection(connString))
			{
				SqlCommand command = new SqlCommand(sql, conn);

				if (parameters != null)
				{
					command.Parameters.AddRange(parameters);
				}

				SqlDataAdapter adapter = new SqlDataAdapter(command);

				DataSet dataset = new DataSet();
				adapter.Fill(dataset, "table");

				return dataset.Tables["table"];
			}
		}
	}
}
