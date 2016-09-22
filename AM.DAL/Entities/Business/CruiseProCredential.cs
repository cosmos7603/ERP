using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CruiseProCredential : AuditableEntity
    {
        [Key, Column(Order = 0)]
        public string CounselorLogin { get; set; }
        [Key, Column(Order = 1)]
        public string AuSyncid { get; set; }
        public string CredentialType { get; set; }
        public bool Active { get; set; }
    }
}
