using BLL.DTO;
using BLL.Interfaces;
using CsvHelper;
using DAL.Models;
using DAL.Repository;
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
        [HttpGet]
        [Route("/genetic/export")]
        public IActionResult ExportGeneticSolve()
        {
        
            if (_geneticSolve == null || !_geneticSolve.Any())
            {
                return NotFound();
            }

            var csvFile = _itemService.ExportItemsToCsv(_geneticSolve);

            return File(csvFile, "application/octet-stream", "genetic_items.csv");
        }

        [HttpGet]
        [Route("/greedy/export")]
        public IActionResult ExportGreedySolve()
        {
            if (_greedySolve == null || !_greedySolve.Any())
            {
                return NotFound();
            }

            var csvFile = _itemService.ExportItemsToCsv(_greedySolve);

            return File(csvFile, "application/octet-stream", "greedy_items.csv");
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
