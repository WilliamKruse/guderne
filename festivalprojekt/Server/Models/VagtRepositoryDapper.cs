using System;
using Dapper;
using Npgsql;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using festivalprojekt.Shared.Models;
using Microsoft.Extensions.Configuration;


namespace festivalprojekt.Server.Models
{
    //definere klasse som implementer interfacet
    public class VagtRepositoryDapper : IVagtRepositoryDapper
	{
        //Variable der gør det muligt at skrive SQL
        private string sql = "";
        //Variable der bruger Context klassen
        private dBContext Context;

        //Constructor
        public VagtRepositoryDapper(dBContext context)
        {
            this.Context = context;
        }

        //Async metode der henter alle vagter via sql statement fra databasen
        public async Task<IEnumerable<VagtView>> HentAlleVagter(string streng, int id)
        {
            if (streng == "ALLE")
            {
				sql = $"SELECT vagt_id AS \"VagtId\", vagt_type_id AS \"VagtTypeId\", start_tid AS \"StartTid\", " +
                    $"slut_tid AS \"SlutTid\", person_id AS \"PersonId\", vagt_type_navn AS \"VagtTypeNavn\", " +
                    $"vagt_type_beskrivelse AS \"VagtTypeBeskrivelse\", vagt_type_område AS \"VagtTypeOmråde\" FROM fuld_vagt_view;";
			}
            else if (streng == "LEDIGE")
            {
				sql = $"SELECT vagt_id AS \"VagtId\", vagt_type_id AS \"VagtTypeId\", start_tid AS \"StartTid\", " +
                    $"slut_tid AS \"SlutTid\", person_id AS \"PersonId\", vagt_type_navn AS \"VagtTypeNavn\", " +
                    $"vagt_type_beskrivelse AS \"VagtTypeBeskrivelse\", vagt_type_område AS \"VagtTypeOmråde\"" +
                    $" FROM fuld_vagt_view WHERE person_id IS NULL;";
            }
            else if (streng == "PERSONLIG")
            {
                sql = $"SELECT vagt_id AS \"VagtId\", vagt_type_id AS \"VagtTypeId\", start_tid AS \"StartTid\", slut_tid AS \"SlutTid\"," +
                    $" person_id AS \"PersonId\", vagt_type_navn AS \"VagtTypeNavn\", vagt_type_beskrivelse AS \"VagtTypeBeskrivelse\"," +
                    $" vagt_type_område AS \"VagtTypeOmråde\" FROM fuld_vagt_view WHERE person_id = {id};";
            }
            try
            {
                    var VagtListe =  await Context.Connection.QueryAsync<VagtView>(sql);
                    return VagtListe.ToList();      
            }
            catch (Exception)
            {
                return new List<VagtView>();
            }
        }

        //Async metode der booker en vagt via VagtId og PersonId
        public async void BookVagt(int VagtId, int PersonId)
        {
            int p_id = PersonId;
            if (p_id == 0)
            {
                sql = $"UPDATE vagter SET person_id = NULL WHERE vagt_id = {VagtId};";
            }
            else
            {
                sql = $"UPDATE vagter SET person_id = {PersonId} WHERE vagt_id = {VagtId};";
            }
                await Context.Connection.ExecuteAsync(sql);
        }

        //Async metode der sletter en vagt på VagtId
        public async void SletVagt(int VagtId)
        {
            sql = $"DELETE FROM vagter WHERE vagt_id = {VagtId};";

            //Execute gør at SQL statementet bliver kørt i databasen
            await Context.Connection.ExecuteAsync(sql);
        }

        //Async metode der opretter en ny vagt
        public async void OpretVagt(VagtDTO NyVagt)
        {
            ////Opretter dictionary til Opret vagt som bruges i sql statment 
            DynamicParameters dp = new DynamicParameters();
            dp.Add("StartTid", NyVagt.StartTid);
            dp.Add("SlutTid", NyVagt.SlutTid);
            dp.Add("VagtType", NyVagt.VagtTypeId);
            dp.Add("PersonId", NyVagt.PersonId);

            sql = $"INSERT INTO vagter (vagt_type_id, start_tid, slut_tid, person_id) VALUES (@VagtType, @StartTid, @SlutTid, @Personid)";

            //Execute gør at SQL statementet bliver kørt i databasen
            await Context.Connection.ExecuteAsync(sql, dp);          
        }
	}
}

