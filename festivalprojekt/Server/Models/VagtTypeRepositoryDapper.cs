using Dapper;
using Npgsql;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using Microsoft.Extensions.DependencyInjection;
using festivalprojekt.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;


namespace festivalprojekt.Server.Models
{
	public class VagtTypeRepositoryDapper : IVagtTypeReposityDapper
	{
        //Variable der gør det muligt at skrive SQL
        private string sql = "";
        //Variable der bruger Context klassen
        private dBContext Context;

        //Constructor
        public VagtTypeRepositoryDapper(dBContext context)
        {
            this.Context = context;
        }

        //Async metode der henter alle status via sql statement fra databasen
        public async Task<IEnumerable<Status>> HentAlleStatus()
        {
            sql = $"SELECT status_id AS \"StatusId\", status_navn AS \"StatusNavn\" FROM status";

            var StatusListe = await Context.Connection.QueryAsync<Status>(sql);
            return StatusListe.ToList();
        }

        //Async metode der henter alle vagt typer via sql statement fra databasen
        public async Task<IEnumerable<VagtTypeDTO>> HentAlleVagtTyper()
        {
            // Det er vigtigt med AS fordi eller kan dapper ikke matche til klassens navne automatisk.
            sql = $"SELECT vagt_type_id AS \"VagtTypeId\", vagt_type_navn AS \"VagtTypeNavn\"," +
                $" vagt_type_beskrivelse AS \"VagtTypeBeskrivelse\", " +
                $"vagt_type_område AS \"VagtTypeOmråde\", status_id AS \"StatusId\" FROM vagt_typer;";

            //try catch, hvis det ikke virker går den til catch
            try
            {                    
                    var VagtTypeListe = await Context.Connection.QueryAsync<VagtTypeDTO>(sql);

                    return VagtTypeListe;               
            }
            catch (NotImplementedException)
            {
                //hvis den ikke kan returne VagtTypeListe returnere den en tom liste
                return new List<VagtTypeDTO>();
            }
        }

        //Async metode der opretter vagt type
        public async void OpretVagtType(VagtTypeDTO NyVagtType)
        {
            sql = $"INSERT INTO vagt_typer (vagt_type_navn, vagt_type_beskrivelse, vagt_type_område, status_id) " +
                    $"VALUES ('{NyVagtType.VagtTypeNavn}', '{NyVagtType.VagtTypeBeskrivelse}', '{NyVagtType.VagtTypeOmråde}', " +
                    $"'{NyVagtType.StatusId}');";

            //Execute gør at SQL statementet bliver kørt i databasen
            await Context.Connection.ExecuteAsync(sql);
        }

        //Async metode der sletter en vagt type
        public async void SletVagtType(int VagtTypeId)
        {
            sql = $"DELETE FROM vagt_typer WHERE vagt_type_id = {VagtTypeId};";    
              
            //Execute gør at SQL statementet bliver kørt i databasen
            await Context.Connection.ExecuteAsync(sql);
        }

        //Async metode der opdater vagt type 
        public async void OpdaterVagtType(VagtTypeDTO NyVagtType)
        {
            sql = $"UPDATE vagt_typer SET vagt_type_navn = '{NyVagtType.VagtTypeNavn}', " +
                    $"vagt_type_beskrivelse = '{NyVagtType.VagtTypeBeskrivelse}', " +
                    $"vagt_type_område = '{NyVagtType.VagtTypeOmråde}'," +
                    $"status_id = '{NyVagtType.StatusId}'" +
                    $"WHERE vagt_type_id = {NyVagtType.VagtTypeID}";

            //Execute gør at SQL statementet bliver kørt i databasen
            await Context.Connection.ExecuteAsync(sql);
        }
    }
}

