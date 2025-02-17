using System;
using System.Collections;
using System.Collections.Generic;

// writting a class that models matrix
namespace MatrixCollectionPrototype

{
    public class Matrix<T> : IEnumerable<T>
        where T: struct, IComparable, IComparable<T>, IEquatable<T>
    {
        protected List<List<T>> data;
        private int NOrows { get; set; }
        private int NOcolumns { get; set; }

        // Building the Constructor
        public Matrix()
        {
            // initialising number of columans and rows
            this.NOrows = 0;
            this.NOcolumns = 0;

            // initialising the matrix
            data = new List<List<T>>();
        }

        // method to access Rows
        public int GetRow()
        {
            return NOrows;
        }
        // method to access Rows
        public int GetCol()
        {
            return NOcolumns;
        }

        // Method for adding rows to a matrix
        public void AddRow (List<T> newRow)
        {
            data.Add(newRow);
            this.NOrows++;
            this.NOcolumns = newRow.Count;
        }

        // Method for Row indexing through the matrix
        public List<T> this[int row]
        {
            get => data[row];
            set => data[row] = value;
        }
        // Method for Element indexing through the matrix
        public T this[int row, int col]
        {
            get => data[row][col];
            set => data[row][col]=value;
        }

        // Implementing IEnumerable
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var dataRow in data)
            {
                foreach(var element in dataRow)
                {
                    yield return element;
                }
            }
        }

        // Explicit implementation of IEnumerable
        // For Non-Generic Method
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Print the matrix to console
        public override String ToString ()
        {
            foreach (var row in data)
            {
                foreach(T element in row)
                {
                    Console.Write($"    {element}");
                }
                Console.WriteLine();
            }
            return "";
        }

        // Adds two matrices together
        public static Matrix<T> AddMatrices(Matrix<T>[] matrices)
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

        // Method to carryout scalar multiplication
        public Matrix<double>ScalarMultiply(double scalar)
        {
            Matrix<double> result = new Matrix<double>();
            for(int i=0; i<this.NOrows; i++)
            {
                List<double>resultRow = new List<double>();
                for(int j=0; j<NOcolumns; j++)
                {
                    double res = (dynamic)data[i][j]*scalar;
                    resultRow.Add(res);
                }
                result.AddRow(resultRow);
            }
            return result;
        }

        // Vertical stacking of a matrix with a vector
        public Matrix<T> ColumnStack(List<T> matrix1D)
        {
            // checking input dimensions
            if(matrix1D == null || matrix1D.Count != NOrows)
            {
                throw new ArgumentException("The input columne must have same of length with matrix");
            }
            
            // creates a new matrix for the result
            // logic to carryout stacking
            Matrix<T> result = new Matrix<T>();

            for ( int i = 0; i<NOrows; i++)
            {
                List<T> resultRow = new List<T>();
                resultRow = data[i];
                resultRow.Add(matrix1D[i]);
                result.AddRow(resultRow);
                Console.WriteLine(resultRow.Count);
            }

            return result;
        }
    }
}