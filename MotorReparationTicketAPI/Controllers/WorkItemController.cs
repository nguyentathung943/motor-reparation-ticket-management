using DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorReparationTicketAPI.Repository.IRepository;

namespace MotorReparationTicketAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/ticket_items")]
public class WorkItemController: ControllerBase
{
    private readonly IWorkItemRepository _workItemService;

    public WorkItemController(IWorkItemRepository workItemService)
    {
        _workItemService = workItemService;
    }

    [HttpGet("get-work-items-by-ticket-id/{ticketId}")]
    public async Task<IActionResult> GetAllWorkItemsByTicketId(int ticketId)
    {
        try
        {
            var result = await _workItemService.GetAllWorkItemsByTicketId(ticketId);
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

    [HttpGet("{itemId}")]
    public async Task<IActionResult> GetWorkItemById(int itemId)
    {
        try
        {
            var result = await _workItemService.GetWorkItemById(itemId);
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
    public async Task<IActionResult> CreateWorkItem([FromBody] TicketWorkItemCreateUpdateDTO itemModel)
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

            var result = await _workItemService.CreateWorkItem(itemModel);
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

    [HttpPut("{itemId}")]
    public async Task<IActionResult> UpdateWorkItem(int itemId,[FromBody] TicketWorkItemCreateUpdateDTO itemModel)
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
            
            
            var result = await _workItemService.UpdateWorkItem(itemId, itemModel);
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

    [HttpDelete("{itemId}")]
    public async Task<IActionResult> DeleteWorkItem(int itemId)
    {
        try
        {
            var result = await _workItemService.DeleteWorkItem(itemId);
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
