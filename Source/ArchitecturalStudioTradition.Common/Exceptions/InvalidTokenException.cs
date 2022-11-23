namespace ArchitecturalStudioTradition.Common.Exceptions
{
    [Serializable]
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException(string message) : base(message)
        {
        }
    }
}
