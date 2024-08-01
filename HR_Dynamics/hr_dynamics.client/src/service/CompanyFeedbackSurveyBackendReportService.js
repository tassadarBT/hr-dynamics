import { apiUrl } from './Constants.js';
import { LoginService } from './LoginService.js';

export class CompanyFeedbackSurveyBackendReportService {    

    constructor() {
        this.loginService = new LoginService();
    }

    getFilterData() {
        return fetch(`${apiUrl}/api/CompanyFeedbackSurveyReportBackend/GetFilterData`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${this.loginService.getToken()}`
            }
        });
    }

    getReportData(filter) {        
        return fetch(`${apiUrl}/api/CompanyFeedbackSurveyReportBackend/GetReportData`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${this.loginService.getToken()}`
            },
            body: JSON.stringify(filter)
        });
    }

    exportReportData(filter) {
        return fetch(`${apiUrl}/api/CompanyFeedbackSurveyReportBackend/ExportReportData`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${this.loginService.getToken()}`
            },
            body: JSON.stringify(filter)
        });
    }
}
