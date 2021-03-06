﻿using System;
using System.IO;
using System.Threading;
using ZeroLog.ConfigResolvers;
using ZeroLog.Utils;

namespace ZeroLog.Config
{
    public static class JsonConfigurator
    {
        public static ILogManager ConfigureAndWatch(string configFilePath)
        {
            var configFileFullPath = Path.GetFullPath(configFilePath);

            var resolver = new HierarchicalResolver();

            var config = ConfigureResolver(configFileFullPath, resolver);

            var watcher = new FileSystemWatcher
            {
                Path = Path.GetDirectoryName(configFileFullPath),
                NotifyFilter = NotifyFilters.LastWrite,
                EnableRaisingEvents = true
            };

            watcher.Changed += (sender, args) =>
            {
                try
                {
                    if (!string.Equals(args.FullPath, configFileFullPath, StringComparison.InvariantCultureIgnoreCase))
                        return;

                    var newConfig = ReadConfiguration(configFileFullPath);
                    resolver.Build(newConfig);
                }
                catch (Exception e)
                {
                    LogManager.GetLogger(typeof(JsonConfigurator))
                        .FatalFormat("Updating config failed with: {0}", e.Message);
                }
            };

            return LogManager.Initialize(resolver, config.LogEventQueueSize, config.LogEventBufferSize);
        }

        private static ZeroLogConfiguration ConfigureResolver(string configFileFullPath, HierarchicalResolver resolver)
        {
            var config = ReadConfiguration(configFileFullPath);
            resolver.Build(config);
            return config;
        }

        private static ZeroLogConfiguration ReadConfiguration(string configFilePath)
        {
            var filecontent = ReadFileContentWithRetry(configFilePath);
            return DeserializeConfiguration(filecontent);
        }

        internal static ZeroLogConfiguration DeserializeConfiguration(string jsonConfiguration)
        {
            var config = JsonExtensions.DeserializeOrDefault(jsonConfiguration, new ZeroLogConfiguration());
            return config;
        }

        private static string ReadFileContentWithRetry(string filepath)
        {
            const int numberOfRetries = 3;
            const int delayOnRetry = 1000;

            for (var i = 0; i < numberOfRetries; i++)
            {
                try
                {
                    return File.ReadAllText(filepath);
                }
                catch (IOException)
                {
                    Thread.Sleep(delayOnRetry);
                }
            }

            return null;
        }
    }
}