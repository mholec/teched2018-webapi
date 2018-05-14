using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Teched2018.Repositories;

namespace Teched2018.ApiModels
{
    public class Product : IValidatableObject
    {
        private Context Context { get; set; }

        public Product()
        {
        }

        //private Product(Context context)
        //{
        //    this.Context = context;
        //}

        public Guid ProductId { get; set; }

		[Required]
		public string Title { get; set; }
		public List<Tag> Tags { get; set; }
	    

	    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	    {
            if (Title.StartsWith("B"))
		    {
			    yield return new ValidationResult("Title cannot starts with B", new[] {"Title"});
		    }
        }
    }
}
