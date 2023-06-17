﻿using System.Collections.Generic;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IEntityService<T> : IDbService where T : Entity
    {
        T Get(int id);
        public T Create(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public List<T> GetAll();
    }
}
