using System.Threading.Tasks;

namespace WeatherApp.Web.Commands
{
	public interface IAsyncCommand<TAudit, TResult>
	{
		string CommandName { get; }

		Task<TResult> ExecuteAsync(string auditId);
	}

	public interface IAsyncCommand<TAudit, TParam, TResult>
	{
		string CommandName { get; }

		Task<TResult> ExecuteAsync(TAudit audit, TParam parameter);
	}
}
