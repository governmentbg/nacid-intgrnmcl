using Microsoft.EntityFrameworkCore;
using Models;
using Models.Interfaces;
using Models.Models.Nomenclatures.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Nomenclatures.Base
{
    public abstract class BaseReadonlyNomenclatureService<T>
        where T : Nomenclature, IIncludeAll<T>, new()
    {
        protected readonly NomenclaturesDbContext context;

        protected BaseReadonlyNomenclatureService(NomenclaturesDbContext context)
        {
            this.context = context;
        }

        public async virtual Task<T> Get(int id)
        {
            var nomenclature = await new T().IncludeAll(context.Set<T>().AsQueryable())
                        .AsNoTracking()
                        .SingleOrDefaultAsync(e => e.Id == id);

            return nomenclature;
        }

        public async virtual Task<List<T>> GetAll()
        {
            var nomenclatureList = await new T().IncludeAll(context.Set<T>().AsQueryable())
                        .AsNoTracking()
                        .ToListAsync();

            return nomenclatureList;
        }
    }
}
