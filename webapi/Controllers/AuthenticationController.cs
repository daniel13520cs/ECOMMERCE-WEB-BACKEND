using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[Route("authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public static readonly string SECRET_KEY = "QD+5s0-75NnAUCMyUsUf@;YdDwudCu";
    public const string appName = "WORKDEMO";

    private static string? appToken;
    public AuthenticationController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel login)
    {
        if (appToken == null) {
            appToken = GenerateJwtToken(appName, SECRET_KEY);
        }
        // Validate the username and password (you should use more secure methods)
        MySqlDataAccess db = new MySqlDataAccess();
        LoginModel user = db.GetUser(login);
        if (user == null || user.Password != login.Password) {
            return Unauthorized("Invalid username or password");
        }
        string sessionToken = db.GetSessionToken(login.Username);
        if (sessionToken == null) {
            sessionToken = GenerateJwtToken(login.Username, SECRET_KEY);
            db.CreateSessionToken(login.Username, sessionToken);
            return Ok(new { appToken, sessionToken });
        } else {
            return Ok(new {appToken, sessionToken});
        }
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] LoginModel login){
        MySqlDataAccess db = new MySqlDataAccess();
        LoginModel user = db.GetUser(login);
        if (user != null) {
            return Unauthorized("username exists");
        }
        db.CreateUser(login);
        return Ok();
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        // Clear the session
        //_httpContextAccessor.HttpContext.Session.Clear();

        return Ok("Logged out.");
    }

private string GenerateJwtToken(string username, string secretKey)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, username)
        }),
        Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}

    public static string GenerateSecretKey(int keyLength)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            var data = new byte[keyLength];
            rng.GetBytes(data);
            return Convert.ToBase64String(data);
        }
    }
}
