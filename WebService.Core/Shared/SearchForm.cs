﻿// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="SearchForm.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Shared;

/// <summary>Class SearchForm.</summary>
public class SearchForm
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Core.Shared.SearchForm" /> class.</summary>
    public SearchForm(string textField, ICollection<TagDTO> tags, ICollection<LevelDTO> levels,
        ICollection<ProgrammingLanguageDTO> programmingLanguages, ICollection<LanguageDTO> languages,
        ICollection<MediaDTO> medias, int rating)
    {
        TextField = textField;
        Tags = tags;
        Levels = levels;
        ProgrammingLanguages = programmingLanguages;
        Languages = languages;
        Medias = medias;
        Rating = rating;
    }

    public string TextField { get; set; }
    public ICollection<TagDTO> Tags { get; set; }
    public ICollection<LevelDTO> Levels { get; }
    public ICollection<ProgrammingLanguageDTO> ProgrammingLanguages { get; }
    public ICollection<LanguageDTO> Languages { get; }
    public ICollection<MediaDTO> Medias { get; }
    public int Rating { get; }
}