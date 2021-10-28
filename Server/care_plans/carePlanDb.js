const records = []
module.exports.addCarePlan = (carePlan) => {
    const entity = {
        care_plan_id: records.length + 1,
        ...carePlan
    };
    records.push(entity);
    return entity;
}

module.exports.getCarePlan = (care_plan_id) => records.find(x => x.care_plan_id == care_plan_id)

module.exports.getAllCarePlans = () => records

module.exports.updateCarePlan = (carePlan) => {
    records.forEach((record, i) => {
        if (record.care_plan_id == carePlan.care_plan_id)
            records[i] = carePlan;
    });
};

module.exports.removeCarePlan = (care_plan_id) => {
    records.forEach((record, i) => {
        if (record.care_plan_id == care_plan_id)
            records.splice(i, 1);
    });
}