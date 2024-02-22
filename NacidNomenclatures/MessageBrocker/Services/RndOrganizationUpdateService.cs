using Microsoft.EntityFrameworkCore;
using Models;
using Models.Enums;
using Models.Models.Nomenclatures.Institution;
using Services.Entity;
using System;
using System.Threading.Tasks;

namespace MessageBroker.Services
{
    public class RndOrganizationUpdateService
    {
        private readonly NomenclaturesDbContext context;

        public RndOrganizationUpdateService(
            NomenclaturesDbContext context
            )
        {
            this.context = context;
        }

        public async Task UpdateOrganization(Institution institutionForUpdate)
        {
            if (Enum.IsDefined(typeof(OrganizationType), institutionForUpdate.OrganizationType) && institutionForUpdate.Level < Level.Third)
            {
                var institution = await context.Institutions
                    .SingleOrDefaultAsync(e => e.Id == institutionForUpdate.Id);

                EntityService.ClearSkipProperties(institutionForUpdate);

                if (institution != null)
                {
                    EntityService.Update(institution, institutionForUpdate, context);
                    await context.SaveChangesAsync();
                }
                else
                {
                    await context.Institutions.AddAsync(institutionForUpdate);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
