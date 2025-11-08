import { fileURLToPath } from 'node:url'
import { defineConfig } from 'vite'
import { configDefaults } from 'vitest/config'
import viteConfigCallback from './vite.config'

const viteConfig = viteConfigCallback({ mode: 'development', command: 'serve' });

export default defineConfig({
  ...viteConfig,
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
test: {
        environment: 'jsdom',
        exclude: [...configDefaults.exclude, 'e2e/*'],
        root: fileURLToPath(new URL('./', import.meta.url)),
        setupFiles: ['./vitest.setup.ts'],
      }
})