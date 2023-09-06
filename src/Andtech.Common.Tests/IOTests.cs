using NUnit.Framework.Internal;

namespace Andtech.Common.Tests
{

	public class IOTests
	{

		[Test]
		public void FileRename()
		{
			Assert.That(ShellUtil.GetDestinationPath("f.txt", "g.txt"), Is.SamePath("g.txt"));
		}
		[Test]
		public void FileToDirectory()
		{
			Assert.That(ShellUtil.GetDestinationPath("f.txt", "a"), Is.SamePath("a/f.txt"));
		}
		[Test]
		public void FileToNonExistingDirectory()
		{
			Assert.That(ShellUtil.GetDestinationPath("f.txt", "c/"), Is.SamePath("c/f.txt"));
		}
		[Test]
		public void DirectoryRename()
		{
			Assert.That(ShellUtil.GetDestinationPath("a", "c"), Is.SamePath("c"));
		}
		[Test]
		public void DirectoryToDirectory()
		{
			Assert.That(ShellUtil.GetDestinationPath("a", "b"), Is.SamePath("b/a"));
		}
		[Test]
		public void DirectoryToNonExistingDirectory()
		{
			Assert.That(ShellUtil.GetDestinationPath("a", "c/"), Is.SamePath("c/a"));
		}
	}
}