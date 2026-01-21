
namespace Web.Tools;
public class Output : IOutput{
    private String uuid = "";

    public Output(){
        uuid = Guid.NewGuid().ToString();
    }
    
    public void Print(string msg){
        Console.WriteLine(uuid+" "+msg);
    }

}
