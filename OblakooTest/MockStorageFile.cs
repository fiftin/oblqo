using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oblakoo;

namespace OblakooTest
{
    class MockStorageFile : StorageFile
    {
        public override string Id
        {
            get { throw new NotImplementedException(); }
        }

        public override string Name
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsFolder
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsRoot
        {
            get { throw new NotImplementedException(); }
        }
    }
}
