﻿using FuneralHome.Repositories;
using Microsoft.AspNetCore.Mvc;
using FuneralHome.DTOs;
using FuneralHome.Repositories.SqlServer;
using BaseLibrary;
using System.Data;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuneralHome.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PogrebController : ControllerBase
{
    private readonly IPogrebRepository _pogrebRepository;
    private readonly IOpremaRepository _opremaRepository;
    private readonly IOsiguranjeRepository _osiguranjeRepository;

    public PogrebController(IPogrebRepository repository,
        IOpremaRepository opremaRepository,
        IOsiguranjeRepository osiguranjeRepository)
    {
        _pogrebRepository = repository;
        _opremaRepository = opremaRepository;
        _osiguranjeRepository = osiguranjeRepository;
    }

    // GET: api/Pogreb
    [HttpGet]
    public ActionResult<IEnumerable<Pogreb>> GetAllPogreb()
    {
        var pogrebResults = _pogrebRepository.GetAll()
            .Map(p => p.Select(DtoMapping.ToDto));

        return pogrebResults
            ? Ok(pogrebResults.Data)
            : Problem(pogrebResults.Message, statusCode: 500);
    }

    // GET: api/Pogreb/Aggregates
    [HttpGet("Aggregates")]
    public ActionResult<IEnumerable<PogrebAggregate>> GetAllPogrebAggregate()
    {
        var pogrebResults = _pogrebRepository.GetAllAggregates()
            .Map(p => p.Select(DtoMapping.ToAggregateDto));
        return pogrebResults
            ? Ok(pogrebResults.Data)
            : Problem(pogrebResults.Message, statusCode: 500);
    }

    // GET: api/Pogreb/PogrebSmrtniSlucaj
    [HttpGet("PogrebSmrtniSlucaj")]
    public ActionResult<IEnumerable<PogrebSmrtniSlucaj>> GetAllPogrebSmrtniSlucaj()
    {
        var pogrebResults = _pogrebRepository.GetAllPogrebSmrtniSlucaj()
            .Map(p => p.Select(DtoMapping.ToDto));
        return pogrebResults
            ? Ok(pogrebResults.Data)
            : Problem(pogrebResults.Message, statusCode: 500);
    }

    // GET : api/Pogreb/Korisnik/5
    [HttpGet("Korisnik/{id}")]
    public ActionResult<IEnumerable<PogrebSmrtniSlucaj>> GetAllPogrebByKorisnikId(int id)
    {
        var pogrebResults = _pogrebRepository.GetAllByKorisnikId(id)
            .Map(p => p.Select(DtoMapping.ToDto));
        return pogrebResults
            ? Ok(pogrebResults.Data)
            : Problem(pogrebResults.Message, statusCode: 500);
    }


    // GET: api/Pogreb/5
    [HttpGet("{id}")]
    public ActionResult<Pogreb> GetPogreb(int id)
    {
        var pogrebResult = _pogrebRepository.Get(id).Map(DtoMapping.ToDto);

        return pogrebResult switch
        {
            { IsSuccess: true } => Ok(pogrebResult.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(pogrebResult.Message, statusCode: 500)
        };
    }

    // GET: api/Pogreb/5
    [HttpGet("SmrtniSlucaj/{id}")]
    public ActionResult<Pogreb> GetPogrebBySmrtniSlucajId(int id)
    {
        var pogrebResult = _pogrebRepository.GetBySmrtniSlucajId(id).Map(DtoMapping.ToDto);

        return pogrebResult switch
        {
            { IsSuccess: true } => Ok(pogrebResult.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(pogrebResult.Message, statusCode: 500)
        };
    }


    [HttpGet("/api/[controller]/Aggregate/{id}")]
    public ActionResult<PogrebAggregate> GetPogrebAggregate(int id)
    {
        var pogrebResult = _pogrebRepository.GetAggregate(id).Map(DtoMapping.ToAggregateDto);
        
        return pogrebResult switch
        {
            { IsSuccess: true } => Ok(pogrebResult.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(pogrebResult.Message, statusCode: 500)
        };
    }


    [HttpPost("AddOprema/{pogrebId}")]
    public IActionResult AddOprema(int pogrebId, PogrebOprema pogrebOprema)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var pogrebResult = _pogrebRepository.GetAggregate(pogrebId);
        if (pogrebResult.IsFailure)
        {
            return NotFound();
        }
        if (pogrebResult.IsException)
        {
            return Problem(pogrebResult.Message, statusCode: 500);
        }

        var pogreb = pogrebResult.Data;

        var domainPogrebOprema = pogrebOprema.ToDomain(pogrebId);
        var validationResult = domainPogrebOprema.IsValid();

        if (!validationResult)
        {
            return Problem(validationResult.Message, statusCode: 500);
        }

        if (pogreb.AddOprema(domainPogrebOprema))
        {
            var opremaResult = _opremaRepository.DecreaseZaliha(domainPogrebOprema.Oprema, domainPogrebOprema.Kolicina);
            if (opremaResult.IsFailure)
            {
                return Problem(opremaResult.Message, statusCode: 500);
            }
        };

        var osiguranjeResult = _osiguranjeRepository.GetBySmrtniSlucajId(pogreb.SmrtniSlucajId);
        if (!osiguranjeResult)
        {
            return Problem(osiguranjeResult.Message, statusCode: 500);
        }
        if (osiguranjeResult.Data != null)
            if(osiguranjeResult.Data.Count() > 0)
                pogreb.CalculateDiscount(osiguranjeResult.Data.First());

        var updateResult =
            pogreb.IsValid()
            .Bind(() => _pogrebRepository.UpdateAggregate(pogreb));

        return updateResult
            ? Accepted()
            : Problem(updateResult.Message, statusCode: 500);
    }

    
    [HttpPost("RemoveOprema/{pogrebId}")]
    public IActionResult RemoveOprema(int pogrebId, Oprema oprema)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var pogrebResult = _pogrebRepository.GetAggregate(pogrebId);
        if (pogrebResult.IsFailure)
        {
            return NotFound();
        }
        if (pogrebResult.IsException)
        {
            return Problem(pogrebResult.Message, statusCode: 500);
        }

        var pogreb = pogrebResult.Data;
        
        var domainOprema = oprema.ToDomain();

        var kolicina = pogreb.PogrebOprema
            .Where(o => o.Oprema.Id == oprema.Id)
            .Select(o => o.Kolicina).FirstOrDefault();

        if (kolicina == 0)
        {
            return NotFound($"Couldn't find equipment kolicina {oprema.Naziv}");
        }

        if (!pogreb.RemoveOprema(domainOprema))
        {
            return NotFound($"Couldn't find equipment {oprema.Naziv}");
        }

        var opremaResult = _opremaRepository.IncreaseZaliha(domainOprema, kolicina);
        if (opremaResult.IsFailure)
        {
            return Problem(opremaResult.Message, statusCode: 500);
        }

        var osiguranjeResult = _osiguranjeRepository.GetBySmrtniSlucajId(pogreb.SmrtniSlucajId);
        if (!osiguranjeResult)
        {
            return Problem(osiguranjeResult.Message, statusCode: 500);
        }
        if (osiguranjeResult.Data != null)
            if (osiguranjeResult.Data.Count() > 0)
                pogreb.CalculateDiscount(osiguranjeResult.Data.First());

        var updateResult =
            pogreb.IsValid()
            .Bind(() => _pogrebRepository.UpdateAggregate(pogreb));

        return updateResult
            ? Accepted()
            : Problem(updateResult.Message, statusCode: 500);
    }

    [HttpPost("IncrementOprema/{pogrebId}")]
    public IActionResult IncrementOprema(int pogrebId, int opremaId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var pogrebResult = _pogrebRepository.GetAggregate(pogrebId);
        if (pogrebResult.IsFailure)
        {
            return NotFound();
        }
        if (pogrebResult.IsException)
        {
            return Problem(pogrebResult.Message, statusCode: 500);
        }

        var pogreb = pogrebResult.Data;
    
        if (pogreb.IncrementOprema(opremaId))
        {
            var oprema = _opremaRepository.Get(opremaId).Data;
            var opremaResult = _opremaRepository.DecreaseZaliha(oprema, 1);
            if (opremaResult.IsFailure)
            {
                return Problem(opremaResult.Message, statusCode: 500);
            }
        };

        var osiguranjeResult = _osiguranjeRepository.GetBySmrtniSlucajId(pogreb.SmrtniSlucajId);
        if (!osiguranjeResult)
        {
            return Problem(osiguranjeResult.Message, statusCode: 500);
        }
        if (osiguranjeResult.Data != null)
            if (osiguranjeResult.Data.Count() > 0)
                pogreb.CalculateDiscount(osiguranjeResult.Data.First());

        var updateResult =
            pogreb.IsValid()
            .Bind(() => _pogrebRepository.UpdateAggregate(pogreb));

        return updateResult
            ? Accepted()
            : Problem(updateResult.Message, statusCode: 500);
    }

    [HttpPost("DecrementOprema/{pogrebId}")]
    public IActionResult DecrementOprema(int pogrebId, int opremaId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var pogrebResult = _pogrebRepository.GetAggregate(pogrebId);
        if (pogrebResult.IsFailure)
        {
            return NotFound();
        }
        if (pogrebResult.IsException)
        {
            return Problem(pogrebResult.Message, statusCode: 500);
        }
        var pogreb = pogrebResult.Data;

        if (pogreb.DecrementOprema(opremaId))
        {
            var oprema = _opremaRepository.Get(opremaId).Data;
            var opremaResult = _opremaRepository.IncreaseZaliha(oprema, 1);
            if (opremaResult.IsFailure)
            {
                return Problem(opremaResult.Message, statusCode: 500);
            }
        };

        var osiguranjeResult = _osiguranjeRepository.GetBySmrtniSlucajId(pogreb.SmrtniSlucajId);
        if (!osiguranjeResult)
        {
            return Problem(osiguranjeResult.Message, statusCode: 500);
        }
        if (osiguranjeResult.Data != null)
            if (osiguranjeResult.Data.Count() > 0)
                pogreb.CalculateDiscount(osiguranjeResult.Data.First());

        var updateResult =
            pogreb.IsValid()
            .Bind(() => _pogrebRepository.UpdateAggregate(pogreb));
        return updateResult
            ? Accepted()
            : Problem(updateResult.Message, statusCode: 500);
    }


    [HttpPost("AddUsluga/{pogrebId}")]
    public IActionResult AddUsluga(int pogrebId, Usluga usluga)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var pogrebResult = _pogrebRepository.GetAggregate(pogrebId);
        if (pogrebResult.IsFailure)
        {
            return NotFound();
        }
        if (pogrebResult.IsException)
        {
            return Problem(pogrebResult.Message, statusCode: 500);
        }

        var pogreb = pogrebResult.Data;

        var domainPogrebUsluga = usluga.ToDomain();
        var validationResult = domainPogrebUsluga.IsValid();

        if (!validationResult)
        {
            return Problem(validationResult.Message, statusCode: 500);
        }

        pogreb.AddUsluga(domainPogrebUsluga);

        var osiguranjeResult = _osiguranjeRepository.GetBySmrtniSlucajId(pogreb.SmrtniSlucajId);
        if (!osiguranjeResult)
        {
            return Problem(osiguranjeResult.Message, statusCode: 500);
        }
        if (osiguranjeResult.Data != null)
            if(osiguranjeResult.Data.Count() > 0)
                pogreb.CalculateDiscount(osiguranjeResult.Data.First());

        var updateResult =
            pogreb.IsValid()
            .Bind(() => _pogrebRepository.UpdateAggregate(pogreb));

        return updateResult
            ? Accepted()
            : Problem(updateResult.Message, statusCode: 500);
    }


    [HttpPost("RemoveUsluga/{pogrebId}")]
    public IActionResult RemoveUsluga(int pogrebId, Usluga usluga)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var pogrebResult = _pogrebRepository.GetAggregate(pogrebId);
        if (pogrebResult.IsFailure)
        {
            return NotFound();
        }
        if (pogrebResult.IsException)
        {
            return Problem(pogrebResult.Message, statusCode: 500);
        }

        var pogreb = pogrebResult.Data;

        var domainUsluga = usluga.ToDomain();

        if (!pogreb.RemoveUsluga(domainUsluga))
        {
            return NotFound($"Couldn't find service {usluga.Naziv}");
        }

        var osiguranjeResult = _osiguranjeRepository.GetBySmrtniSlucajId(pogreb.SmrtniSlucajId);
        if (!osiguranjeResult)
        {
            return Problem(osiguranjeResult.Message, statusCode: 500);
        }
        if (osiguranjeResult.Data != null)
            if (osiguranjeResult.Data.Count() > 0)
                pogreb.CalculateDiscount(osiguranjeResult.Data.First());

        var updateResult =
            pogreb.IsValid()
            .Bind(() => _pogrebRepository.UpdateAggregate(pogreb));

        return updateResult
            ? Accepted()
            : Problem(updateResult.Message, statusCode: 500);
    }


    // PUT: api/Pogreb/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public IActionResult EditPogreb(int id, Pogreb pogreb)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != pogreb.Id)
        {
            return BadRequest();
        }

        if (!_pogrebRepository.Exists(id))
        {
            return NotFound();
        }

        var domainPogreb = pogreb.ToDomain();

        var result =
            domainPogreb.IsValid()
            .Bind(() => _pogrebRepository.Update(domainPogreb));

        return result
            ? AcceptedAtAction("EditPogreb", pogreb)
            : Problem(result.Message, statusCode: 500);
    }

    // PUT: api/Pogreb/SmrtniSlucaj/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("PogrebSmrtniSlucaj/{id}")]
    public IActionResult EditPogrebSmrtniSlucaj(int id, PogrebSmrtniSlucaj pogreb)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != pogreb.Id)
        {
            return BadRequest();
        }

        if (!_pogrebRepository.Exists(id))
        {
            return NotFound();
        }

        var domainPogreb = pogreb.ToDomain();

        var result =
            domainPogreb.IsValid()
            .Bind(() => _pogrebRepository.Update(domainPogreb));

        return result
            ? AcceptedAtAction("EditPogrebSmrtniSlucaj", pogreb)
            : Problem(result.Message, statusCode: 500);
    }

    // POST: api/Pogreb
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public ActionResult<Pogreb> CreatePogreb(Pogreb pogreb)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var domainPogreb = pogreb.ToDomain();

        var validationResult = domainPogreb.IsValid();
        if (!validationResult)
        {
            return Problem(validationResult.Message, statusCode: 500);
        }

        var result =
            domainPogreb.IsValid()
            .Bind(() => _pogrebRepository.Insert(domainPogreb));

        return result
            ? CreatedAtAction("GetPogreb", new { id = pogreb.Id }, pogreb)
            : Problem(result.Message, statusCode: 500);
    }

    // POST: api/Pogreb
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("PogrebSmrtniSlucaj")]
    public ActionResult<Pogreb> CreatePogreb(PogrebSmrtniSlucaj pogreb)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var domainPogreb = pogreb.ToDomain();

        var validationResult = domainPogreb.IsValid();
        if (!validationResult)
        {
            return Problem(validationResult.Message, statusCode: 500);
        }

        var result =
            domainPogreb.IsValid()
            .Bind(() => _pogrebRepository.Insert(domainPogreb));

        return result
            ? CreatedAtAction("GetPogrebSmrtniSlucaj", new { id = pogreb.Id }, pogreb)
            : Problem(result.Message, statusCode: 500);
    }

    // DELETE: api/Pogreb/5
    [HttpDelete("{id}")]
    public IActionResult DeletePogreb(int id)
    {
        if (!_pogrebRepository.Exists(id))
            return NotFound();

        var deleteResult = _pogrebRepository.Remove(id);
        return deleteResult
            ? NoContent()
            : Problem(deleteResult.Message, statusCode: 500);
    }

    [HttpPut("AddPogreb")]
    public IActionResult AddPogreb(AddPogreb addPogreb)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var domainPogreb = addPogreb.Pogreb.ToDomain();

        var validationResult = domainPogreb.IsValid();
        if (!validationResult)
        {
            return Problem(validationResult.Message, statusCode: 500);
        }

        var result =
            domainPogreb.IsValid()
            .Bind(() => _pogrebRepository.Insert(domainPogreb));

        if(!result)
        {
            return Problem(result.Message, statusCode: 500);
        }

        var idResult = _pogrebRepository.GetBySmrtniSlucajId(addPogreb.Pogreb.SmrtniSlucajId);
        if (!idResult)
        {
            return Problem(idResult.Message, statusCode: 500);
        }
        int id = idResult.Data.Id;

        addPogreb.Pogreb.Id = id;

        var domainPogrebWithId = addPogreb.Pogreb.ToDomain();

        var validationResulWithId = domainPogreb.IsValid();
        if (!validationResulWithId)
        {
            var deleteResult = _pogrebRepository.Remove(id);
            return Problem(validationResulWithId.Message, statusCode: 500);
        }

        foreach (PogrebOprema pogrebOprema in addPogreb.Oprema)
        {
            var domainPogrebOprema = pogrebOprema.ToDomain(id);
            var pogrebOpremavalidationResult = domainPogrebOprema.IsValid();
            if (!pogrebOpremavalidationResult)
            {
                return Problem(pogrebOpremavalidationResult.Message, statusCode: 500);
            }

            if (domainPogrebWithId.AddOprema(domainPogrebOprema))
            {
                var opremaResult = _opremaRepository.DecreaseZaliha(domainPogrebOprema.Oprema, domainPogrebOprema.Kolicina);
                if (!opremaResult)
                {
                    var deleteResult = _pogrebRepository.Remove(id);
                    return Problem(opremaResult.Message, statusCode: 500);
                }
            };   
        }

        foreach(Usluga usluga in addPogreb.Usluga)
        {
            var domainUsluga = usluga.ToDomain();
            var uslugaValidationResult = domainUsluga.IsValid();
            if (!uslugaValidationResult)
            {
                var deleteResult = _pogrebRepository.Remove(id);
                return Problem(uslugaValidationResult.Message, statusCode: 500);
            }
            domainPogrebWithId.AddUsluga(domainUsluga);
        }

        var osiguranjeResult = _osiguranjeRepository.GetBySmrtniSlucajId(domainPogrebWithId.SmrtniSlucajId);
        if (!osiguranjeResult)
        {
            var deleteResult = _pogrebRepository.Remove(id);
            return Problem(osiguranjeResult.Message, statusCode: 500);
        }
        if(osiguranjeResult.Data != null)
        {
            domainPogrebWithId.CalculateDiscount(osiguranjeResult.Data.First());
        }

        var updateResult = domainPogrebWithId.IsValid()
                .Bind(() => _pogrebRepository.UpdateAggregate(domainPogrebWithId));

        if (!updateResult)
        {
            // obriši pogreb
            var deleteResult = _pogrebRepository.Remove(id);
            return Problem(updateResult.Message, statusCode: 500);
        }

        return CreatedAtAction("GetPogreb", new { id = id }, addPogreb);
    }
    
}
