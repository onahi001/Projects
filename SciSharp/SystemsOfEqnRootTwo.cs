// using System;
// using System.Diagnostics;
// using System.Collections.Generic;
// using MatrixCollection;

// namespace SystemOfEqnRoot
// {
//     public class SystemsOfEqnTWO
//     {
//         // defining some properties

//         public delegate double Function(
//             double x, double y, double z, double a, double b);
//         public List<double> start { get; set; }
//         public int n { get; set;}
//         public List<Function> func { get; set;}

//         // Defining a delegate for a function of 
//         // multiple variables
//         List<Function> equations = new List<Function>
//             {
//                 (x, y, z, a, b) => (x*x) + (y*2) - 4,
//                 (x, y, z, a, b) => (x*x) + (z*2) - 9,
//                 (x, y, z, a, b) => (y*2) + (a*2) - 16,
//                 (x, y, z, a, b) => (z*2) + (b*2) - 25,
//                 (x, y, z, a, b) => (a*2) + (x*2) - 20
//             };

//         // creating class constructor
//         public SystemsOfEqnTWO(List<double> start)
//         {
//             //start = [2.5, 2.0];
//             n = 5;
//             this.start = start;
//             this.func = equations;
//         }

//         // Access start
//         public List<double> GetStart()
//         {
//             return start;
//         }

        
        
//         // Defining the Jacobian Class
//         public (Matrix<double>, List<double>) Jacobi(double h=1e-6)
//         {
//             Matrix<double> jacobiMat = new Matrix<double>();
//             List<double> funcMat = new List<double>();

//             foreach (var fun in func)
//             {
//                 // Evaluating the function
//                 double result = fun(start[0], start[1]);
//                 funcMat.Add(result);
//                 List<double> partialDerivative = 
//                     new List<double>();
                
//                 for(int i=0; i<func.Count; i++)
//                 {
//                     List<double> perturbed_point = new List<double>(start);
//                     perturbed_point[i] += h;
//                     double partial_dx = (
//                         fun(perturbed_point[0], perturbed_point[1]) -
//                             result) / h;
//                     partialDerivative.Add(partial_dx);
//                 }
//                 jacobiMat.AddRow(partialDerivative);
//             }

//             for(int i=0; i<funcMat.Count; i++)
//             {
//                 funcMat[i] = funcMat[i]*-1;
//             }

//             return (jacobiMat, funcMat);
//         }

//         // main
//         static void Mainer()
//         {
//             Stopwatch stopwatch = Stopwatch.StartNew();
//             int n=10;
//             double tol=1e-6;
//             int maxIter=100;
//             //Executing the Newton Raphson
//             int count =1;
//             List<double> start= new List<double>{1, 2, 0.7, 3, 1.2};
//             while (count<maxIter)
//             {
//                 SystemsOfEqnTWO Eqns = new SystemsOfEqnTWO(start);
//                 (Matrix<double>EqnJab, List<double>ConstMat)  = Eqns.Jacobi();
//                 List<double> soln = MatrixExtensions.GaussJordanElimination(EqnJab, ConstMat);

//                 List<double> values = new List<double>();
//                 List<double> errorList = new List<double>();
//                 for(int i=0; i<soln.Count; i++)
//                 {
//                     double xValues  = start[i] + soln[i];
//                     values.Add(xValues);
//                     double error = Math.Abs((xValues-start[i])/start[i]);
//                     errorList.Add(error);
//                 }

//                 if (errorList.Sum() < tol) 
//                 {
//                     MatrixOperations.PrintList(values);
//                     Console.WriteLine($"Numer of iterations = {count}");
//                     break;
//                 }
//                 else
//                 {
//                     if (count == maxIter)
//                     {
//                         Console.WriteLine($"Newton Raphson unsuccessful at Max Iteration");
//                         MatrixOperations.PrintList(values);
//                     }
//                     start = new List<double>(values);
//                     count +=1;
//                 }
//             }
//             stopwatch.Stop();
//             Console.WriteLine($"Elapsed time: {stopwatch.Elapsed.Milliseconds} ms");
//         }
//     }
// }