﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FuneralHome.DataAccess.SqlServer.Data.DbModels
{
    public partial class VrstaOpremeUsluge
    {
        public VrstaOpremeUsluge()
        {
            OpremaUsluga = new HashSet<OpremaUsluga>();
        }

        [Key]
        public int IdVrstaOpremeUsluge { get; set; }
        [Required]
        [StringLength(50)]
        public string Naziv { get; set; }
        public bool JeOprema { get; set; }
        public int JedinicaMjereId { get; set; }

        [ForeignKey("JedinicaMjereId")]
        [InverseProperty("VrstaOpremeUsluge")]
        public virtual JedinicaMjere JedinicaMjere { get; set; }
        [InverseProperty("VrstaOpremeUsluge")]
        public virtual ICollection<OpremaUsluga> OpremaUsluga { get; set; }
    }
}