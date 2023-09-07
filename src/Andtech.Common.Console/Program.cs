// See https://aka.ms/new-console-template for more information

using Andtech.Common;

public class Program
{

	class SongName : ISearchable
	{
		public string keywords;

		public SongName(string keywords)
		{
			this.keywords = keywords;
		}

		string ISearchable.Text => keywords;
	}

	public static void Main(string[] args)
	{
		var search = new StringRankedSearch(args[0]);
		var hasResult = search.Search(args.Skip(1), out var best);

		if (hasResult)
		{
			Log.WriteLine(best, ConsoleColor.Green);
		}
		else
		{
			Log.WriteLine("NO MATCH", ConsoleColor.Red);
		}
	}
}

