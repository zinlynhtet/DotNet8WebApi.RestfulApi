﻿namespace DotNet8WebApi.Features.Blog
{
    public class BlogDataResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public BlogDataModel Data { get; set; }
    }

    public class BlogResponseModel
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public List<BlogDataModel> Data { get; set; }
    }
}
