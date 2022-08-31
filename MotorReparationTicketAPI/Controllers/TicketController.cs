using Microsoft.AspNetCore.Mvc;
using DataModel;
using MotorReparationTicketAPI.Repository.IRepository;

namespace MotorReparationTicketAPI.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketController: ControllerBase
{
    private readonly ITicketRepository _ticketService;

    public TicketController(ITicketRepository ticketService)
    {
        _ticketService = ticketService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTickets()
    {
        var result = await _ticketService.GetAllTickets();
        return Ok(result);
    }
    
    [HttpGet("{ticketId}")]
    public async Task<IActionResult> GetTicketById(int ticketId)
    {
        try
        {
            var result = await _ticketService.GetTicketById(ticketId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorModel()
            {
                ErrorMessage = ex.Message
            });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTicket([FromBody] TicketCreateUpdateDTO ticketModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ErrorModel()
            {
                ErrorMessage = "Wrong Data Format",
                StatusCode = 500,
                Title = "Error"
            });
        }
        
        var result = await _ticketService.CreateTicket(ticketModel);
        return Ok(result);
    }

    [HttpPut("{ticketId}")]
    public async Task<IActionResult> UpdateTicket(int ticketId, [FromBody] TicketCreateUpdateDTO ticketModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid Data Format",
                    StatusCode = 500
                });
            }

            var result = await _ticketService.UpdateTicket(ticketId, ticketModel);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorModel()
            {
                ErrorMessage = ex.Message
            });
        }
    }

    [HttpDelete("{ticketId}")]
    public async Task<IActionResult> DeleteTicket(int ticketId)
    {
        try
        {
            var result = await _ticketService.DeleteTicket(ticketId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorModel()
            {
                ErrorMessage = ex.Message
            });
        }
    }
}