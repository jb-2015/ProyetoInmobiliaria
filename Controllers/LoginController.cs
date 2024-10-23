using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using ProyetoInmobiliaria.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProyetoInmobiliaria.Models;

    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Autenticacion  ([FromBody]LoginViewModel user){
            RepositorioLogin _repoLogin = new RepositorioLogin();
            Usuario u = _repoLogin.Verificar(new LoginViewModel { Email = user.Email , Password = user.Password });
            if (u != null){
                var claimList = new List<Claim>{
                    new Claim(ClaimTypes.Name, u.Nombre),
                    new Claim(ClaimTypes.Role, u.Rol),
                    new Claim(ClaimTypes.NameIdentifier, u.IdUsuario.ToString()),
                    new Claim("AvatarUrl", u.Avatar)
                };
                ClaimsIdentity identidad = new ClaimsIdentity(claimList, CookieAuthenticationDefaults.AuthenticationScheme);
                await  HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identidad));

                return Json(new {ok=true});
            }
            return Json(new {ok=false, mensaje="Usuario o contraseña incorrectos"});
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Login");
        }

        [HttpPost("api/login")]
        public IActionResult Login([FromBody] LoginViewModel lvm){
            RepositorioLogin _repoLogin = new RepositorioLogin();
            Usuario u = _repoLogin.Verificar(new LoginViewModel { Email = lvm.Email , Password = lvm.Password });
            if (u != null){
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(u.Password);
                var tokenDescriptor = new SecurityTokenDescriptor                
                {
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new { token = tokenHandler.WriteToken(token) });
            }else{
                return BadRequest("Credenciales Incorrectas");
            }
        }


}
