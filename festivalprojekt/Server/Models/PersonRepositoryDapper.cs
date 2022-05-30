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
    public class PersonRepositoryDapper : IPersonRepositoryDapper
    {

        private string sql = "";
        private dBContext Context;

        public PersonRepositoryDapper(dBContext context)
        {
            this.Context = context;
        }

        public async Task<IEnumerable<Roller>> HentAlleRoller()
        {
            sql = $"SELECT rolle_id AS \"RolleId\", rolle_navn AS \"RolleNavn\" FROM roller";

            var RolleListe = await Context.Connection.QueryAsync<Roller>(sql);
            return RolleListe.ToList();
        }



        public async Task<IEnumerable<Kompetencer>> HentAlleKompetencer()
        {
            sql = $"SELECT kompetence_id AS \"KompetenceId\", kompetence_navn AS \"KompetenceNavn\" FROM kompetencer";

            var KompetenceListe = await Context.Connection.QueryAsync<Kompetencer>(sql);
            return KompetenceListe.ToList();
        }
        public async Task<IEnumerable<PersonDTO>> HentAllePersoner()
        {
            //laver sql statement til query (postgres). Det er vigtigt med AS fordi eller kan dapper ikke matche til klassens navne automatisk.
            sql = $"SELECT kompetence_id AS \"KompetenceId\", kompetence_navn AS \"KompetenceNavn\", person_id AS \"PersonId\", rolle_id AS \"RolleId\", email AS \"Email\", telefon AS \"Telefon\", kodeord AS \"Kodeord\", fornavn AS \"Fornavn\", efternavn AS \"Efternavn\", fødselsdag::text AS \"Fødselsdag\" FROM fuld_person_view_3;";

            //try catch, hvis det ikke virker går den til catch
            try
            {

                var PersonListe = await Context.Connection.QueryAsync<PersonDTO>(sql);

                return PersonListe.ToList();

            }
            catch (NotImplementedException)
            {
                //hvis den ikke kan returne VagtTypeListe returnere den en tom liste
                return new List<PersonDTO>();
            }
        }
        public async Task<IEnumerable<PersonDTO>> HentPerson(int PersonId)
        {
            //laver sql statement til query (postgres). Det er vigtigt med AS fordi eller kan dapper ikke matche til klassens navne automatisk.
            sql = $"SELECT kompetence_id AS \"KompetenceId\", kompetence_navn AS \"KompetenceNavn\", person_id AS \"PersonId\", rolle_id AS \"RolleId\", email AS \"Email\", telefon AS \"Telefon\", kodeord AS \"Kodeord\", fornavn AS \"Fornavn\", efternavn AS \"Efternavn\", fødselsdag::text AS \"Fødselsdag\" FROM fuld_person_view_3 WHERE person_id = {PersonId};";


            //try catch, hvis det ikke virker går den til catch
            try
            {




                var Person = await Context.Connection.QueryAsync<PersonDTO>(sql);

                return Person.ToList();

            }
            catch (NotImplementedException)
            {
                //hvis den ikke kan returne VagtTypeListe returnere den en tom liste
                return new List<PersonDTO>();
            }
        }


        public async void OpretPerson(PersonDTO NyPerson)
        {
            string arr = "";
            int tal = 0;
            if (NyPerson.KompetenceId.Length == 0)
            {
                arr = "1";
                Console.WriteLine("ingen kompetencer");
            }
            else
            {


                foreach (var item in NyPerson.KompetenceId)
                {
                    if (tal == 0)
                    {
                       
                        arr += item + "";
                        Console.WriteLine("debug tal 1: " + arr);

                        tal++;
                    }
                    else
                    {
                        
                        arr += "," + item;
                        Console.WriteLine("debug enter tal 2: " + arr);
                        tal++;
                    }

                }
            }
            Console.WriteLine("repo ramt");

            DynamicParameters dp = new DynamicParameters();
            dp.Add("StartTid", NyPerson.PersonId);
            dp.Add("SlutTid", NyPerson.RolleId);
            dp.Add("VagtType", NyPerson.Email);
            dp.Add("PersonId", NyPerson.Telefon);
            dp.Add("PersonId", NyPerson.Kodeord);
            dp.Add("PersonId", NyPerson.Fornavn);
            dp.Add("PersonId", NyPerson.Efternavn);
            dp.Add("PersonId", NyPerson.RealF);

            sql = $"CALL opret_person(ARRAY[{arr}],(@Personid, @RolleId, @Email, @Telefon, @Kodeord, @Fornavn, @Efternavn, @RealF)";

           // sql = $"CALL opret_person(ARRAY[{arr}],{NyPerson.PersonId},{NyPerson.RolleId},'{NyPerson.Email}','{NyPerson.Telefon}','{NyPerson.Kodeord}','{NyPerson.Fornavn}','{NyPerson.Efternavn}','{NyPerson.RealF}');";
            try
            {

                await Context.Connection.ExecuteAsync(sql, dp);

            }
            catch (NotImplementedException)
            {
                ;
            }
        }
        public async void OpdaterPerson(PersonDTO NyPerson)
        {

            string arr = "";
            int tal = 0;
            foreach (var item in NyPerson.KompetenceId)
            {
                if (tal == 0)
                {
                    Console.WriteLine("debug tal 1: " + arr);
                    arr += item + "";
                    tal++;
                }
                else
                {
                    Console.WriteLine("debug enter tal 2: " + arr);
                    arr += "," + item;
                    tal++;
                }

            }
            Console.WriteLine("debug array: " + arr);

            DynamicParameters dp = new DynamicParameters();
            dp.Add("StartTid", NyPerson.PersonId);
            dp.Add("SlutTid", NyPerson.RolleId);
            dp.Add("VagtType", NyPerson.Email);
            dp.Add("PersonId", NyPerson.Telefon);
            dp.Add("PersonId", NyPerson.Kodeord);
            dp.Add("PersonId", NyPerson.Fornavn);
            dp.Add("PersonId", NyPerson.Efternavn);
            dp.Add("PersonId", NyPerson.RealF);

            sql = $"CALL opret_person(ARRAY[{arr}],(@Personid, @RolleId, @Email, @Telefon, @Kodeord, @Fornavn, @Efternavn, @RealF)";

          //  sql = $"CALL opdater_person(ARRAY[{arr}], {NyPerson.PersonId} ,{NyPerson.RolleId}, '{NyPerson.Email}', '{NyPerson.Telefon}', '{NyPerson.Kodeord}', '{NyPerson.Fornavn}', '{NyPerson.Efternavn}', '{NyPerson.RealF/*ToString("yyyy-MM-dd HH:mm:ss")*/}');";
            try
            {

                await Context.Connection.ExecuteAsync(sql, dp);

            }
            catch (NotImplementedException)
            {
                ;
            }
        }
        public async Task<IEnumerable<PersonDTO>> Login(string email, string kode)
        {
            sql = $"SELECT kompetence_id AS \"KompetenceId\", kompetence_navn AS \"KompetenceNavn\", person_id AS \"PersonId\", rolle_id AS \"RolleId\", email AS \"Email\", telefon AS \"Telefon\", kodeord AS \"Kodeord\", fornavn AS \"Fornavn\", efternavn AS \"Efternavn\", fødselsdag::text AS \"Fødselsdag\" FROM fuld_person_view_3 WHERE email = '{email}' AND kodeord = crypt('{kode}', kodeord);";
            try
            {



                var person = await Context.Connection.QueryAsync<PersonDTO>(sql);
                return person;
            }
            catch (SystemException)
            {
                //hvis den ikke kan returne VagtTypeListe returnere den en tom liste
                throw;
            }


        }
    }
}

