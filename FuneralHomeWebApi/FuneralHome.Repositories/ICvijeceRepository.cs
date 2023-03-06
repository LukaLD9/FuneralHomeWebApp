﻿namespace FuneralHome.Repositories;

/// <summary>
/// Facade interface for a Person repository
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TModel"></typeparam>
public interface ICvijeceRepository<TKey, TModel> : IRepository<TKey, TModel>
{
}