﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace srikanthkonnagula.Models;

public partial class ManagerData
{
    public int ManagerId { get; set; }

    public int? UserId { get; set; }

    public string ManagerName { get; set; }

    public virtual ICollection<Employee> Employee { get; set; } = new List<Employee>();

    public virtual UserDetails User { get; set; }
}