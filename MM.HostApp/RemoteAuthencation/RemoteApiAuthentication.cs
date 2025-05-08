using MM.HostApp.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace MM.HostApp.RemoteAuthencation
{
    public class RemoteApiAuthentication
    {
        private static HttpClient httpClient = new HttpClient();
        public RemoteApiAuthentication()
        {
        }
        public static async Task<ResponseAuthentication?> IsAuthenticated(string url,string refreshToken)
        {
            httpClient.DefaultRequestHeaders.Remove("RefreshToken");
            httpClient.DefaultRequestHeaders.Add("RefreshToken", refreshToken);
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }
            var response = httpClient.GetAsync($"{url}api/authenticateuser/token").Result;
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResponseAuthentication>(content);
            }
            return null;
        }
    }
}
