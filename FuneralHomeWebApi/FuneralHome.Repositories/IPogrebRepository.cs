﻿using BaseLibrary;
using FuneralHome.Domain.Models;
using System;

namespace FuneralHome.Repositories;

/// <summary>
/// Facade interface for a Pogreb repository
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TDomainModel"></typeparam>
public interface IPogrebRepository
    : IRepository<int, Pogreb>,
      IAggregateRepository<int, Pogreb>
{
    Result<IEnumerable<PogrebSmrtniSlucaj>> GetAllPogrebSmrtniSlucaj();
    Result Update(PogrebSmrtniSlucaj model);
    Result Insert(PogrebSmrtniSlucaj model);
    Result<IEnumerable<PogrebSmrtniSlucaj>> GetAllByKorisnikId(int korisnikId);
    Result<Pogreb> GetBySmrtniSlucajId(int smrtniSlucajId);
}