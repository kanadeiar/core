﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication.Domain.Entities;
using WebApplication.Domain.Identity;
using WebApplication1.Services.Interfaces;
using WebApplication1.ViewModel;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = Role.Administrators)]
    public class ProductController : Controller
    {
        private readonly IProductData _ProductData;
        private readonly IWebHostEnvironment _AppEnvironment;

        private readonly Mapper _mapperProductToView =
            new(new MapperConfiguration(c => c.CreateMap<Product, ProductEditViewModel>()            
                .ForMember("SectionName", o => o.MapFrom(p => p.Section.Name))
                .ForMember("BrandName", o => o.MapFrom(p => p.Brand.Name))));
        private readonly Mapper _mapperProductFromView =
            new(new MapperConfiguration(c => c.CreateMap<ProductEditViewModel, Product>()));

        public ProductController(IProductData productData, IWebHostEnvironment appEnvironment)
        {
            _ProductData = productData;
            _AppEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View(_mapperProductToView.Map<IEnumerable<ProductEditViewModel>>(_ProductData.GetProducts().OrderBy(p => p.Order)));
        }

        public IActionResult Create()
        {
            ViewBag.Sections = new SelectList(_ProductData.GetSections(), "Id", "Name");
            ViewBag.Brands = new SelectList(_ProductData.GetBrands(), "Id", "Name");
            return View("Edit", new ProductEditViewModel());
        }

        public IActionResult Edit(int id)
        {
            if (id <= 0)
                return BadRequest();
            if (_ProductData.GetProductById(id) is { } product)
            {
                ViewBag.Sections = new SelectList(_ProductData.GetSections(), "Id", "Name");
                ViewBag.Brands = new SelectList(_ProductData.GetBrands(), "Id", "Name");
                return View(_mapperProductToView.Map<ProductEditViewModel>(product));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditViewModel model, IFormFile uploadedFile)
        {
            if (model is null)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewBag.Sections = new SelectList(_ProductData.GetSections(), "Id", "Name");
                ViewBag.Brands = new SelectList(_ProductData.GetBrands(), "Id", "Name");
                return View(model);
            }

            var product = _mapperProductFromView.Map<Product>(model);

            if (uploadedFile is not null)
            {
                string path = "/images/home/" + uploadedFile.FileName;
                await using (var fileStream = new FileStream(_AppEnvironment.WebRootPath + path, FileMode.Create))
                    await uploadedFile.CopyToAsync(fileStream);
                product.ImageUrl = uploadedFile.FileName;
            }

            if (product.Id == 0)
                _ProductData.Add(product);
            else
                _ProductData.Update(product);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();
            if (_ProductData.GetProductById(id) is { } product)
            {
                return View(_mapperProductToView.Map<ProductEditViewModel>(product));
            }
            return NotFound();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id <= 0)
                return BadRequest();
            _ProductData.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
