using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Services.ActivityHistory
{
    public abstract class ItemBase
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
        public abstract void Process(object _oldInstance, object _newInstance, List<string> changeCollection);
    }
}
