using NUnit.Framework;
using System.Net;

namespace Andtech.Common.Tests
{

	public class IPEndPointTests
	{

		[Test]
		public void Parse()
		{
			var endPoint = ParseUtility.IPEndPoint("192.168.0.0");

			Assert.AreEqual(IPAddress.Parse("192.168.0.0"), endPoint.Address);
		}

		[Test]
		public void ParseWithPort()
		{
			var endPoint = ParseUtility.IPEndPoint("192.168.0.0:8080");

			Assert.AreEqual(IPAddress.Parse("192.168.0.0"), endPoint.Address);
			Assert.AreEqual(8080, endPoint.Port);
		}

		[Test]
		public void ParseLocalhost()
		{
			var endPoint = ParseUtility.IPEndPoint("localhost");

			Assert.AreEqual(IPAddress.Loopback, endPoint.Address);
		}

		[Test]
		public void ParseLocalhostwithPort()
		{
			var endPoint = ParseUtility.IPEndPoint("localhost:8080");

			Assert.AreEqual(IPAddress.Loopback, endPoint.Address);
			Assert.AreEqual(8080, endPoint.Port);
		}

		[Test]
		public void ParseStar()
		{
			var endPoint = ParseUtility.IPEndPoint("*");

			Assert.AreEqual(IPAddress.Any, endPoint.Address);
		}

		[Test]
		public void ParseStarWithPort()
		{
			var endPoint = ParseUtility.IPEndPoint("*:8080");

			Assert.AreEqual(IPAddress.Any, endPoint.Address);
			Assert.AreEqual(8080, endPoint.Port);
		}
	}
}