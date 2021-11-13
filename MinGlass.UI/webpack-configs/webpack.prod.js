const CssMinimizerPlugin = require('css-minimizer-webpack-plugin');
const TerserPlugin = require('terser-webpack-plugin');
const { WebpackManifestPlugin } = require('webpack-manifest-plugin');
const helpers = require('./helpers');

module.exports = {
    mode: 'production',
    entry: './src/index.tsx',
    output: {
        path: helpers.root('build'),
        publicPath: '/',
        filename: '[name].[contenthash:8].bundle.js',
        chunkFilename: '[name].[contenthash:8].chunk.js',
        clean: true,
    },
    optimization: {
        emitOnErrors: false,
        // Use plugins to minimize bundled code.
        minimize: true,
        // Copied from create-react-app configs
        minimizer: [
            new TerserPlugin({
                terserOptions: {
                    parse: {
                        // We want terser to parse ecma 8 code. However, we don't want it
                        // to apply any minification steps that turns valid ecma 5 code
                        // into invalid ecma 5 code. This is why the 'compress' and 'output'
                        // sections only apply transformations that are ecma 5 safe
                        // https://github.com/facebook/create-react-app/pull/4234
                        ecma: 8,
                    },
                    compress: {
                        ecma: 5,
                        warnings: false,
                        // Disabled because of an issue with Uglify breaking seemingly valid code:
                        // https://github.com/facebook/create-react-app/issues/2376
                        // Pending further investigation:
                        // https://github.com/mishoo/UglifyJS2/issues/2011
                        comparisons: false,
                        // Disabled because of an issue with Terser breaking valid code:
                        // https://github.com/facebook/create-react-app/issues/5250
                        // Pending further investigation:
                        // https://github.com/terser-js/terser/issues/120
                        inline: 2,
                    },
                    mangle: {
                        safari10: true,
                    },
                    // Added for profiling in devtools
                    keep_classnames: true,
                    keep_fnames: true,
                    sourceMap: false,
                    output: {
                        ecma: 5,
                        comments: false,
                        ascii_only: true,
                    },
                },
            }),
            new CssMinimizerPlugin(),
        ],
        // Creates a runtime file to be shared for all generated chunks.
        runtimeChunk: 'single',
        // Share chunks between initial, async and non-async chunks
        splitChunks: {
            chunks: 'all',
        },
    },
    plugins: [
        // Generate an asset manifest file with the following content:
        // - "files" key: Mapping of all asset filenames to their corresponding
        //   output file so that tools can pick it up without having to parse
        //   `index.html`
        // - "entrypoints" key: Array of files which are included in `index.html`,
        //   can be used to reconstruct the HTML if necessary
        new WebpackManifestPlugin({
            fileName: 'asset-manifest.json',
            publicPath: helpers.root('build'),
            generate: (seed, files, entrypoints) => {
                const manifestFiles = files.reduce((manifest, file) => {
                    // eslint-disable-next-line no-param-reassign
                    manifest[file.name] = file.path;
                    return manifest;
                }, seed);
                const entrypointFiles = entrypoints.main.filter(
                    (fileName) => !fileName.endsWith('.map')
                );

                return {
                    files: manifestFiles,
                    entrypoints: entrypointFiles,
                };
            },
        }),
    ],
};
