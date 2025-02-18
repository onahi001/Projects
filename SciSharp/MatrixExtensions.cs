using System;
using System.Collections;
using System.Collections.Generic;
using MatrixCollection;

namespace MatrixCollection
{
    public static class MatrixExtensions
    {
        /// <summary>
        /// Perform Gauss-Jordan elimination on
        /// matrices.
        /// </summary>
        /// <param name="matrix">
        /// An input array of class Matrix<T>
        /// which represents the variables of 
        /// simultaneous equations.
        /// </param>
        /// <param name="imatrix">
        /// An input 1D array of class Matrix<double>
        /// which represents the constants of
        /// simultaneous equations.
        /// </param>
        /// <returns>
        /// A new matrix of class Matric<double>
        /// </returns>
        /// <example>
        /// finding the solution of a simultaneous equation
        /// x + y = 5
        /// 2x + y = 6
        /// <code>
        /// Matrix<double> equation = new Matrix<double>();
        /// equation.AddRow(new List<double>{1, 1});
        /// equation.AddRow(new List<double>{2, 1});
        /// 
        /// Matrix<double> constantValues = new Matrix<double>();
        /// constantValues.AddRow(new List<double>{5, 6});
        /// 
        /// List<double> solution = GaussJordanElimination(equation, constantValues);
        /// </code>
        /// </example>
        public static Matrix<double> GaussJordanMatrix(
            Matrix<double> matrix, Matrix<double> imatrix) //where T:
             //struct, IComparable, IComparable<T>, IEquatable<T>
        {
            // augmenting the matrices
            Matrix<double> augEqn = MatrixOperationsTwo.ColumnStack2D(matrix, imatrix);

            // creating solution adn iteration variables
            int noRow = augEqn.GetRow();
            int noCol = augEqn.GetCol();

            // checking and pivoting elements where necessary
            for (int i = 0; i < noRow; i++)
            {
                if (augEqn[i][i] == 0)
                {
                    List<double> temp = new List<double>();
                    for (int k=0; k<noCol; k++)
                    {
                        if (augEqn[k][i]==0)
                        {
                            temp = (dynamic)augEqn[i].ToList();
                            augEqn[i] = augEqn[k];
                            augEqn[k] = (dynamic)temp;
                            Console.WriteLine(temp);
                        }
                    }
                    Console.WriteLine("pivoting done");
                }

                // carrying out gauss-jordan elimination
                double rowDivisor = augEqn[i][i];
                for (int k=0; k<augEqn[i].Count; k++)
                {
                    augEqn[i][k] = augEqn[i][k]/rowDivisor;
                }
                for (int j=0; j<noRow; j++)
                {
                    if ( i != j)
                    {
                        double multiplier = (dynamic)augEqn[j][i]/augEqn[i][i];
                        List<double> result = MatrixOperationsTwo.ScalarMultiply1D(augEqn[i], multiplier);
                        augEqn[j] = MatrixOperations.SubtractMatrices1D([augEqn[j], result]);
                    }
                }
            }
            return augEqn;
        }

        
        /// <summary>
        /// Perform Gauss-Jordan elimination on
        /// matrices.
        /// </summary>
        /// <param name="matrix">
        /// An input array of class Matrix<T>
        /// which represents the variables of 
        /// simultaneous equations.
        /// </param>
        /// <param name="matrix1D">
        /// An input List of class List<T>
        /// which represents the constants of
        /// simultaneous equations.
        /// </param>
        /// <returns>
        /// A list of solutions
        /// </returns>
        /// <example>
        /// finding the solution of a simultaneous equation
        /// x + y = 5
        /// 2x + y = 6
        /// <code>
        /// Matrix<double> equation = new Matrix<double>();
        /// equation.AddRow(new List<double>{1, 1});
        /// equation.AddRow(new List<double>{2, 1});
        /// List<double> constantValues = new List<double>{5,6};
        /// 
        /// List<double> solution = GaussJordanElimination(equation, constantValues);
        /// </code>
        /// </example>
        public static List<double> GaussJordanElimination(
            Matrix<double> matrix, List<double> matrix1D)
        {
            // augmenting the matrices
            Matrix<double> augEqn = MatrixOperationsTwo.ColumnStack1D(matrix, matrix1D);

            // creating solution adn iteration variables
            int noRow = augEqn.GetRow();
            int noCol = augEqn.GetCol();
            List<double> soln = new List<double>(noRow);

            // checking and pivoting elements where necessary
            for (int i = 0; i < noRow; i++)
            {
                if (augEqn[i][i] == 0)
                {
                    List<double> temp = new List<double>();
                    for (int k=0; k<noCol; k++)
                    {
                        if (augEqn[k][i]==0)
                        {
                            temp = (dynamic)augEqn[i].ToList();
                            augEqn[i] = augEqn[k];
                            augEqn[k] = (dynamic)temp;
                            Console.WriteLine(temp);
                        }
                    }
                    Console.WriteLine("pivoting done");
                }

                // carrying out gauss-jordan elimination
                double rowDivisor = augEqn[i][i];
                for (int k=0; k<augEqn[i].Count; k++)
                {
                    augEqn[i][k] = augEqn[i][k]/rowDivisor;
                }
                for (int j=0; j<noRow; j++)
                {
                    if ( i != j)
                    {
                        double multiplier = (dynamic)augEqn[j][i]/augEqn[i][i];
                        List<double> result = MatrixOperationsTwo.ScalarMultiply1D(augEqn[i], multiplier);
                        augEqn[j] = MatrixOperations.SubtractMatrices1D([augEqn[j], result]);
                    }
                }
            }
            for (int i=0; i<noRow; i++)
            {
                soln.Add(augEqn[i, noCol-1]);
            }
            return soln;
        }

        /// <summary>
        ///  finds the inverse of a matrix using Gauss Jordan
        /// Method. The matrix must be a square matrix
        /// </summary>
        /// <param name="matrix">a matrix of type Matrix<double></param>
        /// <returns>
        /// an inverse matrix of type Matrix<double>
        /// </returns>
        /// <example>
        /// inverse of 1 2
        ///            3 4
        /// <code>
        /// Matrix<double> equation = new Matrix<double>();
        /// equation.AddRow(new List<double>{1, 2});
        /// equation.AddRow(new List<double>{3, 4});
        /// 
        /// Matrix<double> inverseMatrix = GaussJordanInv(equation);
        public static Matrix<double> GaussJordanInv(Matrix<double> matrix)
        {
            int noRow = matrix.GetRow();
            int noCol = matrix.GetCol();
            if (matrix == null || noRow != noCol)
            {
                throw new ArgumentException("Enter a square valid matrix");
            }
            Matrix<double> identityMatrix = new Matrix<double>(noRow, noCol);
            Matrix<double> inverseMatrix = new Matrix<double>();

            // creating an identity matrix
            for (int i=0; i<noRow; i++)
            {
                identityMatrix[i][i] = 1.0;
            }

            // carrying out gauss-jordan elimination
            Matrix<double> augEqn = GaussJordanMatrix(matrix, identityMatrix);
            int augCol = augEqn.GetCol(); // augented matrix noCol

            // seperating inverse of the matrix from the augmented matrix
            for (int i=0; i<noRow; i++)
            {
                inverseMatrix.AddRow(augEqn[i,noRow, augCol-1]);
            }

            return inverseMatrix;
        }
    }
}