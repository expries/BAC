﻿namespace BAC.Clocks.PhysicalTimeProviders;

public class PhysicalTimeProvider : ITimeProvider
{
    public DateTime GetCurrentTime()
    {
        return DateTime.UtcNow;
    }
}