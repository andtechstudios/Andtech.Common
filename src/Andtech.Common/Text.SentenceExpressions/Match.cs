using System.Collections;
using System.Collections.Generic;

namespace Andtech.Common.Text.SentenceExpressions
{

	public class Match : IEnumerable<Pair>
	{
		public bool Success { get; internal set; }

		public Pair this[int index]
		{
			get => Pairs[index];
			set => Pairs[index] = value;
		}

		internal Pair[] Pairs { get; set; }

		public IEnumerator<Pair> GetEnumerator()
		{
			return ((IEnumerable<Pair>)Pairs).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return Pairs.GetEnumerator();
		}
	}
}
