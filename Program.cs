using System;
using System.IO;
using log;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string filepath, message, sev = "";
            List<string> messageList = new List<string>();
            Console.WriteLine("Введите имя файла для ввода сообщений");
            filepath = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(filepath))
            {
                Console.WriteLine("Error: Incorrect path to file");
                return;
            }
            Console.WriteLine("Введите сообщение в формате <severity> <сообщение>\n" +
                              "severity:\ntracer\ndebug\ninformation\nwarning\nerror\ncritical\n" +
                              "Пустая строка завершит работу программы");
            using (var lg = new Logger(Path.Combine(Environment.CurrentDirectory, filepath)))
            {
                while (!string.IsNullOrWhiteSpace(message = Console.ReadLine()))
                {
                    try
                    {
                        Logger._severity severity =
                            (Logger._severity) Enum.Parse(typeof(Logger._severity),
                                                                    sev = message.Split(' ')[0], true);
                        if(Enum.IsDefined(typeof(Logger._severity), severity))
                            lg.LoggerWrite(severity, message.Substring(sev.Length,
                                                                        message.Length - sev.Length));
                        else Console.WriteLine("Error: '{0}' is not a member of Logger._severity enumeration", sev);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Error: '{0}' is not a member of Logger._severity enumeration", sev);
                    }

                    Console.WriteLine("Введите сообщение в формате <severity> <сообщение>");
                }
            }
        }
    }
    
    
}