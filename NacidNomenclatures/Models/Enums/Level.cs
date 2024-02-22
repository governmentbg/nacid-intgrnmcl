using System.ComponentModel;

namespace Models.Enums
{
    [Description("Ниво на йерархия")]
    public enum Level
    {
        [Description("Първо")]
        First = 1,

        [Description("Второ")]
        Second = 2,

        [Description("Трето")]
        Third = 3
    }
}
