# Fish-Console-Lib
This is a Console Lib for your Console app!

i did take code form "idk the name"

how to use

WithOut Color 
```c#

using Console = ConsoleLib.Console;

namespace ConsoleThing
{
    public class Logger
    {
        ublic void Write(string message)
        {
            Console.Write(message);
        }
    }
}

```

With Color 
```c#

using Console = ConsoleLib.Console;

namespace ConsoleThing
{
    public class Logger
    {
        ublic void Write(string message)
        {
            Console.Write(message, Color.BlueViolet);
        }
    }
}

```
