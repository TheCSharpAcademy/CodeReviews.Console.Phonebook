using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.kjanos89
{
    public class Validation
    {
        public bool CheckString(string str)
        {
            return !String.IsNullOrEmpty(str);
        }
    }
}
