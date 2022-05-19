using System;
using Dapper;
using Npgsql;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using festivalprojekt.Shared.Models;


namespace festivalprojekt.Server.Models
{
	public class VagtRepositoryDapper
	{

		private string connString = "User ID=postgres;Password=1234;Host=localhost;Port=5432;Database=ProjMgr;";
		private string sql = "";

		public async Task<IEnumerable<VagtView>> HentAlleVagter(string a, int b)
        {
            if (a == "ALLE")
            {
				sql = @"SELECT vagt_id AS VagtId, vagt_type_id AS VagtTypeId, start_tid AS StartTid, slut_tid AS SlutTid, person_id AS PersonId FROM fuld_vagt_view;";
			}
            else if (a == "LEDIGE")
            {
				sql = @"SELECT vagt_id AS VagtId, vagt_type_id AS VagtTypeId, start_tid AS StartTid, slut_tid AS SlutTid, person_id AS PersonId from fuld_vagt_view WHERE person_id IS NULL;";
            }
            else if (a == "PERSONLIG")
            {
                sql = $"SELECT vagt_id AS VagtId, vagt_type_id AS VagtTypeId, start_tid AS StartTid, slut_tid AS SlutTid, person_id AS PersonId FROM fuld_vagt_view WHERE person_id = {b};";
            }
            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var VagtListe =  await connection.QueryAsync<VagtView>(sql);
                    return VagtListe;
                }
            }
            catch (Exception)
            {
                return new List<VagtView>();
            }
			
        }
        public async void BookVagt(int VagtId, int PersonId)
        {
            sql = $"UPDATE TABLE vagter SET person_id = {PersonId} WHERE vagt_id = {VagtId};";
            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    await connection.ExecuteAsync(sql);
                }

            }
            catch (Exception)
            {
                ;
            }
        }
        public async void SletVagt(int VagtId)
        {
            sql = $"DELETE FROM vagter WHERE vagt_id = {VagtId};";
            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    await connection.ExecuteAsync(sql);
                }
            }
            catch (NotImplementedException)
            {
                ;
            }
        }
        public async void OpretVagt(VagtDTO NyVagt)
        {
            sql = $"INSERT INTO vagter (vagt_type_id, start_tid, slut_tid, person_id) VALUES ({NyVagt.VagtTypeId}, {NyVagt.StartTid}, {NyVagt.SlutTid}, {NyVagt.PersonId});";
            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    await connection.ExecuteAsync(sql);
                }
            }
            catch (NotImplementedException)
            {
                ;
            }
        }
 
	}
}

