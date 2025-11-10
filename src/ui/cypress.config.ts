const { defineConfig } = require('cypress');

module.exports = defineConfig({
  e2e: {
    // This now comes from the CYPRESS_BASE_URL env var in docker-compose.e2e.yml
    // baseUrl: 'http://ui:5173', // old
    env: {
      // This now comes from the CYPRESS_API_URL env var
      // apiUrl: 'http://api:8080' // old
    },
    setupNodeEvents(on: any, config: any) {
      on('before:browser:launch', (browser: { name?: string } = {}, launchOptions: any) => {
        if (browser.name === 'electron') {
          launchOptions.args.push('--enable-logging');
        }

        return launchOptions;
      });

      on('task', {
        log(message: any) {
          console.log(`[CYPRESS BROWSER LOG] ${message}`);
          return null;
        },
      });
    },
  },
});