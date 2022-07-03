using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static SearchBar;

public class ApiCall
{

    HttpClient client = new HttpClient();
    public MyJsonApi json;

    string req1 = "https://asos2.p.rapidapi.com/products/v2/list?store=US&offset=0&categoryId=";
    string req2 = "&limit=20&country=US&sort=freshness&currency=USD&sizeSchema=US&lang=en-US";
    public async Task CallAsync(string search)
    {
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
            JsonApi jsonApi = JsonUtility.FromJson<JsonApi>(body);

            //json = JsonUtility.FromJson<JsonApi>(body);

            Debug.Log(jsonApi);
            Debug.Log(jsonApi.searchTerm);
            Debug.Log(jsonApi.itemCount);

            foreach (Product s in jsonApi.products)
            {
                Debug.Log(s.imageUrl);
                s.categori = search;
                await PostProductAsync(JsonUtility.ToJson(s));
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
    [Serializable]
    public class MyJsonApi
    {
        public Product[] products;
    }

    private async Task PostProductAsync(string stringContent)
    {
        await client.PostAsync("https://localhost:7069/products", new StringContent(stringContent, Encoding.UTF8, "application/json"));
    }


    public async Task ControllerAsync(System.Collections.Generic.KeyValuePair<string, CodeBool> pair)
    {
        if (!pair.Value.actif)
        {
            await CallAsync(pair.Key);
            SearchBar.instance.Search[pair.Key].actif = true;
        }
        await CallMyAsync(pair.Key);

    }



    public async Task CallMyAsync(string search)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://localhost:7069/products/categori/" + search),
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            json = JsonUtility.FromJson<MyJsonApi>("{\"products\":"+body+"}");
        }
    }
}


