namespace ScandicCase.Exceptions
{
	using System;

    public class ValidationException : Exception
    {
		// If you create a custom exception you should implement all three base constructors
		// It is also good practice to override serialization methods
        public ValidationException(string s) : base(s)
        {
        }
    }
}
