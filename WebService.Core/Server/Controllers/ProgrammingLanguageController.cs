﻿// ***********************************************************************
// Assembly         : WebService.Core.Server
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : thorl
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="ProgrammingLanguageController.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Server.Controllers;

/// <summary>
///     Class ProgrammingLanguageController.
///     Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class ProgrammingLanguageController : ControllerBase
{
    /// <summary>
    ///     The programming language repository
    /// </summary>
    private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ProgrammingLanguageController" /> class.
    /// </summary>
    /// <param name="programmingLanguageRepository">The programming language repository.</param>
    public ProgrammingLanguageController(IProgrammingLanguageRepository programmingLanguageRepository)
    {
        _programmingLanguageRepository = programmingLanguageRepository;
    }

    /// <summary>
    ///     Gets all ProgrammingLanguageDTOs.
    /// </summary>
    /// <returns>ActionResult&lt;ICollection&lt;ProgrammingLanguageDTO&gt;&gt;.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<ProgrammingLanguageDTO>>> Get()
    {
        var result = await _programmingLanguageRepository.ReadAsync();
        return Ok(result);
    }

    /// <summary>
    ///     Gets the specified ProgrammingLanguageDTO.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>ActionResult&lt;ProgrammingLanguageDTO&gt;.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProgrammingLanguageDTO>> Get(int id)
    {
        var (response, programmingLanguageDTO) = await _programmingLanguageRepository.ReadAsync(id);

        if (response == Status.Found) return Ok(programmingLanguageDTO);
        return NotFound();
    }

    /// <summary>
    ///     Posts the specified programming language.
    /// </summary>
    /// <param name="programmingLanguage">The programming language.</param>
    /// <returns>IActionResult.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Post(CreateProgrammingLanguageDTO programmingLanguage)
    {
        var (response, programmingLanguageDTO) = await _programmingLanguageRepository.CreateAsync(programmingLanguage);

        return response switch
        {
            Status.Created => Created(nameof(Put), programmingLanguageDTO),
            Status.Conflict => Conflict(),
            _ => BadRequest()
        };
    }

    /// <summary>
    ///     Puts the specified programming language.
    /// </summary>
    /// <param name="programmingLanguage">The programming language.</param>
    /// <returns>IActionResult.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Put(ProgrammingLanguageDTO programmingLanguage)
    {
        var response = await _programmingLanguageRepository.UpdateAsync(programmingLanguage);

        return response switch
        {
            Status.Updated => NoContent(),
            Status.Conflict => Conflict(),
            Status.BadRequest => BadRequest(),
            _ => NotFound()
        };
    }

    /// <summary>
    ///     Deletes the specified ProgrammingLanguageDTO.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>IActionResult.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _programmingLanguageRepository.DeleteAsync(id);

        if (response == Status.Deleted) return NoContent();
        return NotFound();
    }
}