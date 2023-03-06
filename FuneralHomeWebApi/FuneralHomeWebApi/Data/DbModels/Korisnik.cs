﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FuneralHomeWebApi.Data.DbModels
{
    [Index("Oib", Name = "IX_Korisnik_OIB", IsUnique = true)]
    public partial class Korisnik
    {
        public Korisnik()
        {
            SmrtniSlucaj = new HashSet<SmrtniSlucaj>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Ime { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Prezime { get; set; }

        //public DateOnly DatumRodenja { get; set; }
        public DateTime DatumRodenja { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Adresa { get; set; }
        [Column("OIB")]
        public int Oib { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Mail { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Lozinka { get; set; }

        public virtual Osiguranje IdNavigation { get; set; }
        [InverseProperty("Korisnik")]
        public virtual ICollection<SmrtniSlucaj> SmrtniSlucaj { get; set; }
    }
}