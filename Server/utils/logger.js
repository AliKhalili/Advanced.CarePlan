const { log } = require("./configuration")
const winston = require('winston');

const logger = winston.createLogger({
    level: log.level,
    // format:winston.format.simple(),
    transports: [
        new winston.transports.Console({
            format: winston.format.simple(),
        })
    ]
})

module.exports = logger