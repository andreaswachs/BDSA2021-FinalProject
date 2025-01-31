﻿// ***********************************************************************
// Assembly         : WebService.Entities
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="Tag.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Entities;

/// <summary>Class Tag.</summary>
public class Tag
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Entities.Tag" /> class.</summary>
    public Tag(string name)
    {
        Name = name;
    }

    /// <summary>Initializes a new instance of the <see cref="T:WebService.Entities.Tag" /> class including id.</summary>
    public Tag(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }

    [StringLength(50)] public string Name { get; set; }
}

/// <summary>
///     Class WeightedTag.
///     Implements the <see cref="T:WebService.Entities.Tag" />
/// </summary>
[Owned]
public class WeightedTag : Tag
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Entities.WeightedTag" /> class.</summary>
    public WeightedTag(string name, int weight) : base(name)
    {
        Weight = weight;
    }

    /// <summary>Initializes a new instance of the <see cref="T:WebService.Entities.WeightedTag" /> class including id.</summary>
    public WeightedTag(int id, string name, int weight) : base(name)
    {
        Id = id;
        Weight = weight;
    }

    [Range(1, 10)] public int Weight { get; set; }
}