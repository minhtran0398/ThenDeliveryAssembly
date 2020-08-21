using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ThenDelivery.Client.ExtensionMethods
{
	public static class HttpClientExtension
	{
		public static async Task<T> CustomGetAsync<T>(
			this HttpClient httpClient, string baseUrl)
			where T : class
		{
			HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
			if (response.IsSuccessStatusCode)
			{
				//Todo
				//Need try catch here
				return await response.Content.ReadFromJsonAsync<T>();
			}
			return null;
		}

		public static async Task<string> CustomPostAsync<TValue>(
			this HttpClient httpClient, string baseUrl, TValue objectToPost)
			where TValue : class
		{
			string jsonInString = JsonConvert.SerializeObject(objectToPost);
			var stringContent = new StringContent(jsonInString, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await httpClient.PostAsync(baseUrl, stringContent);
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadAsStringAsync();
			}
			return null;
		}

		public static async Task<TResult> CustomPostAsync<TValue, TResult>(
			this HttpClient httpClient, string baseUrl, TValue objectToPost)
			where TValue : class
			where TResult : class
		{
			string jsonInString = JsonConvert.SerializeObject(objectToPost);
			var stringContent = new StringContent(jsonInString, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await httpClient.PostAsync(baseUrl, stringContent);
			if (response.IsSuccessStatusCode)
			{
				string content = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<TResult>(content);
			}
			return null;
		}

		public static async Task<TResult> CustomPutAsync<TValue, TResult>(
			this HttpClient httpClient, string baseUrl, TValue objectToPut)
			where TValue : class
			where TResult : class
		{
			string jsonInString = JsonConvert.SerializeObject(objectToPut);
			var stringContent = new StringContent(jsonInString, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await httpClient.PutAsync(baseUrl, stringContent);
			string content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<TResult>(content);
		}
	}
}
