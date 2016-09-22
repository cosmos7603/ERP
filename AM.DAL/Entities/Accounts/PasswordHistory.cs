using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public partial class PasswordHistory
    {
        [Key]
        public int PasswordHistoryId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime PasswordDate { get; set; }
    }
}
