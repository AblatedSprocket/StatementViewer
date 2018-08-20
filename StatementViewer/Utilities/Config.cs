using System.Data.SQLite;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace StatementViewer.Utilities
{
    public static class Config
    {
        private static string _databasePath = ConfigurationManager.AppSettings.Get("DatabasePath");
        private static string _searchDirectory = ConfigurationManager.AppSettings["SearchDirectory"];
        public static string User { get; set; }
        public static IEnumerable<string> VendorCategories { get; private set; } = ParseVendorCategories();
        //public static string DatabaseConnectionString { get; private set; } = ConfigurationManager.ConnectionStrings["Database"].ToString();

        public static string DatabasePath
        {
            get { return _databasePath; }
            set
            {
                _databasePath = value;
                string appPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string configFile = System.IO.Path.Combine(appPath, "StatementViewer.exe.config");
                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                configFileMap.ExeConfigFilename = configFile;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
                if (config.AppSettings.Settings["DatabasePath"] == null)
                {
                    config.AppSettings.Settings.Add("DatabasePath", value);
                }
                else
                {
                    config.AppSettings.Settings["DatabasePath"].Value = value;
                }
                //SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder
                //{
                //    DataSource = value,
                //    DefaultTimeout = 5000,
                //    SyncMode = SynchronizationModes.Off,
                //    JournalMode = SQLiteJournalModeEnum.Memory,
                //    PageSize = 65536,
                //    CacheSize = 16777216,
                //    FailIfMissing = false,
                //    ReadOnly = false
                //};
                ////DatabaseConnectionString = builder.ConnectionString;
                //if (config.ConnectionStrings.ConnectionStrings["Database"] == null)
                //{
                //    config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("Database", builder.ConnectionString));
                //}
                //else
                //{
                //    config.ConnectionStrings.ConnectionStrings["Database"].ConnectionString = builder.ConnectionString;
                //}
                config.Save();
            }
        }
        public static string SearchDirectory
        {
            get { return _searchDirectory; }
            set
            {
                _searchDirectory = value;
                string appPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string configFile = System.IO.Path.Combine(appPath, "StatementViewer.exe.config");
                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                configFileMap.ExeConfigFilename = configFile;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
                if (config.AppSettings.Settings["SearchDirectory"] == null)
                {
                    config.AppSettings.Settings.Add("SearchDirectory", value);
                }
                else
                {
                    config.AppSettings.Settings["SearchDirectory"].Value = value;
                }
                config.Save();
            }
        }
        private static IEnumerable<string> ParseVendorCategories()
        {
            string appPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string configFile = System.IO.Path.Combine(appPath, "StatementViewer.exe.config");
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = configFile;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            string categoryString = config.AppSettings.Settings["CostCategories"].Value;
            string[] categories = categoryString.Split(';');
            return categories;
        }
        //private static void AddVendorCategory(string category)
        //{
        //    List<string> categories = new List<string>(VendorCategories);
        //    categories.Add(category);
        //    VendorCategories = categories;
        //    string appPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //    string configFile = System.IO.Path.Combine(appPath, "DoubleTake.exe.config");
        //    ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
        //    configFileMap.ExeConfigFilename = configFile;
        //    Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
        //    string categoryString = string.Concat(ConfigurationManager.AppSettings["CostCategories"], ';', category);
        //    if (ConfigurationManager.AppSettings["CostCategories"] == null)
        //    {
        //        config.AppSettings.Settings.Add("CostCategories", categoryString);
        //    }
        //    else
        //    {
        //        config.AppSettings.Settings["CostCategories"].Value = categoryString;
        //    }
        //    config.Save();
        //}
    }
}
