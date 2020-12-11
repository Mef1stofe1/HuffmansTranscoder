using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HuffmansNode;

namespace HuffmansTranscoder
{
    /// <summary>
    ///  Class is used to construct the Huffmans tree for future encoding/decoding of the data
    /// </summary>
    /// 
    public class HuffmansTree
    {
        private List<Node> nodes = new List<Node>();
        public  Node Root { get; set; }
        public Dictionary<char, int> Weights = new Dictionary<char, int>();

        /// <summary>
        /// Method is used for building of the Huffmans tree
        /// </summary>
        /// <param name="source">input data</param>
        public Node CreateHuffmansTree(string source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (!Weights.ContainsKey(source[i]))
                {
                    Weights.Add(source[i], 0);
                }

                Weights[source[i]]++;
            }

            // Generate tree nodes using weights
            foreach (var symbol in Weights)
            {
                nodes.Add(new Node() { Symbol = symbol.Key, Weight = symbol.Value });
            }

            while (nodes.Count > 1)
            {
                // sort the nodes by the weight
                List<Node> sortedNodes = nodes.OrderBy(node => node.Weight).ToList<Node>();

                if (sortedNodes.Count >= 2)
                {
                    // Take the first two nodes with the smallest weight
                    List<Node> taken = sortedNodes.Take(2).ToList<Node>();

                    // Create a combined node by combining the weights
                    Node combinedNode = new Node()
                    {
                        Left = taken[0],
                        Right = taken[1],
                        Weight = taken[0].Weight + taken[1].Weight,
                        Symbol = ' ',
                    };

                    nodes.Add(combinedNode);
                    nodes.Remove(taken[0]);
                    nodes.Remove(taken[1]);
                }

                Root = nodes.FirstOrDefault();

            }

            return Root;

        }

    }
}
