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

            HuffmansTree huffmanTree = new HuffmansTree();
            Transcoder transcoder = new Transcoder();
            FileIO file = new FileIO();
            string input = string.Empty;

            Console.WriteLine("Hello! This is the simple implemantation of Huffmans encoding/decoding alorithm." +
                " The transcoder supports only the binary files (up to 10 KB) with '.bin' extention." +
                " In order to get the proper result, please make sure that your file contains the text according to the ASCII character encoding standard. \r\n Select the file to proceed or type 'exit' to quit the program.");

            while (true)
            {
                try
                {
                    Console.WriteLine(" \r\nInsert the file path:");
                    input = Console.ReadLine();

                    if (file.CheckTheFile(input))
                    {
                        
                        string filePath = input;
                        string data = file.ReadFromFile(filePath);


                        // Build the Huffmans tree
                        var huffmanRoot = huffmanTree.CreateHuffmansTree(data);


                        // Encode
                        BitArray encodedbits = transcoder.Encode(data, huffmanRoot);


                        // Decode
                        string decoded = transcoder.Decode(encodedbits, huffmanRoot);

                        //Write to file
                        string fileDirectory = Path.GetDirectoryName(filePath);
                        file.WriteToFile(fileDirectory + @"\encoded.bin", Transcoder.ToByteArray(encodedbits));
                        file.WriteToFile(fileDirectory + @"\decoded.bin", decoded);

                        //Show the result
                        Console.WriteLine();
                        Console.WriteLine("Result is: \r\n");
                        Console.WriteLine("Inserted file data:  " + data);
                        Console.WriteLine();

                        Console.Write("Encoded: ");
                        foreach (bool bit in encodedbits)
                        {
                            Console.Write((bit ? 1 : 0) + "");
                        }

                        Console.WriteLine();
                        Console.WriteLine("\r\nDecoded: " + decoded);

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: Unable to encode/decode. Please try to use different file.");
                }

                if (input == "exit")
                {
                    break;
                }
            }
        }
    }
}
