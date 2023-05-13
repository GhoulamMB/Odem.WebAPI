namespace Odem.WebAPI.Models;


public class Contents
{
    public string en { get; set; }
}
public class Notification
{
    public IList<string> include_player_ids { get; set; }
    
    public Contents contents { get; set; }
    public string name { get; set; }
    public string app_id { get; set; }
}