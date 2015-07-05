using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblakoo
{
    public class AccountCollection
    {
        private readonly SortedDictionary<string, Account> accounts = new SortedDictionary<string, Account>();
        private readonly object locker = new object();

        public Account this[string index]
        {
            get { return accounts[index]; }
        }

        public bool TryGetValue(string name, out Account account)
        {
            return accounts.TryGetValue(name, out account);
        }

        public bool Remove(string name)
        {
            return accounts.Remove(name);
        }

        public bool Remove(Account account)
        {
            var name = GetName(account);
            if (name == null)
            {
                return false;
            }
            return accounts.Remove(name);
        }

        public bool ContainsKey(string name)
        {
            return accounts.ContainsKey(name);
        }

        public void Add(string name, Account account)
        {
            accounts.Add(name, account);
        }

        private string GetName(Account account)
        {
            foreach (KeyValuePair<string, Account> x in accounts)
            {
                if (x.Value == account)
                {
                    return x.Key;
                }
            }
            return null;
        }
    }
}
