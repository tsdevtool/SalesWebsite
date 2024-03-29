using CoreMVC.Models;
using CoreMVC.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Operations;

namespace CoreMVC.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {

        private readonly IproductRepository _productRepository;

        private readonly IcategoryRepository _categoryRepository;
        public CategoryController(IproductRepository productRepository, IcategoryRepository
        categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        // Hiển thị danh sách sản phẩm
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var categorys = await _categoryRepository.GetAllAsync();
            return View(categorys);
        }
        // Hiển thị form thêm sản phẩm mới
        public async Task<IActionResult> Add()
        {

            return View();
        }
        // Xử lý thêm sản phẩm mới
        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.AddAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Display(int id)

        {
            var cate = await _categoryRepository.GetByIdAsync(id);
            if (cate == null)
            {
                return NotFound();
            }
            return View(cate);
        }
        // Hiển thị form cập nhật sản phẩm
        public async Task<IActionResult> Update(int id)
        {
            var cate = await _categoryRepository.GetByIdAsync(id);
            if (cate == null)
            {
                return NotFound();
            }

            return View(cate);
        }
        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, Category category)
        {

            if (id != category.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingCategory = await _categoryRepository.GetByIdAsync(id);
                existingCategory.Name = category.Name;
                await _categoryRepository.UpdateAsync(existingCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var cate = await _categoryRepository.GetByIdAsync(id);
            if (cate == null)
            {
                return NotFound();
            }
            return View(cate);
        }
        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }

}
