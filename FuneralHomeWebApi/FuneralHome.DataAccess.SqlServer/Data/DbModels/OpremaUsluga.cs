﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FuneralHome.DataAccess.SqlServer.Data.DbModels
{
    public partial class OpremaUsluga
    {
        public OpremaUsluga()
        {
            PogrebOpremaUsluge = new HashSet<PogrebOpremaUsluge>();
        }

        [Key]
        public int IdOpremaUsluga { get; set; }
        public int VrstaOpremeUslugeId { get; set; }
        [Required]
        [StringLength(50)]
        public string Naziv { get; set; }
        public byte[] Slika { get; set; }
        public int? Zaliha { get; set; }
        [StringLength(200)]
        public string Opis { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal Cijena { get; set; }

        [ForeignKey("VrstaOpremeUslugeId")]
        [InverseProperty("OpremaUsluga")]
        public virtual VrstaOpremeUsluge VrstaOpremeUsluge { get; set; }
        [InverseProperty("OpremaUsluga")]
        public virtual ICollection<PogrebOpremaUsluge> PogrebOpremaUsluge { get; set; }
    }
}