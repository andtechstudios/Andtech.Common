using System;

namespace Andtech.Common
{

	public class DryRun
	{
		public static bool IsDryRun { get; set; } = false;

		public static bool TryExecute(Action action)
		{
			if (IsDryRun)
			{
				return false;
			}

			action();
			return true;
		}

		public static bool TryExecute(Action action, string message = null, Verbosity verbosity = Verbosity.normal)
		{
			if (IsDryRun)
			{
				Log.WriteLine(message, verbosity);
			}
			else
			{
				Log.WriteLine($"[DRY RUN] {message}", verbosity);
			}

			return TryExecute(action);
		}

		public static bool TryExecute(Action action, string message = null, ConsoleColor color = default, Verbosity verbosity = Verbosity.normal)
		{
			if (IsDryRun)
			{
				Log.WriteLine(message, color, verbosity);
			}
			else
			{
				Log.WriteLine($"[DRY RUN] {message}", color, verbosity);
			}

			return TryExecute(action);
		}
	}
}
