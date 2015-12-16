using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oblqo.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OblqoTest
{
    [TestClass]
    public class GoogleDriveTest
    {
        [TestMethod]
        public async Task TestGetFileAsync()
        {
            var env = await TestEnvironment.CreateSimpleAsync();
            GoogleDrive drive = await GoogleDrive.CreateInstance(env.Account, null, "", CancellationToken.None);
            var xml = new XElement("file");
            xml.SetAttributeValue("fileId", "my-file");
            var file = await drive.GetFileAsync(xml, CancellationToken.None);
        }
    }
}
