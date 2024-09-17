using RestSharp;

namespace SubuProtokol.WEBUI.Service
{

    public interface IApiServiceClient
    {
        RestClient Client { get; }
    }
    public class ApiServiceClient : IApiServiceClient
    {

        public RestClient Client { get; private set; }

        public ApiServiceClient(IConfiguration configuration)
        {
            // Client = new RestClient("https://apiprotokol.subu.edu.tr");
            //Client = new RestClient("http://localhost:5097");
           // Client = new RestClient("http://dk-prj2.subu.edu.tr");

            Client = new RestClient(configuration.GetConnectionString("Endpoint"));
            //Client.Authenticator = new JwtAuthenticator("token");
        }
    }
}
