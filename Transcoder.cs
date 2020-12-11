using HuffmansNode;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HuffmansTranscoder
{

    /// <summary>
    ///  Class performs the encode/decode operations
    /// </summary>
    /// 
    public class Transcoder
    {
        public FileIO file = new FileIO();

        /// <summary>
        /// Encoding of the input data
        /// </summary>
        /// <param name="source">input data</param>
        /// <param name="huffmansRoot">the huffmans tree for data encoding</param>
        public BitArray Encode(string source, Node huffmansRoot)
        {
            List<bool> encodedSequence = new List<bool>();

            for (int i = 0; i < source.Length; i++)
            {
                List<bool> encodedSymbol = huffmansRoot.CreateNodePath(source[i], new List<bool>());
                encodedSequence.AddRange(encodedSymbol);
            }

            BitArray encodedbits = new BitArray(encodedSequence.ToArray());

          

            return encodedbits;
        }


        /// <summary>
        /// Decoding of the input data
        /// </summary>
        /// <param name="bits">the input data</param>
        /// <param name="huffmansRoot">the huffmans tree for data decoding</param>
        /// <param name="fileDirectory">the directory of the decoded file</param>
        public string Decode(BitArray bits, Node huffmansRoot)
        {
            Node currentNode = huffmansRoot;
            string decodedSequence = "";

            foreach (bool bit in bits)
            {
                if (bit)
                {
                    if (currentNode.Right != null)
                    {
                        currentNode = currentNode.Right;
                    }
                }
                else
                {
                    if (currentNode.Left != null)
                    {
                        currentNode = currentNode.Left;
                    }
                }

                if (currentNode.Left == null && currentNode.Right == null)
                {
                    decodedSequence += currentNode.Symbol;
                    currentNode = huffmansRoot;
                }
            }

            return decodedSequence;
        }

        /// <summary>
        ///  Converting of the bits to the byte array
        /// </summary>
        /// <param name="bits">array of the bits</param>
        public static byte[] ToByteArray(BitArray bits)
        {
            int numBytes = bits.Count / 8;
            if (bits.Count % 8 != 0) numBytes++;

            byte[] bytes = new byte[numBytes];
            int byteIndex = 0, bitIndex = 0;

            for (int i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                    bytes[byteIndex] |= (byte)(1 << (7 - bitIndex));

                bitIndex++;
                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    byteIndex++;
                }
            }

            return bytes;
        }

    }
}
