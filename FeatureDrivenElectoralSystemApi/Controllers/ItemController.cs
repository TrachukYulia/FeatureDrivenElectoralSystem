using BLL.DTO;
using BLL.Interfaces;
using CsvHelper;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace FeatureDrivenElectoralSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private IItemService _itemService;
        private static  IEnumerable<ItemRespond> _geneticSolve;
        private static IEnumerable<ItemRespond> _greedySolve;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ItemRespond>> GetAll()
        {
            IEnumerable<ItemRespond> itemRequest = _itemService.GetAll();
            return Ok(itemRequest);
        }
        [HttpGet]
        [Route("/genetic")]
        public ActionResult<IEnumerable<ItemRespond>> GetGeneticSolve([FromQuery] List<int> id)
        {
            _geneticSolve = _itemService.GetGeneticSolve(id);
            return Ok(_geneticSolve);
        }
        [HttpGet]
        [Route("/greedy")]
        public ActionResult<IEnumerable<ItemRespond>> GetGreedySolve([FromQuery] List<int> id)
        {
            _greedySolve = _itemService.GetGreedySolve(id);
            return Ok(_greedySolve);
        }
        //[HttpGet]
        //[Route("/genetic/export")]
        //public IActionResult ExportGeneticSolve()
        //{
        //    return _itemService.ExportToCsv(_geneticSolve, "genetic_items.csv");
        //}
        [HttpGet]
        [Route("/export222")]
        public IActionResult ExportToCsv(IEnumerable<ItemRespond> items, string fileName)
        {
            using (var memoryStream = new MemoryStream())
            using (var writer = new StreamWriter(memoryStream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(items);
                writer.Flush();
                var byteArray = memoryStream.ToArray();
                return File(byteArray, "application/octet-stream", fileName);
            }
            
        }

        [HttpGet]
        [Route("/greedy/export")]
        public IActionResult ExportGreedySolve()
        {
            if (_greedySolve != null)
                return ExportToCsv(_greedySolve, "greedy_items.csv");
            else 
                return Ok();
        }
        [HttpPost]
        public ActionResult CreateFesture(ItemRequest itemRequest)
        {
            _itemService.Create(itemRequest);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<ItemRequest> GetFeatureById(int id)
        {
            ItemRequest itemRequest = _itemService.Get(id);

            return Ok(itemRequest);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateFeature(int id, [FromBody] ItemRequest itemRequest)
        {
            _itemService.Update(itemRequest, id);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteById(int id)
        {
            _itemService.Delete(id);
            return NoContent();
        }
    }
}
