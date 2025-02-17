using System;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// A libarary that models a matrix
/// with methods to carry out operations
/// </summary>
namespace MatrixCollection

{
    /// <summary>
    /// This models a matrix data type
    /// Uses List generic for the implementation
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements stored in the Matrix.
    /// </typeparam>
    /// <remarks>
    /// This class basic eunumeration for type T.
    /// The type T is restricted to numeric types
    /// </remarks>
    public class Matrix<T> : IEnumerable<T>
        where T: struct, IComparable, IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// The fields for the classes
        /// </summary>
        protected List<List<T>> data;
        private int NOrows { get; set; }
        private int NOcolumns { get; set; }

        /// <summary>
        /// This is a constructor to initialise the class
        /// It creates an empty matrix
        /// The class has no data after initialisation.
        /// Data can be added using a method
        /// </summary>
        /// <example>
        /// Example of creating an empty matrix for integers:
        /// <code>
        /// Matrix<int> matrix = new Matrix<int>();
        /// </code>
        /// </example>
        public Matrix()
        {
            // initialising number of columans and rows
            this.NOrows = 0;
            this.NOcolumns = 0;

            // initialising the matrix
            data = new List<List<T>>();
        }

        // Building another Constructor
        /// <summary>
        /// This is a constructor to initiate the class
        /// It initialises the matrix with default values
        /// by using an array and converting to List
        /// </summary>
        /// <example>
        /// Example of creating a 3 x 3 matrix for integers:
        /// <code>
        /// Matrix<int> matrix = new Matrix<int>(3,3);
        /// </code>
        /// </example>
        public Matrix(int rows, int columns)
        {
            data = new List<List<T>>();
            // initialising number of columns and rows
            for (int i=0; i<rows; i++)
            {
                List<T> matrixRow = new List<T>(new T[columns]);
                data.Add(matrixRow);
                this.NOrows++;
            }
            
            NOcolumns = columns;
        }

        /// <summary>
        /// Accessing the fields of the Matrix 
        /// data types.
        /// GetRow accesses NOrows
        /// GetCol accesses NOcolumns
        /// Data accesses the data field
        /// </summary>
        /// <returns>
        /// returns value of the fields
        /// </returns>
        public int GetRow()
        {
            return NOrows;
        }

        public int GetCol()
        {
            return NOcolumns;
        }

        public List<List<T>> Data()
        {
            return data;
        }

        /// <summary>
        /// Method for adding rows to a Matrix
        /// </summary>
        /// <param name="newRow">
        /// It is a List<T> of numeric values of type T
        /// It represents row of the Matrix.
        /// </param>
        public void AddRow (List<T> newRow)
        {
            data.Add(newRow);
            this.NOrows++;
            this.NOcolumns = newRow.Count;
        }

        /// <summary>
        /// Method for accessing parts of the
        /// encapsulated data using indexing.
        /// </summary>
        /// <value>
        /// The value is of type of List<T> stored in the
        /// row of the matrix
        /// </value>
        public List<T> this[int row]
        {
            get => data[row];
            set => data[row] = value;
        }
        /// <summary>
        /// Method for accessing parts of the
        /// encapsulated data using indexing.
        /// </summary>
        /// <value>
        /// The value is of type T stored in the Matrix
        /// </value>
        public T this[int row, int col]
        {
            get => data[row][col];
            set => data[row][col]=value;
        }

        public List<T> this[int row, int start, int end]
        {
            get
            {
                if (row<0 || row>=NOrows)
                {
                    throw new IndexOutOfRangeException(
                        "Row index is out of bounds.");
                }
                if (start<0 || end>=NOcolumns || start>end)
                {
                    throw new ArgumentOutOfRangeException(
                        "Invalid range for slicing");
                }
                return data[row].GetRange(start, end-start+1);
            }
        }

        /// <summary>
        /// Implementing the Enumerator for 
        /// iterating through the data field of
        /// the Matrix datatype
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Explicit implementation of IEnumerable
        /// For Non-Generic Method.
        /// </summary>
        /// <returns> 
        /// calls the Generator and returns value of the code
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Print the matrix to console
        /// <summary>
        /// prints the Matrix<T> type to the console
        /// in a custom traditional matrix form
        /// by overriding the default ToString method
        /// </summary>
        /// <returns>
        /// It returns an empty string
        /// </returns>
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

        public Matrix<double> DotProduct(Matrix<double> matrix2)
        {
            if (NOcolumns != matrix2.GetRow())
                throw new ArgumentException (
                    "Row of first must equal col of");
            
            Matrix<double> result = new Matrix<double>();
            int matrix2Row = matrix2.GetRow();

            for(int i=0; i<NOrows; i++)
            {
                List<double> newRow = new List<double>();
                for(int j=0; j<matrix2.GetCol(); j++)
                {
                    double res = 0.0;
                    for (int k=0; k<NOcolumns; k++)
                    {
                        res += (dynamic)data[i][k] * matrix2[k][j];
                    }
                    newRow.Add(res);  
                }
                result.AddRow(newRow);
            }
            return result;
        }

        public List<double> DotProduct1D(List<double> matrix1D)
        {
            if (NOcolumns != matrix1D.Count())
                throw new ArgumentException (
                    "Row of first must equal col of");
            
            List<double> result = new List<double>();
            int matrix1DRow = matrix1D.Count();

            for(int i=0; i<NOrows; i++)
            {
                double res = 0.0;
                for(int j=0; j<matrix1D.Count(); j++)
                {
                    
                    res += (dynamic)data[i][j] * matrix1D[j];
                }
                result.Add(res);
            }
            return result;
        }

        public List<T> columnSlice(int column, int start, int end)
        {
            
            if (column<0 || column>=NOcolumns)
            {
                throw new IndexOutOfRangeException(
                    "Column index is out of bounds.");
            }
            if (start<0 || end>=NOrows || start>end)
            {
                throw new ArgumentOutOfRangeException(
                    "Invalid range for slicing");
            }
            List<T> columnValues = new List<T>();
            
            for(int i=start;  i<end+1; i++)
            {
                columnValues.Add(data[i][column]);
            }
            return columnValues;
        }
        
    }
}