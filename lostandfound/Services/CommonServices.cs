using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ModernHttpClient;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace lostandfound
{
	public static class CommonService
	{
		public const string BaseUrl = "http://52.172.50.65/";
		public static async Task<RootObject> HttpPostOperation(string url, MultipartFormDataContent content)
		{
			try
			{
				using (var client = new HttpClient(new NativeMessageHandler()))
				{
					client.BaseAddress = new Uri(BaseUrl);
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					var response = await client.PostAsync(url, content);
					if (response.IsSuccessStatusCode)
					{
						var json = await response.Content.ReadAsStringAsync();
						var result = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<RootObject>(json));
						return result;
					}
					else
					{
						var rootnode = new RootObject() { status = 666, message = response.StatusCode.ToString() };
						return rootnode;
					}
				}
			}
			catch (Exception e)
			{
				var rootnode = new RootObject() { status = 666, message = e.ToString() };
				return rootnode;
			}
		}
	}
}
