using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oblqo;
using Oblqo.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OblqoTest
{
    [TestClass]
    public class SynchronizeFileTaskTest
    {
        [TestMethod]
        public async Task TestToXml()
        {
            var env = await TestEnvironment.CreateSimpleAsync();
            var sourceFile = await env.GetFileByFullPathAsync("/photos2015/PHOTO1.jpg");
            var task = new SynchronizeFileTask(env.Account, "", 10, new AsyncTask[0], sourceFile);
            var xml = task.ToXml();
            var xmlSourceFile = xml.Element("sourceFile");
            var xmlDriveFiles = xmlSourceFile.Element("driveFiles");
            Assert.AreEqual(
                sourceFile.DriveFiles[0].Id,
                xmlDriveFiles.Elements().First().Attribute("id").Value);
            var xmlStorageFile = xmlSourceFile.Element("storageFile");
            Assert.IsNotNull(xmlStorageFile);
        }
    }
}
