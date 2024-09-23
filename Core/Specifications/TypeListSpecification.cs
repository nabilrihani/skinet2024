using System;
using Core.Entities;

namespace Core.Specifications;

public class TypeListSpecfication: BaseSpecifications<Product,string>
{
public TypeListSpecfication()
{
    AddSelect(x=>x.Type);
    ApplyDistinct();
}
}
