using System.Collections.Generic;

namespace TestTask
{
    public interface ILogger
    {
        void WriteLog(string message, List<List<int>> array);
    }
}
