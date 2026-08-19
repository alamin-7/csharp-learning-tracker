
using Microsoft.AspNetCore.Mvc;
using LearningTracker.Api.dtos;

[ApiController]
[Route("api/topics")]
public class TopicsController : ControllerBase
{
    private readonly LearningTopicService _learningTopicService;

    public TopicsController(LearningTopicService learningTopicService)
    {
        _learningTopicService = learningTopicService;
    }

[HttpGet("all")]
public async Task<
    ActionResult<IEnumerable<TopicResponse>>>
    GetAll()
{
    var topics =
        await _learningTopicService.GetAllTopicsAsync();

    var response =
        topics.Select(topic =>
            new TopicResponse(
                topic.Id,
                topic.Name,
                topic.Status));

    return Ok(response);
}

[HttpPost("create")]
public async Task<ActionResult<TopicResponse>>
    Create(CreateTopicRequest request)
{
    var topic =
        await _learningTopicService.AddTopicAsync(
            request.name);

    var response =
        new TopicResponse(
            topic.Id,
            topic.Name,
            topic.Status);

    return CreatedAtAction(
        nameof(GetById),
        new { id = topic.Id },
        response);
}
[HttpGet("by-id/{id:guid}")]
public async Task<ActionResult<TopicResponse>>
    GetById(Guid id)
{
    var topic =
        await _learningTopicService.FindByIdAsync(id);

    if (topic is null)
    {
        return NotFound();
    }

    return Ok(
        new TopicResponse(
            topic.Id,
            topic.Name,
            topic.Status));
}
[HttpPut("{id:guid}/status")]
public async Task<IActionResult>
    UpdateStatus(
        Guid id,
        UpdateTopicStatusRequest request)
{
    await _learningTopicService.UpdateTopicStatusAsync(
        id,
        request.Status);

    return NoContent();
}
[HttpDelete("{id:guid}")]
public async Task<IActionResult>
    Delete(Guid id)
{
    await _learningTopicService.DeleteTopicAsync(id);

    return NoContent();
}
}

