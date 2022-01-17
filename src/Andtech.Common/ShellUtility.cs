using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Andtech.Common
{

	public static class ShellUtility
	{

		/// <summary>
		/// Moves the file or directory to the destination.
		/// </summary>
		/// <param name="sourcePath">The name of the file/directory to move.</param>
		/// <param name="destPath">The new path of the file/directory.</param>
		/// <param name="overwrite">true if the destination can be overwritten; false otherwise.</param>
		public static void Move(string sourcePath, string destPath, bool overwrite = true)
		{
			var attributes = File.GetAttributes(sourcePath);
			if (attributes.HasFlag(FileAttributes.Directory))
			{
				if (Directory.Exists(destPath))
				{
					if (!overwrite)
					{
						throw new IOException($"The destination exists and cannot be overwritten: '{destPath}'");
					}

					Directory.Delete(destPath, true);
				}
				
				Directory.Move(sourcePath, destPath);
			}
			else
			{
				File.Move(sourcePath, destPath, overwrite);
			}
		}

		/// <summary>
		/// Copies the file or directory to the destination.
		/// </summary>
		/// <param name="sourcePath">The name of the file/directory to move.</param>
		/// <param name="destPath">The new path of the file/directory.</param>
		/// <param name="overwrite">true if the destination can be overwritten; false otherwise.</param>
		public static void Copy(string sourcePath, string destPath, bool overwrite = false)
		{
			var attributes = File.GetAttributes(sourcePath);
			if (attributes.HasFlag(FileAttributes.Directory))
			{
				CopyDirectory(sourcePath, destPath, true);
			}
			else
			{
				File.Copy(sourcePath, destPath, overwrite);
			}
		}

		private static void CopyDirectory(string sourceDirName, string destDirName, bool copySubDirs, bool overwrite = true)
		{
			// Get the subdirectories for the specified directory.
			DirectoryInfo dir = new DirectoryInfo(sourceDirName);

			if (!dir.Exists)
			{
				throw new DirectoryNotFoundException($"Source directory does not exist or could not be found: '{sourceDirName}'");
			}

			DirectoryInfo[] dirs = dir.GetDirectories();

			// If the destination directory doesn't exist, create it.       
			Directory.CreateDirectory(destDirName);

			// Get the files in the directory and copy them to the new location.
			FileInfo[] files = dir.GetFiles();
			foreach (FileInfo file in files)
			{
				string tempPath = Path.Combine(destDirName, file.Name);
				file.CopyTo(tempPath, overwrite);
			}

			// If copying subdirectories, copy them and their contents to new location.
			if (copySubDirs)
			{
				foreach (DirectoryInfo subdir in dirs)
				{
					string tempPath = Path.Combine(destDirName, subdir.Name);
					CopyDirectory(subdir.FullName, tempPath, copySubDirs);
				}
			}
		}

		/// <summary>
		/// Run an action in a directory, then switch back.
		/// </summary>
		/// <param name="directory">The directory from which to run in.</param>
		/// <param name="action">The action to perform.</param>
		public static void RunInDirectory(string directory, Action action)
		{
			var tempCurrentDirectory = Environment.CurrentDirectory;
			Environment.CurrentDirectory = directory;
			try
			{
				action();
			}
			catch
			{
				throw;
			}
			finally
			{
				Environment.CurrentDirectory = tempCurrentDirectory;
			}
		}

		/// <summary>
		/// Launches the system web browser using <paramref name="url"/>.
		/// </summary>
		/// <param name="url">The url to load.</param>
		public static void OpenBrowser(string url)
		{
			var browser = Environment.GetEnvironmentVariable("BROWSER");

			if (string.IsNullOrEmpty(browser))
			{
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				{
					url = url.Replace("&", "^&");
					Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
				}
				else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
				{
					Process.Start("xdg-open", url);
				}
				else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
				{
					Process.Start("open", url);
				}
				else
				{
					throw new PlatformNotSupportedException();
				}
			}
			else
			{
				var tokens = ParseUtility.QuotedSplit(browser);
				var binary = tokens.FirstOrDefault();
				var arguments = tokens.Skip(1).ToList();
				arguments.Add(url);
				Log.WriteLine($"Binary is: '{binary}'", Verbosity.diagnostic);
				Log.WriteLine($"Arguments are: '{string.Join(", ", arguments)}'", Verbosity.diagnostic);
				Process.Start(browser, arguments);
			}
		}

	}
}
