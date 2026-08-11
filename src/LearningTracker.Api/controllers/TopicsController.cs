
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/topics")]
public class TopicsController : ControllerBase
{
    private readonly LearningTopicService _learningTopicService;

    public TopicsController(LearningTopicService learningTopicService)
    {
        _learningTopicService = learningTopicService;
    }
    [HttpGet("getAll")]
public async Task<ActionResult<IEnumerable<TopicResponse>>>
    GetAll()
{
    var topics =
        await _learningTopicService.GetAllTopicsAsync();

    var response =
        topics.Select(topic =>
            new TopicResponse(
                topic.id,
                topic.Name,
                topic.Status));

    return Ok(response);
}

[HttpPost("create")]
public async Task<ActionResult> Create(
    CreateTopicRequest request)
{
    if (string.IsNullOrWhiteSpace(request.name))
    {
        return BadRequest(
            "Topic name is required.");
    }

    await _learningTopicService.AddTopicAsync(
        request.name);

    return Created();
}

}

