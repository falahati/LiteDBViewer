using System;

namespace LiteDBViewerVersionSelector
{
    public class BadLiteDBVersionException : Exception
    {
        public BadLiteDBVersionException()
        {
            Message = "Selected file is invalid or not supported.";
        }

        public override string Message { get; }
    }
}