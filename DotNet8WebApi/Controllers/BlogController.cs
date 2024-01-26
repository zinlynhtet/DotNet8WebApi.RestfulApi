using DotNet8WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using NUlid;
using System.Linq;
using System.Reflection.Metadata;

namespace DotNet8WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext.AppDbContext _context;

        public BlogController(AppDbContext.AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult BlogList()
        {
            List<BlogDataModel> blogList = _context.Data.ToList();
            return Ok(blogList);
        }

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

        [HttpPost]
        public async Task<IActionResult> BlogCreateAsync(BlogDataModel reqModel)
        {
            reqModel.Blog_Id = Ulid.NewUlid().ToString();
            await _context.Data.AddAsync(reqModel);
            var result = await _context.SaveChangesAsync();
            var model = new BlogDataResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Saving Successful." : "Saving Failed.",
                Data = reqModel
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
