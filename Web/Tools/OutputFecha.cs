
namespace Web.Tools;

public class OutputFecha : IOutput {
    public void Print(string msg) {
        Console.WriteLine(DateTime.Now + " " + msg);
    }
}