using System;
using System.Data;
using Dapper;
using Npgsql;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using festivalprojekt.Shared.Models;
using Microsoft.Extensions.Configuration;

namespace festivalprojekt.Server.Models
{
    //definere klasse som implementer interfacet
    public class PersonRepositoryDapper : IPersonRepositoryDapper
    {
        //Variable der gør det muligt at skrive SQL
        private string sql = "";
        //Variable der bruger Context klassen
        private dBContext Context;

        //Contructor
        public PersonRepositoryDapper(dBContext context)
        {
            this.Context = context;
        }

        //Async metode der henter alle roller via sql statement fra databasen
        public async Task<IEnumerable<Roller>> HentAlleRoller()
        {
            sql = $"SELECT rolle_id AS \"RolleId\", rolle_navn AS \"RolleNavn\" FROM roller";

            var RolleListe = await Context.Connection.QueryAsync<Roller>(sql);
            return RolleListe.ToList();
        }

        //Async metode der henter alle kompetencer via sql statement fra databasen
        public async Task<IEnumerable<Kompetencer>> HentAlleKompetencer()
        {
            sql = $"SELECT kompetence_id AS \"KompetenceId\", kompetence_navn AS \"KompetenceNavn\" FROM kompetencer";

            var KompetenceListe = await Context.Connection.QueryAsync<Kompetencer>(sql);
            return KompetenceListe.ToList();
        }

        //Async metode der henter alle personer via sql statement fra databasen
        public async Task<IEnumerable<PersonDTO>> HentAllePersoner()
        {
            // Det er vigtigt med AS fordi eller kan dapper ikke matche til klassens navne automatisk.
            sql = $"SELECT kompetence_id AS \"KompetenceId\", kompetence_navn AS \"KompetenceNavn\", person_id AS \"PersonId\", " +
                $"rolle_id AS \"RolleId\", email AS \"Email\", telefon AS \"Telefon\", kodeord AS \"Kodeord\", fornavn AS \"Fornavn\", " +
                $"efternavn AS \"Efternavn\", fødselsdag::text AS \"Fødselsdag\" FROM fuld_person_view_3;";

                var PersonListe = await Context.Connection.QueryAsync<PersonDTO>(sql);
                return PersonListe.ToList();
        }

        //Async metode der henter person via sql statement fra databasen
        public async Task<IEnumerable<PersonDTO>> HentPerson(int PersonId)
        {
            sql = $"SELECT kompetence_id AS \"KompetenceId\", kompetence_navn AS \"KompetenceNavn\", person_id AS \"PersonId\", " +
                $"rolle_id AS \"RolleId\", email AS \"Email\", telefon AS \"Telefon\", kodeord AS \"Kodeord\", fornavn AS \"Fornavn\"," +
                $" efternavn AS \"Efternavn\", fødselsdag::text AS \"Fødselsdag\" FROM fuld_person_view_3 WHERE person_id = {PersonId};";
       
                var Person = await Context.Connection.QueryAsync<PersonDTO>(sql);
                return Person.ToList();           
        }

        //Async metode der opretter person med komptencer og hvis man ikke vælger nogle komptencer får man array 5 som default
        public async void OpretPerson(PersonDTO NyPerson)
        {
            string arr = "";
            int tal = 0;
            if (NyPerson.KompetenceId.Length == 0)
            {
                arr = "5";
            }
            else
            {
                foreach (var item in NyPerson.KompetenceId)
                {
                    if (tal == 0)
                    {                      
                        arr += item + "";
                        tal++;
                    }
                    else
                    {                        
                        arr += "," + item;
                        tal++;
                    }
                }
            }

            //Opretter dictionary til Opret person som bruges i sql statment 
            DynamicParameters dp = new DynamicParameters();
            dp.Add("PersonId", NyPerson.PersonId);
            dp.Add("RolleId", NyPerson.RolleId);
            dp.Add("Email", NyPerson.Email);
            dp.Add("Telefon", NyPerson.Telefon);
            dp.Add("Kodeord", NyPerson.Kodeord);
            dp.Add("Fornavn", NyPerson.Fornavn);
            dp.Add("Efternavn", NyPerson.Efternavn);
            dp.Add("RealF", NyPerson.RealF);

            sql = $"CALL opret_person(ARRAY[{arr}], @PersonId, @RolleId, @Email, @Telefon, @Kodeord, @Fornavn, @Efternavn, @RealF);";

            //Execute gør at SQL statementet bliver kørt i databasen
            await Context.Connection.ExecuteAsync(sql, dp);         
        }

        //Async metode der opdaterer en person med komptencer og hvis man ikke vælger nogle komptencer får man array 5 som default.
        public async void OpdaterPerson(PersonDTO NyPerson)
        {

            string arr = "";
            int tal = 0;
            if (NyPerson.KompetenceId.Length == 0)
            {
                arr = "5";
            }
            else
            {
                foreach (var item in NyPerson.KompetenceId)
                {
                    if (tal == 0)
                    {
                        arr += item + "";
                        tal++;
                    }
                    else
                    {
                        arr += "," + item;
                        tal++;
                    }
                }
            }

            //Opretter dictionary til opdater person som bruges i sql statment 
            DynamicParameters dp = new DynamicParameters();
            dp.Add("PersonId", NyPerson.PersonId);
            dp.Add("RolleId", NyPerson.RolleId);
            dp.Add("Email", NyPerson.Email);
            dp.Add("Telefon", NyPerson.Telefon);
            dp.Add("Kodeord", NyPerson.Kodeord);
            dp.Add("Fornavn", NyPerson.Fornavn);
            dp.Add("Efternavn", NyPerson.Efternavn);
            dp.Add("RealF", NyPerson.RealF);

            sql = $"CALL opdater_person(ARRAY[{arr}], @PersonId, @RolleId, @Email, @Telefon, @Kodeord, @Fornavn, @Efternavn, @RealF);";

            //Execute gør at SQL statementet bliver kørt i databasen
            await Context.Connection.ExecuteAsync(sql, dp);
        }

        //Async metode der logger en person ind  
        public async Task<IEnumerable<PersonDTO>> Login(string email, string kode)
        {
            sql = $"SELECT kompetence_id AS \"KompetenceId\", kompetence_navn AS \"KompetenceNavn\", person_id AS \"PersonId\", " +
                $"rolle_id AS \"RolleId\", " +
                $"email AS \"Email\", telefon AS \"Telefon\", kodeord AS \"Kodeord\", fornavn AS \"Fornavn\", efternavn AS \"Efternavn\", " +
                $"fødselsdag::text AS \"Fødselsdag\" FROM fuld_person_view_3 WHERE email = '{email}' AND kodeord = crypt('{kode}', kodeord);";
        
            var person = await Context.Connection.QueryAsync<PersonDTO>(sql);
            return person;
        }
    }
}

