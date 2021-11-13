const { merge } = require('webpack-merge');
const webpackProd = require('./webpack-configs/webpack.prod');
const webpackDev = require('./webpack-configs/webpack.dev');
const getCommonConfig = require('./webpack-configs/webpack.common');

const env = process.env.NODE_ENV;

switch (env) {
    case 'production':
        module.exports = merge(getCommonConfig(env), webpackProd);
        break;
    case 'development':
        module.exports = merge(getCommonConfig(env), webpackDev);
        break;
    default:
        throw new Error('Build environment is invalid.');
}
