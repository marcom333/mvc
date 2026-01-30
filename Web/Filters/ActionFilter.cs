using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters;

public class ActionFilter : IActionFilter
{

    private Stopwatch contador = new ();

    public void OnActionExecuting(ActionExecutingContext context)
    {
        contador = Stopwatch.StartNew();
        Console.WriteLine("Iniciando contador...");
    }
    
    public void OnActionExecuted(ActionExecutedContext context)
    {
        contador.Stop();
        Console.WriteLine($"Termino la ejecución: {contador.ElapsedMilliseconds}");
    }
}