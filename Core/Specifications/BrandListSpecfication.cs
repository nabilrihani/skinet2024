using System;
using Core.Entities;

namespace Core.Specifications;

public class BrandListSpecfication: BaseSpecifications<Product,string>
{
public BrandListSpecfication()
{
    AddSelect(x=>x.Brand);
    ApplyDistinct();
}
}
