using System;
using System.IO;
using Xunit;


namespace ContractSystemsTask.Tests
{
    public class ContractSystemsTaskTest
    {
        [Fact]
        public void TestForDirectptyAnalyzer()
        {
            // Arrange
            var testDirectoryName = CreateTestDirectory();
            var testAnalyzer = new DirectoryAnalyzer(testDirectoryName);

            // Act
            var testDirStructure =
                testAnalyzer.AnalyzeDirectoryStructure(testDirectoryName, 0, new(), new());

            //Assert
            Assert.Equal(2, testDirStructure.Count);
            Assert.Equal(57, testDirStructure[0].FolderSize);
            Assert.Equal("text/plain", testDirStructure[1].Files[0].MimeType);
        }

        [Fact]
        public void TestForMimeInfoAnalyzer()
        {
            var testDirectoryName = CreateTestDirectory();
            var testAnalyzer = new DirectoryAnalyzer(testDirectoryName);

            // Act
            var testDirStructure =
                testAnalyzer.AnalyzeDirectoryStructure(testDirectoryName, 0, new(), new());
            var TestMimeInfo = testAnalyzer.AnalyzeMimeTypes(testDirStructure);
            
            var rootMimeInfo = TestMimeInfo[0];

            // Assert
            Assert.Single(TestMimeInfo);
            Assert.Equal(28.5, rootMimeInfo.AverageFileSize, 3);
            Assert.Equal(100, rootMimeInfo.Percentage, 3);
            Assert.Equal(2, rootMimeInfo.Quantity);
            Assert.Equal("text/plain", rootMimeInfo.MimeType);
        }

        private string CreateTestDirectory()
        {
            var testDirectory = new DirectoryInfo("TestDirectory");
            var testSubDirectory = new DirectoryInfo("TestDirectory/Subdirectory");

            if (testDirectory.Exists) testDirectory.Delete(recursive:true);

            testDirectory.Create();
            testSubDirectory.Create();

            var testDirectoryFile = new FileInfo("TestDirectory/a.txt");
            using (StreamWriter sw1 = testDirectoryFile.AppendText())
            {
                sw1.WriteLine("This");
                sw1.WriteLine("is Extra");
                sw1.WriteLine("Text");
            }


            var testSubdirectoryFile = new FileInfo("TestDirectory/Subdirectory/b.txt");
            using (StreamWriter sw2 = testSubdirectoryFile.AppendText())
            {
                sw2.WriteLine("This");
                sw2.WriteLine("is Subdirectory Extra");
                sw2.WriteLine("Text");
            }

            return testDirectory.FullName;
        }
    }
}
