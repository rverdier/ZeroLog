﻿using System;
using System.Collections.Generic;
using System.Text.Formatting;
using ZeroLog.Appenders;

namespace ZeroLog
{
    internal class NoopLogEvent : IInternalLogEvent
    {
        public static NoopLogEvent Instance { get; } = new NoopLogEvent();

        public Level Level => default(Level);
        public DateTime Timestamp => default(DateTime);
        public int ThreadId => 0;
        public string Name => null;
        public IList<IAppender> Appenders { get; } = new List<IAppender>();

        private NoopLogEvent()
        {
        }

        public void Initialize(Level level, Log log)
        {
        }

        public void AppendFormat(string format)
        {
        }

        public void AppendGeneric<T>(T arg)
        {
        }

        public ILogEvent Append(string s) => this;
        public ILogEvent AppendAsciiString(byte[] bytes, int length) => this;
        public unsafe ILogEvent AppendAsciiString(byte* bytes, int length) => this;
        public ILogEvent Append(bool b) => this;
        public ILogEvent Append(byte b) => this;
        public ILogEvent Append(byte b, string format) => this;
        public ILogEvent Append(char c) => this;
        public ILogEvent Append(short s) => this;
        public ILogEvent Append(short s, string format) => this;
        public ILogEvent Append(int i) => this;
        public ILogEvent Append(int i, string format) => this;
        public ILogEvent Append(long l) => this;
        public ILogEvent Append(long l, string format) => this;
        public ILogEvent Append(float f) => this;
        public ILogEvent Append(float f, string format) => this;
        public ILogEvent Append(double d) => this;
        public ILogEvent Append(double d, string format) => this;
        public ILogEvent Append(decimal d) => this;
        public ILogEvent Append(decimal d, string format) => this;
        public ILogEvent Append(Guid g) => this;
        public ILogEvent Append(Guid g, string format) => this;
        public ILogEvent Append(DateTime dt) => this;
        public ILogEvent Append(DateTime dt, string format) => this;
        public ILogEvent Append(TimeSpan ts) => this;
        public ILogEvent Append(TimeSpan ts, string format) => this;

        public void Log()
        {
        }

        public void WriteToStringBuffer(StringBuffer stringBuffer)
        {
        }

        public void SetTimestamp(DateTime timestamp)
        {
        }

        public void WriteToStringBufferUnformatted(StringBuffer stringBuffer)
        {
        }

        public bool IsPooled => false;
    }
}
