using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Attributes;
using Models.Interfaces;
using Models.Models.Nomenclatures.Base;
using Services.Nomenclatures.Base;
using System.Threading.Tasks;

namespace Server.Controllers.Base
{
    public abstract class BaseReadonlyNomenclatureController<T, TService> : ControllerBase
        where T : Nomenclature, IIncludeAll<T>, new()
        where TService : BaseReadonlyNomenclatureService<T>
    {
        protected readonly TService nomenclatureService;
        protected readonly NomenclaturesDbContext context;

        public BaseReadonlyNomenclatureController(
            TService nomenclatureService,
            NomenclaturesDbContext context
            )
        {
            this.nomenclatureService = nomenclatureService;
            this.context = context;
        }

        [HttpGet("Get")]
        [ShowSwagger]
        public async virtual Task<ActionResult> Get([FromQuery] int id)
        {
            return Ok(await nomenclatureService.Get(id));
        }

        [HttpGet("GetAll")]
        [ShowSwagger]
        public async virtual Task<ActionResult> GetAll()
        {
            return Ok(await nomenclatureService.GetAll());
        }
    }
}
