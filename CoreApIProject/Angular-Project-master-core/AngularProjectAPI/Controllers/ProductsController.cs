using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularProjectAPI.Data;
using AngularProjectAPI.Models;
using AngularProjectAPI.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AngularProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product, int,string> ProductReposatory;
       // private readonly UserManager<User> UserManagerr;


        public ProductsController(IRepository<Product, int, string> _ProductReposatory/*, UserManager<User> _UserManager*/)
        {
            ProductReposatory = _ProductReposatory;
            //UserManagerr = _UserManager;
        }
        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            //var user = await UserManagerr.GetUserAsync(HttpContext.User);
            IEnumerable<Product> Products=ProductReposatory.GetAll();
            if(Products.Count()>0)
                return Products.ToList();
            return NotFound();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = ProductReposatory.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("{ProductName:alpha}")]
        public ActionResult<Product> GetProductName(string ProductName)
        {
            var product = ProductReposatory.GetByName(ProductName);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, Product product)
        {
            if (id != product.ProductID)
            {
                return BadRequest();
            }
            if (!ProductExists(id))
            {
                return NotFound();
            }
            ProductReposatory.Update(product);
            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public ActionResult<Product> PostProduct(Product product)
        {
            ProductReposatory.Add(product);
            return CreatedAtAction("GetProduct", new { id = product.ProductID }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public ActionResult<Product> DeleteProduct(int id)
        {
            var product = ProductReposatory.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            ProductReposatory.Delete(product);
            return product;
        }

        private bool ProductExists(int id)
        {
            if (ProductReposatory.GetById(id)==null)
                return false;
            return true;
        }
    }
}
