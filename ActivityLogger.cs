using System;
using System.Collections.Generic;
using System.Linq;

namespace demo
{
    public class ActivityLogger
    {
        private List<string> logEntries = new List<string>();
        private string currentUsername;

        public void SetUsername(string username)
        {
            currentUsername = username;
        }

        public void LogAction(string action)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            logEntries.Add($"[{timestamp}] {action}");
            // Keep only last 100 to avoid memory bloat
            if (logEntries.Count > 100)
                logEntries.RemoveAt(0);
        }

        public List<string> GetRecentLogs(int count)
        {
            return logEntries.TakeLast(count).ToList();
        }

        public List<string> GetAllLogs()
        {
            return logEntries.ToList();
        }
    }
}