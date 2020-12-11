using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuffmansNode
{
    /// <summary>
    ///  This class describes the construction of the leafs of the tree 
    /// </summary>
    /// 

    public class Node
    {
        public Node Right { get; set; }
        public Node Left { get; set; }
        public int Weight { get; set; }
        public char Symbol { get; set; }

        /// <summary>
        /// Creates the list of encoded data
        /// </summary>
        /// <param name="symbol">symbol of the input sequence</param>
        /// <param name="data">list of the encoded data</param>
        public List<bool> CreateNodePath(char symbol, List<bool> data)
        {
            if (Right == null && Left == null)
            {
                if (symbol.Equals(this.Symbol))
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                List<bool> left = null;
                List<bool> right = null;

                if (Left != null)
                {
                    List<bool> leftPath = new List<bool>();
                    leftPath.AddRange(data);
                    leftPath.Add(false);

                    left = Left.CreateNodePath(symbol, leftPath);
                }

                if (Right != null)
                {
                    List<bool> rightPath = new List<bool>();
                    rightPath.AddRange(data);
                    rightPath.Add(true);
                    right = Right.CreateNodePath(symbol, rightPath);
                }

                if (left != null)
                {
                    return left;
                }
                else
                {
                    return right;
                }
            }
        }
    }
}