﻿using System;
using System.Collections.Generic;

namespace AnalyzeFile.Model.Extensions;

public static class ListExtension
{
    public static void ShowInConsole(this List<(string, string)> list)
    {
        for (var i = 0; i < list.Count; i++)
        {
            Console.WriteLine(list[i].Item1 + " " + list[i].Item2);
        }
    }
}