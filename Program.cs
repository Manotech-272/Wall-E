using System;
using System.Collections.Generic;

namespace WallE
{
    class Program
    {
        static void Main(string[] args)
        {
            // Maximum string size = 5 * 10^6 (for sequences longer than 5 * 10^6 separate into 2 or more strings by order of execution)



            MoveWallE("E");


            MoveWallE("NESO");


            MoveWallE("NSNSNSNSNS");


            MoveWallE("NESONESONESONESO");


            MoveWallE("EOENSNNNNNEOESNEOES");


            MoveWallE("NSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNS");


            MoveWallE("NSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNS", "NSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNSNS");


            MoveWallE("NEEONESSNEOOOESNEOSNESNOEENOSSEENSOENSOEEOSNESSSSONESNEOSNSSNEOSNSOSNSNSOENSOSNENEEENENEENEOEOEOSNSNSOENEOSNSONSONSOEEEEOEEENSOOSNNNNSOSNE");


            MoveWallE("EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");


            MoveWallE("NNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSEEEEEEEEEEEEEEEEEEEEEEEENNNNNNNNNNNNNOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOONNNNNNNNNNNNNEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEENNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOONNNNNNNNNNNNNNNNNNNNNEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
        }



        /// <summary>Moves Wall-E.</summary>
        /// <param name="movementSequences">The sequences of motion as ( (N) North, (S) South, (E) East, (O) West ).</param>
        private static void MoveWallE(params string[] movementSequences)
        {

            if (isSequenceValid(movementSequences))
            {
                PrintInput(movementSequences);
                Console.WriteLine(NumberOfVisitedPositions(movementSequences));
            }
            Console.ReadLine();
        }


        /// <summary>Prints the input.</summary>
        /// <param name="movementSequences">The sequences of motion as ( (N) North, (S) South, (E) East, (O) West ).</param>
        /// <remarks>Serves as helpfull tool to better understand what you are seeing in the console</remarks>
        private static void PrintInput(string[] movementSequences)
        {
            foreach (string s in movementSequences)
            {
                Console.WriteLine(s);
            }
        }


        /// <summary>Determines whether [the specified sequences] are valid.</summary>
        /// <param name="sequences">The movement sequences.</param>
        /// <returns>
        ///   <c>true</c> if [the specified sequences] are ALL valid; otherwise, <c>false</c>.</returns>
        private static bool isSequenceValid(string[] sequences)
        {

            foreach (string sequence in sequences)
            {
                if (sequence.Length > 1000000000)
                {
                    Console.WriteLine("At least one of the sequences you've entered has more than 10^10 characters.");
                    return false;
                }

                foreach (char character in sequence)
                {
                    if (!character.Equals('N') && !character.Equals('S') && !character.Equals('O') && !character.Equals('E'))
                    {
                        Console.WriteLine("At least one of the sequences you've entered is not valid.");
                        return false;
                    }
                }
            }

            return true;
        }



        /// <summary>Numbers the of visited positions.</summary>
        /// <param name="sequences">The movement sequences.</param>
        /// <returns>The number of different visited positions by following the movement sequences</returns>
        private static int NumberOfVisitedPositions(string[] sequences)
        {
            List<HashSet<Tuple<int, int>>> hashList = ParseIntoHashList(sequences);
            return CountWithoutDuplicates(hashList);

        }


        /// <summary>Counts the number of different elements in the hashsets in the <see cref="hashList"/>. </summary>
        /// <param name="hashList">The list containing several hashsets.</param>
        /// <returns>The number of different elements in the hashsets in the <see cref="hashList"/>. </returns>
        private static int CountWithoutDuplicates(List<HashSet<Tuple<int, int>>> hashList)
        {
            int hashCount = 0;

            for (int i = 0; i < hashList.Count; i++)
            {

                for (int j = i + 1; j < hashList.Count; j++)
                {
                    hashList[i].ExceptWith(hashList[j]);
                }

                hashCount += hashList[i].Count;
            }

            return hashCount;
        }


        /// <summary>
        ///    Parses the sequence into list of hashSets.
        /// </summary>
        /// <param name="sequences">The movement sequences.</param>
        /// <returns>A list of hashsets containing all the different positions visited.</returns>
        private static List<HashSet<Tuple<int, int>>> ParseIntoHashList(string[] sequences)
        {
            List<HashSet<Tuple<int, int>>> hashList = new List<HashSet<Tuple<int, int>>>();

            Tuple<int, int> currentPosition = new Tuple<int, int>(0, 0);

            foreach (string sequence in sequences)
            {

                HashSet<Tuple<int, int>> hashSet = new HashSet<Tuple<int, int>>();
                hashSet.EnsureCapacity(sequence.Length);

                hashSet.Add(currentPosition);

                TransverseSequence(hashSet, sequence, currentPosition);

                hashList.Add(hashSet);
            }

            return hashList;
        }


        /// <summary>Transverses the sequence.</summary>
        /// <param name="currentHashSet">The current hash set.</param>
        /// <param name="sequence">The sequence.</param>
        /// <param name="startingPosition">The starting position.</param>
        /// <remarks>Tranverse the <see cref="sequence"/> adding all visited position to the <see cref="currentHashSet"/>. </remarks>
        private static void TransverseSequence(HashSet<Tuple<int, int>> currentHashSet, string sequence, Tuple<int, int> startingPosition)
        {
            int x = startingPosition.Item1, y = startingPosition.Item2;

            foreach (char character in sequence)
            {

                switch (character)
                {

                    case 'N':
                        y++;
                        break;

                    case 'S':
                        y--;
                        break;

                    case 'E':
                        x++;
                        break;

                    case 'O':
                        x--;
                        break;

                }

                currentHashSet.Add(new Tuple<int, int>(x, y));

            }
        }
    }
}