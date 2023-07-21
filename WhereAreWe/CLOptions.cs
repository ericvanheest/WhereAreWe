using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace WhereAreWe
{
    public class CLOptions
    {
        enum NextOption { None, Game, ImportSettings };
        enum NextArgs { None, Map };

        public bool Usage { get; set; }
        public bool Help { get; set; }
        public GameNames Game { get; set; }
        public String Map { get; set; }
        public string Warnings { get; set; }
        public string Errors { get; set; }
        public bool ForceDebugOn { get; set; }
        public bool ForceDebugOff { get; set; }
        public String SettingsFile { get; set; }
        public bool ResetSettings { get; set; }

        static string Version()
        {
            return System.Diagnostics.Process.GetCurrentProcess().MainModule.FileVersionInfo.ProductVersion;
        }

        public static string GetUsage()
        {
            return string.Format("Where Are We, version {0}\r\n\r\n" +
            "Usage:  WhereAreWe.exe [options] [map]\r\n" +
            "Options:\r\n" +
            "    -i SettingsFile (import from previously exported settings)\r\n" +
            "    -g GameName (set the current game)\r\n" +
            "    --resetsettings (set all options to defaults)\r\n" +
            "GameName values: MM1, MM2, MM3, MM45,\r\n" +
            "                 BT1, BT2, BT3,\r\n" +
            "                 WIZ1, WIZ2, WIZ3, WIZ4, WIZ5\r\n"
            , Version());
        }

        public CLOptions(string[] args, bool bSkipZeroArg)
        {
            Usage = false;
            Help = false;
            Map = "";
            Game = GameNames.None;
            ForceDebugOff = false;
            ForceDebugOn = false;
            ResetSettings = false;
            SettingsFile = String.Empty;

            NextOption nextOption = NextOption.None;
            NextArgs nextArg = NextArgs.Map;

            foreach (string strArg in args)
            {
                if (bSkipZeroArg)
                {
                    bSkipZeroArg = false;
                    continue;
                }

                switch (nextOption)
                {
                    case NextOption.None:
                        if (strArg[0] == '-' || strArg[0] == '/')
                        {
                            for (int i = 1; i < strArg.Length; i++)
                            {
                                switch (strArg[i])
                                {
                                    case 'h':
                                    case 'H':
                                    case '?':
                                        Help = true;
                                        break;

                                    case 'g':
                                    case 'G':
                                        nextOption = NextOption.Game;
                                        break;

                                    case 'i':
                                    case 'I':
                                        nextOption = NextOption.ImportSettings;
                                        break;

                                    case '-':
                                        switch (strArg.Substring(i + 1))
                                        {
                                            case "help":
                                                Help = true;
                                                break;

                                            case "debug":
                                                ForceDebugOn = true;
                                                break;

                                            case "nodebug":
                                                ForceDebugOff = true;
                                                break;

                                            case "resetsettings":
                                                ResetSettings = true;
                                                break;

                                            default:
                                                Warnings += String.Format("Ignoring unknown option '{0}'", strArg.Substring(i + 1));
                                                break;
                                        }
                                        i = strArg.Length;
                                        break;
                                    default:
                                        Warnings += String.Format("Ignoring unknown option '{0}'", strArg[i]);
                                        break;
                                }
                            }
                        }
                        else switch (nextArg)
                        {
                            case NextArgs.Map:
                                Map = strArg;
                                break;

                            default:
                                Warnings += String.Format("Ignoring extra command line argument '{0}'", strArg);
                                break;
                        }
                        break;
                        
                    case NextOption.ImportSettings:
                        SettingsFile = strArg;
                        break;

                    case NextOption.Game:
                        Game = Games.GameEnumFromShort(strArg);
                        if (Game == GameNames.None)
                            Warnings += String.Format("Unknown game name '{0}'", strArg);
                        break;

                    default:
                        Errors += "Unknown next option";
                        break;
                }
            }
        }
    }
}
