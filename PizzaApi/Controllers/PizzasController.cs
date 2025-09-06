using Microsoft.AspNetCore.Mvc;
using PizzaApi.Data;
using PizzaApi.Models;

namespace PizzaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzasController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Pizza>> GetAll() => Ok(PizzaRepository.GetAll());

        [HttpGet("{id:int}")]
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = PizzaRepository.Get(id);
            return pizza is null ? NotFound() : Ok(pizza);
        }

        [HttpPost]
        public ActionResult<Pizza> Create([FromBody] Pizza pizza)
        {
            var created = PizzaRepository.Add(pizza);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Pizza pizza)
        {
            if (id != pizza.Id) return BadRequest("Route id and body id must match.");
            return PizzaRepository.Update(id, pizza) ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return PizzaRepository.Delete(id) ? NoContent() : NotFound();
        }
    }
}
