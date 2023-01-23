namespace App.Core.Exceptions;

public class ParameterRequiredException : Exception
{
    public ParameterRequiredException (string errorMessage) : base(errorMessage) {}
}
