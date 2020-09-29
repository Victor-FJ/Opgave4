using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Opgave1;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Opgave4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikesController : ControllerBase
    {
        private static List<Bike> _bikes = new List<Bike>()
        {
            new Bike(1, "Red", 1250, 3),
            new Bike(2, "White", 2545.50, 9),
            new Bike(3, "Blue&Black", 3000, 5)
        };


        // GET: api/<BikesController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Get()
        {
            if (_bikes != null && _bikes.Count != 0)
                return Ok(_bikes);
            return NoContent();
        }

        private Bike GetBike(int id)
        {
            return _bikes.Find(x => x.Id == id);
        }

        // GET api/<BikesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            Bike bike = GetBike(id);
            if (bike != null)
                return Ok(bike);
            return NotFound($"Bike with id '{id}' was not found");
        }

        // POST api/<BikesController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Post([FromBody] Bike value)
        {
            for (int i = 1; i <= _bikes.Count; i++)
                if (_bikes.Find(x => x.Id == i) == null)
                    value.Id = i;
            _bikes.Add(value);
            return Ok($"The bike - {value} - was created");
        }

        // PUT api/<BikesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] Bike value)
        {
            Bike bike = GetBike(id);
            if (bike != null)
            {
                int index = _bikes.IndexOf(bike);
                _bikes[index] = value;
                return Ok($"The bike - {value} - was updated");
            }

            return NotFound($"Bike with id '{id}' was not found");
        }

        // DELETE api/<BikesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            Bike bike = GetBike(id);
            if (bike != null)
            {
                _bikes.Remove(bike);
                return Ok($"The bike - {bike} - was deleted");
            }
            return NotFound($"Bike with id '{id}' was not found");
        }
    }
}
