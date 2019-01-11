using System;

namespace Line.LineSDK {
    public static class Helpers {
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime FromUnixTime(long unixTime) {
            return epoch.AddSeconds(unixTime);
        }
    }
}

