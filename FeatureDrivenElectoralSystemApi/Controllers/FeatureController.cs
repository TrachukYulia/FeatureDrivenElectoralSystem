using BLL.DTO;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeatureDrivenElectoralSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private IFeatureService _featureService;
        public FeatureController(IFeatureService featureService) {
            _featureService = featureService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FeatureModel>> GetAll()
        {
            var featureRequests = _featureService.GetAll();
            return Ok(featureRequests);
        }

        [HttpGet]
        [Route("/name")]
        public ActionResult<IEnumerable<FeatureRespond>> GetFeatureName()
        {
            var featureRequests = _featureService.GetFeatureName();
            return Ok(featureRequests);
        }
        [HttpPost]
        public ActionResult CreateFesture(FeatureRequest featureRequest)
        {
            _featureService.Create(featureRequest);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<FeatureNameRespond> GetFeatureById(int id)
        {
            FeatureNameRespond featureRequest = _featureService.Get(id);

            return Ok(featureRequest);
        }
                
        [HttpPut("{id}")]
        public ActionResult UpdateFeature(int id, [FromBody] FeatureRequest featureRequest)
        {
            _featureService.Update(featureRequest, id);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteById(int id)
        {
            _featureService.Delete(id);
            return NoContent();
        }
    }
}
