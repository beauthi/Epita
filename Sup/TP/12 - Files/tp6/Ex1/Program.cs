using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            const string PATH = "../../../TEXT_FILE";

            write_to_file(PATH, new string[3] {"test\n", "voiture\n", "Banana"});
            Console.WriteLine(read_file(PATH));

            Console.WriteLine("\n======\n");

            write_to_file(PATH, new string[1] { "first\n" }, false);
            Console.WriteLine(read_file(PATH) + "\n");
            write_to_file(PATH, new string[1] { "last\n" }, true);
            Console.WriteLine(read_file(PATH));

            Console.WriteLine("\n======\n");

            Console.ReadLine();
            */
        }

        static string read_file(string filename)
        {
            string content;
            
            if(File.Exists(filename))
            {
                StreamReader stream = new StreamReader(filename);
                content = stream.ReadToEnd();
                stream.Close();
            }
            else
                content = "Error : file not found.";

            return content;
        }

        /// <summary>
        /// Write something to a file
        /// </summary>
        /// <param name="filename">Path to file</param>
        /// <param name="lines">Text to write</param>
        /// <param name="append">APpend to end or not</param>
        static void write_to_file(string filename, string[] lines, bool append = false)
        {
            if (File.Exists(filename) && lines.Length > 0)
            {
                StreamWriter writer = new StreamWriter(filename, append, Encoding.UTF8);

                foreach (string frag in lines)
                    writer.Write(frag);

                writer.Close();

            }
            else
                Console.Error.WriteLine("Nothing has been written");
        }
    }
}
