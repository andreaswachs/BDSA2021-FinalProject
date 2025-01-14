﻿// ***********************************************************************
// Assembly         : WebService.Infrastructure.Tests
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="LevelRepositoryTests.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Diagnostics.CodeAnalysis;

namespace WebService.Infrastructure.Tests.ContextRepositoryTests;

/// <summary>
///     Class LevelRepositoryTests.
///     Contains tests grouped into regions based on the type of method tested.
/// </summary>
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public class LevelRepositoryTests
{
    private readonly TestVariables _v;

    /// <summary>
    ///     Initializes a new instance of the <see cref="LevelRepositoryTests" /> class.
    /// </summary>
    public LevelRepositoryTests()
    {
        _v = new TestVariables();
    }

    #region Create

    /// <summary>
    ///     Defines the test method CreateAsync_level_returns_new_level_with_id.
    /// </summary>
    [Fact]
    public async Task CreateAsync_level_returns_new_level_with_id()
    {
        var level = new CreateLevelDTO("Highschool");

        var actual = await _v.LevelRepository.CreateAsync(level);

        var expected = (Status.Created, new LevelDTO(4, "Highschool"));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method CreateAsync_level_returns_conflict_and_existing_level.
    /// </summary>
    [Fact]
    public async Task CreateAsync_level_returns_conflict_and_existing_level()
    {
        var level = new CreateLevelDTO("PHD");

        var actual = await _v.LevelRepository.CreateAsync(level);

        var expected = (Status.Conflict, new LevelDTO(3, "PHD"));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method CreateAsync_level_returns_count_one_more.
    /// </summary>
    [Fact]
    public async Task CreateAsync_level_returns_count_one_more()
    {
        var level = new CreateLevelDTO("Highschool");

        await _v.LevelRepository.CreateAsync(level);

        var actual = _v.LevelRepository.ReadAsync().Result.Count;

        const int expected = 4;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method CreateAsync_level_returns_bad_request_on_name_tooLong.
    /// </summary>
    [Fact]
    public async Task CreateAsync_level_returns_bad_request_on_name_tooLong()
    {
        var level = new CreateLevelDTO("asseocarnisanguineoviscericartilaginonervomedullary");

        var actual = await _v.LevelRepository.CreateAsync(level);

        var expected = (Status.BadRequest, new LevelDTO(-1, "asseocarnisanguineoviscericartilaginonervomedullary"));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method CreateAsync_level_returns_bad_request_on_name_empty.
    /// </summary>
    [Fact]
    public async Task CreateAsync_level_returns_bad_request_on_name_empty()
    {
        var level = new CreateLevelDTO("");

        var actual = await _v.LevelRepository.CreateAsync(level);

        var expected = (Status.BadRequest, new LevelDTO(-1, ""));

        Assert.Equal(expected, actual);
    }


    /// <summary>
    ///     Defines the test method CreateAsync_level_returns_bad_request_on_name_whitespace.
    /// </summary>
    [Fact]
    public async Task CreateAsync_level_returns_bad_request_on_name_whitespace()
    {
        var level = new CreateLevelDTO(" ");

        var actual = await _v.LevelRepository.CreateAsync(level);

        var expected = (Status.BadRequest, new LevelDTO(-1, " "));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method CreateAsync_level_with_max_length_returns_new_language_with_id.
    /// </summary>
    [Fact]
    public async Task CreateAsync_level_with_max_length_returns_new_language_with_id()
    {
        var level = new CreateLevelDTO("asseocarnisanguineoviscericartilaginonervomedullar");

        var actual = await _v.LevelRepository.CreateAsync(level);

        var expected = (Status.Created, new LevelDTO(4, "asseocarnisanguineoviscericartilaginonervomedullar"));

        Assert.Equal(expected, actual);
    }

    #endregion

    #region Read

    /// <summary>
    ///     Defines the test method ReadAsync_level_by_id_returns_level_and_status_found.
    /// </summary>
    [Fact]
    public async Task ReadAsync_level_by_id_returns_level_and_status_found()
    {
        var actual = await _v.LevelRepository.ReadAsync(1);

        var expected = (Status.Found, new LevelDTO(1, "Bachelor"));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method ReadAsync_level_by_id_returns_empty_level_and_status_notFound.
    /// </summary>
    [Fact]
    public async Task ReadAsync_level_by_id_returns_empty_level_and_status_notFound()
    {
        var actual = await _v.LevelRepository.ReadAsync(4);

        var expected = (Status.NotFound, new LevelDTO(-1, ""));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method ReadAllAsync_returns_all_levels.
    /// </summary>
    [Fact]
    public async Task ReadAllAsync_returns_all_levels()
    {
        var actual = await _v.LevelRepository.ReadAsync();

        var expected1 = new LevelDTO(1, "Bachelor");
        var expected2 = new LevelDTO(2, "Master");
        var expected3 = new LevelDTO(3, "PHD");

        Assert.Collection(actual,
            levelDTO => Assert.Equal(expected1, levelDTO),
            levelDTO => Assert.Equal(expected2, levelDTO),
            levelDTO => Assert.Equal(expected3, levelDTO));
    }

    #endregion

    #region Delete

    /// <summary>
    ///     Defines the test method DeleteAsync_level_by_id_returns_status_deleted.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_level_by_id_returns_status_deleted()
    {
        var actual = await _v.LevelRepository.DeleteAsync(1);

        const Status expected = Status.Deleted;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method DeleteAsync_level_by_id_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_level_by_id_returns_status_notFound()
    {
        var actual = await _v.LevelRepository.DeleteAsync(4);

        const Status expected = Status.NotFound;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method DeleteAsync_level_by_id_returns_count_one_less.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_level_by_id_returns_count_one_less()
    {
        await _v.LevelRepository.DeleteAsync(3);

        var actual = _v.LevelRepository.ReadAsync().Result.Count;

        const int expected = 2;

        Assert.Equal(expected, actual);
    }

    #endregion

    #region Update

    /// <summary>
    ///     Defines the test method UpdateAsync_level_by_id_returns_status_updated.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_level_by_id_returns_status_updated()
    {
        var updateLevelDTO = new LevelDTO(3, "Highschool");

        var actual = await _v.LevelRepository.UpdateAsync(updateLevelDTO);

        const Status expected = Status.Updated;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_level_by_id_read_updated_returns_status_found_and_updated_level.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_level_by_id_read_updated_returns_status_found_and_updated_level()
    {
        var updateLevelDTO = new LevelDTO(3, "Highschool");

        await _v.LevelRepository.UpdateAsync(updateLevelDTO);

        var actual = await _v.LevelRepository.ReadAsync(3);

        var expected = (Status.Found, updateLevelDTO);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_level_by_id_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_level_by_id_returns_status_notFound()
    {
        var updateLevelDTO = new LevelDTO(4, "Highscool");

        var actual = await _v.LevelRepository.UpdateAsync(updateLevelDTO);

        const Status expected = Status.NotFound;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_level_by_id_returns_status_conflict.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_level_by_id_returns_status_conflict()
    {
        var updateLevelDTO = new LevelDTO(3, "Bachelor");

        var actual = await _v.LevelRepository.UpdateAsync(updateLevelDTO);

        const Status expected = Status.Conflict;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_level_returns_bad_request_on_name_tooLong.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_level_returns_bad_request_on_name_tooLong()
    {
        var level = new LevelDTO(1, "asseocarnisanguineoviscericartilaginonervomedullary");

        var actual = await _v.LevelRepository.UpdateAsync(level);

        const Status expected = Status.BadRequest;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_level_returns_bad_request_on_name_empty.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_level_returns_bad_request_on_name_empty()
    {
        var level = new LevelDTO(1, "");

        var actual = await _v.LevelRepository.UpdateAsync(level);

        const Status expected = Status.BadRequest;

        Assert.Equal(expected, actual);
    }


    /// <summary>
    ///     Defines the test method UpdateAsync_level_returns_bad_request_on_name_whitespace.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_level_returns_bad_request_on_name_whitespace()
    {
        var level = new LevelDTO(1, " ");

        var actual = await _v.LevelRepository.UpdateAsync(level);

        const Status expected = Status.BadRequest;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_level_with_max_length_returns_updated.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_level_with_max_length_returns_updated()
    {
        var level = new LevelDTO(1, "asseocarnisanguineoviscericartilaginonervomedullar");

        var actual = await _v.LevelRepository.UpdateAsync(level);

        const Status expected = Status.Updated;

        Assert.Equal(expected, actual);
    }

    #endregion
}