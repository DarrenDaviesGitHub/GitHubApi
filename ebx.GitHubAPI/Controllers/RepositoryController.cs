using ebx.GitHubAPI.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ebx.GitHubAPI.Controllers
{
	[ApiController]
	public class RepositoryController : ControllerBase
	{
		private readonly IMediator _mediator;

		public RepositoryController(IMediator mediator) 
		{ 
			_mediator = mediator;
		}

		[HttpGet]
		[Route("api/v1/{owner}/{repo}/contributors")]
		public async Task<ActionResult> Contributors(string owner, string repo)
		{
			if (string.IsNullOrWhiteSpace(owner) || string.IsNullOrWhiteSpace(repo))
			{
				return BadRequest($"{nameof(owner)} and {nameof(repo)} must be supplied");
			}

			try
			{
				var repositoryContributors = await _mediator.Send(new GetRepositoryContributorsQuery(owner, repo));

				if (repositoryContributors.Any())
				{
					return Ok(repositoryContributors);
				}

				return NotFound($"No contributors were found for owner: {owner} and repository {repo}.");
			}
			catch (Exception ex) 
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex);
			}
		}
	}
}
