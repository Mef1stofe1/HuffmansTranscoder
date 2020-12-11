using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HuffmansTranscoder
{
    /// <summary>
    ///  Helper class for inpput/output file operations
    /// </summary>
    /// 
    public class FileIO
    {

        /// <summary>
        /// Method is used for reading from file
        /// </summary>
        /// <param name="path">the location of the file</param>
        public string ReadFromFile(string path)
        {

            string input = string.Empty;
            using (BinaryReader binaryReader = new BinaryReader(File.Open(path, FileMode.Open), Encoding.ASCII))
            {

                while (binaryReader.PeekChar() > -1)
                {
                    input += binaryReader.ReadString();
                }
            }

            return input;
        }

        /// <summary>
        /// Method is used for writing to the file 
        /// </summary>
        /// <param name="path">the location of the file</param>
        /// <param name="bytes">binary data to be written</param>
        public void WriteToFile(string path, byte[] bytes)
        {
            using (BinaryWriter binWriter = new BinaryWriter(File.Open(path, FileMode.Create)))
            {

                binWriter.Write(bytes);

            }

        }
        /// <summary>
        /// Method is used for writing to the file 
        /// </summary>
        /// <param name="path">the location of the file</param>
        /// <param name="data">string data to be written</param>
        public void WriteToFile(string path, string data)
        {
            using (BinaryWriter binWriter = new BinaryWriter(File.Open(path, FileMode.Create), Encoding.ASCII))
            {

                binWriter.Write(data);

            }

        }
        /// <summary>
        /// Checks if the input file satisfies the conditions
        /// </summary>
        /// <param name="path">The path of the file</param>
        /// <returns>False if any of the required conditions are failled</returns>
        public bool CheckTheFile(string path)
        {
            FileInfo fileInfo = new FileInfo(path);

            if (!File.Exists(path))
            {
                Console.WriteLine("The file path is incorrect. Please provide the correct filepath.");
                return false;
            }
            if (fileInfo.Extension != ".bin")
            {
                Console.WriteLine("The selected file is not supported. Please select the file with '.bin' extention.");
                return false;
            }

            if (fileInfo.Length > 10240)
            {
                Console.WriteLine("The selected file is to big. Please select the file with max 10 KB size.");
               
                return false;
            }

            if (fileInfo.Length == 0)
            {
                Console.WriteLine("The selected file is empty. Please select the another file.");
                return false;
            }

            return true;
        }
    }
}
