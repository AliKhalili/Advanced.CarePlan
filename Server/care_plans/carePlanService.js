const db = require('./carePlanDb')


module.exports.create = async (req, res) => {
    let newRecord = req.body;
    if (!newRecord.completed || newRecord.completed.toLowerCase() === "no") {
        delete newRecord.end_date;
        delete newRecord.outcomes;
        newRecord.completed = "No";
    }
    else {
        newRecord.completed = "Yes";
    }

    newRecord = db.addCarePlan(newRecord);
    return res.json(db.getCarePlan(newRecord.care_plan_id));
}

module.exports.list = async (req, res) => {
    return res.json(db.getAllCarePlans());
}

module.exports.get = async (req, res) => {
    const record = db.getCarePlan(Number(req.params.id));
    if (record)
        return res.json(record);
    return res.status(400).end();
}

module.exports.update = async (req, res) => {
    let record = db.getCarePlan(Number(req.params.id));
    if (record) {
        delete req.body.care_plan_id;
        record = { ...record, ...req.body };
        if (!record.completed || record.completed.toLowerCase() === "no") {
            delete record.end_date;
            delete record.outcomes;
            record.completed = "No";
        }
        else {
            record.completed = "Yes";
        }

        db.updateCarePlan(record);
        return res.status(204).json(record);
    }
    return res.status(400).end();
}

module.exports.remove = async (req, res) => {
    const record = db.getCarePlan(req.params.id);
    if (record) {
        db.removeCarePlan(record.care_plan_id);
        return res.status(204).end();
    }
    return res.status(400).end();
}