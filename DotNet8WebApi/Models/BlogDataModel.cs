using NUlid;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNet8WebApi.Models
{
    [Table("Blog")]
    public class BlogDataModel
    {
        [Key]
        [Column("Blog_Id")]
        public string Blog_Id { get; set; }
        public string? Blog_Title { get; set; }
        public string? Blog_Author { get; set; }
        public string? Blog_Content { get; set; }

    }
    public class BlogListModel
    {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public List<BlogDataModel> Data { get; set; }
       

    }
}
