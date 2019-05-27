using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using WordPuzzle.Interfaces;
using WordPuzzle.Models;
using WordPuzzle.Tests;

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
                throw new ArgumentException("not enough arguments");

            AppProperties props = new AppProperties()
            {
                FilePath = args[0],
                StartWord = args[1],
                EndWord = args[2],
                ResultPath = args[3]
            };
            
            //TODO: error handling, words must be in dictionary
            //Unit tests etc
            //log to screen
            //File Log?
            //Max iteration should be noe more than letters differetn? x2?
            //If no children can be found?
            //Performasmnce test: regex, vs compare


            //setup our DI
            var serviceProvider = new ServiceCollection()
           //     .AddLogging()
                .AddScoped<IPuzzleProcessor, PuzzleProcessor>()
                .AddScoped<IFileUtility, FileUtility>()
                .AddScoped<IWordFilter, WordFilter>()
                .AddScoped<IWordUtility, WordUtility>()
                .AddScoped<INodeProcessor, NodeProcessor>()
                .BuildServiceProvider();

            //configure console logging
            //serviceProvider
            //    .GetService<ILoggerFactory>()
            //    .AddConsole(LogLevel.Debug);

            //var logger = serviceProvider.GetService<ILoggerFactory>()
            //    .CreateLogger<Program>();
            //logger.LogDebug("Starting application");

            //do the actual work here
            var pp = serviceProvider.GetService<IPuzzleProcessor>();
            pp.Process("spin", "rent");

            // logger.LogDebug("All done!");

            //PuzzleProcessor pp = new PuzzleProcessor(null, null, null);

            //pp.Process("spin", "spot");
                            
            

          
        }    
    }
}
