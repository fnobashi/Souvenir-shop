using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souvenir.ViewModels.Souvenirs
{
    public class CommentsViewModel
    {
        public int CommentId { get; set; }
        public int SouvenirId { get; set; }
        public string CommentText { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsReply { get; set; }
    }
}
