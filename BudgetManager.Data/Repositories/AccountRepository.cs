﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetManager.Data.Entities;

namespace BudgetManager.Data.Repositories
{
    public class AccountRepository : Repository<AccountEntity>, IAccountRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<AccountEntity> FindByIdAsync(Guid id)
        {
            string sql = "SELECT * FROM Accounts WHERE Id = @Id";
            return ExecuteScalarAsync<AccountEntity>(_unitOfWork.DbConnection, sql, new { Id = id });
        }

        public Task<AccountEntity> FindByUserNameAsync(string userName)
        {
            string sql = "SELECT * FROM Accounts WHERE UserName = @Username";
            return ExecuteScalarAsync<AccountEntity>(_unitOfWork.DbConnection, sql, new { UserName = userName });
        }

        public Task<IEnumerable<AccountEntity>> GetAllAsync()
        {
            string sql = "SELECT * FROM Accounts";
            return Query(_unitOfWork.DbConnection, sql);
        }

        public Task InsertAsync(AccountEntity entity)
        {
            string sql = @"INSERT INTO Accounts (Id, FirstName, LastName, UserName, Password, Salt)
                           VALUES (@Id, @FirstName, @LastName, @UserName, @Password, @Salt)";
            return ExecuteAsync(_unitOfWork.DbConnection, sql, Mapping(entity), _unitOfWork.DbTransaction);
        }

        public Task RemoveAsync(AccountEntity entity)
        {
            return RemoveAsync(entity.Id);
        }

        public Task RemoveAsync(Guid id)
        {
            string sql = "DELETE FROM Accounts WHERE Id = @Id";
            return ExecuteAsync(_unitOfWork.DbConnection, sql, new { Id = id });
        }

        public Task UpdateAsync(AccountEntity entity)
        {
            string sql = @"UPDATE Accounts SET
                             FirstName = @FirstName,
                             LastName = @LastName,
                             UserName = @UserName,
                             Password = @Password,
                             Salt = @Salt
                           WHERE Id = @Id";
            return ExecuteAsync(_unitOfWork.DbConnection, sql, Mapping(entity), _unitOfWork.DbTransaction);
        }

        protected override dynamic Mapping(AccountEntity item)
        {
            return new
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                UserName = item.UserName,
                Password = item.Password,
                Salt = item.Salt
            };
        }
    }
}