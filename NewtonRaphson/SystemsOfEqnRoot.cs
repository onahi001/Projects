using System;
using System.Collections.Generic;
using Matrices;

namespace SystemOfEqnRoot
{
    public class SystemsOfEqn
    {
        // defining some properties

        public delegate double Function(double x, double y);
        public double [] start { get; set; }
        public int n { get; set;}
        public List<Function> func { get; set;}

        // Defining a delegate for a function of 
        // multiple variables
        List<Function> equations = new List<Function>
            {
                (x, y) => y - 0.5*(Math.Exp(x/2) + Math.Exp(-x/2)),
                (x, y) => 25*Math.Pow(y,2) + 9*Math.Pow(x,2) -225
            };

        // creating class constructor
        public SystemsOfEqn(double[] start)
        {
            //start = [2.5, 2.0];
            //n = 2;
            this.start = start;
            this.func = equations;
        }

        
        
        // Defining the Jacobian Class
        public (Matrix<double>, List<double>) Jacobi(double h=1e-6)
        {
            Matrix<double> jacobi_mat = new Matrix<double>();
            List<double> func_mat = new List<double>();

            foreach (var fun in func)
            {
                // Evaluating the function
                double result = fun(start[0], start[1]);
                func_mat.Add(result);
                List<double> partialDerivative = 
                    new List<double>();
                
                for(int i=0; i<func.Count; i++)
                {
                    double[] perturbed_point = (double[])start.Clone();
                    perturbed_point[i] += h;
                    double partial_dx = (
                        fun(perturbed_point[0], perturbed_point[1]) -
                            result) / h;
                    partialDerivative.Add(partial_dx);
                }
                jacobi_mat.AddElement(partialDerivative);
            }

            return (jacobi_mat, func_mat);
        }
    }
}