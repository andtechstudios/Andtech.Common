using System;

namespace Andtech.Common
{

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
