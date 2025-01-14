﻿// ***********************************************************************
// Assembly         : WebService.Infrastructure.Tests
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="TagRepositoryTests.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Diagnostics.CodeAnalysis;

namespace WebService.Infrastructure.Tests.ContextRepositoryTests;

/// <summary>
///     Class TagRepositoryTests.
///     Contains tests grouped into regions based on the type  of method tested.
/// </summary>
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public class TagRepositoryTests
{
    private readonly TestVariables _v;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TagRepositoryTests" /> class, creating new test variables.
    /// </summary>
    public TagRepositoryTests()
    {
        _v = new TestVariables();
    }

    #region Create

    /// <summary>
    ///     Defines the test method CreateAsync_tag_returns_new_tag_with_id.
    /// </summary>
    [Fact]
    public async Task CreateAsync_tag_returns_new_tag_with_id()
    {
        var tag = new CreateTagDTO("Database");

        var actual = await _v.TagRepository.CreateAsync(tag);

        var expected = (Status.Created, new TagDTO(4, "Database"));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method CreateAsync_tag_returns_conflict_and_existing_tag.
    /// </summary>
    [Fact]
    public async Task CreateAsync_tag_returns_conflict_and_existing_tag()
    {
        var tag = new CreateTagDTO("API");

        var actual = await _v.TagRepository.CreateAsync(tag);

        var expected = (Status.Conflict, new TagDTO(3, "API"));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method CreateAsync_tag_returns_count_one_more.
    /// </summary>
    [Fact]
    public async Task CreateAsync_tag_returns_count_one_more()
    {
        var tag = new CreateTagDTO("Database");

        await _v.TagRepository.CreateAsync(tag);

        var actual = _v.TagRepository.ReadAsync().Result.Count;

        const int expected = 4;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method CreateAsync_tag_returns_bad_request_on_name_tooLong.
    /// </summary>
    [Fact]
    public async Task CreateAsync_tag_returns_bad_request_on_name_tooLong()
    {
        var tag = new CreateTagDTO("asseocarnisanguineoviscericartilaginonervomedullary");

        var actual = await _v.TagRepository.CreateAsync(tag);

        var expected = (Status.BadRequest, new TagDTO(-1, "asseocarnisanguineoviscericartilaginonervomedullary"));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method CreateAsync_tag_returns_bad_request_on_name_empty.
    /// </summary>
    [Fact]
    public async Task CreateAsync_tag_returns_bad_request_on_name_empty()
    {
        var tag = new CreateTagDTO("");

        var actual = await _v.TagRepository.CreateAsync(tag);

        var expected = (Status.BadRequest, new TagDTO(-1, ""));

        Assert.Equal(expected, actual);
    }


    /// <summary>
    ///     Defines the test method CreateAsync_tag_returns_bad_request_on_name_whitespace.
    /// </summary>
    [Fact]
    public async Task CreateAsync_tag_returns_bad_request_on_name_whitespace()
    {
        var tag = new CreateTagDTO(" ");

        var actual = await _v.TagRepository.CreateAsync(tag);

        var expected = (Status.BadRequest, new TagDTO(-1, " "));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method CreateAsync_tag_with_max_length_returns_new_language_with_id.
    /// </summary>
    [Fact]
    public async Task CreateAsync_tag_with_max_length_returns_new_language_with_id()
    {
        var tag = new CreateTagDTO("asseocarnisanguineoviscericartilaginonervomedullar");

        var actual = await _v.TagRepository.CreateAsync(tag);

        var expected = (Status.Created, new TagDTO(4, "asseocarnisanguineoviscericartilaginonervomedullar"));

        Assert.Equal(expected, actual);
    }

    #endregion

    #region Read

    /// <summary>
    ///     Defines the test method ReadAsync_tag_by_id_returns_tag_and_status_found.
    /// </summary>
    [Fact]
    public async Task ReadAsync_tag_by_id_returns_tag_and_status_found()
    {
        var actual = await _v.TagRepository.ReadAsync(1);

        var expected = (Status.Found, new TagDTO(1, "SOLID"));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method ReadAsync_tag_by_id_returns_empty_tag_and_status_notFound.
    /// </summary>
    [Fact]
    public async Task ReadAsync_tag_by_id_returns_empty_tag_and_status_notFound()
    {
        var actual = await _v.TagRepository.ReadAsync(4);

        var expected = (Status.NotFound, new TagDTO(-1, ""));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method ReadAllAsync_returns_all_tags.
    /// </summary>
    [Fact]
    public async Task ReadAllAsync_returns_all_tags()
    {
        var actual = await _v.TagRepository.ReadAsync();

        var expected1 = new TagDTO(1, "SOLID");
        var expected2 = new TagDTO(2, "RAD");
        var expected3 = new TagDTO(3, "API");

        Assert.Collection(actual,
            tagDTO => Assert.Equal(expected1, tagDTO),
            tagDTO => Assert.Equal(expected2, tagDTO),
            tagDTO => Assert.Equal(expected3, tagDTO));
    }

    #endregion

    #region Delete

    /// <summary>
    ///     Defines the test method DeleteAsync_tag_by_id_returns_status_deleted.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_tag_by_id_returns_status_deleted()
    {
        var actual = await _v.TagRepository.DeleteAsync(1);

        const Status expected = Status.Deleted;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method DeleteAsync_tag_by_id_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_tag_by_id_returns_status_notFound()
    {
        var actual = await _v.TagRepository.DeleteAsync(4);

        const Status expected = Status.NotFound;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method DeleteAsync_tag_by_id_returns_count_one_less.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_tag_by_id_returns_count_one_less()
    {
        await _v.TagRepository.DeleteAsync(3);

        var actual = _v.TagRepository.ReadAsync().Result.Count;

        const int expected = 2;

        Assert.Equal(expected, actual);
    }

    #endregion

    #region Update

    /// <summary>
    ///     Defines the test method UpdateAsync_tag_by_id_returns_status_updated.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_tag_by_id_returns_status_updated()
    {
        var updateTagDTO = new TagDTO(3, "Database");

        var actual = await _v.TagRepository.UpdateAsync(updateTagDTO);

        const Status expected = Status.Updated;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_tag_by_id_read_updated_returns_status_found_and_updated_tag.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_tag_by_id_read_updated_returns_status_found_and_updated_tag()
    {
        var updateTagDTO = new TagDTO(3, "Database");

        await _v.TagRepository.UpdateAsync(updateTagDTO);

        var actual = await _v.TagRepository.ReadAsync(3);

        var expected = (Status.Found, updateTagDTO);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_tag_by_id_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_tag_by_id_returns_status_notFound()
    {
        var updateTagDTO = new TagDTO(4, "Database");

        var actual = await _v.TagRepository.UpdateAsync(updateTagDTO);

        const Status expected = Status.NotFound;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_tag_by_id_returns_status_conflict.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_tag_by_id_returns_status_conflict()
    {
        var updateTagDTO = new TagDTO(3, "SOLID");

        var actual = await _v.TagRepository.UpdateAsync(updateTagDTO);

        const Status expected = Status.Conflict;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_tag_returns_bad_request_on_name_tooLong.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_tag_returns_bad_request_on_name_tooLong()
    {
        var tag = new TagDTO(1, "asseocarnisanguineoviscericartilaginonervomedullary");

        var actual = await _v.TagRepository.UpdateAsync(tag);

        const Status expected = Status.BadRequest;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_tag_returns_bad_request_on_name_empty.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_tag_returns_bad_request_on_name_empty()
    {
        var tag = new TagDTO(1, "");

        var actual = await _v.TagRepository.UpdateAsync(tag);

        const Status expected = Status.BadRequest;

        Assert.Equal(expected, actual);
    }


    /// <summary>
    ///     Defines the test method UpdateAsync_tag_returns_bad_request_on_name_whitespace.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_tag_returns_bad_request_on_name_whitespace()
    {
        var tag = new TagDTO(1, " ");

        var actual = await _v.TagRepository.UpdateAsync(tag);

        const Status expected = Status.BadRequest;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_tag_with_max_length_returns_updated.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_tag_with_max_length_returns_updated()
    {
        var tag = new TagDTO(1, "asseocarnisanguineoviscericartilaginonervomedullar");

        var actual = await _v.TagRepository.UpdateAsync(tag);

        const Status expected = Status.Updated;

        Assert.Equal(expected, actual);
    }

    #endregion
}