const path = require('path');

const rootPath = path.resolve(__dirname, '..');

function root(...args) {
    const argsArray = Array.prototype.slice.call(args);
    return path.join(...[rootPath].concat(argsArray));
}

exports.root = root;
