using System.IdentityModel.Tokens.Jwt;

namespace PMS.Helpers.Interface
{
    public class JwtLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JwtLoggingMiddleware> _logger;

        public JwtLoggingMiddleware(RequestDelegate next, ILogger<JwtLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Extract the Authorization header
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();

                try
                {
                    var handler = new JwtSecurityTokenHandler();

                    if (handler.CanReadToken(token))
                    {
                        var jwtToken = handler.ReadJwtToken(token);

                        // Extract key claims (Adjust standard names based on your token configuration)
                        var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == JwtRegisteredClaimNames.Sub)?.Value;
                        var email = jwtToken.Claims.FirstOrDefault(c => c.Type == "email" || c.Type == JwtRegisteredClaimNames.Email)?.Value;

                        _logger.LogInformation("Inbound Request: Path {Path} | User ID: {UserId} | Email: {Email}",
                            context.Request.Path, userId ?? "Unknown", email ?? "Unknown");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to decode inbound JWT token payload.");
                }
            }
            else
            {
                _logger.LogInformation("Inbound Request: Path {Path} | Anonymous User", context.Request.Path);
            }

            await _next(context); // Continue pipeline
        }
    }
}
