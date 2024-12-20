using System;
using System.IO;

namespace LastTexture
{
    class Program
    {
        static void Main(string[] args)
        {
            // Verifica se os parâmetros foram passados corretamente
            //SHTXWritter writer = new SHTXWritter("2334-nq8.png", "teste.shtxps");
           // SHTXReader reader = new SHTXReader("teste.shtxps", "2334-test.png");

           // return;
            if (args.Length < 2)
            {
                ShowHelp();
                Console.ReadLine();
                return;
            }

            string command = args[0].ToLower();
            string inputPath = args[1];
            string outputPath = args.Length > 2 ? args[2] : null; // Pasta de saída, opcional

            // Verifica o tipo de operação que o usuário deseja
            if (command == "-e")
            {
                // Se for um diretório
                if (Directory.Exists(inputPath))
                {
                    // Se o usuário não especificou um diretório de saída, usa o diretório de entrada como saída
                    outputPath = outputPath ?? inputPath;
                    ConvertAllSHTXPSInFolderToPNG(inputPath, outputPath); // Converter todos os SHTXPS para PNG
                }
                // Se for um arquivo
                else if (File.Exists(inputPath) && Path.GetExtension(inputPath).ToLower() == ".shtxps")
                {
                    string outputFile = outputPath ?? Path.ChangeExtension(inputPath, ".png");
                    ConvertSHTXPSToPNG(inputPath, outputFile); // Converter de SHTXPS para PNG
                }
                else
                {
                    Console.WriteLine("Invalid file or directory.");
                }
            }
            else if (command == "-r")
            {
                // Se for um diretório
                if (Directory.Exists(inputPath))
                {
                    // Se o usuário não especificou um diretório de saída, usa o diretório de entrada como saída
                    outputPath = outputPath ?? inputPath;
                    ConvertAllPNGInFolderToSHTXPS(inputPath, outputPath); // Converter todos os PNG para SHTXPS
                }
                // Se for um arquivo
                else if (File.Exists(inputPath) && Path.GetExtension(inputPath).ToLower() == ".png")
                {
                    string outputFile = outputPath ?? Path.ChangeExtension(inputPath, ".shtxps");
                    ConvertPNGToSHTXPS(inputPath, outputFile); // Converter de PNG para SHTXPS
                }
                else
                {
                    Console.WriteLine("Invalid file or directory.");
                }
            }
            else if (command == "-help")
            {
                ShowHelp(); // Exibe o comando de ajuda
            }
            else
            {
                Console.WriteLine("Invalid command.");
                ShowHelp();
            }
        }

        // Exibe a ajuda sobre os comandos
        static void ShowHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  -e [directory or file] [output directory or file] : Convert SHTXPS to PNG.");
            Console.WriteLine("  -r [directory or file] [output directory or file] : Convert PNG to SHTXPS.");
            Console.WriteLine("  -help : Display this help command.");
            Console.WriteLine();
            Console.WriteLine("If the parameter is a directory, the program will convert all .SHTXPS files\nto PNG or all .PNG files to SHTXPS in the folder.");
            Console.WriteLine("If the output parameter is not provided, the input directory will be used as\nthe output directory.");
        }

        // Método para converter de SHTXPS para PNG
        static void ConvertSHTXPSToPNG(string inputFile, string outputFile)
        {
            SHTXReader reader = new SHTXReader(inputFile, outputFile);
            //reader.Read(); // Presumindo que o método Read converta o arquivo SHTXPS para PNG
            Console.WriteLine($"File {inputFile} converted to {outputFile}");
        }

        // Método para converter de PNG para SHTXPS
        static void ConvertPNGToSHTXPS(string inputFile, string outputFile)
        {
            SHTXWritter writer = new SHTXWritter(inputFile, outputFile);
            // writer.Write(); // Presumindo que o método Write converta o arquivo PNG para SHTXPS
            Console.WriteLine($"File {inputFile} converted to {outputFile}.");
        }

        // Método para converter todos os arquivos SHTXPS em uma pasta para PNG
        static void ConvertAllSHTXPSInFolderToPNG(string folderPath, string outputFolder)
        {
            var files = Directory.GetFiles(folderPath, "*.shtxps");
            foreach (var file in files)
            {
                string outputFile = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(file) + ".png");
                ConvertSHTXPSToPNG(file, outputFile); // Converter cada arquivo SHTXPS para PNG
            }
        }

        // Método para converter todos os arquivos PNG em uma pasta para SHTXPS
        static void ConvertAllPNGInFolderToSHTXPS(string folderPath, string outputFolder)
        {
            var files = Directory.GetFiles(folderPath, "*.png");
            foreach (var file in files)
            {
                string outputFile = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(file) + ".shtxps");
                ConvertPNGToSHTXPS(file, outputFile); // Converter cada arquivo PNG para SHTXPS
            }
        }
    }
}
