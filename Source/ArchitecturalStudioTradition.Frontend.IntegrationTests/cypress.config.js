const { defineConfig } = require("cypress");

module.exports = defineConfig({
  defaultCommandTimeout: 8000,
  requestTimeout: 20000,
  video: false,

  e2e: {
    setupNodeEvents(on, config) {
      // implement node event listeners here
    },
  },
});
