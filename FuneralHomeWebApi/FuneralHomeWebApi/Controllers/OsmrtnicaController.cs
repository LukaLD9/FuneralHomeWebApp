﻿using FuneralHome.Repositories;
using Microsoft.AspNetCore.Mvc;
using FuneralHome.DTOs;
using FuneralHome.Repositories.SqlServer;
using BaseLibrary;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuneralHome.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OsmrtnicaController : ControllerBase
{
    private readonly IOsmrtnicaRepository _osmrtnicaRepository;

    public OsmrtnicaController(IOsmrtnicaRepository repository)
    {
        _osmrtnicaRepository = repository;
    }

    // GET: api/Osmrtnica
    [HttpGet]
    public ActionResult<IEnumerable<Osmrtnica>> GetAllOsmrtnica()
    {
        var osmrtnicaResults = _osmrtnicaRepository.GetAll()
            .Map(o => o.Select(DtoMapping.ToDto));

        return osmrtnicaResults
            ? Ok(osmrtnicaResults.Data)
            : Problem(osmrtnicaResults.Message, statusCode: 500);
    }

    // GET: api/Osmrtnica/5
    [HttpGet("{id}")]
    public ActionResult<Osmrtnica> GetOsmrtnica(int id)
    {
        var osmrtnicaResult = _osmrtnicaRepository.Get(id).Map(DtoMapping.ToDto);

        return osmrtnicaResult switch
        {
            { IsSuccess: true } => Ok(osmrtnicaResult.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(osmrtnicaResult.Message, statusCode: 500)
        };
    }

   

    // PUT: api/Osmrtnica/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public IActionResult EditOsmrtnica(int id, Osmrtnica osmrtnica)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != osmrtnica.Id)
        {
            return BadRequest();
        }

        if (!_osmrtnicaRepository.Exists(id))
        {
            return NotFound();
        }

        var domainOsmrtnica = osmrtnica.ToDomain();

        var result =
            domainOsmrtnica.IsValid()
            .Bind(() => _osmrtnicaRepository.Update(domainOsmrtnica));

        return result
            ? AcceptedAtAction("EditOsmrtnica", osmrtnica)
            : Problem(result.Message, statusCode: 500);
    }

    // POST: api/Osmrtnica
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public ActionResult<Osmrtnica> CreateOglas(Osmrtnica osmrtnica)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var domainOsmrtnica = osmrtnica.ToDomain();

        var validationResult = domainOsmrtnica.IsValid();
        if (!validationResult)
        {
            return Problem(validationResult.Message, statusCode: 500);
        }

        var result =
            domainOsmrtnica.IsValid()
            .Bind(() => _osmrtnicaRepository.Insert(domainOsmrtnica));

        return result
            ? CreatedAtAction("GetOglas", new { id = osmrtnica.Id }, osmrtnica)
            : Problem(result.Message, statusCode: 500);
    }

    // DELETE: api/Osmrtnica/5
    [HttpDelete("{id}")]
    public IActionResult DeleteOsmrtnica(int id)
    {
        if (!_osmrtnicaRepository.Exists(id))
            return NotFound();

        var deleteResult = _osmrtnicaRepository.Remove(id);
        return deleteResult
            ? NoContent()
            : Problem(deleteResult.Message, statusCode: 500);
    }
}
