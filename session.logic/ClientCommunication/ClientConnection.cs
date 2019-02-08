using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace session.Logic.ClientCommunication
{
	public class ClientConnection<T>
	{
		static HttpClient client = new HttpClient();

		public static async Task<Uri> CreateTrailAsync(T clientRes, string path)
		{
			var httpRequestMessage = new HttpRequestMessage(method: HttpMethod.Post, requestUri: path);
			httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var settings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};

			var jsonData = JsonConvert.SerializeObject(clientRes, settings);
			httpRequestMessage.Content = new StringContent(jsonData, encoding: Encoding.UTF8, mediaType: "application/json");

			var cts = new CancellationTokenSource();
			var request = await client.SendAsync(httpRequestMessage, cts.Token);
			var response = await request.Content.ReadAsStringAsync();

			// return URI of the created resource.
			return null;
		}
	}
}
