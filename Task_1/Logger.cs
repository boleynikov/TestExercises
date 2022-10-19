using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestTask
{
    class Logger : ILogger
    {
        public readonly string FilePath;
        private readonly StringBuilder _builder;
        public Logger(string filename)
        {
            FilePath = filename;
            _builder = new StringBuilder();
        }

        public void WriteLog(string message, List<List<int>> array)
        {
            _builder.AppendLine()
                   .Append(message)
                   .AppendLine();

            array.ForEach(list =>
            {
                list.ForEach(item => _builder.Append($"{item} "));
                _builder.AppendLine();
            });

            File.WriteAllTextAsync(FilePath, _builder.ToString());
        }
    }
}
