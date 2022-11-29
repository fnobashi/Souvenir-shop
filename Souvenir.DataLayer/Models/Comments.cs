using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Souvenir.DataLayer
{
    public class Comments
    {
        [Key]
        public int CommentId  { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public int SouvenirID { get; set; }

        // relations

        public virtual Comments ParentCommnet { get; set; } 
        public int? ParentCommentId { get; set; }


        public virtual ICollection<Comments> CommentReplies { get; set; } 
        public virtual ApplicationUser User { get; set; }
        public virtual Souvenirs Souvenir { get; set; }

        public Comments()
        {

        }

    }
}
