﻿using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository repo) : ControllerBase
{
   
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>>  GetProducts(string? brand,
    string? type,string? sort)
    {
        return Ok(await repo.GetProductsAsync(brand,type,sort));
    }

    [HttpGet("{id:int}")] // api/products/2
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);

        if (product == null) return NotFound();

        return product;
    }   
[HttpGet("brands")] // api/brands
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        return Ok(await repo.GetBrandsAsync());
    } 
    [HttpGet("types")] // api/types
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        return Ok(await repo.GetTypesAsync());
    } 
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
       repo.AddProduct(product);

        if(await repo.SavesChangesAsync())
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

        repo.UpdateProduct(product);

          if(await repo.SavesChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Product is not updated!!");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);

        if (product == null) return NotFound();

        repo.DeleteProduct(product);

          if(await repo.SavesChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Product is not deleted a problem occur when deleting the product!!!");
    }

    private bool ProductExists(int id)
    {
        return repo.ProductExists(id);
    }
}
