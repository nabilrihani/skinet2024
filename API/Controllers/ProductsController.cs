﻿using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IGenericRepository<Product> repo) : ControllerBase
{
   
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>>  GetProducts(string? brand,
    string? type,string? sort)
    {
        var spec= new ProductSpecification(brand,type,sort);
        var products = await repo.ListAsync(spec);
       // return Ok(await repo.ListAllAsync());       before specification
       return Ok(products);  // After using specification
    }

    [HttpGet("{id:int}")] // api/products/2
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);

        if (product == null) return NotFound();

        return product;
    }   
[HttpGet("brands")] // api/brands
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        //implement method later
         // return Ok(await repo.GetBrandsAsync());
         var spec=new BrandListSpecfication();
         return Ok(await repo.ListAsync(spec));
    } 
    [HttpGet("types")] // api/types
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        //implement method later
       // return Ok(await repo.GetTypesAsync());
       var spec=new TypeListSpecfication();
         return Ok(await repo.ListAsync(spec));
        
    } 
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
       repo.Add(product);

        if(await repo.SaveAllAsync())
        {
            return CreatedAtAction("GetProduct",new {id=product.Id});
        }

        return BadRequest("Problem creating Product!!!");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !ProductExists(id)) 
            return BadRequest("Cannot update this product");

        repo.Update(product);

          if(await repo.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("Product is not updated!!");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);

        if (product == null) return NotFound();

        repo.Remove(product);

          if(await repo.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("Product is not deleted a problem occur when deleting the product!!!");
    }

    private bool ProductExists(int id)
    {
        return repo.Exists(id);
    }
}
