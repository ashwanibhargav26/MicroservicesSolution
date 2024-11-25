using System.Text.Json.Serialization;

namespace AuthServiceApi.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        Admin,
        User
    }
}
