namespace ArchitecturalStudioTradition.Domain.SeedWork
{
    public static class Clock
    {
        private static DateTime? _overriddenDate;

        public static DateTime Now => _overriddenDate ?? DateTime.UtcNow;

        public static void Set(DateTime overriddenDate) => _overriddenDate = overriddenDate;

        public static void Reset() => _overriddenDate = null;
    }
}