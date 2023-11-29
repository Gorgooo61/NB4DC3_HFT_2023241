using Microsoft.AspNetCore.Mvc;
using NB4DC23_HFT_2023241.Models;
using NB4DC3_HFT_2023241.Logic;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NB4DC3_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderLogic logic;

        public OrderController(IOrderLogic logic)
        {
            this.logic = logic;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<Order> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public Order Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<OrderController>
        [HttpPost]
        public void Create([FromBody] Order value)
        {
            this.logic.Create(value);
        }

        // PUT api/<OrderController>/5
        [HttpPut]
        public void Update([FromBody] Order value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
