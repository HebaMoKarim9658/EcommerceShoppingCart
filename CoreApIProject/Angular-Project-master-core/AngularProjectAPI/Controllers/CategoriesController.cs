using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AngularProjectAPI.Models;
using AngularProjectAPI.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepository<Category, int, string> CategoryRepository;
        private readonly UserManager<User> UserManagerr;


        public CategoriesController(IRepository<Category, int, string> _categoryRepository, UserManager<User> _UserManager)
        {
            CategoryRepository = _categoryRepository;
            UserManagerr = _UserManager;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            IEnumerable<Category> categories = CategoryRepository.GetAll();
            if (categories.Count() > 0)
                return categories.ToList();
            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory(int id)
        {
            var category = CategoryRepository.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }


        [HttpPut("{id}")]
        public IActionResult PutCategory(int id, Category category)
        {
            if (id != category.CategoryID)
            {
                return BadRequest();
            }
            if (!CategoryExists(id))
            {
                return NotFound();
            }
            CategoryRepository.Update(category);
            return NoContent();
        }


        [HttpPost]
        public ActionResult<Order> PostCategory(Category category)
        {
            CategoryRepository.Add(category);
            return CreatedAtAction("GetCategory", new { id = category.CategoryID }, category);
        }

        [HttpDelete("{id}")]
        public ActionResult<Category> DeleteCategory(int id)
        {
            var category = CategoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            CategoryRepository.Delete(category);
            return category;
        }

        private bool CategoryExists(int id)
        {
            if (CategoryRepository.GetById(id) == null)
                return false;
            return true;
        }
    }
}