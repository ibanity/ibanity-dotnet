using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.XS2A.Models
{
    /// <summary>
    /// This endpoint provides a list of the unique countries for which there are financial institutions available in the list financial institutions endpoint. These codes can be used to filter the financial institutions by country.
    /// </summary>
    [DataContract]
    public class FinancialInstitutionCountry : Identified<string> { }
}
