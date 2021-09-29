using System;
using System.IO;

namespace log
{
    public class Logger : IDisposable
    {
        
        public enum _severity
        {
            tracer,
            debug,
            information,
            warning,
            error,
            critical
        }
        private sealed class DisposableEmp : IDisposable
        {
            private StreamWriter _streamWriter;

            public DisposableEmp(string filepath)
            {
                _streamWriter = new StreamWriter(filepath);
            }

            public void LoggerWrite(_severity severity, string data)
            {
                _streamWriter.Write($"[{DateTime.Now.ToString("f")}]" +
                                    $"[{severity.ToString("g")}]: {data}\n");
            }
            
            public void Dispose()
            {
                _streamWriter.Dispose();    
            }
            
        }

        private DisposableEmp _wr;
        
        public Logger(string filepath)
        {
            _wr = new DisposableEmp(filepath);
        }

        public void LoggerWrite(_severity severity, string data)
        {
            _wr.LoggerWrite(severity, data);   
        }        
        
        public void Dispose()
        {
            _wr.Dispose();
            GC.SuppressFinalize(this);
        }
        
    }
}