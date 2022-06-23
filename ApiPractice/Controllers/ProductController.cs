﻿using ApiPractice.Data.DAL;
using ApiPractice.Data.Entities;
using ApiPractice.Dto;
using ApiPractice.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Member")]
    public class ProductController : ControllerBase
    {
        private AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;


        public ProductController(IWebHostEnvironment env, AppDbContext context,IMapper mapper)
        {
            _env=env;
            _context=context;
            _mapper=mapper;
        }


       
        [HttpGet]
        public IActionResult Get()
        {
            List<Product> products = _context.Products.Include(p => p.Category).Where(c => c.IsDeleted==false).ToList();

            List<ProductReturnDto> productsList = _mapper.Map<List<Product>, List<ProductReturnDto>>(products);

            return StatusCode(200, productsList);
            //return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Product product = _context.Products.Include(p => p.Category).Where(c => c.IsDeleted==false).FirstOrDefault(p=>p.Id==id);
            if (product==null)
            {
                return NotFound();
            }
            // product.ImageUrl=$"https://localhost:44358/img/{product.ImageUrl}";
            ProductReturnDto productReturn = _mapper.Map<Product,ProductReturnDto>(product);



            return StatusCode(200, productReturn);
            //return Ok(products);
        }


        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm] ProductCreateDto productCreateDto)
        {
            Product newProduct = new Product();
            if (!productCreateDto.Photo.IsImage())
            {
                return BadRequest(ErrorMessage.IsNotImageFormat);
            }

            if (productCreateDto.Photo.CheckSize(200000))
            {
                return BadRequest(ErrorMessage.ValueIsGreaterThan200);
            }

            newProduct.Name=productCreateDto.Name;
            newProduct.Desc=productCreateDto.Desc;
            newProduct.CategoryId=productCreateDto.CategoryId;
            newProduct.Price=productCreateDto.Price;
            newProduct.ImageUrl=await productCreateDto.Photo.SaveImage(_env,"img");
            _context.Add(newProduct);
            _context.SaveChanges();
            return StatusCode(201, newProduct);
        }
    }
}
