using Models.Interfaces;

namespace Models.Models.Base
{
    public class EntityVersion : IEntityVersion
    {
        public int Id { get; set ; }
        public int Version { get; set; }
    }
}
