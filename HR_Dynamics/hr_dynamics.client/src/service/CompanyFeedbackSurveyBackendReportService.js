import { apiUrl } from './Constants.js';
export class CompanyFeedbackSurveyBackendReportService {    
    getSurveyFilterData() {
        return fetch(`${apiUrl}/api/CompanyFeedbackSurvey/GetSurveyData`, { method: 'GET', headers: { 'Content-Type': 'application/json' } });
    }

    getSurveyData() {
        return fetch(`${apiUrl}/api/CompanyFeedbackSurveyFrontend/GetSurveyData`, { method: 'GET', headers: { 'Content-Type': 'application/json' } });
    }
}
