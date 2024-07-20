# Fish-Console-Lib

Fish-Console-Lib is a library designed to enhance your console applications by providing advanced logging functionalities, including color support.

## Features

- Simple integration with existing console applications
- Support for colored output

## Installation

To use Fish-Console-Lib, add the library to your project through DLL in your project references.

## Usage

### Without Color

You can use Fish-Console-Lib without color support as shown below:

```csharp
using Console = ConsoleLib.Console;

namespace ConsoleApp
{
    public class Logger
    {
        public void Write(string message)
        {
            Console.Write(message);
        }
    }
}
```

### With Color

To add color support to your console output, you can specify a color for the message:

```csharp
using Console = ConsoleLib.Console;
using System.Drawing; 

namespace ConsoleApp
{
    public class Logger
    {
        public void Write(string message)
        {
            Console.Write(message, Color.BlueViolet);
        }
    }
}
```

## Acknowledgments

Some code and inspiration were taken from various sources. Special thanks to them.
