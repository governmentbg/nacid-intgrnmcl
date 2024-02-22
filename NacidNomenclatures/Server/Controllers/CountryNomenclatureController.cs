using MessageBroker.Enums;
using MessageBroker.Publisher;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Models.Nomenclatures.Country;
using Server.Controllers.Base;
using Services.Nomenclatures;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/Nomenclature/Country")]
    public class CountryNomenclatureController : BaseNomenclatureController<Country, CountryNomenclatureService>
    {
        public CountryNomenclatureController(
            CountryNomenclatureService countryNomenclatureService,
            NomenclaturesDbContext context,
            NomenclaturesMbPublisher nomenclaturesMbPublisher
            )
            : base(countryNomenclatureService, context, nomenclaturesMbPublisher, NomenclatureType.Country)
        {
        }
    }

    [ApiController]
    [Route("api/Nomenclature/Settlement")]
    public class SettlementNomenclatureController : BaseNomenclatureController<Settlement, SettlementNomenclatureService>
    {
        public SettlementNomenclatureController(
            SettlementNomenclatureService settlementNomenclatureService,
            NomenclaturesDbContext context,
            NomenclaturesMbPublisher nomenclaturesMbPublisher
            )
            : base(settlementNomenclatureService, context, nomenclaturesMbPublisher, NomenclatureType.Settlement)
        {
        }
    }

    [ApiController]
    [Route("api/Nomenclature/District")]
    public class DistrictNomenclatureController : BaseNomenclatureController<District, DistrictNomenclatureService>
    {
        public DistrictNomenclatureController(
            DistrictNomenclatureService districtNomenclatureService,
            NomenclaturesDbContext context,
            NomenclaturesMbPublisher nomenclaturesMbPublisher
            )
            : base(districtNomenclatureService, context, nomenclaturesMbPublisher, NomenclatureType.District)
        {
        }
    }

    [ApiController]
    [Route("api/Nomenclature/Municipality")]
    public class MunicipalityNomenclatureController : BaseNomenclatureController<Municipality, MunicipalityNomenclatureService>
    {
        public MunicipalityNomenclatureController(
            MunicipalityNomenclatureService municipalityNomenclatureService,
            NomenclaturesDbContext context,
            NomenclaturesMbPublisher nomenclaturesMbPublisher
            )
            : base(municipalityNomenclatureService, context, nomenclaturesMbPublisher, NomenclatureType.Municipality)
        {
        }
    }
}
