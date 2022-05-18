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
    public class PersonRepositoryDapper
    {
        private string connString = "User ID=postgres;Password=1234;Host=localhost;Port=5432;Database=ProjMgr;";
        private string sql = "";

        public async void OpretPerson(PersonDTO NyPerson)
        {
            sql = $"CALL opret_person({NyPerson.RolleId}, {NyPerson.Email}, {NyPerson.Telefon}, {NyPerson.Kodeord}, {NyPerson.Fornavn}, {NyPerson.Efternavn}, {NyPerson.Fødselsdag}, {NyPerson.KompetenceId}; )";
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

