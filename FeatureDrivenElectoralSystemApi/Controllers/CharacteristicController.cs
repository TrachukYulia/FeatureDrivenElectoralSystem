using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FeatureDrivenElectoralSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacteristicController : ControllerBase
    {
        private ICharacteristicService _characteristicService;
        public CharacteristicController(ICharacteristicService characteristicService)
        {
            _characteristicService = characteristicService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CharacteristicRespond>> GetAll()
        {
            IEnumerable<CharacteristicRespond> characteristicRequests = _characteristicService.GetAll();
            return Ok(characteristicRequests);
        }

        [HttpPost]
        public ActionResult CreateCharacteristic(CharacteristicRequest characteristicRequest)
        {
            if (characteristicRequest == null)
            {
                throw new ArgumentNullException();
            }
            _characteristicService.Create(characteristicRequest);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<CharacteristicNameRespond> GetCharacteristicById(int id)
        {
            CharacteristicNameRespond characteristicRequest = _characteristicService.Get(id);

            return Ok(characteristicRequest);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCharacteristic(int id, [FromBody] CharacteristicRequest characteristicRequest)
        {
            _characteristicService.Update(characteristicRequest, id);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteById(int id)
        {
            _characteristicService.Delete(id);
            return NoContent();
        }
    }
}
