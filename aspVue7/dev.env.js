const prodEnv = require('./prod.env')

module.exports = merge(prodEnv,{
    NODE_ENV:'"development"',
    API_HOST:"//localhost:8080/api/"
})