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

        // Access number of rows
        public int Count()
        {
            return this.equations.Count;
        }

        // indexing through equations
        public SystemsOfEqn.Function this[int index]
        {
            get => this.equations[index];
            set => this.equations[index] = value;
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
    }
}