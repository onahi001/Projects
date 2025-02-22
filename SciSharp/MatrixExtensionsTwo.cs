using System;
using System.Collections;
using System.Collections.Generic;
using MatrixCollection;

namespace MatrixCollection
{
    /// <summary>
    /// A class with methods to carryout LU
    /// Decomposition operations
    /// </summary>
    public static class MatrixExtensionsTwo
    {
        /// <summary>
        /// decomposes a matrix into L, U and P
        /// where P is the pivoting factor.
        /// </summary>
        /// <param name="augEqn">
        /// a matrix of equation
        /// </param>
        /// <returns>
        /// a tuple containing 3 matrices of type Matrix<double>
        /// it returns in the order L, U and P 
        /// </returns>
        public static (Matrix<double>L, Matrix<double>U, Matrix<double>P) LUCroutDecomposition (
            Matrix<double> augEqn)
        {
            int norow = augEqn.GetRow();
            int nocol = augEqn.GetCol();

            Matrix<double> L = new Matrix<double>(norow, nocol);
            Matrix<double> U = new Matrix<double>(norow, nocol);

            // carrying out pivoting
            Matrix<double> P = new Matrix<double>(norow, nocol);
            for (int i=0; i<norow; i++)
            {
                P[i][i] = 1;
            }

            for (int m=0; m<norow-1; m++)
            {
                if (augEqn[m][m] == 0)
                {
                    for(int k=m+1; k<nocol; k++)
                    {
                        if (augEqn[k][m] !=0)
                        {
                            List<double> mTemp = new List<double>(augEqn[m]);
                            augEqn[m] = augEqn[k];
                            augEqn[k] = mTemp;
                            List<double> pTemp = new List<double>(P[m]);
                            P[m] = P[k];
                            P[k] = pTemp;
                            break;
                        }
                    }
                }
            }

            // getting the first row of the L column
            for(int i=0; i<norow; i++)
            {
                L[i][0] = augEqn[i][0];
            }

            // getting the diagonal of U
            for(int i=0; i<norow; i++)
            {
                U[i][i] = 1;
            }

            // calculating the first row of the matrix U
            for(int i=0; i<nocol; i++)
            {
                U[0][i] = augEqn[0][i]/L[0][0];
            }

            // calculating other rows of the matrix L and U
            for(int i=1; i<norow; i++)
            {
                for(int j=1; j<i+1; j++)
                {
                    List<double> temp1 = L[i, 0, j];
                    List<double> temp2 = U.columnSlice(j, 0, j);
                    double multiplyElementSum = MatrixOperationsTwo.sumElement(temp1, '*', temp2);
                    L[i][j] = (augEqn[i][j] - multiplyElementSum);
                }

                for(int j=i+1; j<norow; j++)
                {
                    List<double> temp1 = L[i, 0, i];
                    List<double> temp2 = U.columnSlice(j, 0, i);
                    double multiplyElementSum2 = MatrixOperationsTwo.sumElement(temp1, '*', temp2);
                    U[i][j] = (augEqn[i][j] - (multiplyElementSum2))/L[i,i];
                    
                }
            }
            return (L, U, P);
        }
        
        // carrying forward substitution on the matrix
        /// <summary>
        ///  Carries out forward substitution on the L matrix
        /// </summary>
        /// <param name="L">
        /// a decomposed L matrix of type Matrix<double>
        /// </param>
        /// <param name="P">
        /// the pivoting P factor matrix of type Matrix<double>
        /// </param>
        /// <param name="constant">
        /// the constants of the equation of type List<double>
        /// </param>
        /// <returns>
        /// a List of solutions of type List<double>
        /// </returns>
        public static List<double> forwardSub(
            Matrix<double> L, Matrix<double> P, List<double> constant)
        {
            int NOrows = L.GetRow();
            int NOcols = L.GetCol();
            List<double> newConst = P.DotProduct1D(constant);
            List<double> result = new List<double>(NOrows);
            result.Add(newConst[0]/L[0][0]);

            for(int i=1; i<NOrows; i++)
            {
                double Var = 0.00;
                for(int j=0; j<i; j++)
                {
                    Var += L[i][j] * result[j];
                }
                result.Add((newConst[i] - Var)/L[i][i]);
            }
            return result;
        }

        /// <summary>
        ///  carrying out backward substitution on the matrix
        /// </summary>
        /// <param name="U">
        /// a decomposed U matrix of type Matrix<double>
        /// </param>
        /// <param name="LSubstituted">
        /// the solution from backward substitution into L Matrix
        /// of type List<double>
        /// </param>
        /// <returns>
        /// a List of solutions for equatin of type List<double>
        /// </returns>
        public static List<double> backwardSub(
            Matrix<double> U, List<double> LSubstituted)
        {
            int NOrow = U.GetRow();
            double[] solution = new double[NOrow];
            solution[NOrow-1] = LSubstituted[NOrow-1];

            for(int i=NOrow-2; i>-1; i--)
            {
                double Var = 0.0;
                for(int j=i+1; j<NOrow; j++)
                {
                    Var += U[i, j]*solution[j];
                }
                solution[i] = LSubstituted[i] - Var;
            }
            return solution.ToList();
        }

        /// <summary>
        ///  Finding the inverse of a Matrix using LU Decomposition
        /// Method. The matrix must be a square matrix
        /// </summary>
        /// <param name="eqnmatrix">a matrix of type Matrix<double></param>
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
        /// Matrix<double> inverseMatrix =LUMatrixInv(equation);
        public static Matrix<double> LUMatrixInv(Matrix<double> eqnMatrix)
        {
            int NOrows = eqnMatrix.GetRow();
            int NOcols = eqnMatrix.GetCol();

            if(NOrows == 0)
            {
                throw new ArgumentNullException(
                    "Enter a valid matrix");
            }   
            if(NOrows != NOcols)
            {
                throw new ArgumentException(
                    "Enter a Square Matrix");
            }
            Matrix<double> identityMatrix = new Matrix<double>(NOrows, NOcols);
            Matrix<double> invMatrix = new Matrix<double>(NOrows, NOcols);

            // creating an identity matrix
            for(int i=0; i<NOrows; i++)
            {
                identityMatrix[i, i] = 1;
            }

            // carrying out LU Decomposition
            (Matrix<double>L, Matrix<double>U, Matrix<double>P) = LUCroutDecomposition(eqnMatrix);
            
            for(int i=0; i<NOrows; i++)
            {
                List<double> ICol = new List<double>(identityMatrix.columnSlice(i, 0, NOrows-1));
                List<double> LSubstituted = forwardSub(L, P, ICol);
                List<double> InvRow = backwardSub(U, LSubstituted);
                for(int j=0; j<InvRow.Count; j++)
                {
                    invMatrix[j,i] = InvRow[j];
                }
            }
            return invMatrix;
        }
    }
}