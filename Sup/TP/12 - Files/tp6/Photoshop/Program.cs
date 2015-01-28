using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Photoshop
{
    class Program
    {
        static void Main(string[] args)
        {
            const string PATH = "../../../BMP_FILE";

            BitmapReader reader = new BitmapReader(PATH);
            reader.display_header();
            
            Console.ReadLine();
        }
    }
}
