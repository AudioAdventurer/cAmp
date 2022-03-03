const path = require('path');

const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const ReactRefreshWebpackPlugin = require('@pmmmwh/react-refresh-webpack-plugin');

module.exports = (_env, args) => {
  const prod = args.mode === "production";

  return {
    context: __dirname,
    devServer: {
      hot: true,
      host: 'local-ipv4',
      port: 3000,
      open: true
    },
    entry: ['./src/index.js'],
    mode: prod ? "production" : "development",
    module: {
      rules: [
        {
          test: /\.js$/,
          loader: 'babel-loader',
          exclude: /node_modules/
        },
        {
          test: /\.css$/,
          use: ['style-loader', 'css-loader']
        },
        {
          test: /\.(png|jpe?g|gif)$/,
          use: [
            {
              loader: "url-loader",
              options: {
                limit:10000,
                mimeType: "image/png",
                encoding: true
              }
          }]
        }

      ]
    },
    output: {
      path: path.resolve(__dirname, 'dist'),
      filename: 'bundle.js',
      chunkFilename: '[id].js',
      publicPath: ''
    },
    resolve: {
      extensions: ['.js', '.jsx']
    },
    plugins: [
      new HtmlWebpackPlugin({
        template: './src/index.html',
      }),
      ... (prod ? [] : [new ReactRefreshWebpackPlugin()])
    ]
  };
}