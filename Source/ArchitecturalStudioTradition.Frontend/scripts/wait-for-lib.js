const fs = require('fs');

function waitLib(path) {
  if (!fs.existsSync(path)) setTimeout(() => waitLib(path), 1000);
}
waitLib('./dist/archtradition-contract');
