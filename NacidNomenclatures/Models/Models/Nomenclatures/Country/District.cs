using Models.Interfaces;
using Models.Models.Nomenclatures.Base;
using System.Linq;

namespace Models.Models.Nomenclatures.Country
{
    public class District: NomenclatureCode, IIncludeAll<District>
    {
        public string Code2 { get; set; }
        public string SecondLevelRegionCode { get; set; }
        public string MainSettlementCode { get; set; }

        public IQueryable<District> IncludeAll(IQueryable<District> query)
        {
            return query;
        }
    }
}
