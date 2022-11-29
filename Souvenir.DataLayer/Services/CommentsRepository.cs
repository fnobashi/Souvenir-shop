using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Souvenir.DataLayer
{
    public class CommentsRepository:ICommentsRepository
    {

        private ApplicationDbContext db;

        public CommentsRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<Result> AddCommentAsync(Comments comment , int? ParentId)
        {
            return await Task.Run(() => {
                try
                {
                    comment.ParentCommnet = db.Comments.Find(ParentId);
                    db.Comments.Add(comment);
                    db.SaveChanges();
                    return Result.Success;
                }
                catch (Exception)
                {
                    return Result.Failiure;
                    
                }
                
            });
        }


        public async Task<IEnumerable<Comments>> GetCommentsAsync(int postId , int? parentId)
        {
            return await Task.Run(() => {

                return db.Comments.Include(c => c.Souvenir)
                .Include(c => c.CommentReplies)
                .Include(c => c.ParentCommnet)
                .Where(c => c.SouvenirID == postId && c.ParentCommentId == parentId);
            });
        }

    }
}
