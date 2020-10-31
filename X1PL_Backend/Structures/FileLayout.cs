using System;
using System.Collections.Generic;
using System.Text;

namespace X1PL_Backend.Structures
{
    public struct FileLayout
    {
        public readonly string[] lines;
        public readonly string[,] commands;

        public readonly string fileName;

        public FileLayout(string[] l, string[,] c, string f) => (lines, commands, fileName) = (l, c, f);
    }

}
