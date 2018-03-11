using System;

namespace OriginalSINe.GroupMeAPI
{
    static public class UtilityMisc
    {
        static public DateTime ToDateTime(this long secondsSinceEpoch)
        => _epoch_.AddSeconds(secondsSinceEpoch);

        private static readonly DateTime _epoch_ = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }
}
