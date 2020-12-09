using HuffmansTranscoder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HuffmansTranscoder
{
    class Program
    {
        static void Main(string[] args)
        {

            string filePath = @"C:\Huffman\test.bin";
            string input = "";
            string original = "";
            try
            {
                // создаем объект BinaryWriter
                using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(filePath, FileMode.OpenOrCreate), Encoding.ASCII))
                {
                    binaryWriter.Write("huffmans encoder");
                }

                using (BinaryReader binaryReader = new BinaryReader(File.Open(filePath, FileMode.Open), Encoding.ASCII))
                {
                    while (binaryReader.PeekChar() > -1)
                    {
                        original += binaryReader.ReadString();
                    }
                }




                // создаем объект BinaryReader
                using (BinaryReader binaryReader = new BinaryReader(File.Open(filePath, FileMode.Open), Encoding.ASCII))
                {

                    while (binaryReader.PeekChar() > -1)
                    {
                        input += binaryReader.ReadString();
                    }
                }

                HuffmanTree huffmanTree = new HuffmanTree();


                // Build the Huffman tree
                huffmanTree.Build(input);

                // Encode
                BitArray encoded = huffmanTree.Encode(input);

                // Console.WriteLine("Original: " + original);

                Console.WriteLine("Decoded: " + original);

                Console.WriteLine();

                Console.Write("Encoded: ");
                foreach (bool bit in encoded)
                {
                    Console.Write((bit ? 1 : 0) + "");
                }
                Console.WriteLine();
                Console.WriteLine();
                // Decode
                string decoded = huffmanTree.Decode(encoded).ToString();

                Console.WriteLine("Decoded: " + decoded);

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
    }
}
