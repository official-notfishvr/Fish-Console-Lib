﻿using System;

namespace ConsoleLib.ColorStuff.Exception
{
    /// <summary>
    /// Encapsulates information relating to exceptions thrown while making calls to the console via the Win32 API.
    /// </summary>
    public sealed class ConsoleAccessException : System.Exception
    {
        /// <summary>
        /// Encapsulates information relating to exceptions thrown while making calls to the console via the Win32 API.
        /// </summary>
        public ConsoleAccessException()
            : base("Color conversion failed because a handle to the actual windows console was not found.")
        {
        }
    }
}
