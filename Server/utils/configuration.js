const path = require("path");

require('dotenv').config({
    path: path.join(__dirname, '../config.env')
})

module.exports = {
    env: process.env.NODE_ENV,
    port: process.env.PORT,
    log:{
        level : process.env.LOG_LEVEL
    }
}