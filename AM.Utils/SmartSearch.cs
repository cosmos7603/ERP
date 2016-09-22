using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AM.Utils
{
    public static class SmartSearch
    {
        public static SmartSearchParameters ParseParametersFromSearchString(string searchString)
        {
            var termsList = searchString.Split(' ');

            var parameters = new SmartSearchParameters();

            var accumulatedSearchString = string.Empty;
            var currentToken = string.Empty;

            termsList.ToList().ForEach((x) =>
            {
                int searchNumber = 0;
                var isNumber = Int32.TryParse(x, out searchNumber);

                if (x.StartsWith(SmartSearchToken.Address) || (!x.IsSearchToken() && currentToken == SmartSearchToken.Address))
                {
                    currentToken = SmartSearchToken.Address;
                    var stringWithoutToken = x.Replace(SmartSearchToken.Address, "");
                    if (string.IsNullOrWhiteSpace(parameters.HomeAddress))
                        parameters.HomeAddress = stringWithoutToken;
                    else if(!string.IsNullOrWhiteSpace(stringWithoutToken))
                        parameters.HomeAddress = parameters.HomeAddress + " " + stringWithoutToken;
                }
                else if (x.StartsWith(SmartSearchToken.City) || (!x.IsSearchToken() && currentToken == SmartSearchToken.City))
                {
                    currentToken = SmartSearchToken.City;
                    var stringWithoutToken = x.Replace(SmartSearchToken.City, "");
                    if (string.IsNullOrWhiteSpace(parameters.City))
                        parameters.City = stringWithoutToken;
                    else if (!string.IsNullOrWhiteSpace(stringWithoutToken))
                        parameters.City = parameters.City + " " + stringWithoutToken;
                }
                else if (x.StartsWith(SmartSearchToken.Zip) || (!x.IsSearchToken() && currentToken == SmartSearchToken.Zip))
                {
                    currentToken = SmartSearchToken.Zip;
                    var stringWithoutToken = x.Replace(SmartSearchToken.Zip, "");
                    if (string.IsNullOrWhiteSpace(parameters.Zip))
                        parameters.Zip = stringWithoutToken;
                    else if (!string.IsNullOrWhiteSpace(stringWithoutToken))
                        parameters.Zip = parameters.Zip + " " + stringWithoutToken;
                }
                else if (x.StartsWith(SmartSearchToken.Street) || (!x.IsSearchToken() && currentToken == SmartSearchToken.Street))
                {
                    currentToken = SmartSearchToken.Street;
                    var stringWithoutToken = x.Replace(SmartSearchToken.Street, "");
                    if (string.IsNullOrWhiteSpace(parameters.State))
                        parameters.State = stringWithoutToken;
                    else if (!string.IsNullOrWhiteSpace(stringWithoutToken))
                        parameters.State = parameters.State + " " + stringWithoutToken;
                }
                else if (x.StartsWith("(") || (!x.IsSearchToken() && currentToken == "phon")){
                    currentToken = "phon";
                    if (parameters.PhoneNumber == string.Empty)
                    {
                        if(x.EndsWith(")") && x.Length <= 5)
                        {
                            parameters.PhoneNumber = x.Replace("(", "").Replace(")", "").Replace("-", "");
                        }
                        else
                        {
                            var stringOnlyNumbers = x.Replace("(", "").Replace(")", "").Replace("-", "");
                            parameters.PhoneNumber = Regex.Replace(stringOnlyNumbers, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
                            currentToken = string.Empty;
                        }
                    }
                    else
                    {
                        var stringOnlyNumbers = parameters.PhoneNumber + x.Replace("(", "").Replace(")", "").Replace("-", "");
                        parameters.PhoneNumber = Regex.Replace(stringOnlyNumbers, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
                        currentToken = string.Empty;
                    }
                }
                else if (isNumber)
                {
                    if (parameters.Id == 0)
                        parameters.Id = searchNumber;
                    if (string.IsNullOrWhiteSpace(parameters.SearchString))
                        parameters.SearchString = x;
                    else
                        parameters.SearchString = parameters.SearchString + " " + x;
                    parameters.ConfirmationNumber = x;
                }
                else
                {                  
                    if (x.Contains("/"))
                    {
                        DateTime date;
                        if (DateTime.TryParse(x, out date))
                            parameters.SearchDate = date;
                    }
                    else if (x.Contains("@"))
                        parameters.Email = x;
                    else if (x.Contains("-"))
                        parameters.PhoneNumber = x.Replace("(", "").Replace(")", "").Replace("-", "");
                    else if (!x.Any(char.IsDigit))
                    {
                        if (string.IsNullOrWhiteSpace(parameters.SearchString))
                            parameters.SearchString = x;
                        else
                            parameters.SearchString = parameters.SearchString + " " + x;
                        //If the term contains no numbers and last name has not been set then set last name
                        if (string.IsNullOrWhiteSpace(parameters.LastName))
                        {
                            parameters.LastName = x;
                        }
                        //If the term contains no numbers and last name has been set then the current string is
                        //the last name, and the previous string guessed as last name is really the first name.
                        else
                        {
                            parameters.FirstName = parameters.LastName;
                            parameters.LastName = x;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(parameters.SearchString))
                            parameters.SearchString = x;
                        else
                            parameters.SearchString = parameters.SearchString + " " + x;
                        parameters.ConfirmationNumber = x;
                    }
                }
            }
            );
            return parameters;
        }

        public static string AppendZip(string searchString, string searchZip)
        {
            if (string.IsNullOrWhiteSpace(searchZip))
                return searchString;
            if (!string.IsNullOrWhiteSpace(searchString))
                return searchString + " " + SmartSearchToken.Zip + searchZip;
            else
                return searchString + SmartSearchToken.Zip + searchZip;
        }

        public static string AppendCity(string searchString, string searchCity)
        {
            if (string.IsNullOrWhiteSpace(searchCity))
                return searchString;
            if (!string.IsNullOrWhiteSpace(searchString))
                return searchString + " " + SmartSearchToken.City + searchCity;
            else
                return searchString + SmartSearchToken.City + searchCity;
        }

        public static string AppendAddress(string searchString, string searchAddress)
        {
            if (string.IsNullOrWhiteSpace(searchAddress))
                return searchString;
            if (!string.IsNullOrWhiteSpace(searchString))
                return searchString + " " + SmartSearchToken.Address + searchAddress;
            else
                return searchString + SmartSearchToken.Address + searchAddress;
        }

        public static string AppendState(string searchString, string searchState)
        {
            if (string.IsNullOrWhiteSpace(searchState))
                return searchString;
            if (!string.IsNullOrWhiteSpace(searchString))
                return searchString + " " + SmartSearchToken.Street + searchState;
            else
                return searchString + SmartSearchToken.Street + searchState;
        }

        public static string RemoveTokens(string searchString)
        {
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                List<int> indexes = new List<int>();

                indexes.Add(searchString.IndexOf(SmartSearchToken.Address));
                indexes.Add(searchString.IndexOf(SmartSearchToken.City));
                indexes.Add(searchString.IndexOf(SmartSearchToken.Street));
                indexes.Add(searchString.IndexOf(SmartSearchToken.Zip));

                //Get of the present indexes, the first appearing in the search string
                var firstTokenIndex = indexes.Where(x => x > 0).DefaultIfEmpty(-1).Min();

                if (firstTokenIndex != -1)
                    return searchString.Substring(0, firstTokenIndex);
            }
            return searchString;
        }
    }
    #region Classes
    public class SmartSearchParameters
    {
        public SmartSearchParameters()
        {
            Id = 0;
            ConfirmationNumber = string.Empty;
            LastName = string.Empty;
            FirstName = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            HomeAddress = string.Empty;
            City = string.Empty;
            State = string.Empty;
            Zip = string.Empty;
            SearchDate = null;
            SearchString = string.Empty;
        }
        public int Id { get; set; }
        public string ConfirmationNumber { get; set; }
        public DateTime? SearchDate { get; set; }
        public string SearchString {get;set;}
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string HomeAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
    #endregion

    #region Extensions
    public static class SmartSearchExtensions
    {
        public static bool IsSearchToken(this string term)
        {
            if (term.StartsWith(SmartSearchToken.Address) || term.StartsWith(SmartSearchToken.City) 
                || term.StartsWith(SmartSearchToken.Zip) || term.StartsWith(SmartSearchToken.Street))
                return true;
            else
                return false;
        }
    }
    #endregion

    #region Enum
    public static class SmartSearchToken
    {
        public static string Address { get { return "addr:"; } }
        public static string City { get { return "city:"; } }
        public static string Zip { get { return "zip:"; } }
        public static string Street { get { return "st:"; } }
    }
    #endregion
}
