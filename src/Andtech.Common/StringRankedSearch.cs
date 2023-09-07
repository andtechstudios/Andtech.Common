using System.Collections.Generic;
using System.Linq;

namespace Andtech.Common
{
	public class StringRankedSearch
	{
		private RankedSearch<Node> search;

		public StringRankedSearch(string query)
		{
			search = new RankedSearch<Node>(query);
		}

		public bool Search(IEnumerable<string> options, out string item)
		{
			var success = search.Search(options.Select(x => new Node(x)), out var node);
			item = node.Text;

			return success;
		}

		public class Node : ISearchable
		{
			public string Text { get; internal set; }

			public Node(string text)
			{
				Text = text;
			}
		}
	}
}
