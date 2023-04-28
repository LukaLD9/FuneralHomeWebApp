﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FuneralHome.DataAccess.SqlServer.Data.DbModels
{
    public partial class Oprema
    {
        public Oprema()
        {
            PogrebOprema = new HashSet<PogrebOprema>();
        }

        [Key]
        public int IdOprema { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Naziv { get; set; }
        public int VrstaOpremeId { get; set; }
        public byte[] Slika { get; set; }
        public int ZalihaOpreme { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal Cijena { get; set; }

        [ForeignKey("VrstaOpremeId")]
        [InverseProperty("Oprema")]
        public virtual VrstaOpreme VrstaOpreme { get; set; }
        [InverseProperty("Oprema")]
        public virtual ICollection<PogrebOprema> PogrebOprema { get; set; }
    }
}