using NUnit.Framework;
using ReadFileService.Models;
using ReadFileService.Services;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TestFileProcessing
{
    public class Tests
    {
        private ReadTXTText actualObject = new ReadTXTText();

        [SetUp]
        public void Setup()
        {
        }

        #region TestFile

        [Test]
        public async Task ReadTextFileTestAsync()
        {
            try
            {
                string expected = File.ReadAllText(".\\TestFiles\\TestFile.txt");
                var actual = await actualObject.ReadText("TestFile.txt");
                Assert.AreEqual(expected, actual.Content);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        [Test]
        public async Task ReadTextNoFileTestAsync()
        {
            ReadTXTText actualObj = new ReadTXTText();

            var actual = await actualObj.ReadText("FileNotExist.txt");
            Assert.AreEqual(actual.Content, new ReturnInfo<string>(new HttpError(404, "Can't find file")).Content);
        }

        #endregion TestFile

        #region TextTextProcessing

        [Test]
        public void TextTest()
        {
            var actual = actualObject.ProcessText("Go! do, that thing that    you do so well????");
            Assert.AreEqual(actual.Content,
                new Dictionary<string, int>()
            {
                { "Go",1 },
                { "do",2 },
                { "that",2 },
                { "thing",1 },
                { "you",1 },
                { "so",1 },
                { "well",1 }
            });
        }

        [Test]
        public void TextEmptyTextTest()
        {
            var actual = actualObject.ProcessText("! , ???? &@#$%");
            Assert.AreEqual(actual.Content, new Dictionary<string, int>());
        }

        #endregion TextTextProcessing
        [Test]
        public async Task CreateFileTestAsync()
        {
            try
            {
                ProcessFileDataService service = new ProcessFileDataService();
                service.CreateFile(new CreateFileRequest
                {
                    Name = "TestingFile.txt",
                    Text = "Text Text"
                });
                var actual = await actualObject.ReadText("TestingFile.txt");
                Assert.AreEqual(actual.Content, "Text Text");
            }
            catch (System.Exception ex )
            {

                throw;
            }
            
        }
    }
}