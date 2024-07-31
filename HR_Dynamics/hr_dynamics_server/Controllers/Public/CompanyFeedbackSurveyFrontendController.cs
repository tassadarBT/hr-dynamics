using hr_dynamics_server.Services.Survey.Implementation;
using hr_dynamics_server.ViewModels.Shared;
using hr_dynamics_server.ViewModels.Survey.Frontend.CompanyFeedbackSurvey;
using Microsoft.AspNetCore.Mvc;

namespace hr_dynamics_server.Controllers.Public
{
    [ApiController, Route("api/[controller]")]
    public class CompanyFeedbackSurveyFrontendController : ControllerBase
    {
        private readonly ICompanyFeedbackSurveyFrontendService _frontendSurveyService;
        public CompanyFeedbackSurveyFrontendController(ICompanyFeedbackSurveyFrontendService frontendSurveyService)
        {
            _frontendSurveyService = frontendSurveyService;
        }

        [HttpGet, Route("GetSurveyData")]
        public async Task<IActionResult> GetSurveyData(CancellationToken cancellationToken)
        {
            var res = await _frontendSurveyService.GetCompanyFeedbackSurveyData(cancellationToken);
            return Ok(res);
        }

        [HttpPost, Route("SaveSurveyData")]
        public async Task<IActionResult> SaveSurveyData([FromBody] CompanyFeedbackSurveySaveViewModel vm, CancellationToken cancellationToken)
        {
            SaveResultViewModel<int> res;
            if (ModelState.IsValid)            
                res = await _frontendSurveyService.SaveCompanyFeedbackSurveyData(vm, cancellationToken);            
            else
                res = new SaveResultViewModel<int> { Success = false, ErrorMessage = "Validation failed!" };
            return Ok(res);
        }
    }
}
