using Microsoft.Extensions.DependencyInjection;
using System;
using WordPuzzle.Interfaces;
using WordPuzzle.Models;

namespace WordPuzzle
{
    class Program
    {

        IFileUtility _fileUtility;
        static void Main(string[] args)
        {
          
            if (args.Length < 4)
                throw new ArgumentException("not enough arguments");

            if (args.Length > 4)
                throw new ArgumentException("too many arguments");

            AppProperties props = new AppProperties()
            {
                FilePath = args[0],
                StartWord = args[1],
                EndWord = args[2],
                ResultPath = args[3]
            };                    

            //setup our DI
            var serviceProvider = new ServiceCollection()
           
                .AddScoped<IPuzzleProcessor, PuzzleProcessor>()
                .AddScoped<IFileUtility, FileUtility>()
                .AddScoped<IWordFilter, WordFilter>()
                .AddScoped<IWordUtility, WordUtility>()
                .AddScoped<INodeProcessor, NodeProcessor>()
                .BuildServiceProvider();
            
            
            var pp = serviceProvider.GetService<IPuzzleProcessor>();
            pp.Process(props);           

          
        }    
    }
}
