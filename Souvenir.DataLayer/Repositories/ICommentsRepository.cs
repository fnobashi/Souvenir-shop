using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Souvenir.DataLayer
{
    public interface ICommentsRepository
    {
        Task<Result> AddCommentAsync(Comments comment, int? ParentId);
        Task<IEnumerable<Comments>> GetCommentsAsync(int postId, int? parentId);

    }
}

