using System;
using System.Collections.Generic;
using System.Collections;

// writting a class that models matrix
namespace Matrix
{   
    public class Matrix<T>
        where T: struct, IComparable, IComparable<T>, IEquatable<T>
    {
        private T[,] data;
        private int NOrows { get; set; }
        private int NOcols { get; set; }

        // Building the Constructor
        public Matrix(int rows, int cols)
        {
            //initialising number of rows and columns
            this.NOrows = rows;
            this.NOcols = cols;

            // initialising the matrix
            data = new T[rows, cols];
        }

        public Matrix(T[,] rowElement)
        {
            this.data = new int[,] (rowElement);
        }
    }
}