using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Andtech.Common
{

	public static class ParseUtility
	{

		/// <summary>
		/// Splits the input based on standard quoting rules.
		/// </summary>
		/// <param name="input">The string to split.</param>
		/// <returns>A collection of tokens.</returns>
		public static IEnumerable<string> QuotedSplit(string input)
		{
			var regex = new Regex(@"\""(?<match>((\\\"")|[^\""])*)\""|'(?<match>((\\')|[^'])*)'|(?<match>[^\s]+)");
			return
				from match in regex.Matches(input)
				select match.Groups["match"].Value;
		}

		public static string GlobToRegex(string glob)
		{
			glob = Regex.Escape(glob);
			glob = Regex.Replace(glob, @"\\\*\\\*(/|\\)", @"([^/]+/)*");
			glob = Regex.Replace(glob, @"\\\*", @"([^/]*)");
			glob = Regex.Unescape(glob);
			glob = Regex.Replace(glob, @"^(/|\\)", @"^");
			if (!Regex.IsMatch(glob, @"(/|\\)$"))
			{
				glob += "$";
			}

			return glob;
		}

		public static IPEndPoint IPEndPoint(string input)
		{
			var match = Regex.Match(input, @"(?<ip>.+?)(:(?<port>\d+))?$");
			var ipGroup = match.Groups["ip"];
			var portGroup = match.Groups["port"];

			string ipString = input;
			if (ipGroup.Success)
			{
				ipString = ipGroup.Value;
			}
			IPAddress address;
			if (string.IsNullOrEmpty(ipString) || ipString == "*")
			{
				address = IPAddress.Any;
			}
			else if (ipString == "localhost" || ipString == "127.0.0.1" || ipString == "::1")
			{
				address = IPAddress.Loopback;
			}
			else
			{
				address = IPAddress.Parse(ipString);
			}

			int port = 0;
			if (portGroup.Success)
			{
				port = int.Parse(portGroup.Value);
			}

			return new IPEndPoint(address, port);
		}

		public static string Http(string url)
		{
			url = Regex.Replace(url, @"^https?://", string.Empty);
			url = $"http://{url}"; 

			return url;
		}

		public static string Https(string url)
		{
			url = Regex.Replace(url, @"^https?://", string.Empty);
			url = $"https://{url}";

			return url;
		}
	}
}
