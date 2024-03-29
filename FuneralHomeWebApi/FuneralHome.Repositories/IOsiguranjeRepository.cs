﻿using BaseLibrary;
using FuneralHome.Domain.Models;
using System.Data;

namespace FuneralHome.Repositories;

/// <summary>
/// Facade interface for a Osiguranje repository
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TDomainModel"></typeparam>
public interface IOsiguranjeRepository
    : IRepository<int, Osiguranje>,
    IAggregateRepository<int, Osiguranje>
{
    Result<IEnumerable<Osiguranje>> GetByKorisnikId(int id);
    Result<IEnumerable<Osiguranje>> GetBySmrtniSlucajId(int id);
}