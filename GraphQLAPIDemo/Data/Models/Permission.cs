﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GraphQLAPIDemo.Data.Models
{
    public partial class Permission
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [Required]
        public string TableName { get; set; }
        public short TableAccessLevel { get; set; }
        [Required]
        public string FieldName { get; set; }
        public short FieldAccessLevel { get; set; }
        public string FieldDataType { get; set; }
        public string FieldProperties { get; set; }
        public int? FieldOrder { get; set; }
    }
}