﻿using DeviceManagement_WebApp.Models;
using System;

namespace DeviceManagement_WebApp.Repository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public Category CreateID(Category category);

        public bool CategoryExists(Guid? id);
    }

    
}
