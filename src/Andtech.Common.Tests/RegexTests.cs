using NUnit.Framework;
using System.Text.RegularExpressions;

namespace Andtech.Common.Tests
{

	public class RegexTests
	{

		[Test]
		public void ParseRooted()
		{
			var expression = ParseUtility.GlobToRegex("/alpha");

			Assert.AreEqual("^alpha$", expression);
		}

		[Test]
		public void MatchAnywhere()
		{
			var expression = ParseUtility.GlobToRegex("alpha");
			var regex = new Regex(expression);

			Assert.IsTrue(regex.IsMatch("alpha"));
			Assert.IsTrue(regex.IsMatch("bravo/alpha"));
			Assert.IsTrue(regex.IsMatch("charlie/delta/alpha"));
		}

		[Test]
		public void MatchRootedOnly()
		{
			var expression = ParseUtility.GlobToRegex("/alpha");
			var regex = new Regex(expression);

			Assert.IsTrue(regex.IsMatch("alpha"));
			Assert.IsFalse(regex.IsMatch("bravo/alpha"));
			Assert.IsFalse(regex.IsMatch("charlie/delta/alpha"));
		}

		[Test]
		public void MatchWildcard()
		{
			var expression = ParseUtility.GlobToRegex("alpha/*");
			var regex = new Regex(expression);

			Assert.IsTrue(regex.IsMatch("alpha/bravo"));
			Assert.IsTrue(regex.IsMatch("alpha/charlie"));
			Assert.IsTrue(regex.IsMatch("alpha/delta"));
		}

		[Test]
		public void MatchGlobstar()
		{
			var expression = ParseUtility.GlobToRegex("**/alpha");
			var regex = new Regex(expression);

			Assert.IsTrue(regex.IsMatch("alpha"));
			Assert.IsTrue(regex.IsMatch("bravo/alpha"));
			Assert.IsTrue(regex.IsMatch("charlie/delta/alpha"));
		}

		[Test]
		public void ParseHttp()
		{
			var url = ParseUtility.Http("example.com");

			Assert.AreEqual("http://example.com", url);
		}

		[Test]
		public void ParseHttpAlready()
		{
			var url = ParseUtility.Http("http://example.com");

			Assert.AreEqual("http://example.com", url);
		}

		[Test]
		public void ParseHttpFromHttps()
		{
			var url = ParseUtility.Http("https://example.com");

			Assert.AreEqual("http://example.com", url);
		}

		[Test]
		public void ParseHttps()
		{
			var url = ParseUtility.Https("example.com");

			Assert.AreEqual("https://example.com", url);
		}

		[Test]
		public void ParseHttpsAlready()
		{
			var url = ParseUtility.Https("https://example.com");

			Assert.AreEqual("https://example.com", url);
		}

		[Test]
		public void ParseHttpsFromHttp()
		{
			var url = ParseUtility.Https("http://example.com");

			Assert.AreEqual("https://example.com", url);
		}
	}
}