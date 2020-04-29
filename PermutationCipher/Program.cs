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

            Console.WriteLine("Cryption result:");
            Console.WriteLine(result);

            result = Depermutate(code, inputString);

            Console.WriteLine();

            Console.WriteLine("Decryption result:");
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

            codePositions.Unify();

            Array.Sort(codePositions, parts);

            string result = "";

            for (int i = 0; i < parts.Length; i++) {
                result += parts[i] + ' ';
            }

            return result.Trim();
        }

        public static string Depermutate(string code, string inputString) {

            var splitted = inputString.Split(' ').Where((i) => {
                return i != "";
            }).ToList();

            var matrixWidth = code.Length;
            var matrixHeight = splitted.Max(item => item.Length);

            permutationMatrix = new char[matrixWidth, matrixHeight];

            codePositions = new int[code.Length];

            for (int i = 0; i < code.Length; i++) {
                codePositions[i] = code[i];
            }

            codePositions.Unify();

            int min;
            int minPos;

            for (int i = 0; i < splitted.Count; i++) {
                min = codePositions[0];
                minPos = 0;
                for (int j = 0; j < codePositions.Length; j++) {
                    if (codePositions[j] < min) {
                        min = codePositions[j];
                        minPos = j;
                    }
                }

                for (int j = 0; j < splitted[i].Length; j++) {
                    permutationMatrix[minPos, j] = splitted[i][j];
                }

                codePositions[minPos] = int.MaxValue;
            }

            var result = "";

            for (int i = 0; i < permutationMatrix.GetLength(1); i++) {
                for (int j = 0; j < permutationMatrix.GetLength(0); j++) {
                    result += permutationMatrix[j, i];
                }
            }

            return result;

        }

    }

    public static class Extensions {

        /// <summary>
        /// Метод унификации ключей. Нужен для правильной расшифровки
        /// </summary>
        /// <param name="values">Значения массива ключей</param>
        public static void Unify(this int[] values) {

            int counter;

            for (int i = 0; i < values.Length - 1; i++) {
                counter = 0;
                for (int j = i + 1; j < values.Length; j++) {
                    if (values[i] == values[j]) {
                        values[j] += ++counter;
                    }
                }
            }

        }

    }


}
