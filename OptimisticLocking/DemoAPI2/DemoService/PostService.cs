using DemoCommon.Exceptions;
using DemoCommon.Models;
using DemoCommon.ReqModels;
using DemoCommon.ResModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DemoService
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPost(string? keyword);
        Task<List<Product>> GetAllProduct();
        Task<Product> GetPost(int? id);
        Task<Post> AddPost(Post post);
        Task<Product> UpdatePost(int id, Product post);
        void DeletePost(int id);
        //Task<List<Post>> Search(string keyword);
        Task<Post> LikePost(int postId,int userId);
        Task<Post> UnlikePost (int id);
        Task<List<User>> GetUserLiked(int postId);
        bool HasUserLikedPost(int postId, int userId);
    }
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;
        public PostService(ApplicationDbContext context)
        {
           _context = context;
        }
        public async Task<List<Post>> GetAllPost(string? keyword)
        {
            //if(!string.IsNullOrEmpty(keyword))
            //    return await _context.Posts.Include(p => p.User).Include(p => p.Group).Where(x => x.Content.ToLower().Contains(keyword.ToLower())).ToListAsync();
            //return await _context.Posts.Include(p => p.User).Include(p => p.Group).ToListAsync();

            var query = _context.Posts.Include(p => p.User).Include(p => p.Group).Include(p=>p.Comments).OrderByDescending(p=>p.CreatedDateTime).AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                var lowerKeyword = keyword.ToLower();
                query = query.Where(x => x.Content.ToLower().Contains(lowerKeyword));
            }

            return await query.ToListAsync();
        }
        public async Task<List<Product>> GetAllProduct()
        {
            //if(!string.IsNullOrEmpty(keyword))
            //    return await _context.Posts.Include(p => p.User).Include(p => p.Group).Where(x => x.Content.ToLower().Contains(keyword.ToLower())).ToListAsync();
            //return await _context.Posts.Include(p => p.User).Include(p => p.Group).ToListAsync();

           
            return await _context.Products.ToListAsync();
        }
        public async Task<Product> GetPost(int? id)
        {
            var entity = await _context.Products.FirstOrDefaultAsync(x => x.Id == id)
                    ?? throw new NotFoundException($"Id:{id} không tồn tại !");

            return entity;
        }
        public async Task<Post> AddPost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }
        public async Task<Product> UpdatePost(int id, Product post)
        {
            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return post;
        }
        public void DeletePost(int id)
        {
            var category = _context.Posts.Find(id);

            if (category == null)
            {
                throw new NotFoundException("Id không tồn tại");
            }

            _context.Entry(category).State = EntityState.Deleted;
            _context.SaveChanges();
        }
        public async Task<Post> LikePost(int postId, int userId)
        {
            var post =  _context.Posts.Find(postId);
            if (post == null)
                throw new NotFoundException("Id không tồn tại");
            if (post.Like==null)
            {
                post.Like = 0;
            }          
                  
            post.Like=post.Like+1;
            var postLike = new PostLike { PostId = postId, UserId = userId };
           _context.PostLikes.Add(postLike);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<Post> UnlikePost(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);
            if (post == null)
                return null;

            if (post.Like > 0)
                post.Like--;

            await _context.SaveChangesAsync();

            return post;
        }
        public async Task<List<User>> GetUserLiked(int postId)
        {
            return await _context.PostLikes.Include(p=>p.User)
                .Where(pl => pl.PostId == postId)
                .Select(pl => pl.User)
                .ToListAsync();
        }
        public bool HasUserLikedPost(int postId, int userId)
        {
            var post = _context.Posts
                .Include(p => p.Likes)
                .FirstOrDefault(p => p.PostId == postId);

            if (post == null)
                return false;

            return post.Likes.Any(user => user.UserId == userId);
        }
    }
}
