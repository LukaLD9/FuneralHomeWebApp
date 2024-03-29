﻿using FuneralHome.DataAccess.SqlServer.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainModels = FuneralHome.Domain.Models;

namespace FuneralHome.DTOs;

public class PogrebAggregate
{
    public int Id { get; set; }
    public int SmrtniSlucajId { get; set; }
    //public DateOnly DatumPogreba { get; set; }
    public DateTime DatumPogreba { get; set; }
    public bool Kremacija { get; set; }
    public DateTime DatumUgovaranja { get; set; }
    public decimal UkupnaCijena { get; set; }
    public IEnumerable<PogrebOpremaUsluge> OpremaUsluge { get; set; } = Enumerable.Empty<PogrebOpremaUsluge>();

    public SmrtniSlucaj SmrtniSlucaj { get; set; } = new SmrtniSlucaj();
    public Korisnik Korisnik { get; set; } = new Korisnik();
}


public static partial class DtoMapping
{
    public static PogrebAggregate ToAggregateDto(this DomainModels.Pogreb pogreb)
        => new PogrebAggregate()
        {
            Id = pogreb.Id,
            SmrtniSlucajId = pogreb.SmrtniSlucajId,
            DatumPogreba = pogreb.DatumPogreba,
            Kremacija = pogreb.Kremacija,
            DatumUgovaranja = pogreb.DatumUgovaranja,
            UkupnaCijena = pogreb.UkupnaCijena,
            OpremaUsluge = pogreb.PogrebOpremaUsluge == null
                            ? new List<PogrebOpremaUsluge>()
                            : pogreb.PogrebOpremaUsluge.Select(pa => pa.ToDto()).ToList(),
            SmrtniSlucaj = pogreb.SmrtniSlucaj!.ToDto(),
            Korisnik = pogreb.Korisnik!.ToDto()
        };

    public static DomainModels.Pogreb ToDomain(this PogrebAggregate pogreb)
        => new DomainModels.Pogreb(
            pogreb.Id,
            pogreb.SmrtniSlucajId,
            pogreb.DatumPogreba,
            pogreb.Kremacija,
            pogreb.DatumUgovaranja,
            pogreb.UkupnaCijena,
            pogreb.Korisnik.ToDomain(),
            pogreb.SmrtniSlucaj.ToDomain(),
            pogreb.OpremaUsluge.Select(ToDomain)
        );
}
