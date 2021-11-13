const webpack = require('webpack');
const ReactRefreshWebpackPlugin = require('@pmmmwh/react-refresh-webpack-plugin');
const helpers = require('./helpers');

module.exports = {
    mode: 'development',
    entry: './src/index.tsx',
    output: {
        path: helpers.root('dist'),
        publicPath: '/',
        filename: '[name].bundle.js',
        chunkFilename: '[name].chunk.js',
        clean: true,
    },
    // Enable hot module replacement and fast refresh on development mode.
    plugins: [new webpack.HotModuleReplacementPlugin(), new ReactRefreshWebpackPlugin()],
    devtool: 'inline-source-map',
    devServer: {
        static: { directory: helpers.root('dist') },
        historyApiFallback: true,
        port: 3000,
        host: 'localhost',
        hot: true,
        allowedHosts: 'auto',
    },
};
