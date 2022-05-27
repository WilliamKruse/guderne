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
	public class VagtRepositoryDapper : IVagtRepositoryDapper
	{

        private string sql = "";
        private dBContext Context;

        public VagtRepositoryDapper(dBContext context)
        {
            this.Context = context;
        }

        public async Task<IEnumerable<VagtView>> HentAlleVagter(string streng, int id)
        {
            if (streng == "ALLE")
            {
				sql = $"SELECT vagt_id AS \"VagtId\", vagt_type_id AS \"VagtTypeId\", start_tid AS \"StartTid\", slut_tid AS \"SlutTid\", person_id AS \"PersonId\", vagt_type_navn AS \"VagtTypeNavn\", vagt_type_beskrivelse AS \"VagtTypeBeskrivelse\", vagt_type_område AS \"VagtTypeOmråde\" FROM fuld_vagt_view;";
			}
            else if (streng == "LEDIGE")
            {
				sql = $"SELECT vagt_id AS \"VagtId\", vagt_type_id AS \"VagtTypeId\", start_tid AS \"StartTid\", slut_tid AS \"SlutTid\", person_id AS \"PersonId\", vagt_type_navn AS \"VagtTypeNavn\", vagt_type_beskrivelse AS \"VagtTypeBeskrivelse\", vagt_type_område AS \"VagtTypeOmråde\" FROM fuld_vagt_view WHERE person_id IS NULL;";
            }
            else if (streng == "PERSONLIG")
            {
                sql = $"SELECT vagt_id AS \"VagtId\", vagt_type_id AS \"VagtTypeId\", start_tid AS \"StartTid\", slut_tid AS \"SlutTid\", person_id AS \"PersonId\", vagt_type_navn AS \"VagtTypeNavn\", vagt_type_beskrivelse AS \"VagtTypeBeskrivelse\", vagt_type_område AS \"VagtTypeOmråde\" FROM fuld_vagt_view WHERE person_id = {id};";
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
        public async void BookVagt(int VagtId, int PersonId)
        {
            sql = $"UPDATE vagter SET person_id = {PersonId} WHERE vagt_id = {VagtId};";
          
               
                    await Context.Connection.ExecuteAsync(sql);
                
        }
        public async void SletVagt(int VagtId)
        {
            sql = $"DELETE FROM vagter WHERE vagt_id = {VagtId};";
            try
            {
                
                    await Context.Connection.ExecuteAsync(sql);
                
            }
            catch (NotImplementedException)
            {
                ;
            }
        }
        public async void OpretVagt(VagtDTO NyVagt)
        {
         
            sql = $"INSERT INTO vagter (vagt_type_id, start_tid, slut_tid, person_id) VALUES ({NyVagt.VagtTypeId}, '{NyVagt.StartTid/*.ToString("yyyy-MM-dd H:mm:ss")*/}', '{NyVagt.SlutTid/*.ToString("yyyy-MM-dd H:mm:ss")*/}', null);";
            try
            {
                
                    await Context.Connection.ExecuteAsync(sql);
                
            }
            catch (NotImplementedException)
            {
                ;
            }
        }
 
	}
}

