using Newtonsoft.Json;
using Odem.WebAPI.Models;
using RestSharp;

namespace Odem.WebAPI.Services;

public class NotificationsService
{
    private const string URL = "https://onesignal.com/api/v1/notifications";
    private const string APP_ID = "47f46191-c166-40ca-995b-1c1942125df2";
    
    public async Task<bool> SendNotification(string playerId, string message)
    {
        var client = new RestClient(URL);
        var request = new RestRequest();
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Authorization", "Basic ZDE1MTQ3ODMtNTAyYi00Y2Q3LThlM2QtMGEyZjI0YmYxNDlh");
        request.AddHeader("Content-Type", "application/json");

        var notification = new Notification
        {
            app_id = APP_ID,
            contents = new Contents{en = message},
            include_player_ids = new List<string> { playerId },
            name = "INTERNAL_CAMPAIGN_NAME"
        };
        var jsonString = JsonConvert.SerializeObject(notification);
        request.AddParameter("application/json",jsonString,ParameterType.RequestBody);

        if ((await client.PostAsync(request)).IsSuccessful)
        {
            return true;
        }
        return false;
    }
}