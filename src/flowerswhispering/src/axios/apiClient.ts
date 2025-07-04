import axios from 'axios';

const apiClient = axios.create({
  baseURL: process.env.VUE_APP_API_BASE_URL || 'http://localhost:5006',
  headers: {
    'Content-Type': 'application/json',
  },
});

export default apiClient;
