using System;
using System.Collections;
using System.Collections.Generic;
using MatrixCollection;
using SystemOfEqnRoot;
using NewRaphsonMethods;

public class Program
{
    static void Main()
    {
       // Define a system of equations
       List<SystemsOfEqn.Function> equations = new List<SystemsOfEqn.Function>
       {
           vars => vars[1] - 0.5 * (Math.Exp(vars[0]/2) + Math.Exp(-vars[0]/2)),
           vars => 25 * Math.Pow(vars[1], 2) + 9 * Math.Pow(vars[0], 2) - 225
       };

       // Define starting values
       List<double> startValues = new List<double>{2.5, 2.0};

       // Define a system of equations
       List<SystemsOfEqn.Function> equations2 = new List<SystemsOfEqn.Function>
       {
           vars => Math.Pow(vars[0], 2) + Math.Pow(vars[1], 2) -4,
           vars => Math.Pow(vars[0], 2) + Math.Pow(vars[2], 2) -9,
           vars => Math.Pow(vars[1], 2) + Math.Pow(vars[3], 2) -16,
           vars => Math.Pow(vars[2], 2) + Math.Pow(vars[4], 2) -25,
           vars => Math.Pow(vars[3], 2) + Math.Pow(vars[0], 2) -20
       };
       // Define starting values
        List<double> startValues2 = new List<double>{1, 2, 0.7, 3, 1.2};

       // Carrying out Newton Raphson using Gauss Jordan Method
       NewRaphsonSolver.NSolver(equations2, startValues2, 1e-6, 100);
       Console.WriteLine();
       Console.WriteLine("LU Crout Decomposition Method ");
       NewRaphsonSolver.NSolver(equations2, startValues2, 1e-6, 100,3);
    }

    
    
    
    
    
    public static void Example()
    {
        (double V, double Cao, double vo, double To) = (100, 5, 10, 300);
        (double rho, double Cp, double UA, double R) = (1, 4200, 10000, 8.314);
        (double k1o, double k2o, double k3o, double k4o, double k5o) = (
            5, 3, 1, 0.5, 0.2);
        (double Ea1, double Ea2, double Ea3, double Ea4, double Ea5) = (
            50000, 60000, 70000, 80000, 90000);
        (double dH1, double dH2, double dH3, double dH4, double dH5) = (
            -40000, -30000, -50000, -20000, -60000);
        // Define a system of equations
        List<SystemsOfEqn.Function> equations3 = new List<SystemsOfEqn.Function>
        {
            x => vo * (Cao - x[0]) - V * (k1o * Math.Exp(-Ea1 / (R * x[6])) * x[0]) - 
                V * (k3o * Math.Exp(-Ea3 / (R * x[6])) * x[0] * x[1])  - V * 
                (k5o * Math.Exp(-Ea5/(R*x[6])) * x[0] * x[3]),
            x => -vo * x[1] + V * (k1o * Math.Exp(-Ea1 / (R * x[6])) * x[0]) - V * 
                (k2o * Math.Exp(-Ea2 / (R * x[6])) * x[1])  - V * 
                (k3o * Math.Exp(-Ea3 / (R * x[6])) * x[0] * x[1]),
            x => -vo * x[2] + V * (k2o * Math.Exp(-Ea2 / (R * x[6])) * x[1]) - V * 
                (k4o * Math.Exp(-Ea4 / (R * x[6])) * x[2] * x[3]),
            x => -vo * x[3] + V * (k3o * Math.Exp(-Ea3 / (R * x[6])) * x[0] * x[1])
                 - V * (k4o * Math.Exp(-Ea4 / (R * x[6])) * x[2] * x[3]) - V *
                 (k5o * Math.Exp(-Ea5/(R*x[6])) * x[0] * x[3]),
            x => -vo * x[4] + V * (k4o * Math.Exp(-Ea4 / (R * x[6])) * x[2] * x[3]),
            x => -vo * x[5] + V * (k5o * Math.Exp(-Ea5/(R*x[6])) * x[0] * x[3]),
            x => vo * rho * Cp * (To - x[6]) + V * ( (k1o * Math.Exp(-Ea1 / (R * x[6])) * 
                x[0]) * dH1 + (k2o * Math.Exp(-Ea2 / (R * x[6])) * x[1]) * 
                dH2 + (k3o * Math.Exp(-Ea3 / (R * x[6])) * x[0] * x[1]) * 
                dH3 + (k4o * Math.Exp(-Ea4 / (R * x[6])) * x[2] * x[3]) * 
                dH4+ (k5o * Math.Exp(-Ea5/(R*x[6])) * x[0] * x[3]) * dH5) - UA*(x[6]-To)
        };

        // Define starting values
        List<double> startValues3 = new List<double>{2, 1, 0.5, 0.2, 0.1, 0.1, 350};

        // Carrying out Newton Raphson using Gauss Jordan Method
        NewRaphsonSolver.NSolver(equations3, startValues3, 1e-6, 100);
       Console.WriteLine();
       Console.WriteLine("LU Crout Decomposition Method ");
       NewRaphsonSolver.NSolver(equations3, startValues3, 1e-6, 100,2);
    }
}