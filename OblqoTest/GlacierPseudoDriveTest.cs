using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oblqo.Amazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OblqoTest
{
    [TestClass]
    public class GlacierPseudoDriveTest
    {
        [TestMethod]
        public async Task TestReadFromJson()
        {
            var env = await TestEnvironment.CreateSimpleAsync();
            var doc = new XDocument(new XElement("vault"));
            var drive = new GlacierPseudoDrive(env.Account, "dummy_id", doc, "test");
            var json = "{" +
            "  ArchiveList: [" +
            "    {" +
            "      ArchiveId: \"dummy_archive_id_1\"," +
            "      ArchiveDescription: \"/my/photos/2015/city/car.jpg\"," +
            "      CreationDate: \"2015-10-10 4:43:33\"," +
            "      Size: 1034" +
            "    }," +
            "    {" +
            "      ArchiveId: \"dummy_archive_id_2\"," +
            "      ArchiveDescription: \"/my/photos/2014/home/me.jpg\"," +
            "      CreationDate: \"2014-08-12 3:33:22\"," +
            "      Size: 323" +
            "    }" +
            "  ]" +
            "}";
            drive.ReadFromJson(new System.IO.StringReader(json));
            var mem = new System.IO.MemoryStream();
            await drive.SaveAsync(mem);
            mem.Seek(0, System.IO.SeekOrigin.Begin);
            var xml = Encoding.UTF8.GetString(mem.GetBuffer());
        }
    }
}
