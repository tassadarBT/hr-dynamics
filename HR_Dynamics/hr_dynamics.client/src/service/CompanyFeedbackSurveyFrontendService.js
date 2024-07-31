import { apiUrl } from './Constants.js';
export class CompanyFeedbackSurveyFrontendService {    
    getSurveyData() {
        return fetch(`${apiUrl}/api/CompanyFeedbackSurveyFrontend/GetSurveyData`, { method: 'GET', headers: { 'Content-Type': 'application/json' } });
    }

    saveSurveyData(vm) {
        return fetch(`${apiUrl}/api/CompanyFeedbackSurveyFrontend/SaveSurveyData`, { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(vm) });
    }
}
