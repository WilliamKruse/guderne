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


//Using skal ændres, der er nogle af dem vi ikke bruger
namespace festivalprojekt.Server.Models
{
	public class VagtTypeRepositoryDapper : IVagtTypeReposityDapper
	{

   
        private string sql = "";
        private dBContext Context;

        public VagtTypeRepositoryDapper(dBContext context)
        {
            this.Context = context;
        }

        public async Task<IEnumerable<VagtTypeDTO>> HentAlleVagtTyper()
        {
            //laver sql statement til query (postgres). Det er vigtigt med AS fordi eller kan dapper ikke matche til klassens navne automatisk.
            sql = "SELECT vagt_type_id AS \"VagtTypeId\", vagt_type_navn AS \"VagtTypeNavn\", vagt_type_beskrivelse AS \"VagtTypeBeskrivelse\", vagt_type_område AS \"VagtTypeOmråde\" FROM vagt_typer;";

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

        public async void OpretVagtType(VagtTypeDTO NyVagtType)
        {
            //laver sql statement til query (postgres)
            sql = $"INSERT INTO vagt_typer (vagt_type_navn, vagt_type_beskrivelse, vagt_type_område) VALUES ('{NyVagtType.VagtTypeNavn}', '{NyVagtType.VagtTypeBeskrivelse}', '{NyVagtType.VagtTypeOmråde}');";

            //try catch, hvis det ikke virker går den til catch
            try
            {
                
                    //Execute gør at SQL statementet bliver kørt i databasen (query bruges kun når man henter data, ellers bruges execute).
                    await Context.Connection.ExecuteAsync(sql);
               
            }
            catch (NotImplementedException)
            {
                ;
            }
        }


        public async void SletVagtType(int VagtTypeId)
        {
            //laver sql statement til query (postgres)
            sql = $"DELETE FROM vagt_typer WHERE vagt_type_id = {VagtTypeId};";

            //try catch, hvis det ikke virker går den til catch
            try
            {
                //dapper syntax for at lave en ny connection til databasen
              
                    //Execute gør at SQL statementet bliver kørt i databasen (query bruges kun når man henter data, ellers bruges execute).
                    await Context.Connection.ExecuteAsync(sql);
                
            }
            catch (NotImplementedException)
            {
                ;
            }
        }

        public async void OpdaterVagtType(VagtTypeDTO NyVagtType)
        {
            sql = $"UPDATE vagt_typer SET vagt_type_navn = {NyVagtType.VagtTypeNavn}, " +
                    $"vagt_type_beskrivelse = {NyVagtType.VagtTypeBeskrivelse}, " +
                    $"vagt_type_område = {NyVagtType.VagtTypeOmråde} " +
                    $"WHERE vagt_type_id = {NyVagtType.VagtTypeID}";
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

