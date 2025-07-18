﻿using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product,IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Save the file to wwwroot/images
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    // Save the filename to the DB
                    product.ImageFileName = fileName;
                }
                _productService.AddProduct(product);
                return RedirectToAction("AllProduct", "Product");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult AllProduct()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,Product product,IFormFile imageFile)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Save the file to wwwroot/images
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    // Save the filename to the DB
                    product.ImageFileName = fileName;
                }
                _productService.UpdateProduct(product);
                return RedirectToAction("AllProduct", "Product"); // or to your Product list page if you add one
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            _productService.DeleteProduct(id);
            return RedirectToAction("AllProduct", "Product");
        }
    }
}
