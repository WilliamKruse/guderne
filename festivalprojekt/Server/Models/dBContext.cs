using System;
using Npgsql;
namespace festivalprojekt.Server.Models

{
	public class dBContext
	{

		public NpgsqlConnection Connection { get; }
		

		public dBContext(IConfiguration _configuration)
		{
			string connString = _configuration.GetConnectionString("Azure");
			Console.WriteLine("Azure constring get done" + connString);
			this.Connection = new NpgsqlConnection(connString);
		}
	}
}

