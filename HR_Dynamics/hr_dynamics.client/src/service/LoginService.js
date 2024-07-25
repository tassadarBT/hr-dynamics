import { apiUrl } from './Constants.js';
export class LoginService {    
    login(vm) {
        return fetch(`${apiUrl}/login/login`, { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(vm) });
    }
}
