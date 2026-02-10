

using Microsoft.AspNetCore.Http.HttpResults;

namespace Web.ViewModel;

public class ApiResponseViewModel {
    public string Status {get; set;} = "";
    public string Msg {get; set;} = "";
    public object? Obj {get; set;}

    public static ApiResponseViewModel Ok(string msg, object obj) {
        return new ApiResponseViewModel() {
            Status= "OK",
            Msg=msg,
            Obj = obj
        };
    }

    public static ApiResponseViewModel BadRequest(string msg, object obj) {
        return new ApiResponseViewModel() {
            Status= "Bad Request",
            Msg = msg,
            Obj = obj
        };
    }
}