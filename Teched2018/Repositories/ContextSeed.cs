using System;
using System.Collections.Generic;
using Teched2018.ApiModels;

namespace Teched2018.Repositories
{
    public static class ContextSeed
    {
        public static void Initialize(Context context)
        {
            context.Products.Add(
                new Product
                {
                    ProductId = Guid.Parse("eeca4000-0dbb-447c-a4b7-d836559f5278"),
                    Title = "Glock 19",
                    Tags = new List<Tag>
                    {
                        new Tag {TagId = Guid.Parse("da421131-7997-4396-8133-531fcffd34a7"), Name = "Glock"},
                        new Tag {TagId = Guid.Parse("d131808c-89fe-4400-a69b-d633e755d1ca"), Name = "Austria"}
                    }
                }
            );

            context.Products.Add(
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Title = "CZ Skorpion 61 S",
                    Tags = new List<Tag>
                    {
                        new Tag {TagId = Guid.Parse("4e40f581-720a-4d9f-8be4-8a175a2f5418"), Name = "Česká zbrojovka"},
                        new Tag {TagId = Guid.Parse("1335da88-d07b-49ee-828d-32806e631943"), Name = "Czechia"}
                    }
                }
            );

            context.SaveChanges();
        }
    }
}
