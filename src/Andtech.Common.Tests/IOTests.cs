using NUnit.Framework.Internal;

namespace Andtech.Common.Tests
{

	public class IOTests
	{

		[Test]
		public void FileRename()
		{
			Assert.That(ShellUtil.GetDestinationPath("TestFiles/f.txt", "TestFiles/g.txt"), Is.SamePath("TestFiles/g.txt"));
		}
		[Test]
		public void FileToDirectory()
		{
			Assert.That(ShellUtil.GetDestinationPath("TestFiles/f.txt", "TestFiles/a"), Is.SamePath("TestFiles/a/f.txt"));
		}
		[Test]
		public void FileToNonExistingDirectory()
		{
			Assert.That(ShellUtil.GetDestinationPath("TestFiles/f.txt", "TestFiles/c/"), Is.SamePath("TestFiles/c/f.txt"));
		}
		[Test]
		public void DirectoryRename()
		{
			Assert.That(ShellUtil.GetDestinationPath("TestFiles/a", "TestFiles/c"), Is.SamePath("TestFiles/c"));
		}
		[Test]
		public void DirectoryToDirectory()
		{
			Assert.That(ShellUtil.GetDestinationPath("TestFiles/a", "TestFiles/b"), Is.SamePath("TestFiles/b/a"));
		}
		[Test]
		public void DirectoryToNonExistingDirectory()
		{
			Assert.That(ShellUtil.GetDestinationPath("TestFiles/a", "TestFiles/c/"), Is.SamePath("TestFiles/c/a"));
		}
	}
}