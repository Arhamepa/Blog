using Blog_CoreLayer.DTOs.Post;
using Blog_CoreLayer.Utilities;

namespace Blog_CoreLayer.DTOs.Post;

using Blog_DataLayer.Entities;
public class PostFilterDto:BasePagination
{
    public List<PostDto> Posts { get; set; }
    public PostFilterParams FilterParams { get; set; }

}

public class PostFilterParams
{
    public string Title { get; set; }
    public int Take { get; set; }
    public int PageId { get; set; }
    public string CategorySlug { get; set; }
}
