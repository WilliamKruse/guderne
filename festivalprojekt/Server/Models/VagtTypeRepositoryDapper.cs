using Dapper;
using Npgsql;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using festivalprojekt.Shared.Models;

//Using skal ændres, der er nogle af dem vi ikke bruger
namespace festivalprojekt.Server.Models
{
	public class VagtTypeRepositoryDapper : IVagtTypeReposityDapper
	{
        //definer en connection string som der er vores adgang til databasen(skal ikke være her når vi er færdige)
        private string connString = "User ID=postgres;Password=godtkodeord ;Host=localhost;Port=5432;Database=miliøguderne;";

        //tom string vi ændrer når en funktion bliver kaldt
        private string sql = "";

        public async Task<IEnumerable<VagtTypeDTO>> HentAlleVagtTyper()
        {
            //laver sql statement til query (postgres). Det er vigtigt med AS fordi eller kan dapper ikke matche til klassens navne automatisk.
            sql = "SELECT vagt_type_id AS VagtTypeId, vagt_type_navn AS VagtTypeNavn, vagt_type_beskrivelse AS VagtTypeBeskrivelse, vagt_type_område AS VagtTypeOmråde FROM vagt_typer;";

            //try catch, hvis det ikke virker går den til catch
            try
            {
                //dapper syntax for at lave en ny connection til databasen
                using (var connection = new NpgsqlConnection(connString))
                {
                    //laver variabel og fylder data fra query ind i listen.
                    //Vi specificerer hvilken type data der forventes(VagtTypeDTO) og hvis navnene fra databasens kolonner
                    //matcher navnene i klassens properties
                    //laver den automatisk rigtige udfyldte objekter.
                    var VagtTypeListe = await connection.QueryAsync<VagtTypeDTO>(sql);

                    return VagtTypeListe;
                }
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
            sql = $"INSERT INTO vagter (vagt_type_navn, vagt_type_beskrivelse, vagt_type_område) VALUES ({NyVagtType.VagtTypeNavn}, {NyVagtType.VagtTypeBeskrivelse}, {NyVagtType.VagtTypeOmråde});";

            //try catch, hvis det ikke virker går den til catch
            try
            {
                //dapper syntax for at lave en ny connection til databasen
                using (var connection = new NpgsqlConnection(connString))
                {
                    //Execute gør at SQL statementet bliver kørt i databasen (query bruges kun når man henter data, ellers bruges execute).
                    await connection.ExecuteAsync(sql);
                }
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
                using (var connection = new NpgsqlConnection(connString))
                {
                    //Execute gør at SQL statementet bliver kørt i databasen (query bruges kun når man henter data, ellers bruges execute).
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

