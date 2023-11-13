using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Authenticators;
using System.Net.Http;

namespace fsmAPI.Controllers
{
    [ApiController]
    [Route("echo")]
    public class EchoController : ControllerBase
    {
        public EchoController()
        {

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(DateTime.Now);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("news")]
        public async Task<IActionResult> GetNews()
        {


                string apiUrl = "https://newsapi.org/v2/top-headlines?country=br&apiKey=6aed265a9b0446e8aabcf8e27cf95e70";


            var news = "";



            var options = new RestClientOptions(apiUrl)
            {
            };
            // Create RestClient
            var client = new RestClient(apiUrl);
            
            // Create RestRequest with Method.GET
            var request = new RestRequest();
            request.Method = Method.Get;

            // Execute the request and get the response
            RestResponse response = client.Execute(request);

            // Check if the request was successful (status code 200)
            if (response.IsSuccessful)
            {
                string responseData = response.Content;
                return Ok(response.Content);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ErrorMessage}");
            }

            return Ok(news);
        }
    }
}
