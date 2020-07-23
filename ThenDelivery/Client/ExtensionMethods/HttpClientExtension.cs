using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ThenDelivery.Client.ExtensionMethods
{
	public static class HttpClientExtension
	{
		public static async Task<IEnumerable<T>> CustomGetAsync<T>(this HttpClient httpClient, string baseUrl)
			where T : class
		{
			HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<List<T>>();
			}
			return null;
		}
	}
}
