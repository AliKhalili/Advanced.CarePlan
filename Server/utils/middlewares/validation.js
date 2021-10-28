const validator = (schema) => (req, res, next) => {
    const { error } = schema.validate(req.body, { abortEarly: false });
    if (!error) {
        next()
    }
    else {
        return res.status(400).json({
            message: "Invalid request",
            errors: error.details.map(i => {
                return {
                    source: i.context.key,
                    type: i.type,
                    message: i.message
                }
            })
        })
    }
}

module.exports = validator