using hr_dynamics_server.Services.Survey.Interface;
using hr_dynamics_server.ViewModels.Survey.Backend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;


namespace hr_dynamics_server.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class CompanyFeedbackSurveyReportBackendController : ControllerBase
    {
        private readonly ICompanyFeedbackSurveyReportBackendService _companyFeedbackSurveyReportBackendService;

        public CompanyFeedbackSurveyReportBackendController(ICompanyFeedbackSurveyReportBackendService companyFeedbackSurveyReportBackendService)
        {
           _companyFeedbackSurveyReportBackendService = companyFeedbackSurveyReportBackendService;
        }


        [HttpGet, Route("GetFilterData")]
        public async Task<IActionResult> GetFilterData(CancellationToken cancellationToken)
        {
            var res = await _companyFeedbackSurveyReportBackendService.GetFilterData(cancellationToken);
            return Ok(res);
        }

        [HttpPost, Route("GetReportData")]
        public async Task<IActionResult> GetReportData([FromBody] CompanyFeedbackSurveyBackendReportFilterViewModel vm, CancellationToken cancellationToken)
        {
            var res = await _companyFeedbackSurveyReportBackendService.GetReportData(vm, cancellationToken);
            return Ok(res);
        }

        [HttpPost, Route("ExportReportData")]
        public async Task<IActionResult> ExportReportData([FromBody] CompanyFeedbackSurveyBackendReportFilterViewModel vm, CancellationToken cancellationToken)
        {
            var excelBytes = await _companyFeedbackSurveyReportBackendService.ExportReportData(vm, cancellationToken);
            return File(excelBytes, "application/excel", $"Company_Feedback_Survey_{DateTime.UtcNow.Date.ToShortDateString()}_{DateTime.UtcNow.ToShortTimeString()}.xlsx");
        }
    }
}
