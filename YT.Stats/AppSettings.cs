using System.IO;
using Newtonsoft.Json.Linq;

namespace YT.Stats
{
	public class AppSettings
	{
		public string FilePath { get; }
		public string Mode { get; }
		public string ServerUrl { get; }
		public string Token { get; }
		public decimal FixedSalary { get; }

		public AppSettings(string serverUrl, string token, string filePath, string mode, decimal fixedSalary)
		{
			ServerUrl = serverUrl;
			Token = token;
			FilePath = filePath;
			Mode = mode;
			FixedSalary = fixedSalary;
		}
		
		public static AppSettings Load(string path)
		{
			var json = JToken.Parse(ReadFromFile(path));
			var serverUrl = json.SelectToken("serverUrl").ToObject<string>();
			var token = json.SelectToken("token").ToObject<string>();
			var filePath = json.SelectToken("fileName").ToObject<string>();
			var mode = json.SelectToken("mode").ToObject<string>();
			var salary = json.SelectToken("fixed-salary").ToObject<decimal>();
			
			return new AppSettings(serverUrl, token, filePath, mode, salary);
		}

		private static string ReadFromFile(string path)
		{
			var fileSource = string.Empty;
			
			using (var stream = new FileStream(path, FileMode.Open))
			{
				using (var reader = new StreamReader(stream))
				{
					fileSource = reader.ReadToEnd();
				}
			}

			return fileSource;
		}
	}
}