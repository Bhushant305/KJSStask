using KjssAPI.Data;
using KjssAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KjssAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDBContext _db;

        public ProductController(ProductDBContext db)
        {
            _db = db;
        }
        [HttpGet]
        public ActionResult GetProducts()
        {
            var products=_db.Products.ToList();
            return Ok(products);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        [ProducesResponseType(404)]

        [HttpGet("{id}", Name = "GetProduct")]
        public ActionResult GetProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var product = _db.Products.FirstOrDefault(u => u.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public IActionResult Create([FromBody] Product Prod)
        {
          

            if (Prod == null)
            {
                return BadRequest();
            }
          
            _db.Products.Add(Prod);
            _db.SaveChanges();
            return CreatedAtRoute("GetProduct", new { id = Prod.Id }, Prod);

        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}", Name = "Update")]
        public IActionResult UpdateProduct(int id, [FromBody] Product Prod)
        {
            if (Prod == null || id != Prod.Id)
            {
                return BadRequest();
            }
            var newProduct =_db.Products.FirstOrDefault(u => u.Id == id);
            if (newProduct == null)
            {
                return NotFound();
            }
            newProduct.Name = Prod.Name;
            newProduct.Description = Prod.Description;
            newProduct.Price = Prod.Price;
            _db.Update(newProduct);
            _db.SaveChanges();

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var Product= _db.Products.FirstOrDefault(p=>p.Id == id);
         
            if (Product == null)
            {
                return NotFound();
            }
            _db.Products.Remove(Product);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
