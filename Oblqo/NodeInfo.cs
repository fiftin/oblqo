namespace Oblqo
{
    /// <summary>
    /// Type of node in left side tree view.
    /// </summary>
    public enum NodeType
    {
        Account,
        Folder
    }

    /// <summary>
    /// Information about file or directory associated with tree vew node and file list view item.
    /// </summary>
    public class NodeInfo
    {
        public NodeInfo(AccountInfo accountInfo)
        {
            Type = NodeType.Account;
            AccountInfo = accountInfo;
            AccountName = AccountInfo.AccountName;
        }

        public NodeInfo(AccountFile file, string accountName)
        {
            Type = NodeType.Folder;
            File = file;
            AccountName = accountName;
        }

        public NodeType Type { get; private set; }
        public AccountFile File { get; set; }
        public AccountInfo AccountInfo { get; private set; }
        public string AccountName { get; private set; }
    }
}
