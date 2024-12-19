using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly IFeature _feature;
        public FeatureController(IFeature feature)
        {
            this._feature = feature;    
        }
        [HttpGet]
        [Route("GetFeatureById")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _feature.GetById(id);
            return Ok(data);
        }
        [HttpPost]
        [Route("CreateUpdateFeature")]
        public async Task<IActionResult> CreateUpdate(Feature feature)
        {
            var data = await _feature.CreateUpdate(feature);
            return Ok(data);    
        }
        [HttpGet]
        [Route("GetFeatures")]
        public async Task<ActionResult<Feature>> GetFeatures()
        {
            var data = await _feature.GetFeatures();
            return Ok(data);
        }

    }
}
