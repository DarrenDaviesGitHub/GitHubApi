using ebx.GitHubAPI.Queries;
using MediatR;
using Octokit;

namespace ebx.GitHubAPI.Handlers
{
	public class GetRepositoryContributorHandler : IRequestHandler<GetRepositoryContributorsQuery, IEnumerable<RepositoryContributor>>
	{
		public async Task<IEnumerable<RepositoryContributor>> Handle(GetRepositoryContributorsQuery query, CancellationToken cancellationToken)
		{
			IEnumerable<RepositoryContributor> result = new List<RepositoryContributor>();

			try
			{
				var productInformation = new ProductHeaderValue(Constants.Application.Name);
				var client = new GitHubClient(productInformation);

				var contributors = await client.Repository.GetAllContributors(query.Owner, query.Repository);

				if (contributors.Any())
				{
					result = (IEnumerable<RepositoryContributor>)contributors.Take(Constants.Application.DefaultPageSize);
				}
			}
			catch (Exception ex)
			{
				// Log exception to ApplicationInsights, RayGun, Log4Net or another exception/error management provider etc.
			}
			
			return result;
		}
	}
}
