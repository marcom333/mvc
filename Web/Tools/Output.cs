namespace Web.Tools;

public class Output :IOutput
{
    public void Print(string data)
    {
       Console.WriteLine(data);
    }
}