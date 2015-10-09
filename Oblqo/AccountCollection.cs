using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblqo
{
    public class AccountCollection
    {
        private readonly SortedDictionary<string, Account> accounts = new SortedDictionary<string, Account>();

        public Account this[string index]
        {
            get
            {
                Account ret;
                if (!accounts.TryGetValue(index, out ret))
                {
                    throw new ArgumentException("Invalid index value: " + index, "index");
                }
                return ret;
            }
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
            return name != null && accounts.Remove(name);
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
            return (from x in accounts where x.Value == account select x.Key).FirstOrDefault();
        }
    }
}
