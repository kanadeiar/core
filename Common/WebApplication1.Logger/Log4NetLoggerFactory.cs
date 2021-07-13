﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Logger
{
    public static class Log4NetLoggerFactory
    {
        private static string CheckFilePath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("Не указан путь к конфигурационному файлу");
            if (Path.IsPathRooted(filePath))
                return filePath;
            var assemply = Assembly.GetEntryAssembly();
            var dir = Path.GetDirectoryName(assemply!.Location);
            return Path.Combine(dir, filePath);
        }

        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory, string configFile = "log4net.config")
        {
            factory.AddProvider(new Log4NetLoggerProvider(CheckFilePath(configFile)));
            return factory;
        }
    }
}