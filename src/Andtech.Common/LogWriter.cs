using System;
using System.IO;

namespace Andtech.Common
{

	public class LogWriter
	{
		internal Verbosity Verbosity { get; set; }

		private readonly TextWriter writer;

		public LogWriter(TextWriter writer)
		{
			this.writer = writer;
		}

		/// <summary>
		/// Writes a blank line if <paramref name="verbosity"/> is allowed.
		/// </summary>
		/// <param name="verbosity">The minimum verbosity required.</param>
		public void Write(Verbosity verbosity = default) => Write(string.Empty, Console.ForegroundColor, verbosity);

		/// <summary>
		/// Writes the text representation of <paramref name="message"/> if <paramref name="verbosity"/> is allowed.
		/// </summary>
		/// <param name="message">The message to print.</param>
		/// <param name="verbosity">The minimum verbosity required.</param>
		public void Write(object message, Verbosity verbosity = default) => Write(message, Console.ForegroundColor, verbosity);

		/// <summary>
		/// Writes the text representation of <paramref name="message"/> if <paramref name="verbosity"/> is allowed.
		/// </summary>
		/// <param name="message">The message to print.</param>
		/// <param name="foregroundColor">The color of the foreground.</param>
		/// <param name="verbosity">The minimum verbosity required.</param>
		public void Write(object message, ConsoleColor foregroundColor, Verbosity verbosity = default)
		{
			if ((int)verbosity <= (int)Verbosity)
			{
				var temp = Console.ForegroundColor;
				Console.ForegroundColor = foregroundColor;
				writer.Write(message);
				Console.ForegroundColor = temp;
			}
		}

		/// <summary>
		/// Writes a blank line followed by line termination, if <paramref name="verbosity"/> is allowed.
		/// </summary>
		/// <param name="verbosity">The minimum verbosity required.</param>
		public void WriteLine(Verbosity verbosity = default) => WriteLine(string.Empty, Console.ForegroundColor, verbosity);

		/// <summary>
		/// Writes the text representation of <paramref name="message"/> followed by line termination, if <paramref name="verbosity"/> is allowed.
		/// </summary>
		/// <param name="message">The message to print.</param>
		/// <param name="verbosity">The minimum verbosity required.</param>
		public void WriteLine(object message, Verbosity verbosity = default) => WriteLine(message, Console.ForegroundColor, verbosity);

		/// <summary>
		/// Writes the text representation of <paramref name="message"/> followed by line termination, if <paramref name="verbosity"/> is allowed.
		/// </summary>
		/// <param name="message">The message to print.</param>
		/// <param name="foregroundColor">The color of the foreground.</param>
		/// <param name="verbosity">The minimum verbosity required.</param>
		public void WriteLine(object message, ConsoleColor foregroundColor, Verbosity verbosity = default)
		{
			if ((int)verbosity <= (int)Verbosity)
			{
				var temp = Console.ForegroundColor;
				Console.ForegroundColor = foregroundColor;
				writer.WriteLine(message);
				Console.ForegroundColor = temp;
			}
		}
	}
}
