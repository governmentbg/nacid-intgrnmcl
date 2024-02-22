using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Attributes;
using Models.Models.Nomenclatures.Institution;
using Server.Controllers.Base;
using Services.Nomenclatures;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/Nomenclature/Institution")]
    public class InstitutionNomenclatureController : BaseReadonlyNomenclatureController<Institution, InstitutionNomenclatureService>
    {
        public InstitutionNomenclatureController(
            InstitutionNomenclatureService nomenclatureService,
            NomenclaturesDbContext context) 
            : base(nomenclatureService, context)
        {
        }
    }
}
