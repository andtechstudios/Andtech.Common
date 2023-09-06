using System.Text.RegularExpressions;

namespace Andtech.Common.Tests
{

	public class RegexTests
	{

		[Test]
		public void ParseRooted()
		{
			var expression = ParseUtil.GlobToRegex("/alpha");

			Assert.AreEqual("^alpha$", expression);
		}

		[Test]
		public void MatchAnywhere()
		{
			var expression = ParseUtil.GlobToRegex("alpha");
			var regex = new Regex(expression);

			Assert.IsTrue(regex.IsMatch("alpha"));
			Assert.IsTrue(regex.IsMatch("bravo/alpha"));
			Assert.IsTrue(regex.IsMatch("charlie/delta/alpha"));
		}

		[Test]
		public void MatchRootedOnly()
		{
			var expression = ParseUtil.GlobToRegex("/alpha");
			var regex = new Regex(expression);

			Assert.IsTrue(regex.IsMatch("alpha"));
			Assert.IsFalse(regex.IsMatch("bravo/alpha"));
			Assert.IsFalse(regex.IsMatch("charlie/delta/alpha"));
		}

		[Test]
		public void MatchWildcard()
		{
			var expression = ParseUtil.GlobToRegex("alpha/*");
			var regex = new Regex(expression);

			Assert.IsTrue(regex.IsMatch("alpha/bravo"));
			Assert.IsTrue(regex.IsMatch("alpha/charlie"));
			Assert.IsTrue(regex.IsMatch("alpha/delta"));
		}

		[Test]
		public void MatchGlobstar()
		{
			var expression = ParseUtil.GlobToRegex("**/alpha");
			var regex = new Regex(expression);

			Assert.IsTrue(regex.IsMatch("alpha"));
			Assert.IsTrue(regex.IsMatch("bravo/alpha"));
			Assert.IsTrue(regex.IsMatch("charlie/delta/alpha"));
		}

		[Test]
		public void ParseHttp()
		{
			var url = ParseUtil.Http("example.com");

			Assert.AreEqual("http://example.com", url);
		}

		[Test]
		public void ParseHttpAlready()
		{
			var url = ParseUtil.Http("http://example.com");

			Assert.AreEqual("http://example.com", url);
		}

		[Test]
		public void ParseHttpFromHttps()
		{
			var url = ParseUtil.Http("https://example.com");

			Assert.AreEqual("http://example.com", url);
		}

		[Test]
		public void ParseHttps()
		{
			var url = ParseUtil.Https("example.com");

			Assert.AreEqual("https://example.com", url);
		}

		[Test]
		public void ParseHttpsAlready()
		{
			var url = ParseUtil.Https("https://example.com");

			Assert.AreEqual("https://example.com", url);
		}

		[Test]
		public void ParseHttpsFromHttp()
		{
			var url = ParseUtil.Https("http://example.com");

			Assert.AreEqual("https://example.com", url);
		}
	}
}