namespace EventSourcing.Api.Common.Extensions
{
    public static class GuidExtensions
    {
        internal static bool IsEmpty(this Guid guid)
        {
            return (guid == Guid.Empty);
        }
    }
}
