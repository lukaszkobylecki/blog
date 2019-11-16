using Autofac.Extensions.DependencyInjection;
using Blog.IntegrationTests.Results;
using Blog.Infrastructure.Commands.Auth;
using Blog.Infrastructure.DTO;
using Blog.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Blog.IntegrationTests.Controllers
{
    public abstract class ControllerTestsBase
    {
        protected readonly TestServer Server;

        protected readonly HttpClient Client;

        public ControllerTestsBase()
        {
            var host = CreateHostBuilder().Build();
            host.StartAsync();

            Server = host.GetTestServer();
            Client = host.GetTestClient();
        }

        private IHostBuilder CreateHostBuilder() 
            => Host.CreateDefaultBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseEnvironment("Test");
                    webBuilder.UseTestServer();
                    webBuilder.UseStartup<Startup>();
                });

        protected StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        protected async Task<GetResult<T>> GetResource<T>(string path)
        {
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            return new GetResult<T>
            {
                Response = response,
                Data = JsonConvert.DeserializeObject<T>(responseString)
            };
        }

        protected async Task<T> DeserializeAsync<T>(HttpResponseMessage response)
            => JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

        protected async Task AddAuthTokenAsync(string email, string password)
        {
            var command = new Login
            {
                Email = email,
                Password = password
            };
            var payload = GetPayload(command);

            var response = await Client.PostAsync("auth", payload);
            var jwt = await DeserializeAsync<JwtDto>(response);

            Client.DefaultRequestHeaders.Add("Authorization", $"bearer {jwt.Token}");
        }

        protected void RemoveAuthToken()
            => Client.DefaultRequestHeaders.Remove("Authorization");
    }
}
