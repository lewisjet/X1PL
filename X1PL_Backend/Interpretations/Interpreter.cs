using System;
using System.Collections.Generic;
using System.Text;

namespace X1PL_Backend.Interpretations
{
    public static class Interpreter
    {
        public static Dictionary<CommandLayout, Commands> LanguageCommands
        {
            get
            {
                var i = new Dictionary<CommandLayout, Commands>();
                #region Add
                i.Add
                (
                new CommandLayout
                {
                    Keyword = "print",
                    Args = new Type[] { typeof(string) }
                },
                Commands.Print
                );
                #endregion
                return i;
            }
        }
        public static CompleteCommand[] GetCommands(CommandLayout[] commands)
        {
            List<KeyValuePair<CommandLayout,Commands>> ret = new List<KeyValuePair<CommandLayout, Commands>> ();
            var lc = LanguageCommands;
            foreach (var i in commands)
            {
               if(i.Keyword == "#") { continue; }

                if (!lc.ContainsKey(i))
                {
                    string exept = $"InvalidOperationException: {i.Keyword}: ";
                    foreach (var x in i.Args)
                    {
                        exept += $"{x} ";
                    }
                    throw new Exception(exept);
                }

                ret.Add(new KeyValuePair<CommandLayout, Commands>(i ,LanguageCommands[i]));
            }

            CompleteCommand[] pret = new CompleteCommand[ret.Count - 1];
            for (int i = 0; i < pret.Length; i++)
            {
                pret[i] = new CompleteCommand { ComLay = ret[i].Key, command = ret[i].Value };
            }
            return pret;

        }
    }

    public enum Commands
    {
        Print
    }

    public struct CompleteCommand
    {
        public CommandLayout ComLay { get; set; }
        public Commands command { get; set; }
    }
    public struct CommandLayout
    {
       public string Keyword { get; set; }
       public Type[] Args { get; set; }
       public string[] ArgsString { get; set; }

        public override string ToString()
        {
            return string.Format($"{Keyword}: {0}", Args);
        }
    }

}
