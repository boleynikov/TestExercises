using System.Collections.Generic;

namespace TestTask
{
    public interface ILogger
    {
        /// <summary>
        /// Write array to file
        /// </summary>
        /// <param name="message">Display message</param>
        /// <param name="array">Array for log</param>
        void WriteLog(string message, List<List<int>> array);
    }
}
