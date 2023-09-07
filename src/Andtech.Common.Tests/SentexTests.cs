using NUnit.Framework;

namespace Andtech.Common.Text.SentenceExpressions.Tests
{

	public class SentexTests
	{

		[Test]
		public void IsMatchEmpty()
		{
			var sentex = new Sentex("master of puppets");

			Assert.False(sentex.IsMatch(string.Empty));
		}

		[Test]
		public void IsMatchTrivial()
		{
			var sentex = new Sentex("master of puppets");

			Assert.IsTrue(sentex.IsMatch("master of puppets"));
		}

		[Test]
		public void IsMatchNotMatch()
		{
			var sentex = new Sentex("the thing that should not be");

			Assert.IsFalse(sentex.IsMatch("holier than thou"));
		}

		[Test]
		public void IsMatchSubstring()
		{
			var sentex = new Sentex("the thing that should not be");

			Assert.IsTrue(sentex.IsMatch("thing that should"));
		}

		[Test]
		public void IsMatchWrongOrder()
		{
			var sentex = new Sentex("the thing that should not be");

			Assert.IsFalse(sentex.IsMatch("thing not should"));
		}

		[Test]
		public void MatchesSuccess()
		{
			var sentex = new Sentex("uptown funk (instrumental)");

			var match = sentex.Match("uptown instrumental");
			Assert.IsTrue(match[0].IsMatch);
			Assert.IsTrue(match[1].IsMatch);
		}

		[Test]
		public void MatchesIncompleteMatch()
		{
			var sentex = new Sentex("uptown funk (instrumental)");

			var match = sentex.Match("up fun");
			Assert.IsFalse(match[0].IsExactMatch);
			Assert.IsFalse(match[1].IsExactMatch);
		}

		[Test]
		public void MatchesCompleteMatch()
		{
			var sentex = new Sentex("uptown funk (instrumental)");

			var match = sentex.Match("uptown funk");
			Assert.IsTrue(match[0].IsExactMatch);
			Assert.IsTrue(match[1].IsExactMatch);
		}
	}
}