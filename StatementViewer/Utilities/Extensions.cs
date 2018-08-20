using System;

namespace StatementViewer.Utilities
{
    public static class Extensions
    {
        public static string ToSQLiteDateString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
