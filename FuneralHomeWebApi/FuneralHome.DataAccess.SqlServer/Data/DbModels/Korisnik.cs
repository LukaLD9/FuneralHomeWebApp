﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FuneralHome.DataAccess.SqlServer.Data.DbModels
{
    [Index("Mail", Name = "IX_Korisnik", IsUnique = true)]
    [Index("Mail", Name = "IX_Korisnik_1", IsUnique = true)]
    [Index("Oib", Name = "IX_Korisnik_OIB", IsUnique = true)]
    public partial class Korisnik
    {
        public Korisnik()
        {
            Kupnja = new HashSet<Kupnja>();
            SmrtniSlucaj = new HashSet<SmrtniSlucaj>();
        }

        [Key]
        public int IdKorisnik { get; set; }
        [Required]
        [StringLength(50)]
        public string Ime { get; set; }
        [Required]
        [StringLength(50)]
        public string Prezime { get; set; }
        public DateTime DatumRodenja { get; set; }
        [Required]
        [StringLength(50)]
        public string Adresa { get; set; }
        [Required]
        [Column("OIB")]
        [StringLength(11)]
        [Unicode(false)]
        public string Oib { get; set; }
        [Required]
        [StringLength(50)]
        public string Mail { get; set; }
        [Required]
        [StringLength(100)]
        public string Lozinka { get; set; }
        [Required]
        [StringLength(1)]
        [Unicode(false)]
        public string VrstaKorisnika { get; set; }

        [InverseProperty("Korisnik")]
        public virtual Osiguranje Osiguranje { get; set; }
        [InverseProperty("Korisnik")]
        public virtual ICollection<Kupnja> Kupnja { get; set; }
        [InverseProperty("Korisnik")]
        public virtual ICollection<SmrtniSlucaj> SmrtniSlucaj { get; set; }
    }
}