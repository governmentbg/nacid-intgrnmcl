using Microsoft.EntityFrameworkCore;
using Models;
using Models.Interfaces;
using Models.Models.Nomenclatures.Base;
using Services.Entity;
using System.Threading.Tasks;

namespace Services.Nomenclatures.Base
{
    public abstract class BaseNomenclatureService<T> : BaseReadonlyNomenclatureService<T>
        where T : Nomenclature, IIncludeAll<T>, new()
    {
        protected BaseNomenclatureService(NomenclaturesDbContext context)
            : base(context)
        {
        }

        public async virtual Task<T> Create(T nomenclature)
        {
            EntityService.ClearSkipProperties(nomenclature);

            context.Set<T>().Add(nomenclature);
            await context.SaveChangesAsync();

            return await Get(nomenclature.Id);
        }

        public async virtual Task Delete(int id)
        {
            try
            {
                var entity = await new T()
                .IncludeAll(context.Set<T>())
                .SingleOrDefaultAsync(e => e.Id == id);

                if (entity != null) 
                {
                    EntityService.Remove(entity, context);
                    await context.SaveChangesAsync();
                }
            }

            // Catch error if nomenclature is in use and cannot be deleted
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public async virtual Task<T> Update(T entity)
        {
            var original = await new T()
                .IncludeAll(context.Set<T>())
                .SingleOrDefaultAsync(e => e.Id == entity.Id);

            if (original != null)
            {
                EntityService.Update(original, entity, context);
                await context.SaveChangesAsync();

                return await Get(entity.Id);
            }

            return null;
        }
    }
}
