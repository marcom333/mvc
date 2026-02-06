

using System.Text.Json.Serialization;

public class PageResult<T> {
    public List<T> Items {get; set;}
    [JsonPropertyName("TPage")]
    public int TotalPages {get; set;}
    public int TotalItems {get;set;}
    public int Page {get;set;}
    public int PageSize {get;set;} 
}