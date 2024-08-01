import { apiUrl } from './Constants.js';
export class LoginService {    
    login(vm) {
        return fetch(`${apiUrl}/api/Auth/Login`, { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(vm) });
    }
    saveToken(token) {
        localStorage.setItem('auth_token', token);
    }
    getToken() {
        return localStorage.getItem('auth_token');
    }

    async isLoggedIn() {
        const res = await (await fetch(`${apiUrl}/api/Auth/IsLoggedIn`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${this.getToken()}`
            }
        })).json();
        return res;
    }
}
