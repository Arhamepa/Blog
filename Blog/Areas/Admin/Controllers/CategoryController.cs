using Blog.Areas.Admin.Models.Categories;
using Blog_CoreLayer.DTOs;
using Blog_CoreLayer.Services.Category;
using CodeYad_Blog.CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Areas.Admin.Controllers
{

    public class CategoryController : AdminBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View(_categoryService.GetAllCategories());
        }

        public IActionResult Create(int? parentId)
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(int? parentId,CreateCategoryViewModel createViewModel)
        {
            createViewModel.ParentId = parentId;
            var category = _categoryService.CreateCategory(createViewModel.MapToDto());
            if (category.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(createViewModel.Slug), category.Message);
                return View();
            }
            return RedirectToAction("Index");
        }
        
        public IActionResult Edit(int id)
        {
            var category = _categoryService.CategoryGetBy(id);
            if (category == null)
            {
                RedirectToAction("Index");
            }

            var model = new EditCategoryViewModel()
            {
                Title = category.Title,
                Slug = category.Slug,
                MetaTag = category.MetaTag,
                MetaDescription = category.MetaDescription
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id , EditCategoryViewModel editModel)
        {
            var res = _categoryService.CategoryEdit(new EditCategoryDto()
            {
                Id = editModel.Id,
                Title = editModel.Title,
                Slug = editModel.Slug,
                MetaDescription = editModel.MetaDescription,
                MetaTag = editModel.MetaTag,
                
            });
            if (res.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(editModel.Slug),res.Message);
                return View();
            }
            return RedirectToAction("Index");
        }

        public IActionResult GetSubCategories(int parentId)
        {
            var result = _categoryService.GetSubCategories(parentId);

            return new JsonResult(result);
        }
    }

}
