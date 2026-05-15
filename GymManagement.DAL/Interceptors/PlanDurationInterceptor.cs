

namespace GymManagementSystem.Interceptors;

public class PlanDurationInterceptor : DbCommandInterceptor
{
    public override InterceptionResult<DbDataReader> ReaderExecuting(
        DbCommand command,
        CommandEventData data,
        InterceptionResult<DbDataReader> result)
    {
        ValidateDuration(command);
        return base.ReaderExecuting(command, data, result);
    }

    public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
        DbCommand command,
        CommandEventData data,
        InterceptionResult<DbDataReader> result,
        CancellationToken cancellationToken = default)
    {
        ValidateDuration(command);
        return base.ReaderExecutingAsync(command, data, result, cancellationToken);
    }

    private static void ValidateDuration(DbCommand command)
    {
        foreach (DbParameter param in command.Parameters)
        {
            if (param.ParameterName.Contains("DurationDays", StringComparison.OrdinalIgnoreCase))
            {
                if (param.Value is int days && (days < 1 || days > 365))
                    throw new InvalidOperationException(
                        $"DurationDays must be between 1 and 365. Got: {days}");
            }
        }
    }
}