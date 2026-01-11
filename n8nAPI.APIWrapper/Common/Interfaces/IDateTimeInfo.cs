namespace n8nAPI.APIWrapper.Common.Interfaces;

internal interface IDateTimeInfo
{
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset UpdatedAt { get; set; }
}
