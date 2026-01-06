namespace n8nAPI.Endpoints.System.HealthCheck;

public record HealthCheckResponse(int UptimeInSeconds, DateTime CurrentServerTime, string Message);
