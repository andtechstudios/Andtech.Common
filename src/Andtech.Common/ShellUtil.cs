using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Andtech.Common
{

	public static class ShellUtil
	{
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
				var tokens = ParseUtil.QuotedSplit(browser);
				var binary = tokens.FirstOrDefault();
				var arguments = tokens.Skip(1).ToList();
				arguments.Add(url);
				Log.WriteLine($"Binary is: '{binary}'", Verbosity.diagnostic);
				Log.WriteLine($"Arguments are: '{string.Join(", ", arguments)}'", Verbosity.diagnostic);
				Process.Start(binary, arguments);
			}
		}

		public static bool Find(string searchRoot, string query, out string path, FindOptions findOptions = FindOptions.RecursiveDown)
		{
			if (findOptions == FindOptions.RecursiveDown)
			{
				throw new NotImplementedException();
			}

			var dir = new DirectoryInfo(searchRoot);
			while (dir != null)
			{
				var expectedPath = Path.Combine(dir.FullName, query);
				if (Directory.Exists(expectedPath))
				{
					path = expectedPath;
					return true;
				}
				if (File.Exists(expectedPath))
				{
					path = expectedPath;
					return true;
				}

				dir = Directory.GetParent(dir.FullName);
			}

			path = string.Empty;
			return false;
		}

		#region io
		/// <summary>
		/// Returns the suggest UNIX-style path of the given transfer operation.
		/// </summary>
		/// <param name="sourcePath">The name of the file/directory to transfer.</param>
		/// <param name="destPath">The new path of the file/directory.</param>
		public static string GetDestinationPath(string src, string dest)
		{
			var destIsDir = Directory.Exists(dest) || Regex.IsMatch(dest, @"(/|\\)$");
			var destIsFile = File.Exists(dest);

			if (destIsDir)
			{
				return Path.Join(dest, Path.GetFileName(src));
			}

			if (destIsFile)
			{
				return dest;
			}

			return dest;
		}

		/// <summary>
		/// Moves the file or directory to the destination.
		/// </summary>
		/// <param name="sourcePath">The name of the file/directory to move.</param>
		/// <param name="destPath">The new path of the file/directory.</param>
		/// <param name="overwrite">true if the destination can be overwritten; false otherwise.</param>
		public static void Move(string src, string dest, bool parents = false)
		{
			dest = GetDestinationPath(src, dest);
			if (parents)
			{
				Directory.CreateDirectory(Path.GetDirectoryName(dest));
			}
			MoveFileOrDirectory(src, dest, overwrite: true);
		}
		/// <summary>
		/// Copies the file or directory to the destination.
		/// </summary>
		/// <param name="sourcePath">The name of the file/directory to move.</param>
		/// <param name="destPath">The new path of the file/directory.</param>
		/// <param name="overwrite">true if the destination can be overwritten; false otherwise.</param>
		public static void Copy(string src, string dest, bool parents = false)
		{
			dest = GetDestinationPath(src, dest);
			if (parents)
			{
				Directory.CreateDirectory(Path.GetDirectoryName(dest));
			}
			CopyFileOrDirectory(src, dest, overwrite: true);
		}

		/// <summary>
		/// Deletes the file or directory.
		/// </summary>
		/// <param name="sourcePath">The name of the file/directory to delete.</param>
		public static void Delete(string path)
		{
			DeleteFileOrDirectory(path);
		}

		internal static void DeleteFileOrDirectory(string path)
		{
			if (Directory.Exists(path))
			{
				Directory.Delete(path);
			}
			else
			{
				File.Delete(path);
			}
		}
		internal static void MoveFileOrDirectory(string src, string dest, bool overwrite = false)
		{
			if (Directory.Exists(src))
			{
				Directory.Move(src, dest);
			}
			else
			{
				File.Move(src, dest, overwrite);
			}
		}
		internal static void CopyFileOrDirectory(string src, string dest, bool overwrite = false)
		{
			if (Directory.Exists(src))
			{
				CopyDirectory(src, dest, true);
			}
			else
			{
				File.Copy(src, dest, overwrite);
			}
		}
		private static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
		{
			// Get information about the source directory
			var dir = new DirectoryInfo(sourceDir);

			// Check if the source directory exists
			if (!dir.Exists)
				throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

			// Cache directories before we start copying
			DirectoryInfo[] dirs = dir.GetDirectories();

			// Create the destination directory
			Directory.CreateDirectory(destinationDir);

			// Get the files in the source directory and copy to the destination directory
			foreach (FileInfo file in dir.GetFiles())
			{
				string targetFilePath = Path.Combine(destinationDir, file.Name);
				file.CopyTo(targetFilePath, true);
			}

			// If recursive and copying subdirectories, recursively call this method
			if (recursive)
			{
				foreach (DirectoryInfo subDir in dirs)
				{
					string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
					CopyDirectory(subDir.FullName, newDestinationDir, true);
				}
			}
		}
		#endregion
	}
}
