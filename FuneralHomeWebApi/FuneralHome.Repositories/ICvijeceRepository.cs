﻿using BaseLibrary;
using FuneralHome.Domain.Models;
using System.Data;

namespace FuneralHome.Repositories;

/// <summary>
/// Facade interface for a Cvijece repository
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TDomainModel"></typeparam>
public interface ICvijeceRepository
    : IRepository<int, Cvijece>
{
   
}