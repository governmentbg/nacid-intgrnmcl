using Microsoft.EntityFrameworkCore;
using Models.Interfaces;
using System.Reflection;
using System;
using System.Linq;
using Models.Models.Nomenclatures.Country;
using Models.Models.Nomenclatures.Institution;
using Microsoft.EntityFrameworkCore.Storage;

namespace Models
{
    public class NomenclaturesDbContext: DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<Institution> Institutions { get; set; }

        public NomenclaturesDbContext(DbContextOptions<NomenclaturesDbContext> options)
            : base(options)
        {
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }

        public void SetValues(IEntityVersion original, IEntityVersion entity)
        {
            entity.Id = original.Id;

            Entry(original).OriginalValues["Version"] = entity.Version;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplyConfigurations(modelBuilder);
            DisableCascadeDelete(modelBuilder);
            ConfigurePgSqlNameMappings(modelBuilder);
        }

        protected void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                   .Where(t => t.GetInterfaces().Any(gi =>
                       gi.IsGenericType
                       && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                   .ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }

        protected void DisableCascadeDelete(ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership
                    && fk.DeleteBehavior == DeleteBehavior.Cascade)
                .ToList()
                .ForEach(e => e.DeleteBehavior = DeleteBehavior.Restrict);
        }

        protected void ConfigurePgSqlNameMappings(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Configure pgsql table names convention.
                entity.SetTableName(entity.ClrType.Name.ToLower());

                // Configure pgsql column names convention.
                foreach (var property in entity.GetProperties())
                    property.SetColumnName(property.Name.ToLower());
            }
        }
    }

}
