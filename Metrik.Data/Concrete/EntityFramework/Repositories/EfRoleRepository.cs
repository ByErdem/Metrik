﻿using Metrik.Data.Abstract;
using Metrik.Entities.Concrete;
using Metrik.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Metrik.Data.Concrete.EntityFramework.Repositories
{
    public class EfRoleRepository : EfEntityRepositoryBase<Role>, IRoleRepository
    {
        public EfRoleRepository(DbContext context) : base(context)
        {
        }
    }
}
