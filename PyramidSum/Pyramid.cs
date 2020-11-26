using System;
using System.Collections;
using System.Collections.Generic;

namespace PyramidSum
{
    public class Pyramid
    {
        List<string[]> PyramidList { get; set; }
        public Pyramid(List<string> pyramidDef)
        {
            PyramidList = new List<string[]>();
            for (int i = 0; i < pyramidDef.Count; i++)
            {
                PyramidList.Add(pyramidDef[i].Split(' '));
            }
        }

        public int FindMaxSum(out string pathMax)
        {
            int maxSum = 0;
            pathMax = "";
            string path;
            int pathCount =(int)Math.Truncate(Math.Pow(2, PyramidList.Count - 1));
            for (int pathNo = 0; pathNo < pathCount; pathNo++)
            {
                int sum = Sum(pathNo, out path);
                if (sum > maxSum)
                {
                    maxSum = sum;
                    pathMax = path;
                }
            }
            return maxSum;
        }
        private int Sum(int pathNo, out string path)
        {
            byte[] bitArr = CreateBitArray(pathNo, PyramidList.Count - 1);
            path = PyramidList[0][0];
            int firstValue = Int32.Parse(PyramidList[0][0]);
            int sum = firstValue;            
            int value;
            int valueOld = firstValue;
            int row = 0;
            int col = 0;
            for (int i = 0; i < PyramidList.Count - 1; i++)
            {
                if (bitArr[i] == 0)
                    row++;
                else
                {
                    row++;
                    col++;
                }
                value = Int32.Parse(PyramidList[row][col]);
                if (IsOdd(valueOld) && !IsOdd(value) || !IsOdd(valueOld) && IsOdd(value))
                {
                    sum += value;
                    path += ", " + PyramidList[row][col];
                }
                else
                {
                    path = "";
                    return 0;
                }
                valueOld = value;
            }
            return sum;
        }
        
        private byte[] CreateBitArray(int integer, int length)
        {
            BitArray boolArr = new BitArray(new int[] { integer });            
            byte[] bitArr = new byte[length];
            for (int i = 0; i < bitArr.Length; i++)
            {
                bitArr[i] = boolArr[i] ? (byte)1 : (byte)0;
            }
            return bitArr;
        }

        private bool IsOdd(int value)
        {
            return value % 2 != 0;
        }
    }
}
