using Blog_CoreLayer.DTOs.Post;
using Blog_CoreLayer.Mappers;
using Blog_CoreLayer.Services.FileManager;
using Blog_CoreLayer.Utilities;
using Blog_DataLayer.Context;
using CodeYad_Blog.CoreLayer.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Blog_CoreLayer.Services.Posts;

public class PostService:IPostService
{
    private readonly BlogDbContext _context;
    private readonly IFileManager _fileManager;

    public PostService(BlogDbContext context, IFileManager fileManager)
    {
        _context = context;
        _fileManager = fileManager;
    }

    public OperationResult CreatePost(CreatePostDto command)
    {
        if(command.ImageFile ==null)
            return OperationResult.Error();

        var post = PostMapper.MapCreatePostDtoToPost(command);

        if (IsSlugExist(post.Slug))
            return OperationResult.Error("Slug تکراری است");
        
        post.ImageName = _fileManager.SaveImageAndReturnImageName(command.ImageFile, Directories.PostImageDirectory);
        _context.Add(post);
        _context.SaveChanges();
        return OperationResult.Success();
    }

    public OperationResult EditPost(EditPostDto command)
    {

        var post = _context.Posts.FirstOrDefault(it => it.Id == command.PostId);
        var oldImage = post.ImageName;

        if (post == null)
            return OperationResult.NotFound();

        var newSlug = command.Slug.ToSlug();

        if (post.Slug != newSlug)
            if (IsSlugExist(newSlug))
                return OperationResult.Error("Slug تکراری است");

        PostMapper.EditPostByMapper(command , post);

        if (command.ImageName != null)
        {
            post.ImageName = _fileManager.SaveImageAndReturnImageName(command.ImageName, Directories.PostImageDirectory);
        }

        _context.SaveChanges();

        if (command.ImageName != null)
        {
            _fileManager.DeleteImageFile(oldImage , Directories.PostImageDirectory);
        }
        return OperationResult.Success();
    }

    public PostDto GetPostById(int postId)
    {
        var post = _context.Posts
            .Include(d=>d.Category)
            .Include(d=>d.SubCategory)
            .FirstOrDefault(it => it.Id == postId);
         return PostMapper.MapToDto(post);
    }

    public PostDto GetPostBySlug(string slug)
    {
        var post = _context.Posts
            .Include(d => d.Category)
            .Include(d => d.SubCategory)
            .Include(m=>m.User)
            .FirstOrDefault(it => it.Slug == slug);
        if (post ==null)
        {
            return null;
        }
        return PostMapper.MapToDto(post);
    }

    public PostFilterDto GetPostFilterByP(PostFilterParams filterParams)
    {
        var result = _context.Posts
            .Include(d=>d.Category)
            .Include(d=>d.SubCategory)
            .OrderByDescending(it => it.CreateDate).AsQueryable();

        if (!string.IsNullOrWhiteSpace(filterParams.CategorySlug))
        {
          result =  result.Where(its => its.Category.Slug == filterParams.CategorySlug || its.SubCategory.Slug == filterParams.CategorySlug);
        }

        if (!string.IsNullOrWhiteSpace(filterParams.Title))
        {
            result = result.Where(its => its.Title == filterParams.Title);
        }

        var skip = (filterParams.PageId - 1) * filterParams.Take;
        var pageCount = result.Count() / filterParams.Take;
        var model = new PostFilterDto()
        {
            Posts = result
                .Skip(skip).Take(filterParams.Take)
                .Select(post => PostMapper.MapToDto(post)).ToList(),
                FilterParams = filterParams,
          
        };
        model.GeneratePaging(result,filterParams.Take,filterParams.PageId);
        return model;
    }

    public bool IsSlugExist(string slug)
    {
       return _context.Posts.Any(it => it.Slug == slug.ToSlug());
    }

    public List<PostDto> GetRelatedPost(int categoryId)
    {
        return _context.Posts
            .Where(it => it.Category_Id == categoryId || it.SubCategory_Id == categoryId)
            .OrderByDescending(o => o.CreateDate)
            .Take(6).Select(post => PostMapper.MapToDto(post)).ToList();
    }

    public List<PostDto> GetPopularPost()
    {
        return _context.Posts
            .Include(u=> u.User)
            .OrderByDescending(o => o.Visit)
            .Take(5).Select(post => PostMapper.MapToDto(post)).ToList();
    }

    public void IncreaseVisit(int postId)
    {
        var post = _context.Posts.First(it => it.Id == postId);
        post.Visit += 1;
        _context.SaveChanges();
    }
}