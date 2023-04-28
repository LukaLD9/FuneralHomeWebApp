﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FuneralHome.DataAccess.SqlServer.Data.DbModels
{
    public partial class VrstaOpreme
    {
        public VrstaOpreme()
        {
            Oprema = new HashSet<Oprema>();
        }

        [Key]
        public int IdVrstaOpreme { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Naziv { get; set; }

        [InverseProperty("VrstaOpreme")]
        public virtual ICollection<Oprema> Oprema { get; set; }
    }
}