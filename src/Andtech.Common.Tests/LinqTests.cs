using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Andtech.Common.Tests
{

	public class LinqTests
	{
		private static int[] Numbers = new int[]
		{
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
		};

		[Test]
		public void TryFirst()
		{
			var success = Numbers.TryFirst(out var value);

			Assert.IsTrue(success);
			Assert.AreEqual(1, value);
		}

		[Test]
		public void TryFirstWithPredicate()
		{
			var success = Numbers.TryFirst(x => x >= 5, out var value);

			Assert.IsTrue(success);
			Assert.AreEqual(5, value);
		}

		[Test]
		public void TryFirstNotFound()
		{
			var success = Numbers.TryFirst(x => x == 100, out var value);

			Assert.IsFalse(success);
		}

		[Test]
		public void TryLast()
		{
			var success = Numbers.TryLast(out var value);

			Assert.IsTrue(success);
			Assert.AreEqual(10, value);
		}

		[Test]
		public void TryLastWithPredicate()
		{
			var success = Numbers.TryLast(x => x < 5, out var value);

			Assert.IsTrue(success);
			Assert.AreEqual(4, value);
		}

		[Test]
		public void TryLastNotFound()
		{
			var success = Numbers.TryFirst(x => x == 100, out var value);

			Assert.IsFalse(success);
		}
	}
}