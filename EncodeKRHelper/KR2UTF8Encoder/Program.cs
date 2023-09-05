using System.Text;

namespace KR2UTF8Encoder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            ContainsKrEncoding();
            GetFiles(@"C:\dev\CGP2nd\ch06");
            //GetKrEncodings();
        }

        // check if cotains korean encoding 
        static bool ContainsKrEncoding()
        {

            // in english: register encoding provider
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // in english: try to get encoding of codepage 949
            Encoding koreanEncoding;
            try
            {
                koreanEncoding = Encoding.GetEncoding(949);
                Console.WriteLine("Successfully retrieved the Korean encoding.");
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving the Korean encoding: " + ex.Message);
                return false;
            }
        }

        // get encodinginfos related to korean
        static void GetKrEncodings()
        {
            EncodingInfo[] encodings = Encoding.GetEncodings();
            foreach (EncodingInfo encoding in encodings)
            {
                //write encoding name and codepage
                Console.WriteLine( encoding.Name + " " + encoding.CodePage);
            }
        }

        // a method that get all files in a directory including subdirectories
        static void GetFiles(string path)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                // if it is hlsl file then convert it
                if (IsHLSLFile(file))
                {
                    ConvertFile(file);
                    Console.WriteLine(file);
                }
            }

            string[] directories = Directory.GetDirectories(path);
            foreach (string directory in directories)
            {
                GetFiles(directory);
            }
        }

        // check if a file is a hlsl file
        static bool IsHLSLFile(string path)
        {
            return Path.GetExtension(path) == ".hlsl";
        }

        // a method that convert a file from KR to UTF8
        static void ConvertFile(string path)
        {
            Encoding koreanEncoding = Encoding.GetEncoding(949);

            string text = File.ReadAllText(path, koreanEncoding);
            File.WriteAllText(path, text, Encoding.UTF8);
        }
    }
}