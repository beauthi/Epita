using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Photoshop
{
    class BitmapReader
    {
        public int height { get; private set; }
        public int width { get; private set; }

        private int pixel_array_offset;
        private short bits_per_pixel;
        
        private byte[] header;
        private Color[,] pixels;

        public BitmapReader(string filename)
        {
            if(File.Exists(filename))
            {
                FileStream reader = new FileStream(filename, FileMode.Open);
                if (is_bitmap(reader))
                {
                    read_header(reader);
                    reader.Close();
                }
                else
                    throw new Exception("This is not a BMP");
            }
        }

        public void save(string filename)
        { }

        public void display_header()
        {
            Console.WriteLine("Width              | {0} pixels", this.width);
            Console.WriteLine("Height             | {0} pixels", this.height);
            Console.WriteLine("Bits per pixel     | {0} bits", this.bits_per_pixel);
            Console.WriteLine("pixel_array_offset | {0} bytes", this.pixel_array_offset);
        }

        private void read_header(FileStream fs)
        {
            this.header = new byte[51];

            if(fs.Length >= 0x2)
                fs.Read(header, 0, 51);

            fs.Seek(0, 0);
            this.width = BitConverter.ToInt32(header, 0x12);
            this.height = BitConverter.ToInt32(header, 0x16);
            this.bits_per_pixel = BitConverter.ToInt16(header, 0x1A);
            this.pixel_array_offset = BitConverter.ToInt32(header, 0xE) + 0xE; ;

        }

        public Color get_pixel(int x, int y)
        {
            return new Color();
        }

        public void set_pixel(int x, int y, Color c)
        { }

        private void read_pixels(FileStream fs)
        {
            pixels = new Color[64];
            
            fs.Read(pixels, 0, fs.Length - pixel_array_offset);
        }

        private bool is_bitmap(FileStream fs)
        {
            if (fs.Length > 2)
            {
                byte[] magic = new byte[2];
                fs.Read(magic, 0, 2);
                return magic[0] == 0x42 && magic[1] == 0x4D;
            }
            else
                return false;
        }


    }
}
