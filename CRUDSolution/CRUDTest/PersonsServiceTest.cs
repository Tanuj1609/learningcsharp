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

namespace CRUDTest
{
    public class PersonsServiceTest
    {   //private field
        private readonly IPersonsService _personsService;
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _testOutputHelper;

        //constructor
        public PersonsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _personsService = new PersonsService();
            _countriesService = new CountriesService(false);
            _testOutputHelper = testOutputHelper;
        }

        #region AddPerson
        //when we supply null value as PersonAddRequest it should throw ArgumentNullExeception
        [Fact]
        public void Addperson_PersonNameIsNull()
        {
            //Arrange
            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = null
            };

            //Act
            Assert.Throws<ArgumentException>(() => { _personsService.AddPerson(personAddRequest); });
        }



        //when we supply proper person details it should insert the person into the persons list
        // and it should return an object of PersonResponse, which includes the newly generated person id

        [Fact]
        public void Addperson_ProperPersonDetails()
        {
            //Arrange
            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "Person",
                Email = "person@example.com",
                Address = "sample address",
                CountryID = Guid.NewGuid(),
                Gender = GenderOptions.Male.ToString(),
                DateOfBirth = DateTime.Parse("2000-01-01"),
                RecieveNewsLetters = true
            };

            //Act
            PersonResponse person_response_from_add = _personsService.AddPerson(personAddRequest);
            List<PersonResponse> person_list = _personsService.GetAllPersons();

            //Arrange 
            Assert.True(person_response_from_add.PersonID != Guid.Empty);
            Assert.Contains(person_response_from_add, person_list);
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
            CountryAddRequest country_request = new CountryAddRequest() { CountryName = "Canada" };
            CountryResponse country_response = _countriesService.AddCountry(country_request);


            //Act
            PersonAddRequest person_request = new PersonAddRequest()
            {
                PersonName = "person name....",
                Email = "email@sample.com",
                Address = "address",
                CountryID = country_response.CountryID,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                Gender = GenderOptions.Male.ToString(),
                RecieveNewsLetters = false
            };

            //Expected result
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
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse countru_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse countru_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "Mohit",
                Email = "something@example.com",
                Gender = GenderOptions.Male.ToString(),
                Address = "Address of mohit",
                CountryID = countru_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2002-05-07"),
                RecieveNewsLetters = true
            };

            PersonAddRequest person_request_2 = new PersonAddRequest()
            {
                PersonName = "Mary",
                Email = "Mary@example.com",
                Gender = GenderOptions.Female.ToString(),
                Address = "Address of Mary",
                CountryID = countru_response_2.CountryID,
                DateOfBirth = DateTime.Parse("2000-02-02"),
                RecieveNewsLetters = false
            };

            PersonAddRequest person_request_3 = new PersonAddRequest()
            {
                PersonName = "rahman",
                Email = "rahman@example.com",
                Gender = GenderOptions.Male.ToString(),
                Address = "Address of rahman",
                CountryID = countru_response_2.CountryID,
                DateOfBirth = DateTime.Parse("1999-03-03"),
                RecieveNewsLetters = true
            };

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
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse countru_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse countru_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "Mohit",
                Email = "something@example.com",
                Gender = GenderOptions.Male.ToString(),
                Address = "Address of mohit",
                CountryID = countru_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2002-05-07"),
                RecieveNewsLetters = true
            };

            PersonAddRequest person_request_2 = new PersonAddRequest()
            {
                PersonName = "Mary",
                Email = "Mary@example.com",
                Gender = GenderOptions.Female.ToString(),
                Address = "Address of Mary",
                CountryID = countru_response_2.CountryID,
                DateOfBirth = DateTime.Parse("2000-02-02"),
                RecieveNewsLetters = false
            };

            PersonAddRequest person_request_3 = new PersonAddRequest()
            {
                PersonName = "rahman",
                Email = "rahman@example.com",
                Gender = GenderOptions.Male.ToString(),
                Address = "Address of rahman",
                CountryID = countru_response_2.CountryID,
                DateOfBirth = DateTime.Parse("1999-03-03"),
                RecieveNewsLetters = true
            };

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
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse countru_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse countru_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "Mohit",
                Email = "something@example.com",
                Gender = GenderOptions.Male.ToString(),
                Address = "Address of mohit",
                CountryID = countru_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2002-05-07"),
                RecieveNewsLetters = true
            };

            PersonAddRequest person_request_2 = new PersonAddRequest()
            {
                PersonName = "Mary",
                Email = "Mary@example.com",
                Gender = GenderOptions.Female.ToString(),
                Address = "Address of Mary",
                CountryID = countru_response_2.CountryID,
                DateOfBirth = DateTime.Parse("2000-02-02"),
                RecieveNewsLetters = false
            };

            PersonAddRequest person_request_3 = new PersonAddRequest()
            {
                PersonName = "rahman",
                Email = "rahman@example.com",
                Gender = GenderOptions.Male.ToString(),
                Address = "Address of rahman",
                CountryID = countru_response_2.CountryID,
                DateOfBirth = DateTime.Parse("1999-03-03"),
                RecieveNewsLetters = true
            };

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
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse countru_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse countru_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "Mohit",
                Email = "something@example.com",
                Gender = GenderOptions.Male.ToString(),
                Address = "Address of mohit",
                CountryID = countru_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2002-05-07"),
                RecieveNewsLetters = true
            };

            PersonAddRequest person_request_2 = new PersonAddRequest()
            {
                PersonName = "Mary",
                Email = "Mary@example.com",
                Gender = GenderOptions.Female.ToString(),
                Address = "Address of Mary",
                CountryID = countru_response_2.CountryID,
                DateOfBirth = DateTime.Parse("2000-02-02"),
                RecieveNewsLetters = false
            };

            PersonAddRequest person_request_3 = new PersonAddRequest()
            {
                PersonName = "rahman",
                Email = "rahman@example.com",
                Gender = GenderOptions.Male.ToString(),
                Address = "Address of rahman",
                CountryID = countru_response_2.CountryID,
                DateOfBirth = DateTime.Parse("1999-03-03"),
                RecieveNewsLetters = true
            };

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
            List<PersonResponse> persons_list_from_sort = _personsService.GetSortedPersons(allPersons,nameof(Person.PersonName), SortOrderOptions.DESC);

            //print person_response_list_from_get
            _testOutputHelper.WriteLine("Actual:");
            foreach (PersonResponse person_response_from_get in persons_list_from_sort)
            {
                _testOutputHelper.WriteLine(person_response_from_get.ToString());
            }

            person_response_list_from_add = person_response_list_from_add.OrderByDescending(temp => temp.PersonName).ToList();

            //Assert
            for(int i=0; i< person_response_list_from_add.Count; i++)
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
            PersonUpdateRequest? person_update_request = new PersonUpdateRequest()
            {
                PersonID = Guid.NewGuid(),
            };

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
            CountryAddRequest country_add_request = new CountryAddRequest() { CountryName = "UK" };
            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

            PersonAddRequest person_add_request = new PersonAddRequest()
            {
                PersonName = "John",
                CountryID = country_response_from_add.CountryID,
                Email = "john@sample.com"
            };

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
            CountryAddRequest country_add_request = new CountryAddRequest() { CountryName = "UK" };
            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

            PersonAddRequest person_add_request = new PersonAddRequest()
            {
                PersonName = "John",
                CountryID = country_response_from_add.CountryID,
                Email ="john@example.com"
            };

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
            CountryAddRequest country_add_request = new CountryAddRequest()
            {
                CountryName = "USA"
            };

            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

            PersonAddRequest person_add_request = new PersonAddRequest()
            {
                PersonName = "Jones",
                Address = "Address",
                CountryID = country_response_from_add.CountryID,
                DateOfBirth = Convert.ToDateTime("2010-01-01"),
                Email = "jones@example.com",
                Gender = GenderOptions.Male.ToString(),
                RecieveNewsLetters = true,
            };
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
    
