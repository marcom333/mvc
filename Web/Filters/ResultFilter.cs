using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters;

public class ResultFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        Console.WriteLine("Result Filter start");
    }
    
    public void OnResultExecuted(ResultExecutedContext context)
    {
        Console.WriteLine("Result Filter end");
    }
}