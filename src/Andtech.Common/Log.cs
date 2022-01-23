using System;

namespace Andtech.Common
{

	public static class Log
	{
		public static Verbosity Verbosity { get; set; } = default;

		/// <summary>
		/// Writes a blank line if <paramref name="verbosity"/> is enabled.
		/// </summary>
		/// <param name="verbosity">The minimum verbosity required.</param>
		public static void WriteLine(Verbosity verbosity = default) => WriteLine(string.Empty, Console.ForegroundColor, verbosity);

		/// <summary>
		/// Writes the text representation of <paramref name="message"/> if <paramref name="verbosity"/> is enabled.
		/// </summary>
		/// <param name="message">The message to print.</param>
		/// <param name="verbosity">The minimum verbosity required.</param>
		public static void WriteLine(object message, Verbosity verbosity = default) => WriteLine(message, Console.ForegroundColor, verbosity);

		/// <summary>
		/// Writes the text representation of <paramref name="message"/> if <paramref name="verbosity"/> is enabled.
		/// </summary>
		/// <param name="message">The message to print.</param>
		/// <param name="foregroundColor">The color of the foreground.</param>
		/// <param name="verbosity">The minimum verbosity required.</param>
		public static void WriteLine(object message, ConsoleColor foregroundColor, Verbosity verbosity = default)
		{
			if ((int)verbosity <= (int)Verbosity)
			{
				var temp = Console.ForegroundColor;
				Console.ForegroundColor = foregroundColor;
				Console.WriteLine(message);
				Console.ForegroundColor = temp;
			}
		}

		public static class Error
		{

			/// <summary>
			/// Writes a blank line to STDERR if <paramref name="verbosity"/> is enabled.
			/// </summary>
			/// <param name="verbosity">The minimum verbosity required.</param>
			public static void WriteLine(Verbosity verbosity = default) => WriteLine(string.Empty, Console.ForegroundColor, verbosity);

			/// <summary>
			/// Writes the text representation of <paramref name="message"/> to STDERR if <paramref name="verbosity"/> is enabled.
			/// </summary>
			/// <param name="message">The message to print.</param>
			/// <param name="verbosity">The minimum verbosity required.</param>
			public static void WriteLine(object message, Verbosity verbosity = default) => WriteLine(message, Console.ForegroundColor, verbosity);

			/// <summary>
			/// Writes the text representation of <paramref name="message"/> to STDERR if <paramref name="verbosity"/> is enabled.
			/// </summary>
			/// <param name="message">The message to print.</param>
			/// <param name="foregroundColor">The color of the foreground.</param>
			/// <param name="verbosity">The minimum verbosity required.</param>
			public static void WriteLine(object message, ConsoleColor foregroundColor, Verbosity verbosity = default)
			{
				if ((int)verbosity <= (int)Verbosity)
				{
					var temp = Console.ForegroundColor;
					Console.ForegroundColor = foregroundColor;
					Console.Error.WriteLine(message);
					Console.ForegroundColor = temp;
				}
			}
		}
	}
}
