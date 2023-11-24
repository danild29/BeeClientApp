using System.IdentityModel.Tokens.Jwt;
namespace BeeClient.Client.Helpers;

public class JwtTokenHelper
{

    public static bool IsTokenExpired(string token)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtHandler.ReadJwtToken(token);

        // Check if the token has an expiration claim
        if (jwtToken.Payload?.Expiration is not null)
        {
            // Get the expiration date and time
            var expirationDate = DateTimeOffset.FromUnixTimeSeconds((long)jwtToken.Payload.Expiration);

            // Compare the expiration date with the current date and time
            if (expirationDate <= DateTimeOffset.UtcNow)
            {
                // Token has expired
                return true;
            }
        }

        // Token has not expired
        return false;
    }

}
