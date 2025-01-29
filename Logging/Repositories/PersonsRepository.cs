using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Entities;
using RepositoryContracts;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace Repositories
{
    public class PersonsRepository : IPersonsRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<PersonsRepository> _logger;

        public PersonsRepository(ApplicationDbContext db, ILogger<PersonsRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public Person AddPerson(Person person)
        {
            _db.Persons.Add(person);
             _db.SaveChangesAsync();

            return person;
        }

        public bool DeletePersonByPersonID(Guid personID)
        {
            _db.Persons.RemoveRange(_db.Persons.Where(temp => temp.PersonID == personID));
            int rowsDeleted = _db.SaveChanges();

            return rowsDeleted > 0;
        }

        public List<Person> GetAllPersons()
        {
            return _db.Persons.Include("Country").ToList();
        }

        public List<Person> GetFilteredPersons(Expression<Func<Person, bool>> predicate)
        {
            _logger.LogInformation("GetFilteredPersons of PersonsRepository");
            return  _db.Persons.Include("Country")
             .Where(predicate)
             .ToList();
        }

        public List<Person> GetPersonById(Guid personid)
        {
            throw new NotImplementedException();
        }

        public Person? GetPersonByPersonID(Guid personID)
        {
            return _db.Persons.Include("Country")
             .FirstOrDefault(temp => temp.PersonID == personID);
        }

        public Person UpdatePerson(Person person)
        {
            Person? matchingPerson = _db.Persons.FirstOrDefault(temp => temp.PersonID == person.PersonID);

            if (matchingPerson == null)
                return person;

            matchingPerson.PersonName = person.PersonName;
            matchingPerson.Email = person.Email;
            matchingPerson.DateOfBirth = person.DateOfBirth;
            matchingPerson.Gender = person.Gender;
            matchingPerson.CountryID = person.CountryID;
            matchingPerson.Address = person.Address;
            matchingPerson.RecieveNewsLetters = person.RecieveNewsLetters;

            int countUpdated =_db.SaveChanges();

            return matchingPerson;
        }

        List<Person> IPersonsRepository.GetPersonByPersonID(Guid personid)
        {
            throw new NotImplementedException();
        }
    }
}