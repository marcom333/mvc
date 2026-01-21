namespace Web.Tools;

public class Output:IOutput
{
    private string uuid = "";
    public Output()
    {
        uuid = Guid.NewGuid().ToString();
    }
    public void Print(string data)
    {
        Console.WriteLine(uuid + " " + data);
    }
}