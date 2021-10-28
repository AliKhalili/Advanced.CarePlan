const http = require('http');

const { env, port } = require("./utils/configuration")
const logger = require('./utils/logger')

const app = require('./app')
const server = http.createServer(app).listen(port, () => {
    logger.log('info',`Server started at port ${port}`)
});

