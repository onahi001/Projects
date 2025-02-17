using System;
using System.Collections;
using System.Collections.Generic;

namespace MatrixCollection
{
    public class MatrixCollectionInterfaces
    {
        public interface IMatrix<T> where T:
             struct, IComparable, IComparable<T>, IEquatable<T>
        {
            Matrix<T> AddMatrices (Matrix<T>[] matrices); 
            Matrix<T> SubstractMatrices (Matrix<T>[] matrices);
        }
    }
}