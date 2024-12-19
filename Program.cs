using System;
using System.IO;

namespace LastTexture
{
    class Program
    {
        static void Main(string[] args)
        {
            // Verifica se os parâmetros foram passados corretamente
            if (args.Length < 2)
            {
                ShowHelp();
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
                    Console.WriteLine("Arquivo ou diretório inválido.");
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
                    ConvertPNGToSHTXPS(inputFile, outputFile); // Converter de PNG para SHTXPS
                }
                else
                {
                    Console.WriteLine("Arquivo ou diretório inválido.");
                }
            }
            else if (command == "help")
            {
                ShowHelp(); // Exibe o comando de ajuda
            }
            else
            {
                Console.WriteLine("Comando inválido.");
                ShowHelp();
            }
        }

        // Exibe a ajuda sobre os comandos
        static void ShowHelp()
        {
            Console.WriteLine("Uso:");
            Console.WriteLine("  -e [diretório ou arquivo] [diretório de saída ou arquivo de saída] : Converte SHTXPS para PNG.");
            Console.WriteLine("  -r [diretório ou arquivo] [diretório de saída ou arquivo de saída] : Converte PNG para SHTXPS.");
            Console.WriteLine("  help : Exibe este comando de ajuda.");
            Console.WriteLine();
            Console.WriteLine("Se o parâmetro for um diretório, o programa irá converter todos os arquivos .SHTXPS para PNG ou todos os arquivos .PNG para SHTXPS na pasta.");
            Console.WriteLine("Se o parâmetro de saída não for fornecido, o diretório de entrada será usado como o diretório de saída.");
        }

        // Método para converter de SHTXPS para PNG
        static void ConvertSHTXPSToPNG(string inputFile, string outputFile)
        {
            SHTXReader reader = new SHTXReader(inputFile, outputFile);
            reader.Read(); // Presumindo que o método Read converta o arquivo SHTXPS para PNG
            Console.WriteLine($"Arquivo {inputFile} convertido para {outputFile}");
        }

        // Método para converter de PNG para SHTXPS
        static void ConvertPNGToSHTXPS(string inputFile, string outputFile)
        {
            SHTXWritter writer = new SHTXWritter(inputFile, outputFile);
            writer.Write(); // Presumindo que o método Write converta o arquivo PNG para SHTXPS
            Console.WriteLine($"Arquivo {inputFile} convertido para {outputFile}");
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
