using DevStore.Common.Data.EventSourcing;
using Microsoft.AspNetCore.Mvc;

namespace DevStore.WebApp.VirtualStore.Controllers.VisualSupport;

public class EventController(IEventSourcingRepository eventSourcingRepository) : Controller
{
    private readonly IEventSourcingRepository _eventSourcingRepository = eventSourcingRepository;

    [HttpGet("event/{id:guid}")]
    public async Task<IActionResult> Index(Guid id)
    {
        var events = await _eventSourcingRepository.GetEvents(id);
        return View(events);
    }
}
