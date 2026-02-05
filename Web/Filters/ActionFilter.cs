
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters;

public class ActionFilter : IActionFilter {

    private Stopwatch Contador = new ();
    

    public void OnActionExecuting(ActionExecutingContext context) { // 1
        Contador.Start();
        Console.WriteLine("Inicio contador");
    }

    public void OnActionExecuted(ActionExecutedContext context) { // 2
        Contador.Stop();
        Console.WriteLine($"Termino la ejecución: {Contador.ElapsedMilliseconds}");
    }
}