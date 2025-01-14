﻿// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="ProgrammingLanguageDTO.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Shared;

/// <summary>
///     Class CreateProgrammingLanguageDTO.
///     Implements the <see cref="System.IEquatable{CreateProgrammingLanguageDTO}" />
/// </summary>
public record CreateProgrammingLanguageDTO
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Core.Shared.CreateProgrammingLanguageDTO" /> class.</summary>
    public CreateProgrammingLanguageDTO(string name)
    {
        Name = name;
    }

    [StringLength(50)] public string Name { get; }
}

/// <summary>
///     Class ProgrammingLanguageDTO.
///     Implements the <see cref="T:WebService.Core.Shared.CreateProgrammingLanguageDTO" /> class with an added id field.
/// </summary>
public record ProgrammingLanguageDTO : CreateProgrammingLanguageDTO
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Core.Shared.ProgrammingLanguageDTO" /> class.</summary>
    public ProgrammingLanguageDTO(int id, string name) : base(name)
    {
        Id = id;
    }

    public int Id { get; }
}