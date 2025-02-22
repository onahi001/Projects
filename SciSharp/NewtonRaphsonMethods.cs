using System;
using System.Diagnostics;
using System.Collections.Generic;   
using System.Linq;
using MatrixCollection;
using SystemOfEqnRoot;
using System.Reflection.Metadata;

namespace NewRaphsonMethods
{
    public class NewRaphsonSolver
    {
        // Defining the Jacobian Class
        /// <summary>
        /// calculates the jacobian of a matrix
        /// </summary>
        /// <param name="h">pertubation value</param>
        /// <returns>
        /// returns a jacobian matrix and solution of the 
        /// equations substituting the starting values
        /// </returns>
        public static (Matrix<double>, List<double>) Jacobian(
            SystemsOfEqn equations, List<double> start, double h=1e-6)
        {
            Matrix<double> jacobiMat = new Matrix<double>();
            List<double> funcMat = equations.Evaluate(start);


            for(int e=0; e<equations.Count();  e++)
            {
                SystemsOfEqn.Function fun = equations[e];
                List<double> partialDerivative = 
                    new List<double>();
               
                for(int i=0; i<equations.Count(); i++)
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
        public static List<double> NewtonRaphsonGauss(
            SystemsOfEqn equations, double tol=1e-6, int maxIter=5)
        {
            //Executing the Newton Raphson
            int count =1;
            List<double> start = equations.GetStart();
            List<double> result = new List<double>();
            while (count<=maxIter)
            {
                (Matrix<double>EqnJab, List<double>ConstMat)  = Jacobian(equations, start);
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
                //Console.WriteLine(errorList.Sum());

                if (errorList.Sum() < tol) 
                {
                    MatrixOperations.PrintList(values);
                    Console.WriteLine($"Number of iterations = {count}");
                    result = new List<double>(values);
                    break;
                    //return values;
                }
                else
                {
                    if (count == maxIter)
                    {
                        Console.WriteLine($"Newton Raphson unsuccessful at Max Iteration");
                        MatrixOperations.PrintList(values);
                        result = new List<double>(values);
                    }
                    start = new List<double>(values);
                    count +=1;
                }
            }
            return result;
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
        public static List<double> NewtonRaphsonLU(
         SystemsOfEqn equations, double tol=1e-6, int maxIter=5)
        {
            //Executing the Newton Raphson
            int count =1;
            List<double> start = equations.GetStart();
            List<double> result = new List<double>();
            while (count<=maxIter)
            {
                (Matrix<double>EqnJab, List<double>ConstMat)  = Jacobian(equations, start);
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
                    result = new List<double>(values);
                    break;
                }
                else
                {
                    if (count == maxIter)
                    {
                        Console.WriteLine($"Newton Raphson unsuccessful at Max Iteration");
                        MatrixOperations.PrintList(values);
                        result = new List<double>(values);
                    }
                    start = new List<double>(values);
                    count +=1;
                }
            }
            return result;
        }

        public static void NSolver(
            List<SystemsOfEqn.Function> equationList, List<double> start,
            double tol=1-6, int maxIter=5, int method=1)
        {
            SystemsOfEqn equations = new SystemsOfEqn(start, equationList);

            if (method == 1)
            {
                NewtonRaphsonGauss(equations, tol, maxIter);
            }
            else
            {
                NewtonRaphsonLU(equations, tol, maxIter);
            }
        }
    }
}