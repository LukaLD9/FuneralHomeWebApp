﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FuneralHome.DataAccess.SqlServer.Data.DbModels
{
    public partial class Osmrtnica
    {
        public Osmrtnica()
        {
            Oglas = new HashSet<Oglas>();
        }

        [Key]
        public int IdOsmrtnica { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Naziv { get; set; }
        public byte[] Slika { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal Cijena { get; set; }

        [InverseProperty("Osmrtnica")]
        public virtual ICollection<Oglas> Oglas { get; set; }
    }
}