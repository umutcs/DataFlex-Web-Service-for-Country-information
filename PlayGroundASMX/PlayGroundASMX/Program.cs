using System;
using System.Threading.Tasks;
using PlayGroundASMX;
using ServiceReference1;

class Program
{
    static async Task Main(string[] args)
    {
        string countryName = "Turkey";
        string countryCode = GetCountryCode(countryName);
        Console.WriteLine("Country Code: " + countryCode);
        string countryName2 = "Germany";
        string countryCode2 = GetCountryCode(countryName2);
        CountryInfo countryInfo = await FullInfo(countryCode2);
        Console.WriteLine(countryInfo.sName);
        Console.WriteLine(countryInfo.Capital);
        Console.WriteLine(countryInfo.sCountryFlag);
        Console.WriteLine(countryInfo.sContientCode);
        Console.WriteLine(countryInfo.sCurrencyISOCode);
        Console.WriteLine(countryInfo.sISOCode);
        Console.WriteLine(countryInfo.sPhoneCode);
    }

    static string GetCountryCode(string countryName)
    {
        using (CountryInfoServiceSoapTypeClient client = new CountryInfoServiceSoapTypeClient(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap))
        {
            var response = client.CountryISOCodeAsync(countryName);
            return response.Result.Body.CountryISOCodeResult;
        }
    }

    static async Task<CountryInfo> FullInfo(string countryCode)
    {
        using (CountryInfoServiceSoapTypeClient client = new CountryInfoServiceSoapTypeClient(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap))
        {
            var response = client.FullCountryInfoAsync(countryCode);
            CountryInfo info = new CountryInfo
            {
                Capital = response.Result.Body.FullCountryInfoResult.sCapitalCity,
                sContientCode = response.Result.Body.FullCountryInfoResult.sContinentCode,
                sCountryFlag = response.Result.Body.FullCountryInfoResult.sCountryFlag,
                sCurrencyISOCode = response.Result.Body.FullCountryInfoResult.sCurrencyISOCode,
                sISOCode = response.Result.Body.FullCountryInfoResult.sISOCode,
                sName = response.Result.Body.FullCountryInfoResult.sName,
                sPhoneCode = response.Result.Body.FullCountryInfoResult.sPhoneCode
            };
            Console.WriteLine("Data about country ");
            return info;
        }
    }
}
