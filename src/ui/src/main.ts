import { createApp } from 'vue'
import App from './App.vue'
import './assets/main.css'

console.log('main.ts: Creating and mounting Vue app...');
createApp(App).mount('#app')
console.log('main.ts: Vue app mounted.');
