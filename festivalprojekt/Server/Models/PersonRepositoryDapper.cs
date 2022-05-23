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
    public class PersonRepositoryDapper : IPersonRepositoryDapper
    {
        private string connString = "User ID=postgres;Password=godtkodeord ;Host=localhost;Port=5432;Database=miliøguderne;";
        private string sql = "";
      


        public async Task<IEnumerable<PersonDTO>> HentAllePersoner()
        {
            //laver sql statement til query (postgres). Det er vigtigt med AS fordi eller kan dapper ikke matche til klassens navne automatisk.
            sql = "SELECT kompetence_id AS KompetenceId, kompetence_navn AS KompetenceNavn, person_id AS PersonId, rolle_id AS RolleId, email AS Email, telefon AS Telefon, kodeord AS Kodeord, fornavn AS Fornavn, efternavn AS Efternavn, fødselsdag AS Fødeselsdag FROM fuld_person_view_3;";

            //try catch, hvis det ikke virker går den til catch
            try
            {
                //dapper syntax for at lave en ny connection til databasen
                using (var connection = new NpgsqlConnection(connString))
                {
                    //laver variabel og fylder data fra query ind i listen.
                    //Vi specificerer hvilken type data der forventes(PersonDTO) og hvis navnene fra databasens kolonner
                    //matcher navnene i klassens properties
                    //laver den automatisk rigtige udfyldte objekter.
                    var PersonListe = await connection.QueryAsync<PersonDTO>(sql);

                    return PersonListe.ToList();
                }
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
            sql = $"SELECT kompetence_id AS KompetenceId, kompetence_navn AS KompetenceNavn, person_id AS PersonId, rolle_id AS RolleId, email AS Email, telefon AS Telefon, kodeord AS Kodeord, fornavn AS Fornavn, efternavn AS Efternavn, fødselsdag AS Fødeselsdag FROM fuld_person_view_3 WHERE person_id = {PersonId};";
          
            //try catch, hvis det ikke virker går den til catch
            try
            {
                //dapper syntax for at lave en ny connection til databasen
                using (var connection = new NpgsqlConnection(connString))
                {
                    //laver variabel og fylder data fra query ind i listen.
                    //Vi specificerer hvilken type data der forventes(PersonDTO) og hvis navnene fra databasens kolonner
                    //matcher navnene i klassens properties
                    //laver den automatisk rigtige udfyldte objekter.
                    var Person = await connection.QueryAsync<PersonDTO>(sql);

                    return Person.ToList();
                }
            }
            catch (NotImplementedException)
            {
                //hvis den ikke kan returne VagtTypeListe returnere den en tom liste
                return new List<PersonDTO>();
            }
        }


        public async void OpretPerson(PersonDTO NyPerson)
        {
            sql = $"CALL opret_person(ARRAY[{NyPerson.KompetenceId}], {NyPerson.PersonId} ,{NyPerson.RolleId}, {NyPerson.Email}, {NyPerson.Telefon}, {NyPerson.Kodeord}, {NyPerson.Fornavn}, {NyPerson.Efternavn}, {NyPerson.Fødselsdag};)";
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
        public async void OpdaterPerson(PersonDTO NyPerson) {

            string arr = "";
            int tal = 0;
            foreach (var item in NyPerson.KompetenceId)
            {
                if (tal==0)
                {
                    arr += item + "";
                }
                arr += "," + item;
                tal++;
            }
            Console.WriteLine("debug array: " + arr);
            sql = $"CALL opdater_person(ARRAY[{arr}], {NyPerson.PersonId} ,{NyPerson.RolleId}, '{NyPerson.Email}', '{NyPerson.Telefon}', '{NyPerson.Kodeord}', '{NyPerson.Fornavn}', '{NyPerson.Efternavn}', '{NyPerson.Fødselsdag.ToString("yyyy-MM-dd HH:mm:ss")}');";
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

