﻿using BaseLibrary;
using FuneralHome.Domain.Models;
using System;

namespace FuneralHome.Repositories;

/// <summary>
/// Facade interface for a SmrtniSlucaj repository
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TDomainModel"></typeparam>
public interface ISmrtniSlucajRepository
    : IRepository<int, SmrtniSlucaj>,
      IAggregateRepository<int, SmrtniSlucaj>
{
    Result<IEnumerable<SmrtniSlucaj>> GetAllWithoutFuneral();
    Result<IEnumerable<SmrtniSlucaj>> GetAllByKorisnikId(int id);
    Result<IEnumerable<SmrtniSlucaj>> GetAllWithoutFuneralByKorisnikId(int id);
}