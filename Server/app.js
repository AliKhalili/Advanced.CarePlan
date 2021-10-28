const express = require('express')
const cors = require('cors');

const app = express();

app.use(cors({origin:'*'}));

// Body parser middleware
app.use(express.json());
app.use(express.urlencoded({ extended: false }));


app.use('/api/care-plans', require('./care_plans/apis'));

module.exports = app