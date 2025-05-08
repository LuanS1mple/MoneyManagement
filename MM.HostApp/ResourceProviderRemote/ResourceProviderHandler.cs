namespace MM.HostApp.ResourceProviderRemote
{
    public class ResourceProviderHandler
    {
        private readonly HttpClient httpClient;
        private static readonly Lazy<ResourceProviderHandler> _instance =
        new Lazy<ResourceProviderHandler>(() => new ResourceProviderHandler());


        private ResourceProviderHandler()
        {
            httpClient = new HttpClient();
        }

        public static ResourceProviderHandler Instance => _instance.Value;

       
        public async Task<string?> GetJsonJars(int id)
        {
            string url = "http://localhost:6969/";
            var response = httpClient.GetAsync($"{url}jar/get?customerId={id}").Result;
            if (response.IsSuccessStatusCode)
            {
                // Đọc nội dung JSON từ response
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
            else
            {
                // Xử lý lỗi nếu API trả về không thành công (ví dụ, 404 hoặc 500)
                return null;
            }
        }
    }
}
