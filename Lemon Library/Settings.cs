﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using static LemonLibrary.InfoHelper;

namespace LemonLibrary
{
    public class Settings
    {
        #region USettings
        public static UserSettings USettings = new UserSettings();
        public static void SaveSettings(string id = "id")
        {
            if (id == "id") id = USettings.LemonAreeunIts;
            File.WriteAllText(USettings.CachePath + id + ".st", TextHelper.TextEncrypt(Convert.ToBase64String(Encoding.UTF8.GetBytes(TextHelper.JSON.ToJSON(Settings.USettings))), TextHelper.MD5.EncryptToMD5string(id + ".st")));
        }
        public static void LoadUSettings(string qq)
        {
            USettings = new UserSettings();
            if (File.Exists(USettings.CachePath + qq + ".st")) {
                string data = Encoding.UTF8.GetString(Convert.FromBase64String(TextHelper.TextDecrypt(File.ReadAllText(USettings.CachePath + qq + ".st"), TextHelper.MD5.EncryptToMD5string(qq + ".st"))));
                JObject o = JObject.Parse(data);
                USettings.LemonAreeunIts = o["LemonAreeunIts"].ToString();
                USettings.UserImage = o["UserImage"].ToString();
                USettings.UserName = o["UserName"].ToString();
                USettings.Playing.GC = o["Playing"]["GC"].ToString();
                USettings.Playing.ImageUrl = o["Playing"]["ImageUrl"].ToString();
                USettings.Playing.MusicID = o["Playing"]["MusicID"].ToString();
                USettings.Playing.MusicName = o["Playing"]["MusicName"].ToString();
                USettings.Playing.Singer = o["Playing"]["Singer"].ToString();
                USettings.jd = Double.Parse(o["jd"].ToString());
                USettings.alljd = Double.Parse(o["alljd"].ToString());
                foreach (var jx in o["MusicLike"].ToArray())
                {
                    foreach (var jm in jx)
                    {
                        if (!USettings.MusicLike.ContainsKey(jm["MusicID"].ToString()))
                            USettings.MusicLike.Add(jm["MusicID"].ToString(), new Music()
                            {
                                GC = jm["GC"].ToString(),
                                MusicID = jm["MusicID"].ToString(),
                                Singer = jm["Singer"].ToString(),
                                ImageUrl = jm["ImageUrl"].ToString(),
                                MusicName = jm["MusicName"].ToString()
                            });
                    }
                }
                foreach (var jcm in o["MusicGD"])
                {
                    foreach (var jm in jcm)
                    {
                        if (!USettings.MusicGD.ContainsKey(jm["id"].ToString()))
                        {
                            var datae = new List<Music>();
                            foreach (var dt in jm["Data"])
                            {
                                datae.Add(new Music()
                                {
                                    GC = dt["GC"].ToString(),
                                    Singer = dt["Singer"].ToString(),
                                    ImageUrl = dt["ImageUrl"].ToString(),
                                    MusicID = dt["MusicID"].ToString(),
                                    MusicName = dt["MusicName"].ToString()
                                });
                            }
                            USettings.MusicGD.Add(jm["id"].ToString(), new MusicGData()
                            {
                                id = jm["id"].ToString(),
                                name = jm["name"].ToString(),
                                pic = jm["pic"].ToString(),
                                Data = datae
                            });
                        }
                    }
                }
                if (data.Contains("Skin_Path")){
                    USettings.Skin_Path = o["Skin_Path"].ToString();
                    USettings.Skin_txt = o["Skin_txt"].ToString();
                    USettings.Skin_Theme_R = o["Skin_Theme_R"].ToString();
                    USettings.Skin_Theme_G = o["Skin_Theme_G"].ToString();
                    USettings.Skin_Theme_B = o["Skin_Theme_B"].ToString();
                }
                if (data.Contains("CachePath"))
                {
                    USettings.CachePath = o["CachePath"].ToString();
                    USettings.DownloadPath = o["DownloadPath"].ToString();
                }
                else {
                    USettings.CachePath = Environment.ExpandEnvironmentVariables(@"%AppData%\LemonApp\Cache\");
                    USettings.DownloadPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "\\LemonApp\\";
                }
            }
            else SaveSettings(qq);
        }
        public class UserSettings {
            public UserSettings() {
                CachePath = Environment.ExpandEnvironmentVariables(@"%AppData%\LemonApp\Cache\");
                DownloadPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "\\LemonApp\\";
            }
            #region 歌单
            public SortedDictionary<string, Music> MusicLike { get; set; } = new SortedDictionary<string, Music>();
            public SortedDictionary<string, MusicGData> MusicGD { get; set; } = new SortedDictionary<string, MusicGData>();
            #endregion
            #region 用户配置
            public string LemonAreeunIts { get; set; } = "你的QQ";
            public string UserName { get; set; } = "";
            public string UserImage { get; set; } = "";
            #endregion
            #region 上一次播放
            public Music Playing { get; set; } = new Music();
            public double jd { get; set; } = 0;
            public double alljd { get; set; } = 0;
            #endregion
            #region 主题配置
            public string Skin_Path { get; set; } = "";
            public string Skin_txt { get; set; } = "";
            public string Skin_Theme_R { get; set; } = "";
            public string Skin_Theme_G { get; set; } = "";
            public string Skin_Theme_B { get; set; } = "";
            #endregion
            #region 缓存/下载路径
            public string CachePath = @"C:\Users\cz241\AppData\Roaming\LemonApp\Cache\Lyric";
            public string DownloadPath = "";
            #endregion
        }
        #endregion

        #region LSettings
        public static LocaSettings LSettings = new LocaSettings();
        public static void LoadLocaSettings() {
            if (File.Exists(USettings.CachePath + "Data.st")){
                string data = Encoding.Default.GetString(Convert.FromBase64String(TextHelper.TextDecrypt(File.ReadAllText(USettings.CachePath + "Data.st"), TextHelper.MD5.EncryptToMD5string("Data.st"))));
                JObject o = JObject.Parse(data);
                LSettings.qq = o["qq"].ToString();
            }
            else SaveLocaSettings();
        }
        public static void SaveLocaSettings(){
            File.WriteAllText(USettings.CachePath + "Data.st", TextHelper.TextEncrypt(Convert.ToBase64String(Encoding.Default.GetBytes(TextHelper.JSON.ToJSON(LSettings))), TextHelper.MD5.EncryptToMD5string("Data.st")));
        }
        public class LocaSettings {
            public string qq { get; set; } = "EX";
        }
        #endregion

        #region WINDOW_HANDLE
        public static void SaveWINDOW_HANDLE(int WINDOW_HANDLE) {
            File.WriteAllText(USettings.CachePath + "WINDOW_HANDLE.INT", WINDOW_HANDLE.ToString());
        }
        public static int ReadWINDOW_HANDLE(){
          return int.Parse(File.ReadAllText(USettings.CachePath + "WINDOW_HANDLE.INT"));
        }
        #endregion
    }
}
