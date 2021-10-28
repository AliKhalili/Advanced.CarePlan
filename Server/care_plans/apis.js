const express = require('express');
const validator = require('./../utils/middlewares/validation')
const { create, list, get, update, remove } = require('./carePlanService')
const care_plan_schema = require('./carePlanValidator')
const router = express.Router();

router
    .route('/')
    .post(validator(care_plan_schema), create)
    .get(list);

router
    .route('/:id')
    .get(get)
    .put(validator(care_plan_schema), update)
    .delete(remove);

module.exports = router;
