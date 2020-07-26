using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ThenDelivery.Client.ExtensionMethods
{
	public static class HttpClientExtension
	{
		public static async Task<IEnumerable<T>> CustomGetAsync<T>(
			this HttpClient httpClient, string baseUrl)
			where T : class
		{
			HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
			if (response.IsSuccessStatusCode)
			{
				//Todo
				//Need try catch here
				return await response.Content.ReadFromJsonAsync<IEnumerable<T>>();
			}
			return null;
		}

		public static async Task<object> CustomPostAsync<TValue>(
			this HttpClient httpClient, string baseUrl, TValue objectToPost)
			where TValue : class
		{
			string jsonInString = JsonConvert.SerializeObject(objectToPost);
			var stringContent = new StringContent(jsonInString, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await httpClient.PostAsync(baseUrl, stringContent);
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<object>();
			}
			return null;
		}
	}
}
