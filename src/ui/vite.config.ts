import { fileURLToPath, URL } from 'node:url'
import { defineConfig, loadEnv } from 'vite'
import vue from '@vitejs/plugin-vue'

export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd(), '');

  console.log('VITE_API_PROXY_TARGET:', env.VITE_API_PROXY_TARGET);
  const proxyTarget = env.VITE_API_PROXY_TARGET || 'http://localhost:8080';
  console.log('Vite Proxy Target:', proxyTarget);

  return {
    plugins: [ vue() ],
    resolve: { alias: { '@': fileURLToPath(new URL('./src', import.meta.url)) } },
    server: {
      host: true,
      port: 5173,
      proxy: {
        '/api/v1': {
          target: proxyTarget,
          changeOrigin: true,
        }
      },
      allowedHosts: [
        'localhost',
        'ui'
      ]
    },
    // This configures the production preview server used in E2E tests
    preview: {
      port: 5173,
      host: true,
      proxy: {
        // Any request to /api/v1 will be forwarded to the API container
        '/api/v1': {
          target: 'http://api:8080', // Hardcode the E2E target here
          changeOrigin: true,
        }
      }
    }
  }
})