﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CarRace
{
    public static class Winapi
    {
        [DllImport("GDI32.dll")]
        public static extern void SwapBuffers(uint hdc); 
    }
}
