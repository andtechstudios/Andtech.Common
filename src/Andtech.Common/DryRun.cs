﻿using System;

namespace Andtech.Common
{

	public static class DryRun
	{
		public static bool IsDryRun { get; set; } =
			Environment.GetEnvironmentVariable("DRY_RUN").Equals("1")
			|| Environment.GetEnvironmentVariable("DRY_RUN").Equals("TRUE", StringComparison.InvariantCultureIgnoreCase);

		public static bool TryExecute(Action action)
		{
			if (IsDryRun)
			{
				return false;
			}

			action();
			return true;
		}

		public static bool TryExecute(Action action, string message)
			=> TryExecute(action, message, Verbosity.normal);

		public static bool TryExecute(Action action, string message = null, Verbosity verbosity = Verbosity.normal)
		{
			if (IsDryRun)
			{
				Log.WriteLine($"[DRY RUN] {message}", verbosity);
			}
			else
			{
				Log.WriteLine(message, verbosity);
			}

			return TryExecute(action);
		}

		public static bool TryExecute(Action action, string message = null, ConsoleColor color = default, Verbosity verbosity = Verbosity.normal)
		{
			if (IsDryRun)
			{
				Log.WriteLine($"[DRY RUN] {message}", color, verbosity);
			}
			else
			{
				Log.WriteLine(message, color, verbosity);
			}

			return TryExecute(action);
		}
	}
}
