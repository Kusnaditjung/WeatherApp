using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WeatherApp.Web.Commands
{
    public static class CommandHelper
    {
		public const string ApplicationAuditCategory = "WeatherApp Auditing";

		public static async Task<T> GetData<T>(
			string auditId,
			string commandName,
			string commandParameter,
			Func<Task<T>> dataRetriever,
			ILogger logger)
		{
			bool success = true;

			try
			{
				return await dataRetriever().ConfigureAwait(false);
			}
			catch
			{
				success = false;
				throw;
			}
			finally
			{
				Log(
					logger,
					success,
					auditId,
					commandName,
					commandParameter);
			}
		}

		private static void Log(ILogger logger, bool success, string auditId, string commandName, string commandParameter)
		{
			if (logger == null)
			{
				throw new ArgumentNullException(nameof(logger));
			}

			var eventId = new EventId(100, commandName);
			DateTime time = DateTime.Now;

			string failedSuccess = success ? "Success" : "Fail";

			string parameterPart = string.IsNullOrWhiteSpace(commandParameter) ? string.Empty : $"with parameter '{commandParameter}'";
			string message = $"WeatherAppAuditing: '{commandName}' {failedSuccess} {parameterPart}. Timestamp: {time}, AuditID: {auditId}, RequestID: {auditId}";

			if (success)
			{
				logger.LogInformation(eventId, message, auditId, time);
			}
			else
			{
				logger.LogError(eventId, message, auditId, time);
			}
		}
	}
}
