using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class ApiCall
{

    public JsonApi json;

    string req1 = "https://asos2.p.rapidapi.com/products/v2/list?store=US&offset=0&categoryId=";
    string req2 = "&limit=20&country=US&sort=freshness&currency=USD&sizeSchema=US&lang=en-US";
    public async Task CallAsync(string search)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(req1+search+req2),
            Headers =
    {
        { "X-RapidAPI-Host", "asos2.p.rapidapi.com" },
        { "X-RapidAPI-Key", "2bff117810msh6ce0f4dd2dff12bp1b0be6jsnc5f1a78877f9" },
    },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Debug.Log(body);
            //JsonApi json = JsonUtility.FromJson<JsonApi>(body);

            json = JsonUtility.FromJson<JsonApi>(body);

            Debug.Log(json);
            Debug.Log(json.searchTerm);
            Debug.Log(json.itemCount);

            foreach (Product s in json.products)
            {
                Debug.Log(s.imageUrl);
            }

        }
    }

    [Serializable]
    public class JsonApi
    {
        public string searchTerm;
        public Product[] products;
        public string categoryName;
        public int itemCount;
    }

    
}


