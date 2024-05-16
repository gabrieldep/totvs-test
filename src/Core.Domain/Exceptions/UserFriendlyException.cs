namespace Core.Domain.Exceptions;

[Serializable]
public class UserFriendlyException : Exception
{
    public UserFriendlyException() { }
    public UserFriendlyException(string message) : base(message) { }
    public UserFriendlyException(string message, Exception inner) : base(message, inner) { }
    protected UserFriendlyException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
