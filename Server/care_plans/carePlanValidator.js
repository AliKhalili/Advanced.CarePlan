const Joi = require('joi');

const care_plan_schema = Joi.object({
    title: Joi.string().max(450).required(),
    patient_name: Joi.string().max(450).required(),
    create_by_user: Joi.string().max(450).required(),
    start_date: Joi.date().iso().required(),
    target_date: Joi.date().iso().required(),
    reasons: Joi.string().max(1000).required(),
    actions: Joi.string().max(1000).required(),
    frequency: Joi.string().max(1000),
    completed: Joi.boolean().truthy('yes').falsy('no'),
    end_date: Joi.when(Joi.ref('completed'), { is: true, then: Joi.date().iso().greater(Joi.ref('start_date')).required() }),
    outcomes: Joi.when(Joi.ref('completed'), { is: true, then: Joi.string().max(1000).required() })
});

module.exports = care_plan_schema
