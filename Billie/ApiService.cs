using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Billie
{
    internal class ApiService
    {
        private static readonly HttpClient client = new HttpClient();

        public class ProductResponse
        {
            public Product product { get; set; }
            public int status { get; set; }
        }

        public class Product
        {
            public string product_name { get; set; }
            public string brands { get; set; }
        }

        public static async Task<Product> FetchProductDetailsFromApi(long barcode)
        {
            var apiUrl = $"https://world.openfoodfacts.org/api/v/product/{barcode}.json";
            var response = await client.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            var productResponse = JsonSerializer.Deserialize<ProductResponse>(responseData);

            if (productResponse.status == 0) { return null; }

            return productResponse.product;
        }
    }
}
