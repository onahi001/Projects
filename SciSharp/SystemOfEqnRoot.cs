using System;
using System.Diagnostics;
using System.Collections.Generic;   
using System.Linq;
using MatrixCollection;

namespace SystemOfEqnRoot
{
    /// <summary>
    ///  This class represents systems of equations
    /// and some operations that can be performed on the equations.
    /// It was designed for solving non linear systems of equations
    /// </summary>
    public class SystemsOfEqn
    {
        // defining some properties

        public delegate double Function(List<double> variables);
        public List<double> start { get; set; }
        public int n { get; set;}
        public List<Function> equations { get; set;}

        // Constructor accepting initial values and equations
        // creating class constructor
        /// <summary>
        /// Constructor accepting initial values and equations
        /// creating class constructor
        /// </summary>
        /// <param name="start">the starting values or initial guess</param>
        /// <param name="equations">
        /// list of equations of Function type each equal to zero
        /// </param>
        /// <example> creating a system of equation for 2 variables
        /// x^2 + y^2 = 0
        /// x + y = 0
        /// <code>
        /// List<SystemsOfEqn.Function>equationList = new List<SystemsOfEqn.Function>
        /// {
        ///    x=> x[0]*x[0] + x[1]*x[1]
        ///    x=> x[0] + x[1]
        /// }
        /// List<double> startValues = new List<double>{1,2}
        /// </code>
        /// </example>
        public SystemsOfEqn(List<double> start, List<Function> equations)
        {
            this.start = start;
            this.equations = equations;
        }

        // Access start
        public List<double> GetStart()
        {
            return start;
        }


        // Evaluating Functions
        public List<double> Evaluate(List<double> values)
        {
            if (values.Count != equations.Count)
                throw new ArgumentException(
                    "Number of values must match the number of equations");

            List<double> results = new List<double>();
            foreach(var eq in equations)
            {
                results.Add(eq(values));
            }
            return results;
        }

        
        
        // Defining the Jacobian Class
        /// <summary>
        /// calculates the jacobian of a matrix
        /// </summary>
        /// <param name="h">pertubation value</param>
        /// <returns>
        /// returns a jacobian matrix and solution of the 
        /// equations substituting the starting values
        /// </returns>
        public (Matrix<double>, List<double>) Jacobi(double h=1e-6)
        {
            Matrix<double> jacobiMat = new Matrix<double>();
            List<double> funcMat = Evaluate(start);


            for(int e=0; e<equations.Count;  e++)
            {
                Function fun = equations[e];
                List<double> partialDerivative = 
                    new List<double>();
               
                for(int i=0; i<equations.Count; i++)
                {
                    // duplicating the starting value for pertubation
                    List<double> perturbedPoint = [..start];
                    perturbedPoint[i] += h;
                    double partial_dx = (
                        fun(perturbedPoint) -
                            funcMat[e]) / h;
                    partialDerivative.Add(partial_dx);
                }
                jacobiMat.AddRow(partialDerivative);
            }

            for(int i=0; i<funcMat.Count; i++)
            {
                funcMat[i] = funcMat[i]*-1;
            }

            return (jacobiMat, funcMat);
        }

        /// <summary>
        /// Calculates the roots of a systems of equations
        /// using Gauss-Jordan Elimination method.
        /// prints the solution of the system of Non-Linear
        /// equations to the console
        /// </summary>
        /// <param name="start">List of starting values</param>
        /// <param name="equations">List of Equations of type Function</param>
        /// <param name="tol">tolerance for the absolute error</param>
        /// <param name="maxIter">maximum iterations</param>
        /// <typeparam name="Function">
        /// a delegate that represents equations
        /// </typeparam>
        /// <example>
        /// <code>
        /// List<SystemsOfEqn.Function>equationList = new List<SystemsOfEqn.Function>
        /// {
        ///    x=> x[0]*x[0] + x[1]*x[1]
        ///    x=> x[0] + x[1]
        /// }
        /// List<double> startValues = new List<double>{1,2}
        /// 
        /// NewtonRaphsonGauss(startValues, equations, 1e-3, 10)
        /// </code>
        /// </example>
        public static void NewtonRaphsonGauss(List<double> start,
         List<Function> equations, double tol=1e-6, double maxIter=5)
        {
            //Executing the Newton Raphson
            int count =1;
            while (count<maxIter)
            {
                SystemsOfEqn Eqns = new SystemsOfEqn(start, equations);
                (Matrix<double>EqnJab, List<double>ConstMat)  = Eqns.Jacobi();
                List<double> soln = MatrixExtensions.GaussJordanElimination(EqnJab, ConstMat);

                List<double> values = new List<double>();
                List<double> errorList = new List<double>();
                for(int i=0; i<soln.Count; i++)
                {
                    double xValues  = start[i] + soln[i];
                    values.Add(xValues);
                    double error = Math.Abs((xValues-start[i])/start[i]);
                    errorList.Add(error);
                }

                if (errorList.Sum() < tol) 
                {
                    MatrixOperations.PrintList(values);
                    Console.WriteLine($"Number of iterations = {count}");
                    break;
                }
                else
                {
                    if (count == maxIter)
                    {
                        Console.WriteLine($"Newton Raphson unsuccessful at Max Iteration");
                        MatrixOperations.PrintList(values);
                    }
                    start = new List<double>(values);
                    count +=1;
                }
            }
        }

        /// <summary>
        /// Calculates the roots of a systems of equations
        /// using Crout LU decomposition method.
        /// prints the solution of the system of Non-Linear
        /// equations to the console
        /// </summary>
        /// <param name="start">List of starting values</param>
        /// <param name="equations">List of Equations of type Function</param>
        /// <param name="tol">tolerance for the absolute error</param>
        /// <param name="maxIter">maximum iterations</param>
        /// <typeparam name="Function">
        /// a delegate that represents equations
        /// </typeparam>
        /// <example>
        /// <code>
        /// List<SystemsOfEqn.Function>equationList = new List<SystemsOfEqn.Function>
        /// {
        ///    x=> x[0]*x[0] + x[1]*x[1]
        ///    x=> x[0] + x[1]
        /// }
        /// List<double> startValues = new List<double>{1,2}
        /// 
        /// NewtonRaphsonLU(startValues, equations, 1e-3, 10)
        /// </code>
        /// </example>
        public static void NewtonRaphsonLU(List<double> start,
         List<Function> equations, double tol=1e-6, double maxIter=5)
        {
            //Executing the Newton Raphson
            int count =1;
            while (count<maxIter)
            {
                SystemsOfEqn Eqns = new SystemsOfEqn(start, equations);
                (Matrix<double>EqnJab, List<double>ConstMat)  = Eqns.Jacobi();
                Matrix<double> EqnJabInv = MatrixExtensionsTwo.LUMatrixInv(EqnJab);
                List<double> DeltaVar = EqnJabInv.DotProduct1D(ConstMat);

                List<double> values = new List<double>();
                List<double> errorList = new List<double>();

                for(int i=0; i<DeltaVar.Count; i++)
                {
                    //Console.WriteLine(DeltaVar[i]);
                    double xValues = start[i] + DeltaVar[i];
                    //Console.WriteLine(xValues);
                    values.Add(xValues);
                    double error = Math.Abs((xValues-start[i])/start[i]);
                    errorList.Add(error);
                }
                //MatrixOperations.PrintList(values);
                if (errorList.Sum() < tol) 
                {
                    MatrixOperations.PrintList(values);
                    Console.WriteLine($"Numer of iterations = {count}");
                    break;
                }
                else
                {
                    if (count == maxIter)
                    {
                        Console.WriteLine($"Newton Raphson unsuccessful at Max Iteration");
                        MatrixOperations.PrintList(values);
                    }
                    start = new List<double>(values);
                    count +=1;
                }
            }
        }
    }
}