using System;
using System.Linq;
using OneAdvanced.CarePlan.WebAPIs.Application.Infrastructures;
using OneAdvanced.CarePlan.WebAPIs.Application.Models;

namespace OneAdvanced.CarePlan.WebAPIs.Application
{
    public class CarePlanService
    {
        private readonly CarePlanDbContext _db;
        public CarePlanService(CarePlanDbContext db)
        {
            _db = db;
        }

        public CarePlanEntity Create(CreateCarePlanModel model)
        {
            var entity = model.ToEntity();
            entity.ModifiedDate = DateTime.Now;
            _db.CarePlans.Add(entity);
            if (_db.SaveChanges() > 0)
                return entity;
            return null;
        }

        public CarePlanEntity GetById(int carePlanId)
        {
            return _db.CarePlans.FirstOrDefault(x => x.CarePlanId == carePlanId);
        }

        public CarePlanEntity[] GetAll(string patientName)
        {
            var query = _db.CarePlans.AsQueryable();
            if (!string.IsNullOrEmpty(patientName))
                query = query.Where(x => x.PatientName.Equals(patientName, StringComparison.OrdinalIgnoreCase));
            return query.OrderBy(x=>x.CarePlanId).ToArray();
        }

        public bool Update(int carePlanId, UpdateCarePlanModel model)
        {
            var entity = _db.CarePlans.Single(x => x.CarePlanId == carePlanId);

            var newEntity = model.ToEntity();

            entity.Title = newEntity.Title;
            entity.PatientName = newEntity.PatientName;
            entity.CreatedByUser = newEntity.CreatedByUser;
            entity.StartDate = newEntity.StartDate;
            entity.TargetDate = newEntity.TargetDate;
            entity.Reasons = newEntity.Reasons;
            entity.Actions = newEntity.Actions;
            entity.Frequency = newEntity.Frequency;
            entity.Completed = newEntity.Completed;
            entity.EndDate = newEntity.EndDate;
            entity.Outcomes = newEntity.Outcomes;

            entity.ModifiedDate = DateTime.Now;

            return _db.SaveChanges() > 0;
        }

        public bool Delete(int carePlanId)
        {
            var entity = _db.CarePlans.Single(x => x.CarePlanId == carePlanId);

            _db.CarePlans.Remove(entity);

            return _db.SaveChanges() > 0;
        }
    }
}