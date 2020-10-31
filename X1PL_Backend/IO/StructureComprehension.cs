using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using X1PL_Backend.Structures;


namespace X1PL_Backend.IO
{
    public class StringArray
    {
        public string[] Connections { get; set; }
    }

    public static class StructureComprehender
    {
       
        public static void Work(string pathToConnector, out FileLayout[] allFiles)
        {
            DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(StringArray));

            string[] conrs;

            using (FileStream fs = new FileStream(pathToConnector,FileMode.Open))
            {
                conrs = (dataContractJsonSerializer.ReadObject(fs) as StringArray).Connections;
            }

            List<string> files = new List<string>();


            for (int i = 0; i < conrs.Length; i++)
            {
                files.AddRange(Directory.GetFiles(conrs[i]));
            }

            List<FileLayout> ret = new List<FileLayout>();

            foreach (var file in files)
            {
                var contents = File.ReadAllLines(file);

                List<string[]> allContents = new List<string[]>();
                int largestLength = 0;

                foreach (var i in contents)
                {
                    allContents.Add(i.Split(' '));
                    if (i.Split(' ').Length > largestLength) { largestLength = i.Split(' ').Length; }
                }

                string[,] formcont = new string[contents.Length - 1, largestLength];

                for (int x = 0; x < allContents.Count; x++)
                {
                    for (int y = 0; y < allContents[x].Length; y++)
                    {
                        formcont[x, y] = allContents[x][y];
                    }
                }

                ret.Add(new FileLayout
                    (
                    contents,
                    formcont
                    ,
                    Path.GetFileName(file)
                    ));

            }

            allFiles = ret.ToArray();
        }
    }
}
