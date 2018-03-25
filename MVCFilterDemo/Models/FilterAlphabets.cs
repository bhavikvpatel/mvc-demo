using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCFilterDemo
{
    public static class FilterAlphabets
    {
        public static List<char> Alphabets { get; set; } = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().ToList();
    }
}