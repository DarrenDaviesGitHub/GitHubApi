using MediatR;
using Octokit;

namespace ebx.GitHubAPI.Queries
{
	public class GetRepositoryContributorsQuery : IRequest<IEnumerable<RepositoryContributor>> 
	{
		public string Owner { get; set; }
		public string Repository { get; set; }

		public GetRepositoryContributorsQuery(string owner, string repository)
		{
			Owner = owner;
			Repository = repository;
		}
	}
}