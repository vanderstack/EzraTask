console.log('[OBSERVABILITY] main.ts entry point reached');

import { createApp } from 'vue';
import App from './App.vue';
import './assets/main.css';

const app = createApp(App);

app.config.errorHandler = (err, instance, info) => {
  console.error("[OBSERVABILITY] Global Vue Error:", err);
  console.error("[OBSERVABILITY] Vue instance:", instance);
  console.error("[OBSERVABILITY] Error info:", info);
};

app.mount('#app');

console.log('[OBSERVABILITY] main.ts: Vue app mounted or mount process initiated.');