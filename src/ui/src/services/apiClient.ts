import axios from 'axios';

const client = axios.create({
  withCredentials: true,
});

// Add a request interceptor for logging
client.interceptors.request.use(
  (config) => {
    console.log(`[API Client Request] ${config.method?.toUpperCase()} ${config.url}`, { config });
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