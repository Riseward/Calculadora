using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Calculadora.Models;

namespace Calculadora.Controllers{
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

        [HttpPost]
        public IActionResult Index(string visor, string bt, string operando, string operador, bool limparVisor)
        {
            // identificar o valor da variável "bt"
            switch (bt)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                    if (visor == "0" || limparVisor) visor = bt;
                    else visor += bt; //visor = visor + bt;

                    // impedir o visor de ser limpo
                    limparVisor = false;

                    break;

                case "+/-":
                    // inverter o valor do visor
                    // pode ser feito de duas formas:
                    //  - multiplicar por -1 -> converter o valor para numero
                    //  - processar a string: visor.StartsWith().ToString().Substring().Length
                    visor = Convert.ToDouble(visor) * -1 + "";

                    break;

                case ",":
                    if (!visor.Contains(",")) visor += bt;
                    break;

                case "+":
                case "-":
                case ":":
                case "x":
                case "=":

                    //primeira vez que um operador foi selecionado
                    if (operador != null)
                    {

                        // executar a operação
                        // variaveis auxiliares
                        double operando1 = Convert.ToDouble(operando);
                        double operando2 = Convert.ToDouble(visor);

                        switch (operador)
                        {
                            case "+":
                                visor = operando1 + operando2 + "";
                                break;
                            case "-":
                                visor = operando1 - operando2 + "";
                                break;
                            case "x":
                                visor = operando1 * operando2 + "";
                                break;
                            case ":":
                                visor = operando1 / operando2 + "";
                                break;

                            case "c":
                                visor = "0";
                                operador = "";
                                operando = "";
                                limparVisor = true;
                                break;

                        }


                    }
                    // guardar valores para "memoria futura"
                    if (bt != "=") operador = bt;
                    else operador = "";
                    operando = visor;
                    limparVisor = true;
                    // guardar valores para "memoria futura"
                    //operador = bt;

                    break;



            }

            // levar o resultado do visor
            ViewBag.Visor = visor;
            ViewBag.Operador = operador;
            ViewBag.Operando = operando;
            ViewBag.LimparVisor = limparVisor + "";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
