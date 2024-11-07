using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne
{
    public class Todo
    {
        public Todo()
        {
            Desc = string.Empty;
        }
        public string Desc {  get; set; }
        public Boolean IsDone { get; set; }
    }
}
