using log4net.Repository.Hierarchy;

namespace Tests;

public static class HierarchyExtensions
{
    private const string ConsoleAppenderName = "ConsoleAppender";
    private const string FileAppenderName = "FileAppender";

    extension(Hierarchy hierarchy)
    {
        public void HandleConsoleOutput(bool isConsoleOutput)
        {
            if (!isConsoleOutput)
            {
                hierarchy.Root.RemoveAppender(ConsoleAppenderName);
            }
        }

        public void HandleFileOutput(bool isFileOutput)
        {
            if (!isFileOutput)
            {
                hierarchy.Root.RemoveAppender(FileAppenderName);
            }
        }
    }
}