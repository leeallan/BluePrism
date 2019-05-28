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
            AppProperties props = new AppProperties();
            props.MismatchThreshold = Constants.MismatchThresholdDefault;

            if (args.Length < 4)
                throw new ArgumentException("not enough arguments");

            if (args.Length > 5)
                throw new ArgumentException("too many arguments");

            if (args.Length == 5)
            {
                if (byte.TryParse(args[4], out byte threshold))
                {
                    if (threshold < 2)
                        throw new ArgumentException("threshold must be 2 or greater, or all words will be removed, and no route found");

                    props.MismatchThreshold = threshold;
                }
                else
                {
                    throw new ArgumentException("Mismatch Threshold paramert must be numeric byte");
                }
            }

            props.FilePath = args[0];
            props.StartWord = args[1];
            props.EndWord = args[2];
            props.ResultPath = args[3];
                             

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
