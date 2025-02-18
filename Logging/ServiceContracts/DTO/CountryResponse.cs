﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceContracts.DTO
{/// <summary>
/// DTO class that is used as return type for most of the CountryService methods
/// </summary>
    public class CountryResponse
    {
        public Guid CountryID { get; set; }
        public string? CountryName { get; set; }


        //it compares the current object to another object of CountryResponse type and return true, if
        // both values are same; otherwise returns false
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if(obj.GetType() != typeof(CountryResponse))
            {
                return false;
            }

            CountryResponse country_to_compare = (CountryResponse) obj as CountryResponse;
            return CountryID == country_to_compare.CountryID && 
                CountryName == country_to_compare.CountryName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class CountryExtensions
    {
        // converts from country object to countryResponse object
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse() { CountryID = country.CountryID, CountryName = country.CountryName };
        }
    }
}
