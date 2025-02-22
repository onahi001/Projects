using System;
using System.Collections;
using System.Collections.Generic;
using MatrixCollection;

namespace MatrixCollection
{
    ///<summary>
    /// A static class providing operations on matrices.
    /// </summary>
    
    public static class MatrixOperationsTwo
    {
        /// <summary>
        /// Performs vertical stacking of a matrix 
        /// with a column vector. The method appends 
        /// the given 1D vector as an additional column to the matrix.
        /// </summary>
        /// <typeparam name="T">A numeric dataype.</typeparam>
        /// <param name="matrix">The input matrix.</param>
        /// <param name="matrix1D">
        /// A 1D list representing the column to be appended.
        /// It is a List<T> datatype
        /// </param>
        /// <returns>A new matrix with the additional column.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the column vector's length does not match 
        /// the number of rows in the matrix.
        /// </exception>
        /// <example>
        /// Example of stacking a column vector to a matrix:
        /// <code>
        /// List<int> column = new List<int> { 5, 6 };
        /// Matrix<int> result = MatrixOperationsTwo.ColumnStack(matrix, column);
        /// </code>
        /// </example>
        public static Matrix<T> ColumnStack1D<T> (
            Matrix<T> matrix, List<T> matrix1D) where T:
             struct, IComparable, IComparable<T>, IEquatable<T>
        {
            // checking input dimensions
            int row = matrix.GetRow();
            if(matrix1D == null || matrix1D.Count != row)
            {
                throw new ArgumentException("The input column must have same of length with matrix");
            }
            
            // creates a new matrix for the result
            // logic to carryout stacking
            Matrix<T> result = new Matrix<T>();
            List<List<T>> Data = matrix.Data();

            for ( int i = 0; i<row; i++)
            {
                List<T> resultRow = new List<T>();
                resultRow = Data[i].ToList();
                resultRow.Add(matrix1D[i]);
                result.AddRow(resultRow);
            }

            return result;
        }

        /// <summary>
        /// Performs vertical stacking of a matrix 
        /// with another matrix. 
        /// </summary>
        /// <typeparam name="T">A numeric dataype.</typeparam>
        /// <param name="matrix">
        /// The first input matrix of type Matrix.
        /// </param>
        /// <param name="matrix2D">
        /// The second input matrix of type Matrix.
        /// </param>
        /// <returns>A new augmented matrix of type Matrix
        /// .</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the column vector's length does not match 
        /// the number of rows in the matrix.
        /// </exception>
        /// <example>
        /// Example of stacking a matrix to another matrix:
        /// <code>
        /// Matrix<int> firstMatrix = new List<int> { 5, 6 };
        /// Matrix<int> secondMatrix = new List<int> { 5, 6 };
        /// Matrix<int> result = MatrixOperationsTwo.ColumnStack(firstMatrix, secondMatrix);
        /// </code>
        /// </example>
        public static Matrix<T> ColumnStack2D<T> (
            Matrix<T> matrix, Matrix<T> matrix2D) where T:
             struct, IComparable, IComparable<T>, IEquatable<T>
        {
            // checking input dimensions
            int row = matrix.GetRow();
            int noCol = matrix2D.GetCol();
            if(matrix2D == null || matrix2D.GetRow() != row)
            {
                throw new ArgumentException("The input column must have same of length with matrix");
            }
            
            // creates a new matrix for the result
            // logic to carryout stacking
            Matrix<T> result = new Matrix<T>();
            List<List<T>> Data1 = matrix.Data();
            List<List<T>> Data2 = matrix2D.Data();

            for ( int i = 0; i<row; i++)
            {
                List<T> resultRow = new List<T>();
                resultRow = Data1[i].ToList();
                for (int j=0; j<noCol; j++)
                    resultRow.Add(matrix2D[i][j]);
                result.AddRow(resultRow);
            }

            return result;
        }


        /// <summary>
        /// Performs scalar multiplication on a 2D matrix. 
        /// The method multiplies each element of the matrix
        /// with the scalar value.
        /// </summary>
        /// <typeparam name="T">A numeric dataype.</typeparam>
        /// <param name="matrix">
        ///The input matrix.</param>
        /// <param name="scalar">
        /// A number to multiply the matrix
        /// </param>
        /// <returns>
        /// A new matrix with the result and double as 
        /// the datatype.
        /// </returns>
        /// <example>
        /// Example of multiplying matrix with scalar
        /// <code>
        /// double scalar = 3.5;
        /// Matrix<double> result = MatrixOperationsTwo.ScalarMultiply2D(matrix, scalar);
        /// </code>
        /// </example>
        public static Matrix<double> ScalarMultiply2D<T>(
            Matrix<T> matrix, double scalar) where T:struct,
             IComparable, IComparable<T>, IEquatable<T>
        {
            Matrix<double> result = new Matrix<double>();
            List<List<T>> Data = matrix.Data();
            for(int i=0; i<matrix.GetRow(); i++)
            {
                List<double>resultRow = new List<double>();
                for(int j=0; j<matrix.GetCol(); j++)
                {
                    double res = (dynamic)Data[i][j]*scalar;
                    resultRow.Add(res);
                }
                result.AddRow(resultRow);
            }
            return result;
        }

        /// <summary>
        /// Carryout scalar multiplication on a 1 dimensional
        /// matrix of the List<T> data type. It is similar to that
        /// of a 2 dimensional matrix
        /// </summary>
        /// <param name="matrix1D">a matrix of type Matrix</param>
        /// <param name="scalar">a scalar of type double</param>
        /// <typeparam name="T">a numeric datatype</typeparam>
        /// /// <returns>
        /// A new List with the result and double as 
        /// the datatype.
        /// </returns>
        /// <example>
        /// Example of multiplying a vector with scalar
        /// <code>
        /// double scalar = 3.5;
        /// List<double> vector = new List<double>{2,3,4};
        /// Matrix<double> result = MatrixOperationsTwo.ScalarMultiply1D(vector, scalar);
        /// </code>
        /// </example>
        public static List<double> ScalarMultiply1D<T>(
            List<T> matrix1D, double scalar) where T:struct,
             IComparable, IComparable<T>, IEquatable<T>
        {
            if (matrix1D == null)
            {
                throw new ArgumentNullException("Enter some numbers");
            }
            List<double> result = new List<double>();
            for(int i=0; i<matrix1D.Count; i++)
            {
                double res = (dynamic)matrix1D[i]*scalar;
                result.Add(res);
            }
            return result;
        }

        // computes some arithemetics computation
        static double Compute(double num1, char op, double num2)
        {
            return op switch
            {
                '+' => num1 + num2,
                '-' => num1 - num2,
                '*' => num1 * num2,
                '/' => num2 != 0 ? num1 / num2 : throw new DivideByZeroException("Cannot divide by zero"),
                '%' => num2 != 0 ? num1 % num2 : throw new DivideByZeroException("Cannot calculate modulus with zero"),
                _ => throw new ArgumentException("Invalid operator")
            };
        }

        /// <summary>
        /// Performing operation between List elements and
        /// summing all the values
        /// </summary>
        /// <param name="matrix1D">first List datatype</param>
        /// <param name="op">a basic mathematical operator.</param>
        /// <param name="matrix1D2">second List datatype</param>
        /// <typeparam name="T">numeric datatype</typeparam>
        /// <return>a number of double datatype</return>
        public static double sumElement<T>(
            List<T> matrix1D, char op, List<T> matrix1D2) where T:struct,
             IComparable, IComparable<T>, IEquatable<T>
        {
            double ElementSum = 0;
            for(int i=0; i<matrix1D.Count; i++)
            {
                ElementSum += Convert.ToDouble(matrix1D[i]) * Convert.ToDouble(matrix1D2[i]);
            }
            return ElementSum;
        }
    }
}