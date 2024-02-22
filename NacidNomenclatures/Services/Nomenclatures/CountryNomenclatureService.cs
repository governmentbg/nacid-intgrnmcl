using Models;
using Models.Models.Nomenclatures.Country;
using Services.Nomenclatures.Base;

namespace Services.Nomenclatures
{
    public class CountryNomenclatureService : BaseNomenclatureService<Country>
    {
        public CountryNomenclatureService(
            NomenclaturesDbContext context
        ) : base(context)
        {
        }
    }

    public class SettlementNomenclatureService : BaseNomenclatureService<Settlement>
    {
        public SettlementNomenclatureService(
            NomenclaturesDbContext context
        ) : base(context)
        {
        }
    }

    public class DistrictNomenclatureService : BaseNomenclatureService<District>
    {
        public DistrictNomenclatureService(
            NomenclaturesDbContext context
        ) : base(context)
        {
        }
    }

    public class MunicipalityNomenclatureService : BaseNomenclatureService<Municipality>
    {
        public MunicipalityNomenclatureService(
            NomenclaturesDbContext context
        ) : base(context)
        {
        }
    }
}
