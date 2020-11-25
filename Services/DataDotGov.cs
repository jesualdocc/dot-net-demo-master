using APP_Demo__WebAPI_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;

namespace APP_Demo__WebAPI_.Services
{
    public class DataDotGov
    {

        private static HttpClient client;

        public DataDotGov()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task<Page> GetDrugNames(int pageNumber)
        {
            Page pages = null;
            
            UriBuilder uri = new UriBuilder("https://dailymed.nlm.nih.gov/dailymed/services/");
            
            uri.Path += "v2/drugnames";

            uri.Query = $"page={pageNumber}";

            HttpResponseMessage response = await client.GetAsync(uri.Uri);

            if (response.IsSuccessStatusCode)
            { 
                string content = await response.Content.ReadAsStringAsync();

                pages = JsonSerializer.Deserialize<Page>(content);
                
                
            }

            return pages;

        }

       
    }
}
