using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teched2018.ApiModels;
using Teched2018.Dema;
using Teched2018.Repositories;
using Teched2018.Services;

namespace Teched2018.Controllers
{
    //[EnableCors("Default")]
    //[Produces("application/json")]
    //[ApiController]
    //[ResponseCache(Duration = 1200)]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
		private readonly Context _appContext;

	    public ProductsController(Context appContext)
	    {
		    _appContext = appContext;
	    }

        [HttpGet]
        public ActionResult<List<Product>> Get()
	    {
		    List<Product> products = _appContext.Products.Include(x => x.Tags).ToList();

	        return Ok(products);
        }

        //[FormatFilter]
        [HttpGet("{id}",  Name = "GetProduct")]
        public ActionResult<Product> Get([Required]Guid id)
        {
	        Product product = _appContext.Products.Include(x => x.Tags).FirstOrDefault(x => x.ProductId == id);

	        if (product == null)
	        {
		        return NotFound();
	        }

			return Ok(product);
		}

        [HttpPost]
        public ActionResult<Product> Post([FromBody]Product model)
        {
	        if (model == null)
	        {
		        return BadRequest();
	        }

	        string err;
	        if (!ValidationService.ValidateTitle(model.Title, out err))
	        {
		        ModelState.AddModelError("Title", err);
	        }

	        if (!ModelState.IsValid)
	        {
		        return BadRequest(ModelState);
	        }

	        _appContext.Products.Add(model);
	        _appContext.SaveChanges();

	        return CreatedAtRoute("GetProduct", new { id = model.ProductId }, model);
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(Guid id, [FromBody]JsonPatchDocument<Product> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var product = _appContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            patchDocument.ApplyTo(product);

            TryValidateModel(product);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _appContext.SaveChanges();

            return Ok();
        }

        #region Another methods

        [HttpPut("{id}")]
        public ActionResult<Product> Put(Guid id, [FromBody]Product model)
        {
            var product = _appContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            product.Title = model.Title;

            try
            {
                _appContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var product = _appContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _appContext.Products.Remove(product);
            _appContext.SaveChanges();

            return NoContent();
        }

        [HttpGet("{id}/tags")]
        public ActionResult<List<Tag>> GetProductTags(Guid id)
        {
            var product = _appContext.Products.Include(x => x.Tags).FirstOrDefault(x => x.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product.Tags);
        }

        [HttpOptions("{id}/tags")]
        public ActionResult GetProductTagsOptions(Guid id)
        {
            Response.Headers.Add("Allow",  "GET,OPTIONS");

            return Ok();
        }

        #endregion
	}
}
