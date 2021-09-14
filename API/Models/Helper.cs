using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Helper 
    {
        private readonly CovidHttpClient _data;
        public Info GetDetails (string country)
        {
            Info results = _data.GetInfo(country);
            return results;
       }
    }
}
