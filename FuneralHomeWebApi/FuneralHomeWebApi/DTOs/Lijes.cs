﻿using FuneralHome.DataAccess.SqlServer.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainModels = FuneralHome.Domain.Models;


namespace FuneralHome.DTOs;

public class Lijes
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name can't be null")]
    [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters")]
    public string Naziv { get; set; } = string.Empty;

    [Required(ErrorMessage = "Size can't be null")]
    [StringLength(50, ErrorMessage = "Size can't be longer than 50 characters")]
    public string Velicina { get; set; } = string.Empty;

    public byte[]? Slika { get; set; }
    public int Kolicina { get; set; }
    public decimal Cijena { get; set; }
}


public static partial class DtoMapping
{
    public static Lijes ToDto(this DomainModels.Lijes lijes)
        => new Lijes()
        {
            Id = lijes.Id,
            Naziv = lijes.Naziv,
            Velicina = lijes.Velicina,
            Slika = lijes.Slika,
            Kolicina = lijes.Kolicina,
            Cijena = lijes.Cijena
        };

    public static DomainModels.Lijes ToDomain(this Lijes lijes)
        => new DomainModels.Lijes(
            lijes.Id,
            lijes.Naziv,
            lijes.Velicina,
            lijes.Slika,
            lijes.Kolicina,
            lijes.Cijena
        );
}