using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System.Text.Json;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SquareController : ControllerBase
    {
        private static Random random = new Random();

        [HttpGet("GetMemory")]
        public IActionResult GetMemory()
        {

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "memory.json");

            if (!System.IO.File.Exists(filePath))
                return NotFound(new { error = "File not found" });

            string jsonData = System.IO.File.ReadAllText(filePath);

            return Content(jsonData, "application/json");
        }

        [HttpPost("AddNewSquare")]
        public IActionResult AddNewSquare()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "memory.json");
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            if (!System.IO.File.Exists(filePath))
                return NotFound(new { error = "File not found" });

            string json = System.IO.File.ReadAllText(filePath);
            var data = JsonSerializer.Deserialize<Dictionary<string, List<Square>>>(json, jsonOptions);

            if (data == null || !data.ContainsKey("squares"))
                return BadRequest(new { error = "Invalid data format" });

            int RBGValue = random.Next(0x1000000);
            uint argbValue = 0xFF000000 | (uint)RBGValue;
            string color = $"#{argbValue:X8}";

            var squares = data["squares"];

            int maxId = squares.Any() ? squares.Max(sq => sq.Id) : 0;
            bool first = squares.Any();
            var lastSquare = squares.Any() ? squares.Last() : new Square { X = 0, Y = 0, Id = 1, Label = color };

            if (!first)
            {
                squares.Add(lastSquare);
                string initialJson = JsonSerializer.Serialize(data, jsonOptions);
                System.IO.File.WriteAllText(filePath, initialJson);
                return CreatedAtAction(nameof(AddNewSquare), lastSquare);
            }

            int XAdd = 0;
            int YAdd = 0;

            int lastX = lastSquare.X;
            int lastY = lastSquare.Y;
            int layer = Math.Max(lastX, lastY);


            if (lastX == 0 && lastY == layer)
            {
                XAdd = layer + 1;
                YAdd = -layer;
            }
            else if (lastX == layer && lastY < layer)
            {
                YAdd = 1;
            }
            else if (lastX > 0 && lastY == layer)
            {
                XAdd = -1;
            }
            else if (lastX < layer && lastY == 0)
            {
                XAdd = 1;
            }

            var newSquare = new Square
            {
                Id = maxId + 1,
                X = lastX + XAdd,
                Y = lastY + YAdd,
                Label = color
            };

            squares.Add(newSquare);
            string updatedJson = JsonSerializer.Serialize(data, jsonOptions);
            System.IO.File.WriteAllText(filePath, updatedJson);

            return CreatedAtAction(nameof(AddNewSquare), newSquare);
        }
    }
}