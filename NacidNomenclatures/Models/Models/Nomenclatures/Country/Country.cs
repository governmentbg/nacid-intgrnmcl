using Models.Interfaces;
using Models.Models.Nomenclatures.Base;
using System.Linq;

namespace Models.Models.Nomenclatures.Country
{
    public class Country: NomenclatureCode, IIncludeAll<Country>
    {
        public IQueryable<Country> IncludeAll(IQueryable<Country> query)
        {
            return query;
        }
    }
}
