﻿using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace WaitForExit
{
    internal static class Program
    {
        private delegate bool HandlerRoutine(int ctrlType);

        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(HandlerRoutine handler, bool add);

        private static void Main()
        {
            var exitEvent = new ManualResetEventSlim();

            SetConsoleCtrlHandler(ctrlType =>
            {
                if (ctrlType != 0 /* CTRL+C */) return false;
                exitEvent.Set();
                return true;
            }, true);

            Console.WriteLine("Press CTRL+C to exit...");
            exitEvent.Wait();
        }
    }
}
