using System;
using System.Diagnostics;
using System.Collections.Generic;   
using System.Linq;
using MatrixCollection;

namespace SystemOfEqnRoot
{
    public class SystemsOfEqn
    {
        // defining some properties

        public delegate double Function(List<double> variables);
        public List<double> start { get; set; }
        public int n { get; set;}
        public List<Function> equations { get; set;}

        // Constructor accepting initial values and equations
        // creating class constructor
        public SystemsOfEqn(List<double> start, List<Function> equations)
        {
            //start = [2.5, 2.0];
            //n = 2;
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

        // main
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