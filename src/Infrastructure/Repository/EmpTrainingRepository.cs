﻿using AutoMapper;
using Domain.Entities;
using Interface.Repository;
using Persistence.ContextModel;
using Repository.Base;

namespace Repository
{
    public class EmpTrainingRepository : BaseRepository<EmpTraining>, IEmpTrainingRepository, IDisposable
    {
        #region Config
        private ApplicationDbContext Context => Db as ApplicationDbContext;
        private readonly IMapper _iMapper;

        public EmpTrainingRepository(ApplicationDbContext db, IMapper iMapper) : base(db)
        {
            Db = db;
            _iMapper = iMapper;
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            Context.Dispose();
        }

        #endregion
    }
}
