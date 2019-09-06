namespace ScandicCase.Exceptions
{
	using System;

	// You should have each class in its separate file.
	// The only exception that may be accepted is when you have a generic and a non generic implementation so Foo and Foo<T> could be implemented in the same file.
	public class NotFoundException : Exception
	{
		public NotFoundException(string s) : base(s)
		{
		}
	}
}
