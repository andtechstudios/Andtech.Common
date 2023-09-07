
namespace Andtech.Common.Text.SentenceExpressions
{

	public class Pair
	{
		public Word Word { get; internal set; }
		public string Value { get; internal set; }
		public bool IsMatch { get; internal set; }
		public bool IsExactMatch {get; internal set; }

		public Pair(Word word)
		{
			Word = word;
		}
	}
}
