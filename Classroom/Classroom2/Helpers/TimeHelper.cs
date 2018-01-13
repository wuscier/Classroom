using System;

namespace Classroom.Helpers
{
    public class TimeHelper
    {
        public static uint GetCurrentTimeTotalSeconds()
        {
            uint totalSeconds = (uint)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return totalSeconds;
        }
    }
}
