using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace car_listing_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly IMongoCollection<Car> _cars;

        public CarController(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDb"));
            var database = client.GetDatabase("CarMarketplace");
            _cars = database.GetCollection<Car>("Cars");
        }

        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetAll()
        {
            return await _cars.Find(_ => true).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetById(string id)
        {
            var car = await _cars.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (car == null) return NotFound();
            return car;
        }

        [HttpPost]
        public async Task<ActionResult<Car>> Create(Car car)
        {
            await _cars.InsertOneAsync(car);
            // TODO: Publish 'car-listed' event to RabbitMQ
            return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Car updatedCar)
        {
            var result = await _cars.ReplaceOneAsync(c => c.Id == id, updatedCar);
            if (result.MatchedCount == 0) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _cars.DeleteOneAsync(c => c.Id == id);
            if (result.DeletedCount == 0) return NotFound();
            return NoContent();
        }
    }
} 