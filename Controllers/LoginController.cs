using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using ProyetoInmobiliaria.Models;

namespace ProyetoInmobiliaria.Models;

    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Autenticacion(LoginViewModel user)
        {
            RepositorioLogin _repoLogin = new RepositorioLogin();

            Usuario u = _repoLogin.Verificar(new LoginViewModel { Email = user.Email , Password = user.Password });
            
            if (u != null){
                ClaimsIdentity identidad = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identidad.AddClaim(new Claim(ClaimTypes.Name, u.Nombre));
                identidad.AddClaim(new Claim(ClaimTypes.Role, u.Rol));

                await  HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identidad));

                return RedirectToAction("Index","Home");
            }
            else
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos");
                return View("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Login");
        }
}
