﻿// ***********************************************************************
// Assembly         : WebService.Core.Server
// Author           : Group BTG
// Created          : 12-03-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="LanguageController.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Server.Controllers;

/// <summary>
///     Class LanguageController.
///     Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class LanguageController : ControllerBase
{
    /// <summary>
    ///     The language repository
    /// </summary>
    private readonly ILanguageRepository _languageRepository;

    /// <summary>
    ///     Initializes a new instance of the <see cref="LanguageController" /> class.
    /// </summary>
    public LanguageController(ILanguageRepository languageRepository)
    {
        _languageRepository = languageRepository;
    }

    /// <summary>
    ///     Gets all LanguageDTOs.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<LanguageDTO>>> Get()
    {
        var result = await _languageRepository.ReadAsync();
        return Ok(result);
    }

    /// <summary>
    ///     Gets a specified LanguageDTO based on id.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LanguageDTO>> Get(int id)
    {
        var (response, languageDTO) = await _languageRepository.ReadAsync(id);

        if (response == Status.Found) return Ok(languageDTO);
        return NotFound();
    }

    /// <summary>
    ///     Posts a new language.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Post(CreateLanguageDTO language)
    {
        var (response, languageDTO) = await _languageRepository.CreateAsync(language);

        return response switch
        {
            Status.Created => Created(nameof(Put), languageDTO),
            Status.Conflict => Conflict(),
            _ => BadRequest()
        };
    }

    /// <summary>
    ///     Puts a specific, existing language.
    /// </summary>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Put(LanguageDTO language)
    {
        var response = await _languageRepository.UpdateAsync(language);

        return response switch
        {
            Status.Updated => NoContent(),
            Status.Conflict => Conflict(),
            Status.BadRequest => BadRequest(),
            _ => NotFound()
        };
    }

    /// <summary>
    ///     Deletes a specified language based on id.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _languageRepository.DeleteAsync(id);

        if (response == Status.Deleted) return NoContent();
        return NotFound();
    }
}