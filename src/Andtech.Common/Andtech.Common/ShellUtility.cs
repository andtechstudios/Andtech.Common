
namespace Andtech.Common
{

	internal class ShellUtility
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
	}
}
