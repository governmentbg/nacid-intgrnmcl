using Models;
using Models.Models.Nomenclatures.Institution;
using Services.Nomenclatures.Base;

namespace Services.Nomenclatures
{
    public class InstitutionNomenclatureService : BaseReadonlyNomenclatureService<Institution>
    {
        public InstitutionNomenclatureService(NomenclaturesDbContext context)
            : base(context)
        {
        }
    }
}
