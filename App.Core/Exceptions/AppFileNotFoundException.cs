namespace App.Core.Exceptions;

public class AppFileNotFoundException : Exception
{
    public AppFileNotFoundException (string errorMessage) : base(errorMessage) {}
}
