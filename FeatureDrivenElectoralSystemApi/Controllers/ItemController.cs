using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace FeatureDrivenElectoralSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ItemRequest>> GetAll()
        {
            IEnumerable<ItemRequest> itemRequest = _itemService.GetAll();
            return Ok(itemRequest);
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
