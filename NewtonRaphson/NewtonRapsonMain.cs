using System;
using System.Collections.Generic;
using SystemOfEqnRoot;
using Matrices;

public class NewtonRaphsonRoots
{
    public static void Main()
    {
        static void FuncList()
        {
            List<SystemsOfEqn.Function> equations = 
                new List<SystemsOfEqn.Function>
            {
                (x, y) => y - 0.5*(Math.Exp(x/2) + Math.Exp(-x/2)),
                (x, y) => 25*Math.Pow(y,2) + 9*Math.Pow(x,2) -225
            };
        }

        double[] start = {2.5, 2.0};

        SystemsOfEqn test = new SystemsOfEqn(start);
        Matrix<double> jabTest = test.Jacobi().Item1;
        print (jabTest);
    }

    static void print(Matrix<double> jabTest)
    {
        foreach(var jab in jabTest)
        {
            Console.WriteLine(jab);
        }
    }
}