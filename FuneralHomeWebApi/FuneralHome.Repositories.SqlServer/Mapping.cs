﻿using FuneralHome.Domain.Models;
using System.Data;
using System;
using DbModels = FuneralHome.DataAccess.SqlServer.Data.DbModels;
namespace FuneralHome.Repositories.SqlServer;
public static class Mapping
{
    
    public static Korisnik ToDomain(this DbModels.Korisnik korisnik)
        => new Korisnik(
            korisnik.IdKorisnik,
            korisnik.Ime,
            korisnik.Prezime,
            korisnik.DatumRodenja,
            korisnik.Adresa,
            korisnik.Oib,
            korisnik.Mail,
            korisnik.Lozinka,
            korisnik.VrstaKorisnika
            // TO DO mozda neki agregat slozit
        );

    public static DbModels.Korisnik ToDbModel(this Korisnik korisnik)
        => new DbModels.Korisnik()
        {
            IdKorisnik = korisnik.Id,
            Ime = korisnik.Ime,
            Prezime = korisnik.Prezime,
            DatumRodenja = korisnik.DatumRodenja,
            Adresa = korisnik.Adresa,
            Oib = korisnik.Oib,
            Mail = korisnik.Mail,
            Lozinka = korisnik.Lozinka,
            VrstaKorisnika = korisnik.VrstaKorisnika
        };

   

    public static Osmrtnica ToDomain(this DbModels.Osmrtnica osmrtnica)
       => new Osmrtnica(
           osmrtnica.IdOsmrtnica,
           osmrtnica.SlikaPok,
           osmrtnica.Opis,
           osmrtnica.ObjavaNaStranici
       );

    public static DbModels.Osmrtnica ToDbModel(this Osmrtnica osmrtnica)
        => new DbModels.Osmrtnica()
        {
           IdOsmrtnica = osmrtnica.Id,
           SlikaPok = osmrtnica.SlikaPok,
           Opis = osmrtnica.Opis,
           ObjavaNaStranici = osmrtnica.ObjavaNaStranici
        };

    


    public static Osiguranje ToDomain(this DbModels.Osiguranje osiguranje)
       => new Osiguranje(
           osiguranje.IdOsiguranje,
           osiguranje.KorisnikId,
           osiguranje.Korisnik.Ime,
           osiguranje.Korisnik.Prezime,
           osiguranje.DatumUgovaranja,
           osiguranje.PlacanjeNaRate,
           osiguranje.BrojRata,
           osiguranje.PaketOsiguranjaId,
           osiguranje.PaketOsiguranja.Naziv,
           osiguranje.PaketOsiguranja.Cijena
       );

    public static DbModels.Osiguranje ToDbModel(this Osiguranje osiguranje)
        => new DbModels.Osiguranje()
        {
            IdOsiguranje = osiguranje.Id,
            KorisnikId = osiguranje.KorisnikId,
            DatumUgovaranja = osiguranje.DatumUgovaranja,
            PlacanjeNaRate = osiguranje.PlacanjeNaRate,
            BrojRata = osiguranje.BrojRata,
            PaketOsiguranjaId = osiguranje.PaketOsiguranjaId
        };

    public static PaketOsiguranja ToDomain(this DbModels.PaketOsiguranja paket)
        => new PaketOsiguranja(
                paket.IdPaketOsiguranja,
                paket.Naziv,
                paket.Cijena
           );

    public static DbModels.PaketOsiguranja ToDbModel(this PaketOsiguranja paket)
        => new DbModels.PaketOsiguranja()
        {
            IdPaketOsiguranja = paket.Id,
            Naziv = paket.Naziv,
            Cijena = paket.Cijena
        };

    public static Pogreb ToDomain(this DbModels.Pogreb pogreb)
       => new Pogreb(
           pogreb.IdPogreb,
           pogreb.SmrtniSlucajId,
           pogreb.DatumPogreb,
           pogreb.Kremacija,
           pogreb.UkupnaCijena,
           pogreb.SmrtniSlucaj?.Korisnik.ToDomain(),
           pogreb.SmrtniSlucaj?.ToDomain(),
           pogreb.PogrebOprema.Select(ToDomain),
           pogreb.Usluga.Select(ToDomain)
       );

    public static DbModels.Pogreb ToDbModel(this Pogreb pogreb)
        => new DbModels.Pogreb()
        {
            IdPogreb = pogreb.Id,
            SmrtniSlucajId = pogreb.SmrtniSlucajId,
            DatumPogreb = pogreb.DatumPogreba,
            Kremacija = pogreb.Kremacija,
            UkupnaCijena = pogreb.UkupnaCijena,
           // PogrebOprema = pogreb.PogrebOprema.Select(po => po.ToDbModel(pogreb.Id)).ToList(),
           // Usluga = pogreb.PogrebUsluga.Select(pu => pu.ToDbModel()).ToList(),
           // SmrtniSlucaj = pogreb.SmrtniSlucaj?.ToDbModel()
        };

    public static PogrebOprema ToDomain(this DbModels.PogrebOprema pogrebOprema)
        => new PogrebOprema(
            pogrebOprema.Oprema.ToDomain(),
            pogrebOprema.Kolicina
            );
    public static DbModels.PogrebOprema ToDbModel(this PogrebOprema pogrebOprema, int pogrebId)
       => new DbModels.PogrebOprema()
       {
           PogrebId = pogrebId,
           OpremaId = pogrebOprema.Oprema.Id,
           Kolicina = pogrebOprema.Kolicina
       };

    public static PogrebSmrtniSlucaj ToDomain2(this DbModels.Pogreb pogreb)
        => new PogrebSmrtniSlucaj(
                pogreb.IdPogreb,
                pogreb.SmrtniSlucajId,
                pogreb.SmrtniSlucaj.ImePok,
                pogreb.SmrtniSlucaj.PrezimePok,
                pogreb.SmrtniSlucaj.DatumSmrtiPok,
                pogreb.DatumPogreb,
                pogreb.Kremacija,
                pogreb.UkupnaCijena,
                pogreb.SmrtniSlucaj.KorisnikId,
                pogreb.SmrtniSlucaj.Korisnik.Ime,
                pogreb.SmrtniSlucaj.Korisnik.Prezime
            );

    public static DbModels.Pogreb ToDbModel(this PogrebSmrtniSlucaj pogreb)
        => new DbModels.Pogreb()
        {
            IdPogreb = pogreb.Id,
            SmrtniSlucajId = pogreb.SmrtniSlucajId,
            DatumPogreb = pogreb.DatumPogreba,
            Kremacija = pogreb.Kremacija,
            UkupnaCijena = pogreb.UkupnaCijena
        };

    public static SmrtniSlucaj ToDomain(this DbModels.SmrtniSlucaj smrtniSlucaj)
       => new SmrtniSlucaj(
           smrtniSlucaj.IdSmrtniSlucaj,
           smrtniSlucaj.KorisnikId,
           smrtniSlucaj.ImePok,
           smrtniSlucaj.PrezimePok,
           smrtniSlucaj.DatumRodenjaPok,
           smrtniSlucaj.DatumSmrtiPok,
           smrtniSlucaj.Oibpok
       );

    public static DbModels.SmrtniSlucaj ToDbModel(this SmrtniSlucaj smrtniSlucaj)
        => new DbModels.SmrtniSlucaj()
        {
           IdSmrtniSlucaj = smrtniSlucaj.Id,
           KorisnikId = smrtniSlucaj.KorisnikId,
           ImePok = smrtniSlucaj.ImePok,
           PrezimePok = smrtniSlucaj.PrezimePok,
           DatumRodenjaPok = smrtniSlucaj.DatumRodenjaPok,
           DatumSmrtiPok = smrtniSlucaj.DatumSmrtiPok,
           Oibpok = smrtniSlucaj.Oibpok
        };

    public static Usluga ToDomain(this DbModels.Usluga usluga)
       => new Usluga(
           usluga.IdUsluga,
           usluga.Naziv,
           usluga.VrstaUslugeId,
           usluga.VrstaUsluge.Naziv,
           usluga.Opis,
           usluga.Cijena
       );

    public static DbModels.Usluga ToDbModel(this Usluga usluga)
        => new DbModels.Usluga()
        {
            IdUsluga = usluga.Id,
            Naziv = usluga.Naziv,
            VrstaUslugeId = usluga.VrstaUslugeId,
            Opis = usluga.Opis,
            Cijena = usluga.Cijena
        };

    public static VrstaUsluge ToDomain(this DbModels.VrstaUsluge vrstaUsluge)
        => new VrstaUsluge(
               vrstaUsluge.IdVrstaUsluge,
               vrstaUsluge.Naziv
           );

    public static DbModels.VrstaUsluge ToDbModel(this VrstaUsluge vrstaUsluge)
        => new DbModels.VrstaUsluge()
        {
            IdVrstaUsluge = vrstaUsluge.Id,
            Naziv = vrstaUsluge.Naziv
        };

    public static Oprema ToDomain(this DbModels.Oprema oprema)
        => new Oprema(
            oprema.IdOprema,
            oprema.Naziv,
            oprema.VrstaOpremeId,
            oprema.VrstaOpreme.Naziv,
            oprema.Slika,
            oprema.ZalihaOpreme,
            oprema.Cijena
           );

    public static DbModels.Oprema ToDbModel(this Oprema oprema)
        => new DbModels.Oprema()
        {
            IdOprema = oprema.Id,
            Naziv = oprema.Naziv,
            VrstaOpremeId = oprema.VrstaOpremeId,
            Slika = oprema.Slika,
            ZalihaOpreme = oprema.ZalihaOpreme,
            Cijena = oprema.Cijena
        };

    public static VrstaOpreme ToDomain(this DbModels.VrstaOpreme vrstaOpreme)
        => new VrstaOpreme(
                vrstaOpreme.IdVrstaOpreme,
                vrstaOpreme.Naziv
                );

    public static DbModels.VrstaOpreme ToDbModel(this VrstaOpreme vrstaOpreme)
        => new DbModels.VrstaOpreme()
        {
            IdVrstaOpreme = vrstaOpreme.Id,
            Naziv = vrstaOpreme.Naziv
        };

}