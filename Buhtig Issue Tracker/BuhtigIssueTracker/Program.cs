using System.Globalization;
using System.Threading;

using BuhtigIssueTracker.Core;

public class Program
{
    public static void Main()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        var e = new Engine();
        e.Run();
    }
}