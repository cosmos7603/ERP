using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.DAL
{
    public class EmailAttach
    {
        [Key, Column(Order = 0)]
        public int EmailId { get; set; }
        [Key, Column(Order = 1)]
        public int DataFileId { get; set; }

        [ForeignKey("EmailId")]
        public Email Email { get; set; }

        [ForeignKey("DataFileId")]
        public virtual DataFile DataFile { get; set; }
    }
}
