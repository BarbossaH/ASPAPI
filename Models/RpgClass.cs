using System.Text.Json.Serialization;

namespace ASPAPI.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Paladin = 1,
        Hunter = 2,

        Mage = 3,

        DeathKnight = 4,
    }
}