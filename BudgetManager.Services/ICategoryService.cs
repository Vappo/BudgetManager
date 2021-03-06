﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetManager.Domain;

namespace BudgetManager.Services
{
    public interface ICategoryService
    {
        Task AddAsync(Category category);

        Task EditAsync(Category category);

        Task<IEnumerable<Category>> GetAll();

        Task RemoveAsync(Category category);
    }
}