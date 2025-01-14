﻿// ***********************************************************************
// Assembly         : WebService.Infrastructure.Tests
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="LanguageRepositoryTests.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Diagnostics.CodeAnalysis;

namespace WebService.Infrastructure.Tests.ContextRepositoryTests;

/// <summary>
///     Class LanguageRepositoryTests.
///     Contains tests grouped into regions based on the type of method tested.
/// </summary>
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public class LanguageRepositoryTests
{
        private readonly TestVariables _v;

    /// <summary>
    ///     Initializes a new instance of the <see cref="LanguageRepositoryTests" /> class.
    /// </summary>
    public LanguageRepositoryTests()
    {
        _v = new TestVariables();
    }

    #region Create

    /// <summary>
    ///     Defines the test method CreateAsync_language_returns_new_language_with_id.
    /// </summary>
    [Fact]
    public async Task CreateAsync_language_returns_new_language_with_id()
    {
        var language = new CreateLanguageDTO("Deutsch");

        var actual = await _v.LanguageRepository.CreateAsync(language);

        var expected = (Status.Created, new LanguageDTO(4, "Deutsch"));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method CreateAsync_language_returns_conflict_and_existing_language.
    /// </summary>
    [Fact]
    public async Task CreateAsync_language_returns_conflict_and_existing_language()
    {
        var language = new CreateLanguageDTO("Swedish");

        var actual = await _v.LanguageRepository.CreateAsync(language);

        var expected = (Status.Conflict, new LanguageDTO(3, "Swedish"));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method CreateAsync_language_returns_count_one_more.
    /// </summary>
    [Fact]
    public async Task CreateAsync_language_returns_count_one_more()
    {
        var language = new CreateLanguageDTO("Deutsch");

        await _v.LanguageRepository.CreateAsync(language);

        var actual = _v.LanguageRepository.ReadAsync().Result.Count;

        const int expected = 4;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method CreateAsync_language_returns_bad_request_on_name_tooLong.
    /// </summary>
    [Fact]
    public async Task CreateAsync_language_returns_bad_request_on_name_tooLong()
    {
        var language = new CreateLanguageDTO("asseocarnisanguineoviscericartilaginonervomedullary");

        var actual = await _v.LanguageRepository.CreateAsync(language);

        var expected = (Status.BadRequest, new LanguageDTO(-1, "asseocarnisanguineoviscericartilaginonervomedullary"));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method CreateAsync_language_returns_bad_request_on_name_empty.
    /// </summary>
    [Fact]
    public async Task CreateAsync_language_returns_bad_request_on_name_empty()
    {
        var language = new CreateLanguageDTO("");

        var actual = await _v.LanguageRepository.CreateAsync(language);

        var expected = (Status.BadRequest, new LanguageDTO(-1, ""));

        Assert.Equal(expected, actual);
    }


    /// <summary>
    ///     Defines the test method CreateAsync_language_returns_bad_request_on_name_whitespace.
    /// </summary>
    [Fact]
    public async Task CreateAsync_language_returns_bad_request_on_name_whitespace()
    {
        var language = new CreateLanguageDTO(" ");

        var actual = await _v.LanguageRepository.CreateAsync(language);

        var expected = (Status.BadRequest, new LanguageDTO(-1, " "));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method CreateAsync_language_with_max_length_returns_new_language_with_id.
    /// </summary>
    [Fact]
    public async Task CreateAsync_language_with_max_length_returns_new_language_with_id()
    {
        var language = new CreateLanguageDTO("asseocarnisanguineoviscericartilaginonervomedullar");

        var actual = await _v.LanguageRepository.CreateAsync(language);

        var expected = (Status.Created, new LanguageDTO(4, "asseocarnisanguineoviscericartilaginonervomedullar"));

        Assert.Equal(expected, actual);
    }

    #endregion

    #region Read

    /// <summary>
    ///     Defines the test method ReadAsync_language_by_id_returns_language_and_status_found.
    /// </summary>
    [Fact]
    public async Task ReadAsync_language_by_id_returns_language_and_status_found()
    {
        var actual = await _v.LanguageRepository.ReadAsync(1);

        var expected = (Status.Found, new LanguageDTO(1, "Danish"));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method ReadAsync_language_by_id_returns_empty_language_and_status_notFound.
    /// </summary>
    [Fact]
    public async Task ReadAsync_language_by_id_returns_empty_language_and_status_notFound()
    {
        var actual = await _v.LanguageRepository.ReadAsync(4);

        var expected = (Status.NotFound, new LanguageDTO(-1, ""));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method ReadAllAsync_returns_all_languages.
    /// </summary>
    [Fact]
    public async Task ReadAllAsync_returns_all_languages()
    {
        var actual = await _v.LanguageRepository.ReadAsync();

        var expected1 = new LanguageDTO(1, "Danish");
        var expected2 = new LanguageDTO(2, "English");
        var expected3 = new LanguageDTO(3, "Swedish");

        Assert.Collection(actual,
            languageDTO => Assert.Equal(expected1, languageDTO),
            languageDTO => Assert.Equal(expected2, languageDTO),
            languageDTO => Assert.Equal(expected3, languageDTO));
    }

    #endregion

    #region Delete

    /// <summary>
    ///     Defines the test method DeleteAsync_language_by_id_returns_status_deleted.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_language_by_id_returns_status_deleted()
    {
        var actual = await _v.LanguageRepository.DeleteAsync(1);

        const Status expected = Status.Deleted;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method DeleteAsync_language_by_id_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_language_by_id_returns_status_notFound()
    {
        var actual = await _v.LanguageRepository.DeleteAsync(4);

        const Status expected = Status.NotFound;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method DeleteAsync_language_by_id_returns_count_one_less.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_language_by_id_returns_count_one_less()
    {
        await _v.LanguageRepository.DeleteAsync(3);

        var actual = _v.LanguageRepository.ReadAsync().Result.Count;

        const int expected = 2;

        Assert.Equal(expected, actual);
    }

    #endregion

    #region Update

    /// <summary>
    ///     Defines the test method UpdateAsync_language_by_id_returns_status_updated.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_language_by_id_returns_status_updated()
    {
        var updateLanguageDTO = new LanguageDTO(3, "German");

        var actual = await _v.LanguageRepository.UpdateAsync(updateLanguageDTO);

        const Status expected = Status.Updated;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_language_by_id_read_updated_returns_status_found_and_updated_language.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_language_by_id_read_updated_returns_status_found_and_updated_language()
    {
        var updateLanguageDTO = new LanguageDTO(3, "German");

        await _v.LanguageRepository.UpdateAsync(updateLanguageDTO);

        var actual = await _v.LanguageRepository.ReadAsync(3);

        var expected = (Status.Found, updateLanguageDTO);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_language_by_id_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_language_by_id_returns_status_notFound()
    {
        var updateLanguageDTO = new LanguageDTO(4, "German");

        var actual = await _v.LanguageRepository.UpdateAsync(updateLanguageDTO);

        const Status expected = Status.NotFound;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_language_by_id_returns_status_conflict.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_language_by_id_returns_status_conflict()
    {
        var updateLanguageDTO = new LanguageDTO(3, "Danish");

        var actual = await _v.LanguageRepository.UpdateAsync(updateLanguageDTO);

        const Status expected = Status.Conflict;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_language_returns_bad_request_on_name_tooLong.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_language_returns_bad_request_on_name_tooLong()
    {
        var language = new LanguageDTO(1, "asseocarnisanguineoviscericartilaginonervomedullary");

        var actual = await _v.LanguageRepository.UpdateAsync(language);

        const Status expected = Status.BadRequest;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_language_returns_bad_request_on_name_empty.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_language_returns_bad_request_on_name_empty()
    {
        var language = new LanguageDTO(1, "");

        var actual = await _v.LanguageRepository.UpdateAsync(language);

        const Status expected = Status.BadRequest;

        Assert.Equal(expected, actual);
    }


    /// <summary>
    ///     Defines the test method UpdateAsync_language_returns_bad_request_on_name_whitespace.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_language_returns_bad_request_on_name_whitespace()
    {
        var language = new LanguageDTO(1, " ");

        var actual = await _v.LanguageRepository.UpdateAsync(language);

        const Status expected = Status.BadRequest;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_language_with_max_length_returns_updated.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_language_with_max_length_returns_updated()
    {
        var language = new LanguageDTO(1, "asseocarnisanguineoviscericartilaginonervomedullar");

        var actual = await _v.LanguageRepository.UpdateAsync(language);

        const Status expected = Status.Updated;

        Assert.Equal(expected, actual);
    }

    #endregion
}