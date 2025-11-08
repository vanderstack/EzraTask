const { defineConfig } = require('cypress');

module.exports = defineConfig({
  e2e: {
    baseUrl: 'http://ui:5173',
    env: {
      apiUrl: 'http://api:8080'
    },
    setupNodeEvents(on, config) {
      on('before:browser:launch', (browser = {}, launchOptions) => {
        if (browser.name === 'electron') {
          launchOptions.args.push('--enable-logging');
        }

        return launchOptions;
      });

      on('task', {
        log(message) {
          console.log(`[CYPRESS BROWSER LOG] ${message}`);
          return null;
        },
      });
    },
  },
});