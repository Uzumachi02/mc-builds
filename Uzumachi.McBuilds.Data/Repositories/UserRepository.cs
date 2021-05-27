﻿using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Uzumachi.McBuilds.Data.Entities;
using Uzumachi.McBuilds.Data.Repositories.Interfaces;

namespace Uzumachi.McBuilds.Data.Repositories {

  public class UserRepository : IUserRepository {

    private readonly IDbConnection _dbConnection;

    public UserRepository(IDbConnection dbConnection) {
      _dbConnection = dbConnection;
    }


    public async Task<IEnumerable<UserEntity>> GetAll() {
      var sql = "SELECT * FROM public.users ORDER BY ID";

      return await _dbConnection.QueryAsync<UserEntity>(sql);
    }

    public async ValueTask<UserEntity> GetById(int id) {
      var sql = "SELECT * FROM public.users WHERE id = @id";

      return await _dbConnection.QueryFirstOrDefaultAsync<UserEntity>(sql, new { id });
    }
  }
}
