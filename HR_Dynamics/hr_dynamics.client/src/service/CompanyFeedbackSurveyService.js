import { apiUrl } from './Constants.js';
export class CompanyFeedbackSurveyService {    
    getSurveyData() {
        return fetch(`${apiUrl}/api/CompanyFeedbackSurvey/GetSurveyData`, { method: 'GET', headers: { 'Content-Type': 'application/json' } });
    }

    saveSurveyData(vm) {
        return fetch(`${apiUrl}/api/CompanyFeedbackSurvey/SaveSurveyData`, { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(vm) });
    }
}
