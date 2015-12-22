using System;

namespace Oblqo
{
    [Flags]
    public enum AccountFileStates
    {
        New = 1,
        Deleted = 2,
        UnsyncronizedWithDrive = 4,
        UnsyncronizedWithStorage = 8,
        SyncronizedWithDrive = 16,
        SyncronizedWithStorage = 32,
    }
}
