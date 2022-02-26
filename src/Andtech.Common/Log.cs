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

	public static class Log
	{
		public static Verbosity Verbosity
		{
			get => Out.Verbosity;
			set
			{
				Out.Verbosity = value;
				Error.Verbosity = value;
			}
		}

		public static LogWriter Out { get; private set; } = new LogWriter(Console.Out);
		public static LogWriter Error { get; private set; } = new LogWriter(Console.Error);

		/// <inheritdoc cref="LogWriter.Write(Verbosity)" />
		public static void Write(Verbosity verbosity = default) => Write(string.Empty, Console.ForegroundColor, verbosity);

		/// <inheritdoc cref="LogWriter.Write(object, Verbosity)" />
		public static void Write(object message, Verbosity verbosity = default) => Write(message, Console.ForegroundColor, verbosity);

		/// <inheritdoc cref="LogWriter.Write(object, ConsoleColor, Verbosity)" />
		public static void Write(object message, ConsoleColor foregroundColor, Verbosity verbosity = default) => Out.Write(message, foregroundColor, verbosity);

		/// <inheritdoc cref="LogWriter.WriteLine(Verbosity)" />
		public static void WriteLine(Verbosity verbosity = default) => WriteLine(string.Empty, Console.ForegroundColor, verbosity);

		/// <inheritdoc cref="LogWriter.WriteLine(object, Verbosity)" />
		public static void WriteLine(object message, Verbosity verbosity = default) => WriteLine(message, Console.ForegroundColor, verbosity);

		/// <inheritdoc cref="LogWriter.WriteLine(object, ConsoleColor, Verbosity)" />
		public static void WriteLine(object message, ConsoleColor foregroundColor, Verbosity verbosity = default) => Out.WriteLine(message, foregroundColor, verbosity);
	}
}
