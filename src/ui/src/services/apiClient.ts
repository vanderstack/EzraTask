import axios from 'axios';

// Vite will replace `import.meta.env.VITE_API_BASE_URL` with the string value
// at build time. This makes the UI aware of the API's location.
const baseURL = import.meta.env.VITE_API_BASE_URL;

const client = axios.create({
  baseURL: baseURL, // e.g., 'http://api:8080'
  withCredentials: true,
});

// Add a request interceptor for logging
client.interceptors.request.use(
  (config) => {
    // The URL will now be relative to the baseURL, e.g., '/api/v1/todos'
    console.log(`[API Client Request] ${config.method?.toUpperCase()} ${config.baseURL}${config.url}`, { config });
    return config;
  },
  (error) => {
    console.error('[API Client Request Error]', { error });
    return Promise.reject(error);
  }
);

client.interceptors.response.use(
  (response) => {
    console.log(`[API Client Response] ${response.status} ${response.config.url}`, { data: response.data });
    return response;
  },
  (error) => {
    console.error('[API Client Response Error]', {
      message: error.message,
      response: error.response?.data,
      status: error.response?.status,
      config: error.config,
      stack: error.stack,
    });
    return Promise.reject(error);
  }
);

export default client;