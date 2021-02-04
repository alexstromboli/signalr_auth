'use strict';

var path = require("path");
var build = path.join (__dirname, "web", "build");

module.exports =
{
	entry:
	{
		"index": "./src/run.js"
	},
	resolve:
	{
		extensions: ['.js']
	},
	output:
	{
		path: build,
		//publicPath: "//",
		filename: "run.js"
	},
	module:
	{
		rules:
		[
			{
				test: /\.js$/,
				use: 'babel-loader'
			}
		]
	}
};
