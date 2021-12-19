﻿// ***********************************************************************
// Assembly         : WebService.Entities
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-08-2021
// ***********************************************************************
// <copyright file="Media.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Entities;

/// <summary>Class Media.</summary>
public class Media
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Entities.Media" /> class.</summary>
    public Media(string name)
    {
        Name = name;
    }

    /// <summary>Initializes a new instance of the <see cref="T:WebService.Entities.Media" /> class including id.</summary>
    public Media(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public ICollection<Material> Materials { get; set; } = new List<Material>();

    [StringLength(50)] public string Name { get; set; }
}