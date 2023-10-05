using DemoCommon.Models;
using DemoCommon.ReqModels;
using DemoCommon.ResModels;
using DemoService;
using DemoService.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI2.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IFileService _fileService;
        private readonly ApplicationDbContext _context;
        public PostController(IPostService postService,IFileService fileService,ApplicationDbContext context)
        {
            _postService = postService;
            _fileService = fileService;
            _context = context;
        }

        [HttpGet]
        [Route("getAllPost")]
        public async Task<List<PostResponse>> GetAllPost(string keyword)
        {
            var data = await _postService.GetAllPost(keyword);
            var result = data.Select(p => new PostResponse(p)).OrderByDescending(p=>p.CreatedDateTime).ToList();
            return result;
        }

        [HttpGet]
        [Route("getAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            var data = await _postService.GetAllProduct();
            return Ok(data);
        }

        [HttpGet]
        [Route("getById/{id?}")]
        public async Task<Product> GetPost(int? id)
        {
            var data = await _postService.GetPost(id);
            return data;
        }

        //[HttpPost]
        //[Route("addPost")]
        
        //public async Task<PostResponse> AddPost([FromForm] Post post)
        //{
        //    var fileResult = _fileService.SaveFile(post.ImageFile);
        //    if(fileResult.Item1==1)
        //    {
        //        post.PostImage = fileResult.Item2;
        //    }
        //    post.CreatedDateTime = DateTime.Now;
        //    var data = await _postService.AddPost(post);

        //    var result = await GetPost(data.PostId);

        //    return new PostResponse(result);
        //}

        [HttpDelete("{id}")]
        public JsonResult Deletepost(int id)
        {
            _postService.DeletePost(id);
            return new JsonResult("Deleted Successfully");
        }

        //Err
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.Id = id;
            existingProduct.Name = product.Name;
            existingProduct.Version = product.Version;
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product post)
        //{
        //    var existingProduct = await _context.Products.FindAsync(id);
        //    if (existingProduct == null)
        //    {
        //        return NotFound();
        //    }
        //    if (post.Version != existingProduct.Version)
        //    {
        //        return Conflict();
        //    }
        //    existingProduct.Name = post.Name;
        //    existingProduct.Version++;
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}

        ////Cách 2: use Transition
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product post)
        //{
        //    using (var transaction = _context.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            var existingProduct = await _context.Products.FindAsync(id);
        //            if (existingProduct == null)
        //            {
        //                return NotFound();
        //            }
        //            if (post.Version != existingProduct.Version)
        //            {
        //                return Conflict();
        //            }
        //            existingProduct.Name = post.Name;
        //            existingProduct.Version++;
        //            await _context.SaveChangesAsync();
        //            transaction.Commit();
        //            return Ok();
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //    }
        //}
        [HttpPost("{postId}/like/{userId}")]
        public async Task<IActionResult> LikePost(int postId, int userId)
        {
            var post = await _postService.LikePost(postId,userId);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPut("{id}/unlike")]
        public async Task<IActionResult> UnlikePost(int id)
        {
            var post = await _postService.UnlikePost(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }
        [HttpGet]
        [Route("getUserLiked/{id}")]
        public async Task<List<UserResponse>> GetUserLiked(int id)
        {
            var data = await _postService.GetUserLiked(id);
           var result = data.Select(p=>new UserResponse(p)).ToList();
            return result;
        }
        [HttpGet("{postId}/hasliked/{userId}")]
        public IActionResult HasUserLikedPost(int postId, int userId)
        {
            var hasLiked = _postService.HasUserLikedPost(postId, userId);
            return Ok(new { HasLiked = hasLiked });
        }
    }
}
