using DemoCommon.Exceptions;
using DemoCommon.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoCommon;
using Microsoft.EntityFrameworkCore;

namespace DemoService
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAllComment(int postId);
        Task<Comment> GetComment(int? id);
        Task<Comment> PostComment(Comment post);
        void DeleteComment(int commentId,int userId);
    }
    public class CommentService:ICommentService
    {
        private readonly ApplicationDbContext _context;
        public CommentService(ApplicationDbContext context)
        {
                _context = context;
        }
        public async Task<List<Comment>> GetAllComment(int postId)
        {         
           var result = await _context.Comments.Include(c=>c.User).Where(x=>x.Post.PostId==postId).ToListAsync();           
            return result;
        }
        public async Task<Comment> GetComment(int? id)
        {
            var entity = await _context.Comments.Include(c => c.User).FirstOrDefaultAsync(x => x.CommentId == id)
                    ?? throw new NotFoundException($"Id:{id} không tồn tại !");

            return entity;
        }
        public async Task<Comment> PostComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
        public void DeleteComment(int commentId, int userId)
        {
            var comment = _context.Comments.FirstOrDefault(c=>c.CommentId==commentId && c.UserId==userId);

            if (comment == null)
            {
                throw new NotFoundException("Id không tồn tại");
            }

            _context.Entry(comment).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
