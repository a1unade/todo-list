import axios from 'axios';

// Axios-клиент для запросов к API
const apiClient = axios.create({
    baseURL: 'http://localhost:5238/',
});

export default apiClient;