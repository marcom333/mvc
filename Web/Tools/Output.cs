namespace Web.Tools;

public class Output : IOutput
{
    public Output(){}

    public void print(string data)
    {
        Console.WriteLine(data);
    }
}