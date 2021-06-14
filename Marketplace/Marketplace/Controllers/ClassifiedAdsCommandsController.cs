using System.Threading.Tasks;
using Marketplace.Contracts;
using Marketplace.Services;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
    [Route("/ad")]
    public class ClassifiedAdsCommandsController : Controller
    {
        private readonly ClassifiedAdsApplicationService _classifiedAdsApplicationService;


        public ClassifiedAdsCommandsController(ClassifiedAdsApplicationService classifiedAdsApplicationService)
        {
            _classifiedAdsApplicationService = classifiedAdsApplicationService ?? throw new System.ArgumentNullException(nameof(classifiedAdsApplicationService));
        }


        [HttpPost]
        public async Task<IActionResult> Post(ClassifiedAds.V1.Create request)
        {
           await _classifiedAdsApplicationService.Handle(request);

           return Ok();
        }

        [Route("name")]
        [HttpPut]
        public async Task<IActionResult> Put(ClassifiedAds.V1.SetTitle request)
        {
            await _classifiedAdsApplicationService.Handle(request);

            return Ok();
        }

        [Route("text")]
        [HttpPut]
        public async Task<IActionResult> Put(ClassifiedAds.V1.UpdateText request)
        {
            await _classifiedAdsApplicationService.Handle(request);

            return Ok();
        }

        [Route("price")]
        [HttpPut]
        public async Task<IActionResult> Put(ClassifiedAds.V1.UpdatePrice request)
        {
            await _classifiedAdsApplicationService.Handle(request);

            return Ok();
        }

        [Route("publish")]
        [HttpPut]
        public async Task<IActionResult> Put(ClassifiedAds.V1.RequestToPublish request)
        {
            await _classifiedAdsApplicationService.Handle(request);

            return Ok();
        }
    }
}