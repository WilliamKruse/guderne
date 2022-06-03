using System;
using Npgsql;
namespace festivalprojekt.Server.Models

	//DBContext contructor henter connectionstring fra appsettings og bruger den til at skabe en ny npgsql connection.
{
	public class dBContext
	{
		public NpgsqlConnection Connection { get; }
		public dBContext(IConfiguration _configuration)
		{
			string connString = _configuration.GetConnectionString("Azure2");
			this.Connection = new NpgsqlConnection(connString);
		}
	}
}

