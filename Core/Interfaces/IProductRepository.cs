using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
 // Récupérer tous les produits
    Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type,string? sort);

    // Récupérer un produit par son ID
    Task<Product?> GetProductByIdAsync(int id);
    Task<IReadOnlyList<string>> GetBrandsAsync();
    Task<IReadOnlyList<string>> GetTypesAsync();
    // Créer un nouveau produit
    void AddProduct(Product product);

    // Mettre à jour un produit existant
     void UpdateProduct(Product product);

    // Supprimer un produit par son ID
     void DeleteProduct(Product product);
     bool ProductExists(int id);
     Task<bool> SavesChangesAsync();
     
}
