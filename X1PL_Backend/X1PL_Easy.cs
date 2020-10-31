using System;
using System.Collections.Generic;
using System.Text;
using X1PL_Backend.Interpretations;
using X1PL_Backend.IO;
using X1PL_Backend.Structures;

namespace X1PL_Backend
{
    public class X1PL_Easy
    {
        public X1PL_Easy(string conrsPath)
        {
            StructureComprehender.Work(conrsPath, out FileLayout[] files);

            CommandLayout[] commandLayouts = new CommandLayout[files.Length - 1];

            for (int line = 0; line < files.Length; line++)
            {
                for (int word = 0; word < files[line].lines.Length; word++)
                {
                    var sp = files[0].lines[line].Split(' ');

                    commandLayouts[line].Keyword = sp[0];

                    commandLayouts[line].Args = new Type[sp.Length - 2];

                    foreach (var i in sp)
                    {
                        if (commandLayouts[line].Keyword == i) { continue; }

                        try
                        {
                            double.Parse(i);
                            commandLayouts[line].Args[word] = typeof(double);
                        }
                        catch
                        {
                            commandLayouts[line].Args[word] = typeof(string);
                        }

                    }

                    commandLayouts[line].ArgsString = sp;
                }
                
            }

            CompleteCommand[] commands = Interpreter.GetCommands(commandLayouts);

            foreach (var i in commands)
            {
                switch (i.command)
                {
                    case Commands.Print:
                        { Console.WriteLine(i.ComLay.ArgsString[1].ToString()); }
                        break;
                    default:
                        { }
                        break;
                }
            }
        }
    }
}
