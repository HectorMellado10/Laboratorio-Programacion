﻿using Laboratorio_Programacion;
using System;
using System.Windows.Forms;

namespace Laboratorio_Programacion
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
