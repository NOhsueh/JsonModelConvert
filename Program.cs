using Newtonsoft.Json;

namespace 操作json序列化和反序列化
{
    public class ConfigHelper
    {
        class Config
        {
            public string? FilesPath { get; set; }
            public Dictionary<string, bool>? LangMap { get; set; }
        }
        private static string configFilename = @"config.json";

        public static void Main()
        {
            ConfigHelper cfgHelper = new ConfigHelper();

            Console.WriteLine(cfgHelper.GetFilesPath());

            foreach (var langMap in cfgHelper.GetLangMap())
            {
                Console.WriteLine(langMap.Key + ": " + langMap.Value);
            }

            Console.ReadKey();
        }


        //初始化
        ConfigHelper()
        {
            Config config = Json2Model();

            if (config.FilesPath == null)
            {
                config.FilesPath = @"D:\LangBox Files";
            }
            if (config.LangMap == null)
            {
                config.LangMap = new Dictionary<string, bool> {
                    { "C",true},
                    { "CPlusPlus",false},
                    { "Python",false},
                    {"Java", false},
                    {"CSharp",false}
                };
            }

            Model2Json(config);
        }

        private static Config Json2Model()
        {
            if (!File.Exists(configFilename))
            {
                return new Config();

            }
            string str = File.ReadAllText(configFilename);
            Config cfg = JsonConvert.DeserializeObject<Config>(str);

            return cfg;
        }

        private static void Model2Json(Config config)
        {
            string str = JsonConvert.SerializeObject(config); //转为字符串
            File.WriteAllText(configFilename, str);
        }

        public string GetFilesPath()
        {

            string filesPath = Json2Model().FilesPath;
            return filesPath;
        }

        public void SetFilesPath(string filesPath)
        {
            Config cfg = Json2Model();
            cfg.FilesPath = filesPath;
            Model2Json(cfg);
        }

        public Dictionary<string, bool> GetLangMap()
        {
            Dictionary<string, bool> langMap = Json2Model().LangMap;
            return langMap;
        }

        public void SetLangMap(string lang, bool isCheck)
        {
            Config cfg = Json2Model();

            if (cfg.LangMap.ContainsKey(lang))
            {
                cfg.LangMap[lang] = isCheck;
            }
            else
            {
                cfg.LangMap.Add(lang, isCheck);
            }
            Model2Json(cfg);
        }
    }
}




