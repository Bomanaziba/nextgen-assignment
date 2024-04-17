

using Microsoft.AspNetCore.Mvc.Rendering;
using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Models
{
    public sealed class CalculatorViewModel
    {

        public IList<PostalCode> PostalCodes { get; set; }

        public IList<SelectListItem> PostalCodesDropDown { get; set; }

        public string PostalCode { get; set; }

        public decimal Income { get; set; }
    }
}