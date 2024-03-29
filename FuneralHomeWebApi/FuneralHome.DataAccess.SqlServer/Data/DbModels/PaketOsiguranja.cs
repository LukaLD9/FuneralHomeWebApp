﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FuneralHome.DataAccess.SqlServer.Data.DbModels
{
    public partial class PaketOsiguranja
    {
        public PaketOsiguranja()
        {
            Osiguranje = new HashSet<Osiguranje>();
        }

        [Key]
        public int IdPaketOsiguranja { get; set; }
        [Required]
        [StringLength(50)]
        public string Naziv { get; set; }

        [InverseProperty("PaketOsiguranja")]
        public virtual ICollection<Osiguranje> Osiguranje { get; set; }
    }
}