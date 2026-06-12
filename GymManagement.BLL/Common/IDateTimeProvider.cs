namespace GymManagement.BLL.Common;

public interface IDateTimeProvider
{
    DateTime UTCNow { get; }
    DateOnly Today { get; }
}
