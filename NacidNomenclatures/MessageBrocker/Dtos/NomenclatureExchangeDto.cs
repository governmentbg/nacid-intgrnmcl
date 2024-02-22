using MessageBroker.Enums;

namespace MessageBroker.Dtos
{
    public class NomenclatureExchangeDto
    {
        public int? Id { get; set; }
        public object Nomenclature { get; set; }
        public NomenclatureType NomenclatureType { get; set; }
        public NomenclatureOperation Operation { get; set; }
    }
}
