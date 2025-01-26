using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts;
using Xunit;
using ServiceContracts.DTO;
using Services;
using ServiceContracts.Enums;
using Xunit.Abstractions;
using Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCoreMock;
using AutoFixture;
using RepositoryContracts;
using Moq;
using FluentAssertions;

namespace CRUDTest
{
    public class PersonsServiceTest
    {   //private field
        private readonly IPersonsService _personsService;
        private readonly ICountriesService _countriesService;
        private readonly IPersonsRepository _personsRepository;
        private readonly Mock<IPersonsRepository> _personRepositoryMock;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;


        //constructor
        public PersonsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _personRepositoryMock = new Mock<IPersonsRepository>();
            _personsRepository = _personRepositoryMock.Object;
             
            var countriesInitialData = new List<Country>() { };
            var personsInitialData = new List<Person>() { };


            //INITIALIZING THE CLASS AND CREATING A MOCK OBJECT OF THE APPLICATIONDBCONTEXT CLASS 
            DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>(new DbContextOptionsBuilder<ApplicationDbContext>().Options);
            var dbcontext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().Options);

            //CREATING A MOCK OBJECT USING THE DBCONTEXT CLASS AND THE DBCONTEXT HAS BEEN MOCKED
            ApplicationDbContext dbContext = dbContextMock.Object;

            //NOW MOCKING THE DBSET, PREVIOUSLY WE MOCKED THE DBCONTEXT
            dbContextMock.CreateDbSetMock(temp => temp.Countries, countriesInitialData);

            dbContextMock.CreateDbSetMock(temp => temp.Persons, personsInitialData);

            _countriesService = new CountriesService(null);
            
            _personsService = new PersonsService(_personsRepository);

            _testOutputHelper = testOutputHelper;
        }

        #region AddPerson
        //when we supply null value as PersonAddRequest it should throw ArgumentNullExeception
        [Fact]

        public void AddPerson_NullPerson()
        {
            //Arrange
            PersonAddRequest? personAddRequest = null;

            Action action = () =>
            {
                 _personsService.AddPerson(personAddRequest);
            };

            //Assert
            action.Should().Throw<ArgumentNullException>();
        }
        

        //When we supply null value as PersonName, it should throw ArgumentException
        [Fact]
        public void Addperson_PersonNameIsNull()
        {
            //Arrange
            PersonAddRequest personAddRequest = _fixture.Build<PersonAddRequest>().With(temp=> temp.PersonName, null as string).Create();

            //Act
            Assert.Throws<ArgumentException>(() => { _personsService.AddPerson(personAddRequest); });
        }



        //when we supply proper person details it should insert the person into the persons list
        // and it should return an object of PersonResponse, which includes the newly generated person id

        [Fact]
        public void Addperson_FullPersonDetails_ToBeSuccessful()
        { 
            //Arrange

            //MAKING THE USE OF FIXTURE FOR CREATING SAMPLE OR DUMMY DATA BY INSTALLING AUTO FIXTURES IN THE NUDGET PACKAGE MANAGER
            //WE CAN USE ONLY CREATE FUNCTION IN FIXTURE BUT IT WILL INTIALIZE THE VALUES IN IMPROPER FORMAT
            //PersonAddRequest personAddRequest = _fixture.Create<PersonAddRequest>();


            //USING THE BUILD AND WITH METHODS INSTEAD OF CREATE ALONE
            // BUILD METHOD DOES THE JOB OF CREATE AND WITH METHOD ALLOWS CUSTOM VALUES
            // ANY NUMBER OF WITH METHODS CAN BE ADDED TO CUSTOMIZE THE VALUES
            //ONLY USE WITH IF CUSTOMIZATION OF VALUES IS NEEDED ELSE NO NEED OF WITH METHOD IN THE FIXTURES
            PersonAddRequest personAddRequest =
                _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "someone@example.com").Create();

            Person person = personAddRequest.ToPerson();
            PersonResponse person_response_expected = person.ToPersonResponse();
            
            //IF WE SUPPLY ANY ARGUMENT VALUE TO THE AddPerson METHOD, IT SHOULD RETURN THE SAME RETURN VALUE
            _personRepositoryMock.Setup(temp => temp.AddPerson(It.IsAny<Person>())).Returns(person);
                
            //Act
            PersonResponse person_response_from_add = _personsService.AddPerson(personAddRequest);

            //Arrange 
            Assert.True(person_response_from_add.PersonID != Guid.Empty);
            person_response_from_add.Should().Be(person_response_expected);
           
        }


        #endregion

        #region GetPersonByPersonID
        //if we supply person id as null then it should return the personResponse as null
        [Fact]

        public void GetPersonByPersonID_NullPersonID()
        {
            //Arrange
            Guid? personID = null;

            //Act
            PersonResponse? person_response_from_get = _personsService.GetPersonByPersonID(personID);

            //Assert
            Assert.Null(person_response_from_get);
        }


        //if we supply a valid person id, it should the valid person details as PersonResponse object

        [Fact]

        public void GetPersonByPersonID_WithPersonID()
        {
            //Arrange
            CountryAddRequest country_request = _fixture.Create<CountryAddRequest>();
            CountryResponse country_response = _countriesService.AddCountry(country_request);


            //Act
            PersonAddRequest person_request = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "email@smail.com").Create();//Expected result
            PersonResponse person_response_from_add = _personsService.AddPerson(person_request);

            //Actual Result
            PersonResponse? person_response_from_get = _personsService.GetPersonByPersonID(person_response_from_add.PersonID);


            //Assert (Comparsion of expected and actual result)
            Assert.Equal(person_response_from_add, person_response_from_get);
        }


        #endregion


        #region GetAllPerson
        //The GetAllPersons() should return an empty list by default
        [Fact]
        public void GetAllPersons_EmptyList()
        {
            //Act
            List<PersonResponse> persons_from_get = _personsService.GetAllPersons();

            //Assert
            Assert.Empty(persons_from_get);
        }

        [Fact]

        public void GetAllPersons_AddFewPersons()
        {
            //Arrange 
            CountryAddRequest country_request_1 = _fixture.Create<CountryAddRequest>();
            CountryAddRequest country_request_2 = _fixture.Create<CountryAddRequest>();

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "sample01@gmail.com").Create();
            PersonAddRequest person_request_2 = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "sample02@gmail.com").Create();
            PersonAddRequest person_request_3 = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "sample03@gmail.com").Create();

            
            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
                person_request_1, person_request_2, person_request_3
            };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personsService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }

            //print person_response_list_from_add
            _testOutputHelper.WriteLine("Expected:");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }

            //Act
            List<PersonResponse> persons_list_from_get = _personsService.GetAllPersons();

            //print person_response_list_from_get
            _testOutputHelper.WriteLine("Actual:");
            foreach (PersonResponse person_response_from_add in persons_list_from_get)
            {
                _testOutputHelper.WriteLine(persons_list_from_get.ToString());
            }

            //Assert
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                Assert.Contains(person_response_from_add, persons_list_from_get);
            }

            #endregion
        }

        #region GetFilteredPersons

        //if the search text is empty and search by is "PersonName", it should return all persons
        [Fact]

        public void GetFilteredPersons_EmptySearchText()
        {
            //Arrange 
            CountryAddRequest country_request_1 = _fixture.Create<CountryAddRequest>();
            CountryAddRequest country_request_2 = _fixture.Create<CountryAddRequest>();

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "sample01@gmail.com").Create();
            PersonAddRequest person_request_2 = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "sample02@gmail.com").Create();
            PersonAddRequest person_request_3 = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "sample03@gmail.com").Create();


            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
                person_request_1, person_request_2, person_request_3
            };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personsService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }

            //print person_response_list_from_add
            _testOutputHelper.WriteLine("Expected:");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }

            //Act
            List<PersonResponse> persons_list_from_search = _personsService.GetFilteredPersons(nameof(Person.PersonName), "");

            //print person_response_list_from_get
            _testOutputHelper.WriteLine("Actual:");
            foreach (PersonResponse person_response_from_add in persons_list_from_search)
            {
                _testOutputHelper.WriteLine(persons_list_from_search.ToString());
            }

            //Assert
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                Assert.Contains(person_response_from_add, persons_list_from_search);
            }
        }


        //First we will add few persons; and then we will search based on person name with some search string.
        //it should return the matching persons

        [Fact]

        public void GetFilteredPersons_SearchByPersonName()
        {
            //Arrange 
            CountryAddRequest country_request_1 = _fixture.Create<CountryAddRequest>();
            CountryAddRequest country_request_2 = _fixture.Create<CountryAddRequest>();

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = _fixture.Build<PersonAddRequest>().With(temp=>temp.PersonName, "Rahman").With(temp => temp.Email, "sample01@gmail.com").With(temp => temp.CountryID, country_response_1.CountryID).Create();
            PersonAddRequest person_request_2 = _fixture.Build<PersonAddRequest>().With(temp => temp.PersonName, "Mary").With(temp => temp.Email, "sample02@gmail.com").With(temp => temp.CountryID, country_response_1.CountryID).Create();
            PersonAddRequest person_request_3 = _fixture.Build<PersonAddRequest>().With(temp => temp.PersonName, "Scott").With(temp => temp.Email, "sample03@gmail.com").With(temp => temp.CountryID, country_response_2.CountryID).Create();


            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
                person_request_1, person_request_2, person_request_3
            };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personsService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }

            //print person_response_list_from_add
            _testOutputHelper.WriteLine("Expected:");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }

            //Act
            List<PersonResponse> persons_list_from_search = _personsService.GetFilteredPersons(nameof(Person.PersonName), "ma");

            //print person_response_list_from_get
            _testOutputHelper.WriteLine("Actual:");
            foreach (PersonResponse person_response_from_add in persons_list_from_search)
            {
                _testOutputHelper.WriteLine(persons_list_from_search.ToString());
            }

            //Assert
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                if (person_response_from_add.PersonName != null)
                {
                    if (person_response_from_add.PersonName.Contains("ma", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(person_response_from_add, persons_list_from_search);
                    }
                }

            }

        }

        #endregion



        #region GetSortedPersons

        //When we sort based on PersonName in DESC, it should return persons List in descending on PersonName 
        [Fact]
        public void GetSortedPersons()
        {
            //Arrange 
            CountryAddRequest country_request_1 = _fixture.Create<CountryAddRequest>();
            CountryAddRequest country_request_2 = _fixture.Create<CountryAddRequest>();

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = _fixture.Build<PersonAddRequest>().With(temp => temp.PersonName, "Smith").With(temp => temp.Email, "sample01@gmail.com").With(temp => temp.CountryID, country_response_1.CountryID).Create();
            PersonAddRequest person_request_2 = _fixture.Build<PersonAddRequest>().With(temp => temp.PersonName, "Mary").With(temp => temp.Email, "sample02@gmail.com").With(temp => temp.CountryID, country_response_1.CountryID).Create();
            PersonAddRequest person_request_3 = _fixture.Build<PersonAddRequest>().With(temp => temp.PersonName, "Rahman").With(temp => temp.Email, "sample03@gmail.com").With(temp => temp.CountryID, country_response_2.CountryID).Create();


            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
                person_request_1, person_request_2, person_request_3
            };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personsService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }

            //print person_response_list_from_add
            _testOutputHelper.WriteLine("Expected:");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }

            List<PersonResponse> allPersons = _personsService.GetAllPersons();
            //Act
            List<PersonResponse> persons_list_from_sort = _personsService.GetSortedPersons(allPersons, nameof(Person.PersonName), SortOrderOptions.DESC);

            //print person_response_list_from_get
            _testOutputHelper.WriteLine("Actual:");
            foreach (PersonResponse person_response_from_get in persons_list_from_sort)
            {
                _testOutputHelper.WriteLine(person_response_from_get.ToString());
            }

            person_response_list_from_add = person_response_list_from_add.OrderByDescending(temp => temp.PersonName).ToList();

            //Assert
            for (int i = 0; i < person_response_list_from_add.Count; i++)
            {
                Assert.Equal(person_response_list_from_add[i], persons_list_from_sort[i]);
            }
        }

        #endregion


        #region UpdatePerson
        //When we supply null as PersonUpdateRequest, it should throw ArgumentNullException
        [Fact]

        public void UpdatePerson_NullPerson()
        {
            //Arrange
            PersonUpdateRequest? person_update_request = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() => {
                //Act
                _personsService.UpdatePerson(person_update_request);
            });


        }



        //When we supply invalid person id, it should throw Argument exception
        [Fact]

        public void UpdatePerson_InvalidPersonID()
        {
            //Arrange
            PersonUpdateRequest? person_update_request = _fixture.Build<PersonUpdateRequest>().Create();

            //Assert
            Assert.Throws<ArgumentException>(() => {
                //Act
                _personsService.UpdatePerson(person_update_request);
            });


        }



        //When the person name is null it should throw argument exception  
        [Fact]

        public void UpdatePerson_PersonNameIsNull()
        {
            //Arrange
            CountryAddRequest country_request = _fixture.Create<CountryAddRequest>();

            CountryResponse country_response = _countriesService.AddCountry(country_request);


            PersonAddRequest person_add_request = _fixture.Build<PersonAddRequest>().With(temp => temp.PersonName, "Rahman").With(temp => temp.Email, "sample01@gmail.com").With(temp => temp.CountryID, country_response.CountryID).Create();

            PersonResponse person_response_from_add = _personsService.AddPerson(person_add_request);
            PersonUpdateRequest? person_update_request = person_response_from_add.ToPersonUpdateRequest();
            person_update_request.PersonName = null;


            //Assert
            Assert.Throws<ArgumentException>(() =>
            {   //Act
                _personsService.UpdatePerson(person_update_request);

            });
        }



        //First we will add a new person then will try to update the person name and email
        [Fact]

        public void UpdatePerson_PersonFullDetails()
        {
            //Arrange
            CountryAddRequest country_request = _fixture.Create<CountryAddRequest>();

            CountryResponse country_response = _countriesService.AddCountry(country_request);


            PersonAddRequest person_add_request = _fixture.Build<PersonAddRequest>().With(temp => temp.PersonName, "Rahman").With(temp => temp.Email, "sample01@gmail.com").With(temp => temp.CountryID, country_response.CountryID).Create();


            PersonResponse person_response_from_add = _personsService.AddPerson(person_add_request);
            PersonUpdateRequest? person_update_request = person_response_from_add.ToPersonUpdateRequest();
            person_update_request.PersonName = "William";

            //Act
            PersonResponse person_response_from_update = _personsService.UpdatePerson(person_update_request);
            PersonResponse? person_response_from_get = _personsService.GetPersonByPersonID(person_response_from_update.PersonID);


            //Assert
            Assert.Equal(person_response_from_get, person_response_from_update);
        }
        #endregion

        #region DeletePerson
        //If you supply a valid person id, it should return true
        [Fact]
        public void DeletePerson_ValidPersonID()
        {
            //Arrange
            CountryAddRequest country_request = _fixture.Create<CountryAddRequest>();

            CountryResponse country_response = _countriesService.AddCountry(country_request);


            PersonAddRequest person_add_request = _fixture.Build<PersonAddRequest>().With(temp => temp.PersonName, "Rahman").With(temp => temp.Email, "sample01@gmail.com").With(temp => temp.CountryID, country_response.CountryID).Create();

            PersonResponse person_response_from_add = _personsService.AddPerson(person_add_request);

            //Act
            bool isDeleted = _personsService.DeletePerson(person_response_from_add.PersonID);

            //Assert
            Assert.True(isDeleted);
        }


        //If you supply a Invalid person id, it should return false
        [Fact]
        public void DeletePerson_InValidPersonID()
        {

            //Act
            bool isDeleted = _personsService.DeletePerson(Guid.NewGuid());

            //Assert
            Assert.False(isDeleted);
        }
        #endregion
    }
}
