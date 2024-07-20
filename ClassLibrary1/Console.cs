using ConsoleLib.ColorStuff;
using ConsoleLib.ColorStuff.Exception;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;

namespace ConsoleLib
{
    public static class Console
    {
        private const int MaxColorChanges = 16;
        private const int InitialColorChangeCount = 1;

        private static readonly bool IsWindows = ColorManager.IsWindows();
        private static readonly bool IsInCompatibilityMode;
        private static readonly Dictionary<string, COLORREF> DefaultColorMap;
        private static readonly ColorStore ColorStore;
        private static readonly ColorManager ColorManager;
        private static readonly ColorManagerFactory ColorManagerFactory = new ColorManagerFactory();

        public static event ConsoleCancelEventHandler CancelKeyPress = delegate { };

        public static string Title
        {
            get => System.Console.Title;
            set => System.Console.Title = value;
        }

        public static int WindowHeight
        {
            get => System.Console.WindowHeight;
            set => System.Console.WindowHeight = value;
        }

        public static int WindowLeft
        {
            get => System.Console.WindowLeft;
            set => System.Console.WindowLeft = value;
        }

        public static int WindowTop
        {
            get => System.Console.WindowTop;
            set => System.Console.WindowTop = value;
        }

        public static int WindowWidth
        {
            get => System.Console.WindowWidth;
            set => System.Console.WindowWidth = value;
        }

        static Console()
        {
            try { if (IsWindows) { DefaultColorMap = new ColorMapper().GetBufferColors(); } }
            catch (ConsoleAccessException) { IsInCompatibilityMode = true; }

            ColorStore = GetColorStore();
            ColorManager = ColorManagerFactory.GetManager(ColorStore, MaxColorChanges, InitialColorChangeCount, IsInCompatibilityMode);

            ReplaceAllColorsWithDefaults();
            System.Console.CancelKeyPress += OnConsoleCancelKeyPress;
        }
        #region Write

        public static void Write(uint value, Color color) => WriteInColor(() => System.Console.Write(value), color);
        public static void Write(uint value) => System.Console.Write(value);
        public static void Write(string value, Color color) => WriteInColor(() => System.Console.Write(value), color);
        public static void Write(string value) => System.Console.Write(value);
        public static void Write(object value, Color color) => WriteInColor(() => System.Console.Write(value), color);
        public static void Write(object value) => System.Console.Write(value);
        public static void Write(ulong value) => System.Console.Write(value);
        public static void Write(ulong value, Color color) => WriteInColor(() => System.Console.Write(value), color);
        public static void Write(bool value) => System.Console.Write(value);
        public static void Write(bool value, Color color) => WriteInColor(() => System.Console.Write(value), color);
        public static void Write(char value) => System.Console.Write(value);
        public static void Write(char value, Color color) => WriteInColor(() => System.Console.Write(value), color);
        public static void Write(char[] value) => System.Console.Write(value);
        public static void Write(char[] value, Color color) => WriteInColor(() => System.Console.Write(value), color);
        public static void Write(decimal value) => System.Console.Write(value);
        public static void Write(decimal value, Color color) => WriteInColor(() => System.Console.Write(value), color);
        public static void Write(double value) => System.Console.Write(value);
        public static void Write(double value, Color color) => WriteInColor(() => System.Console.Write(value), color);
        public static void Write(float value) => System.Console.Write(value);
        public static void Write(float value, Color color) => WriteInColor(() => System.Console.Write(value), color);
        public static void Write(int value) => System.Console.Write(value);
        public static void Write(int value, Color color) => WriteInColor(() => System.Console.Write(value), color);
        public static void Write(long value) => System.Console.Write(value);
        public static void Write(long value, Color color) => WriteInColor(() => System.Console.Write(value), color);

        #endregion
        #region WriteLine

        public static void WriteLine() => System.Console.WriteLine();
        public static void WriteLine(bool value) => System.Console.WriteLine(value);
        public static void WriteLine(bool value, Color color) => WriteInColor(() => System.Console.WriteLine(value), color);
        public static void WriteLine(char value) => System.Console.WriteLine(value);
        public static void WriteLine(char value, Color color) => WriteInColor(() => System.Console.WriteLine(value), color);
        public static void WriteLine(char[] value) => System.Console.WriteLine(value);
        public static void WriteLine(char[] value, Color color) => WriteInColor(() => System.Console.WriteLine(value), color);
        public static void WriteLine(decimal value) => System.Console.WriteLine(value);
        public static void WriteLine(decimal value, Color color) => WriteInColor(() => System.Console.WriteLine(value), color);
        public static void WriteLine(double value) => System.Console.WriteLine(value);
        public static void WriteLine(double value, Color color) => WriteInColor(() => System.Console.WriteLine(value), color);
        public static void WriteLine(float value) => System.Console.WriteLine(value);
        public static void WriteLine(float value, Color color) => WriteInColor(() => System.Console.WriteLine(value), color);
        public static void WriteLine(int value) => System.Console.WriteLine(value);
        public static void WriteLine(int value, Color color) => WriteInColor(() => System.Console.WriteLine(value), color);
        public static void WriteLine(long value) => System.Console.WriteLine(value);
        public static void WriteLine(long value, Color color) => WriteInColor(() => System.Console.WriteLine(value), color);
        public static void WriteLine(object value) => System.Console.WriteLine(value);
        public static void WriteLine(object value, Color color) => WriteInColor(() => System.Console.WriteLine(value), color);
        public static void WriteLine(string value) => System.Console.WriteLine(value);
        public static void WriteLine(string value, Color color) => WriteInColor(() => System.Console.WriteLine(value), color);
        public static void WriteLine(uint value) => System.Console.WriteLine(value);
        public static void WriteLine(uint value, Color color) => WriteInColor(() => System.Console.WriteLine(value), color);
        public static void WriteLine(ulong value) => System.Console.WriteLine(value);
        public static void WriteLine(ulong value, Color color) => WriteInColor(() => System.Console.WriteLine(value), color);

        #endregion
        private static void WriteInColor(Action action, Color color)
        {
            var oldSystemColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = ColorManager.GetConsoleColor(color);
            action();
            System.Console.ForegroundColor = oldSystemColor;
        }
        private static ColorStore GetColorStore()
        {
            var colorMap = new ConcurrentDictionary<Color, ConsoleColor>();
            var consoleColorMap = new ConcurrentDictionary<ConsoleColor, Color>
            {
                [ConsoleColor.Black] = Color.FromArgb(0, 0, 0),
                [ConsoleColor.Blue] = Color.FromArgb(0, 0, 255),
                [ConsoleColor.Cyan] = Color.FromArgb(0, 255, 255),
                [ConsoleColor.DarkBlue] = Color.FromArgb(0, 0, 128),
                [ConsoleColor.DarkCyan] = Color.FromArgb(0, 128, 128),
                [ConsoleColor.DarkGray] = Color.FromArgb(128, 128, 128),
                [ConsoleColor.DarkGreen] = Color.FromArgb(0, 128, 0),
                [ConsoleColor.DarkMagenta] = Color.FromArgb(128, 0, 128),
                [ConsoleColor.DarkRed] = Color.FromArgb(128, 0, 0),
                [ConsoleColor.DarkYellow] = Color.FromArgb(128, 128, 0),
                [ConsoleColor.Gray] = Color.FromArgb(192, 192, 192),
                [ConsoleColor.Green] = Color.FromArgb(0, 255, 0),
                [ConsoleColor.Magenta] = Color.FromArgb(255, 0, 255),
                [ConsoleColor.Red] = Color.FromArgb(255, 0, 0),
                [ConsoleColor.White] = Color.FromArgb(255, 255, 255),
                [ConsoleColor.Yellow] = Color.FromArgb(255, 255, 0)
            };

            return new ColorStore(colorMap, consoleColorMap);
        }
        public static void ReplaceAllColorsWithDefaults() { if (!IsInCompatibilityMode && IsWindows) { new ColorMapper().SetBatchBufferColors(DefaultColorMap); } }
        private static void OnConsoleCancelKeyPress(object sender, ConsoleCancelEventArgs e) => CancelKeyPress?.Invoke(sender, e);
    }
}
