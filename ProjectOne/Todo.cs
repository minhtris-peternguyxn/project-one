using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne
{
    public class Todo
    {
        
            public Todo(string desc, bool isDone, DateTime createdDate)
            {
                Desc = desc;
                IsDone = isDone;
                CreatedDate = createdDate;
            }

            public string Desc { get; set; }
            public bool IsDone { get; set; }
            public DateTime CreatedDate { get; set; }
        }
    }
