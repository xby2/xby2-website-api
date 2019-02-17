using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Xby2WebsiteAPI.Model;

namespace xby2_website_api.Controllers
{
    [Route("email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        public IConfiguration Configuration { get; }

        public EmailController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "This API only supports POST commands.";
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Xby2Message email)
        {
            var username = Configuration["XBY2_WEBSITE_API_MAILJET_USERNAME"];
            var password = Configuration["XBY2_WEBSITE_API_MAILJET_PASSWORD"];
            if (username == null || password == null)
            {
                return StatusCode(500, "API Credentials not set.");
            }

            var apiUrl = Configuration.GetValue<string>("ApplicationSettings:EmailApiUrl");
            var fromEmail = Configuration.GetValue<string>("ApplicationSettings:FromEmail");
            var toEmail = Configuration.GetValue<string>("ApplicationSettings:ToEmail");
            var subject = Configuration.GetValue<string>("ApplicationSettings:Subject");

            using (HttpClient client = new HttpClient())
            {
                var authorizationString = string.Format("{0}:{1}", username, password);
                var byteArray = Encoding.ASCII.GetBytes(authorizationString);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                MailjetMessage mailjetMessage =
                    MailjetMessage.FromXby2Message(email, fromEmail, toEmail, subject);
                MailjetRequest request = new MailjetRequest(mailjetMessage);
                var requestJson = request.ToJson();

                var response = await client.PostAsync(apiUrl, new StringContent(requestJson, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
            }

            return Ok();
        }
    }
}