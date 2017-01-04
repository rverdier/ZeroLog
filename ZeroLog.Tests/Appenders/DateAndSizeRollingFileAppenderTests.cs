﻿using System;
using System.IO;
using System.Text;
using System.Threading;
using NFluent;
using NUnit.Framework;
using ZeroLog.Appenders;

namespace ZeroLog.Tests.Appenders
{
    [TestFixture]
    public class DateAndSizeRollingFileAppenderTests
    {
        private DateAndSizeRollingFileAppender _appender;

        [SetUp]
        public void SetUp()
        {
            _appender = new DateAndSizeRollingFileAppender("TestLog");
            _appender.SetEncoding(Encoding.Default);
        }

        [TearDown]
        public void Teardown()
        {
            _appender.Close();
        }

        [Test]
        public void should_log_to_file()
        {
            var bytes = new byte[256];

            var message = "Test log message";
            var byteLength = Encoding.Default.GetBytes(message, 0, message.Length, bytes, 0);
            _appender.WriteEvent(new LogEvent(Level.Info), bytes, byteLength);
            _appender.Flush();
            
            var reader = new StreamReader(File.Open(_appender.CurrentFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            string written = null;

            while (!reader.EndOfStream)
            {
                written = reader.ReadLine();
            }

            Check.That(written).IsEqualTo(message);
        }
    }
}