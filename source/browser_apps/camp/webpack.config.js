const path = require('path');

const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CopyPlugin = require('copy-webpack-plugin');
const ReactRefreshWebpackPlugin = require('@pmmmwh/react-refresh-webpack-plugin');

const publicSource = "public";

module.exports = (_env, args) => {
  const prod = args.mode === "production";

  return {
    context: __dirname,
    devServer: {
      hot: true,
      host: '192.168.50.44',
      port: 3000,
      open: true
    },
    devtool: prod ? 'source-map' : 'inline-source-map',
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
      publicPath: '/'
    },
    resolve: {
      extensions: ['.js', '.jsx']
    },
    plugins: [
      new HtmlWebpackPlugin({
        template: './src/index.html',
      }),
      new CopyPlugin({
        patterns: [
          {
            from: path.join(publicSource, "lib"),
            to: "lib"
          },
          {
            from: path.join(publicSource, "manifest.json"),
            to: "manifest.json"
          },
          {
            from: path.join(publicSource, "robots.txt"),
            to: "robots.txt"
          },
          {
            from: path.join(publicSource, "apple-touch-icon.png"),
            to: "apple-touch-icon.png"
          },
          {
            from: path.join(publicSource, "favicon.ico"),
            to: "favicon.ico"
          }
        ]
      }),
      ... (prod ? [] : [new ReactRefreshWebpackPlugin()])
    ]
  };
}