using DotNet8WebApi.EFDbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NUlid;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNet8WebApi.Features.Blogs
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController(AppDbContext context, ILogger<BlogController> logger) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        private readonly ILogger<BlogController> _logger = logger;
        private readonly List<BlogFakerModel> FData = new List<BlogFakerModel>();

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult BlogList()
        {
            //List<BlogFakerModel> blogList = FData.ToList();
            List<BlogDataModel> blogList = _context.Data.ToList();
            _logger.LogInformation("GetBlogList =>" + JsonConvert.SerializeObject(blogList, Formatting.Indented));
            return Ok(blogList);
        }

        [Authorize(Roles = "User")]
        [HttpGet("{id}")]
        public IActionResult BlogList(string id)
        {
            var item = _context.Data.FirstOrDefault(x => x.Blog_Id == id);
            if (item == null)
            {
                BlogDataResponseModel model1 = new BlogDataResponseModel
                {
                    IsSuccess = false,
                    Message = "No Data Found"
                };
                return NotFound(model1);
            }

            BlogDataResponseModel model = new BlogDataResponseModel
            {
                IsSuccess = true,
                Message = "Success.",
                Data = item
            };
            return Ok(model);
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public IActionResult GetByPagination(int pageNo, int pageSize)
        {
            List<BlogDataModel> lst = _context
                .Data
                .OrderByDescending(a => a.Blog_Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            BlogResponseModel model = new BlogResponseModel
            {
                PageNo = pageNo,
                PageSize = pageSize,
                Data = lst
            };
            return Ok(model);
        }

        //[HttpPost]
        //[ActionName("CreateBlog")]
        //public IActionResult CreateBlog(int count)
        //{
        //    var blogFaker = new BlogFakerExample();

        //    var blogs = blogFaker.Generate(count);
        //    FData.AddRange(blogs);
        //    Console.WriteLine(JsonConvert.SerializeObject(blogs, Formatting.Indented));
        //    return Ok(blogs);
        //}

        [HttpPost]
        public async Task<IActionResult> BlogCreate(BlogViewModel reqModel)
        {
            var blogModel = new BlogDataModel
            {
                Blog_Title = reqModel.Blog_Title,
                Blog_Author = reqModel.Blog_Author,
                Blog_Content = reqModel.Blog_Content,
                Blog_Id = Ulid.NewUlid().ToString()
            };

            await _context.Data.AddAsync(blogModel);
            var result = await _context.SaveChangesAsync();
            var model = new BlogDataResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Saving Successful." : "Saving Failed.",
                Data = blogModel
            };
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> BlogUpdate(string id, BlogDataModel reqModel)
        {
            var item = _context.Data.FirstOrDefault(x => x.Blog_Id == id);
            if (item == null)
            {
                BlogDataResponseModel model1 = new BlogDataResponseModel
                {
                    IsSuccess = false,
                    Message = "No Data Found"
                };
                return NotFound(model1);
            }
            item.Blog_Id = id;
            item.Blog_Title = reqModel.Blog_Title;
            item.Blog_Author = reqModel.Blog_Author;
            item.Blog_Content = reqModel.Blog_Content;
            var result = await _context.SaveChangesAsync();
            BlogDataResponseModel model = new BlogDataResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Updating Successful" : "Updating Failed.",
                Data = reqModel
            };
            return Ok(model);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> BlogPatch(string id, BlogDataModel reqModel)
        {
            var item = _context.Data.FirstOrDefault(x => x.Blog_Id == id);
            if (item == null)
            {
                BlogDataResponseModel model1 = new BlogDataResponseModel
                {
                    IsSuccess = false,
                    Message = "No Data Found"
                };
                return NotFound(model1);
            }
            if (!string.IsNullOrEmpty(reqModel.Blog_Title))
            {
                item.Blog_Title = reqModel.Blog_Title;
            }
            if (!string.IsNullOrEmpty(reqModel.Blog_Author))
            {
                item.Blog_Author = reqModel.Blog_Author;
            }
            if (!string.IsNullOrEmpty(reqModel.Blog_Content))
            {
                item.Blog_Content = reqModel.Blog_Content;
            }
            var result = await _context.SaveChangesAsync();
            BlogDataResponseModel model = new BlogDataResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Updating Successful" : "Updating Failed.",
                Data = reqModel
            };
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> BlogDelete(string id)
        {
            var item = _context.Data.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }
            _context.Data.Remove(item);
            var result = await _context.SaveChangesAsync();
            return Ok(result > 0 ? "Deleting Successful." : "Deleting Failed.");
        }
    }
}
