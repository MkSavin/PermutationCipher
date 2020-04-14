using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PermutationCipher {
    /*
     Creator: MkSavin @ Elerance.com
    */
    class Program {

        static void Main(string[] args) {

            Console.WriteLine("Code: ");
            var code = Console.ReadLine().ToLower();

            var inputString = "";
            string input;

            Console.WriteLine("Input string [empty = next step]: ");
            do {
                input = Console.ReadLine().ToLower();
                inputString += input + '\n';
            } while (input != "");

            var result = Permutate(code, inputString);

            Console.WriteLine("Result:");
            Console.WriteLine(result);
            Console.ReadKey();

        }

        public static char[,] permutationMatrix;
        public static string[] parts;
        public static int[] codePositions;

        public static string Permutate(string code, string inputString) {

            inputString = Regex.Replace(inputString, @"\s+", "");

            var matrixWidth = code.Length;
            var matrixHeight = (int) Math.Ceiling((float) inputString.Length / code.Length);

            permutationMatrix = new char[matrixWidth, matrixHeight];
            parts = new string[matrixWidth];
            codePositions = new int[matrixWidth];

            for (int i = 0; i < inputString.Length; i++) {
                permutationMatrix[i % matrixWidth, i / matrixWidth] = inputString[i];
            }

            for (int x = 0; x < matrixWidth; x++) {
                parts[x] = "";
                for (int y = 0; y < matrixHeight; y++) {
                    parts[x] += permutationMatrix[x, y];
                }
            }

            for (int i = 0; i < code.Length; i++) {
                codePositions[i] = code[i];
            }

            Array.Sort(codePositions, parts);

            string result = "";

            for (int i = 0; i < parts.Length; i++) {
                result += parts[i] + ' ';
            }

            return result.Trim();
        }

    }
}
