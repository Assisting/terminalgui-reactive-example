using System.Text.Json.Serialization;

namespace NewYorkTrees.DAL.Models;

public class NewYorkTree
{
    [JsonPropertyName("tree_id")]
    public int TreeId { get; set; }
    
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
    
    public string Status { get; set; }
    
    public string Health { get; set; }
    
    [JsonPropertyName("spc_common")]
    public string Species { get; set; }
    
    [JsonPropertyName("boroname")]
    public string Borough { get; set; }
    
    public string Address { get; set; }
    
    [JsonPropertyName("zip_city")]
    public string City { get; set; }
    
    [JsonPropertyName("zipcode")]
    public string Zip { get; set; }
}