﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FuneralHomeWebApi.Data.DbModels
{
    public partial class NadgrobniZnak
    {
        public NadgrobniZnak()
        {
            Pogreb = new HashSet<Pogreb>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Naziv { get; set; }
        public byte[] Slika { get; set; }
        public int Kolicina { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal Cijena { get; set; }

        [InverseProperty("NadgrobniZnak")]
        public virtual ICollection<Pogreb> Pogreb { get; set; }
    }
}