using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

namespace Matrices
{
    public class Matrix<T> : IEnumerable<T> 
        where T:struct, IComparable, IComparable<T>, IEquatable<T>
    {
        private List<List<T>> data;
        public int rows { get; set;}
        public int columns { get; set;}

        // building constructor
        public Matrix()
        {
            this.rows = 0;
            this.columns = 0;

            // Initialising the constructor
            data = new List<List<T>>();
        }

        //Add element to matrix
        public void AddElement(List<T> newElement)
        {
            data.Add(newElement);
            this.rows ++;
            //this.columns = newElement.Count;
        }

        // row indexer
        public List<T> this [int row]
        {
            get => data[row];
            set => data[row] = value;
        }
        // element indexer
        public T this [int row, int col]
        {
            get => data[row][col];
            set => data[row][col]=value;
        }


        // implement IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var dataRow in data)
            {
                foreach (var element in dataRow)
                {
                    yield return element;
                }
            }
        }

        // Explicit IEnumerator Implementation for non-generic IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Stacking the matrices by column
        private static T[,] ColumnStack(List<List<T>>matrix1, List<T> matrix2)
        {
            T[,] stackedMatrix = new T[matrix1.Count, matrix1.Count + 1];

            for ( int i = 0; i<matrix1.Count; i++)
            {
                for (int j =0; j<matrix1.Count; j++)
                {
                    stackedMatrix[i,j] = matrix1[i][j];
                }
                stackedMatrix[i, matrix1.Count] = matrix2[i];
            }

            return stackedMatrix;
        }


        // Guss jordan method for matrices
        public double [] GaussJordanElimination(
            List<T> constant)
        {
            // augmenting the matrices
            T[,] aug_eqn = ColumnStack(data, constant);

            // creating solution adn iteration variables
            int no_row = aug_eqn.GetLength(1);
            int no_col = aug_eqn.GetLength(1);
            double[] soln = new double[no_row];
            // checking and pivoting elements where necessary
            for (int i = 0; i < no_row; i++)
            {
                if (EqualityComparer<T>.Default.Equals(
                    aug_eqn[i,i], default(T)))
                {
                    for (int k=0; k<no_col; k++)
                    {
                        if (!EqualityComparer<T>.Default.Equals(
                    aug_eqn[k,i], default(T)))
                    {
                        T[] temp = (T[])aug_eqn[i].Clone();
                    }
                    }
                }
            }
            return soln;
        }
    }
}