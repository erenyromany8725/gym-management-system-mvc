using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.BLL.Common;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UTCNow => DateTime.UtcNow;

    public DateOnly Today => DateOnly.FromDateTime(UTCNow);
}
