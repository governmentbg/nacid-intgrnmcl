using MessageBroker.Enums;
using MessageBroker.Publisher;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Attributes;
using Models.Interfaces;
using Models.Models.Nomenclatures.Base;
using Services.Nomenclatures.Base;
using System.Threading.Tasks;

namespace Server.Controllers.Base
{
    public abstract class BaseNomenclatureController<T, TService> : BaseReadonlyNomenclatureController<T, TService>
        where T: Nomenclature, IIncludeAll<T>, new()
        where TService : BaseNomenclatureService<T>
    {
        protected readonly NomenclaturesMbPublisher nomenclaturesMbPublisher;
        private readonly NomenclatureType nomenclatureType;

        public BaseNomenclatureController(
            TService nomenclatureService,
            NomenclaturesDbContext context,
            NomenclaturesMbPublisher nomenclaturesMbPublisher,
            NomenclatureType nomenclatureType
            ): base(nomenclatureService, context)
        {
            this.nomenclaturesMbPublisher = nomenclaturesMbPublisher;
            this.nomenclatureType = nomenclatureType;
        }

        [HttpPost("Create")]
        [ShowSwagger]
        public async virtual Task<ActionResult<T>> Create([FromBody] T entity)
        {
            using var transaction = context.BeginTransaction();
            var createdNomenclature = await nomenclatureService.Create(entity);
            nomenclaturesMbPublisher.PublishNomenclatureChange(entity.Id, entity, nomenclatureType, NomenclatureOperation.Create);
            transaction.Commit();
            return Ok(createdNomenclature);
        }

        [HttpPut("Update")]
        [ShowSwagger]
        public async virtual Task<ActionResult<T>> Update([FromBody] T entity)
        {
            using var transaction = context.BeginTransaction();
            var updatedNomenclature = await nomenclatureService.Update(entity);
            nomenclaturesMbPublisher.PublishNomenclatureChange(entity.Id, updatedNomenclature, nomenclatureType, NomenclatureOperation.Update);
            await transaction.CommitAsync();
            return Ok(updatedNomenclature);
        }

        [HttpPost("Delete")]
        [ShowSwagger]
        public async virtual Task<ActionResult> Delete([FromBody] int id)
        {
            await nomenclatureService.Delete(id);
            nomenclaturesMbPublisher.PublishNomenclatureChange(id, null, nomenclatureType, NomenclatureOperation.Delete);
            return Ok(); 
        }
    }
}
