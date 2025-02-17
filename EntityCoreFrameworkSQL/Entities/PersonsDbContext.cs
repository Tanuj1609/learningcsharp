using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    //DBCONTEXT IS USED FOR DECLARING A CLASS AS A TABLE USING ENTITYCORE FRAMEWORK
    public class PersonsDbContext : DbContext
    {
        public PersonsDbContext(DbContextOptions options) : base(options) { }
        //
        public DbSet<Person> Persons { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().ToTable("Countries");

            modelBuilder.Entity<Person>().ToTable("Persons");

            //SSEDING TO COUNTRIES
            /*ENTERING MANUAL DATA:-
            modelBuilder.Entity<Country>().HasData(new Country()
            {
                CountryID = Guid.NewGuid(),
                CountryName = "Sample"
            });
            */
            //SEEDING THE DATA BY USING JSON FILE
            //FIRST READ ALL TEXT FROM THE JSON FILE AND ASSIGN IT A VARIABLE
            string countriesJSON = System.IO.File.ReadAllText("Countries.JSON");

            //THEN DESERIALIZE THE JSON READ CONTENT INTO A LIST OF COUNTRY OBJECTS
            List<Country> countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countriesJSON);

            //THEN USE A FOR EACH LOOP FOR MAPPING DATA TO COUNTRIES USING MODELBUILDER
            foreach (Country country in countries)
                modelBuilder.Entity<Country>().HasData(country);


            //FIRST READ ALL TEXT FROM THE JSON FILE AND ASSIGN IT A VARIABLE
            string personsJSON = System.IO.File.ReadAllText("persons.JSON");

            //THEN DESERIALIZE THE JSON READ CONTENT INTO A LIST OF COUNTRY OBJECTS
            List<Person> persons = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(personsJSON);

            //THEN USE A FOR EACH LOOP FOR MAPPING DATA TO COUNTRIES USING MODELBUILDER
            foreach (Person person in persons)
                modelBuilder.Entity<Person>().HasData(person);



        }
        public List<Person> sp_GetAllPersons()
        {
            return Persons.FromSqlRaw("EXECUTE [dbo].[GetAllPersons]").ToList();
        }

        public int sp_InsertPerson(Person person)
        {
            SqlParameter[] parameters = new SqlParameter[] {
        new SqlParameter("@PersonID", person.PersonID),
        new SqlParameter("@PersonName", person.PersonName),
        new SqlParameter("@Email", person.Email),
        new SqlParameter("@DateOfBirth", person.DateOfBirth),
        new SqlParameter("@Gender", person.Gender),
        new SqlParameter("@CountryID", person.CountryID),
        new SqlParameter("@Address", person.Address),
        new SqlParameter("@RecieveNewsLetters", person.RecieveNewsLetters)
             };

            return Database.ExecuteSqlRaw("EXECUTE [dbo].[InsertPerson] @PersonID, @PersonName, @Email, @DateOfBirth, @Gender, @CountryID, @Address, @RecieveNewsLetters", parameters);
        }
    }
}