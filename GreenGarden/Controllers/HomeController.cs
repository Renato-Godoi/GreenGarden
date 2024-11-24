using GreenGarden.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace GreenGarden.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Cadastro cadastro)
        {
            if (ModelState.IsValid)
            {
                cadastro.Cadastrar();
                if (cadastro._cadScss == true)
                {
                    TempData["Mensagem"] = "Cadastro realizado com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Mensagem"] = "Erro ao realizar cadastro. Tente novamente.";
                    Console.WriteLine("cadastro mal sucedido");
                }
            }
            else if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Erro: {error.ErrorMessage}");
                }
            }

            return View(cadastro);
        }

        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                login.logar();
                if (login.logScss == true)
                {
                    TempData["Mensagem"] = "Login realizado com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Mensagem"] = "Falha no login. Verifique suas credenciais.";
                    Console.WriteLine("login mal sucedido");
                }
            }
            else if (!ModelState.IsValid)
            {
                TempData["Mensagem"] = "Preencha os campos corretamente.";
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Erro: {error.ErrorMessage}");
                }
            }

            return View(login);
        }

        public IActionResult QuemSomos()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
