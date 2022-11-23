// Karma configuration file, see link for more information
// https://karma-runner.github.io/1.0/config/configuration-file.html

const isLocal = (process.env.NODE_ENV || '').trim() === 'local';
const browser = (process.env.BROWSER || '').trim() || 'ChromeWithoutSandbox';
const isRunningChrome = browser === 'Chrome'
module.exports = function (config) {
  config.set({
    basePath: '',
    frameworks: ['jasmine', '@angular-devkit/build-angular'],
    plugins: [
      require('karma-jasmine'),
      require('karma-chrome-launcher'),
      require('karma-jasmine-html-reporter'),
      require('karma-coverage-istanbul-reporter'),
      require('@angular-devkit/build-angular/plugins/karma'),
      require(isLocal ? 'karma-spec-reporter' :'karma-teamcity-reporter'),
      require('karma-coverage-istanbul-reporter'),
      require('karma-teamcity-reporter')
    ],
    client: {
      jasmine: {
        timeoutInterval: 10000
      },
      clearContext: false // leave Jasmine Spec Runner output visible in browser
    },
    coverageIstanbulReporter: {
      dir: require('path').join(__dirname, './coverage'),
      reports: ['json', 'lcovonly'],
      fixWebpackSourcePaths: true,
      'report-config': {
        json: {
            file: 'app.json'
        }
      }
    },
    reporters: isLocal ? ['spec', 'kjhtml'] : ['progress', 'kjhtml', 'teamcity'],
    port: 9876,
    colors: true,
    logLevel: config.LOG_INFO,
    autoWatch: isRunningChrome,
    watch: isRunningChrome,
    browsers: [browser],
    customLaunchers: {
      ChromeWithoutSandbox: {
        base: 'ChromeHeadless',
        flags: ['--no-sandbox']// running as non-root requires no-sandbox mode
      }
    },
    singleRun: !isRunningChrome
  });
};
