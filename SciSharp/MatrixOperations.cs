using System;
using System.Collections;
using System.Collections.Generic;
using MatrixCollection;

namespace MatrixCollection
{
    /// <summary>
    /// A class with methods to carryout operations
    /// on the Matrix class.
    /// </summary>
    public static class MatrixOperations
    {
        /// <summary>
        /// Adds two or more matrices together
        /// returns the resulting matrix
        /// </summary>
        /// <typeparam name="T">
        /// A numeric type that supports addition.
        /// </typeparam>
        /// <param name="matrices">
        /// An array of matrices to be added.
        /// </param>
        /// <returns>A new matrix containing the sum of
        /// the input matrices.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when less than two matrices are provided
        /// or when matrices have mismatched.
        /// </exception>
        /// <example>
        /// Example of adding two matrices:
        /// <code>
        /// Matrix<int> result = MatrixOperations.AddMatrices(matrix1, matrix2);
        /// </code>
        /// </example>
        public static Matrix<T> AddMatrices<T>(
            params Matrix<T>[] matrices) where T:
             struct, IComparable, IComparable<T>, IEquatable<T>
        {
            Matrix<T> result = new Matrix<T>();
            int rows = matrices[0].GetRow();
            int cols = matrices[0].GetCol();

            // Checks if matrix is valid
            // Check if matrices are of the same size
            foreach (var matrix in matrices)
            {
                if (matrix == null || matrices.Length < 2)
                {
                    throw new ArgumentNullException("At least two matrices is required.");
                }
                
                if (matrix.GetRow() != rows || matrix.GetCol() != cols)
                {
                    throw new ArgumentNullException("Enter matrices with same size");
                }
            }

            // Logic to add two matrices together
            Matrix<T> matrix1 = matrices[0];
            Matrix<T> matrix2 = matrices[1];
            for (int i=0; i<rows; i++)
                {
                    List<T> resultRow = new List<T>();
                    for (int j=0; j<rows; j++)
                    {
                        T sum = (dynamic)matrix1[i][j] + (dynamic)matrix2[i][j];
                        resultRow.Add(sum);
                    }
                    result.AddRow(resultRow);
                }
            return result;
        }

        /// <summary>
        /// Subtracts one matrix from another for matrices
        /// having 2 dimensional direction
        /// </summary>
        /// <typeparam name="T">A numeric type that
        /// supports subtraction.
        /// </typeparam>
        /// <param name="matrices">An array of matrices
        /// for subtraction.
        /// </param>
        /// <returns>
        /// returns the resulting matrix
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when less than two matrices are provided
        /// or when matrices have mismatched.
        /// </exception>
        /// <example>
        /// Example of subtraction between two matrices:
        /// <code>
        /// Matrix<int> result = MatrixOperations.SubtractMatrices2D(matrix1, matrix2);
        /// </code>
        /// </example>
        public static Matrix<T> SubtractMatrices2D<T>(
            params Matrix<T>[] matrices) where T:
             struct, IComparable, IComparable<T>, IEquatable<T>
        {
            Matrix<T> result = new Matrix<T>();
            int rows = matrices[0].GetRow();
            int cols = matrices[0].GetCol();

            // Checks if matrix is valid
            // Check if matrices are of the same size
            foreach (var matrix in matrices)
            {
                if (matrix == null || matrices.Length < 2)
                {
                    throw new ArgumentNullException("At least two matrices is required.");
                }
                
                if (matrix.GetRow() != rows || matrix.GetCol() != cols)
                {
                    throw new ArgumentNullException("Enter matrices with same size");
                }
            }

            // Logic to add two matrices together
            Matrix<T> matrix1 = matrices[0];
            Matrix<T> matrix2 = matrices[1];
            for (int i=0; i<rows; i++)
                {
                    List<T> resultRow = new List<T>();
                    for (int j=0; j<rows; j++)
                    {
                        T sum = (dynamic)matrix1[i][j] - (dynamic)matrix2[i][j];
                        resultRow.Add(sum);
                    }
                    result.AddRow(resultRow);
                }
            return result;
        }

        /// <summary>
        /// It is the same as SubtractMatrices2D but for 
        /// 1 Dimensional array of List type.
        /// </summary>
        public static List<T> SubtractMatrices1D<T>(
            params List<T>[] matrices) where T:
             struct, IComparable, IComparable<T>, IEquatable<T>
        {
            List<T> result = new List<T>();
            int cols = matrices[0].Count;

            // Checks if matrix is valid
            // Check if matrices are of the same size
            foreach (var matrix in matrices)
            {
                if (matrix == null || matrices.Length < 2)
                {
                    throw new ArgumentNullException("At least two matrices is required.");
                }
                
                if (matrix.Count != cols)
                {
                    throw new ArgumentNullException("Enter matrices with same size");
                }
            }

            // Logic perform substraction on two matrices
            List<T> matrix1 = matrices[0];
            List<T> matrix2 = matrices[1];
            for (int i=0; i<cols; i++)
                {
                    T subtracted = (dynamic)matrix1[i] - matrix2[i];
                    result.Add(subtracted);
                }
            return result;
        }

        public static void PrintList<T>(List<T> matrix1D)where T:
             struct, IComparable, IComparable<T>, IEquatable<T>
        {
            foreach (var element in matrix1D)
            {
                Console.Write ($"{element} ");
            }
            Console.WriteLine();

        }
    }
}