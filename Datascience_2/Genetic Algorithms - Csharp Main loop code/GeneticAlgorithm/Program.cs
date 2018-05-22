using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Program
    {
        static Random rng = new Random();
        static void Main(string[] args)
        {
            /* FUNCTIONS TO DEFINE (for each problem):
            Func<Ind> createIndividual;                                 ==> input is nothing, output is a new individual
            Func<Ind,double> computeFitness;                            ==> input is one individual, output is its fitness
            Func<Ind[],double[],Func<Tuple<Ind,Ind>>> selectTwoParents; ==> input is an array of individuals (population) and an array of corresponding fitnesses, output is a function which (without any input) returns a tuple with two individuals (parents)
            Func<Tuple<Ind, Ind>, Tuple<Ind, Ind>> crossover;           ==> input is a tuple with two individuals (parents), output is a tuple with two individuals (offspring/children)
            Func<Ind, double, Ind> mutation;                            ==> input is one individual and mutation rate, output is the mutated individual
            */

            GeneticAlgorithm<List<bool>> fakeProblemGA = new GeneticAlgorithm<List<bool>>(0.0, 0.0, false, 0, 0); // CHANGE THE GENERIC TYPE (NOW IT'S INT AS AN EXAMPLE) AND THE PARAMETERS VALUES
            var solution = fakeProblemGA.Run(CreateIndividual, ComputeFitness, SelectTwoParents, Crossover, Mutation); 
            Console.WriteLine("Solution: ");
            Console.WriteLine(solution);

        }
        
        private static List<bool> CreateIndividual()
        {
            List<bool> individual = new List<bool>();

            for (int i = 0; i < 8; i++)
            {
                individual.Add(rng.Next(0, 1) == 1);
            }
            
            return individual;
        }

        private static double ComputeFitness(List<bool> individual)
        {
            return individual.Count(x => x == true);
        }

        private static Func<Tuple<List<bool>, List<bool>>> SelectTwoParents(List<bool>[] lIndividuals, double[] lFitness)
        {
            return Test;
        }

        private static Tuple<List<bool>, List<bool>> Test()
        {
            return new Tuple<List<bool>, List<bool>>(null, null);
        }

        private static List<bool> Mutation(List<bool> individual, double mutationRate)
        {
            for (int i = 0; i < individual.Count; i++)
            {
                if (rng.NextDouble() <= mutationRate)
                {
                    individual[i] = !individual[i];
                }
            }
            return individual;
        }

        private static Tuple<List<bool>, List<bool>> Crossover(Tuple<List<bool>, List<bool>> parents)
        {
            List<bool> child1 = new List<bool>();
            List<bool> child2 = new List<bool>();

            child1.InsertRange(0, parents.Item1.GetRange(0, 4));
            child1.InsertRange(4, parents.Item2.GetRange(4, 4));

            child2.InsertRange(0, parents.Item2.GetRange(0, 4));
            child2.InsertRange(4, parents.Item1.GetRange(4, 4));

            return new Tuple<List<bool>, List<bool>>(child1, child2);
        }
    }
}
