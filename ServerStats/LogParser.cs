using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace Stats
{
    public class LogParser
    {
        #region private members

        private string[] pattern;
        private SortedDictionary<string, Int32[]> players;
        private string fileName;
        private int percentage = 0;
        private int tempPercentage = 0;
        private Settings settings;
        private FormParser formParser;
        private string[] words;
        private int[] map;

        #endregion

        #region properties

        public SortedDictionary<string, int[]> Players
        {
            get
            {
                return players;
            }
        }

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the FormParser class.
        /// </summary>
        /// <param name="formParser"></param>
        /// <param name="settings">Instance of the Settings class containing the settings of the program.</param>
        /// <param name="fileName">Path of a log file that will be parsed.</param>
        public LogParser(FormParser formParser, Settings settings, string fileName)
        {
            this.formParser = formParser;
            this.settings = settings;
            this.fileName = fileName;
            words = settings.WordNames;
            map = settings.MapPatternsToStats;
            players = new SortedDictionary<string, int[]>();

        }

        #endregion

        #region parse method

        /// <summary>
        /// Parses a log file and stores the stats to the players stats dictionary.
        /// </summary>
        public void Parse()
        {
            try
            {
                int lineCount = 0;
                int max = int.MinValue;

                pattern = new string[Settings.PatternsCount];
                pattern[(int)Patterns.Infantry] = @"^(.+) was shot down by (.+)'s rifle( \(Friendly fire\))?";
                pattern[(int)Patterns.Sniper] = @"^(.+) was sniped by (.*?)( \(Friendly fire\))?$";
                pattern[(int)Patterns.SMG] = @"^(.+) was gunned down by (.+)'s submachinegun( \(Friendly fire\))?";
                pattern[(int)Patterns.LMG] = @"^(.+) was machinegunned by (.+)'s light machine gun( \(Friendly fire\))?";
                pattern[(int)Patterns.HMG] = @"^(.+) was killed by (.+)'s heavy machine gun( \(Friendly fire\))?";
                pattern[(int)Patterns.Bazooka] = @"^(.+) did not survive (.+)'s explosive attack( \(Friendly fire\))?";
                pattern[(int)Patterns.Flamethrower] = @"^(.+) was cremated by (.*?)( \(Friendly fire\))?$";
                pattern[(int)Patterns.Grenade] = @"^(.+) didn't see (.+)'s handgrenade( \(Friendly fire\))?";
                pattern[(int)Patterns.Pistol] = @"^(.+) was capped by (.*?)( \(Friendly fire\))?$";
                pattern[(int)Patterns.Knife] = @"^(.+) was castrated by (.*?)( \(Friendly fire\))?$";
                pattern[(int)Patterns.Airstrike] = @"^(.+) was killed by (.+)'s airstrike( \(Friendly fire\))?";
                pattern[(int)Patterns.TNT] = @"^(.+) didn't see (.+)'s TNT( \(Friendly fire\))?";
                pattern[(int)Patterns.Punch] = @"^(.+) was punched out by (.*?)( \(Friendly fire\))?$";
                pattern[(int)Patterns.Helmet] = @"^(.+) was helmeted by (.*?)( \(Friendly fire\))?$";
                pattern[(int)Patterns.Wound] = @"^(.+) died of severe wounds inflicted by (.*?)( \(Friendly fire\))?$";
                pattern[(int)Patterns.Rocket] = @"^(.+) ate (.+)'s rocket( \(Friendly fire\))?";
                pattern[(int)Patterns.Airstrike2] = @"^(.+) ate (.+)'s airstrike( \(Friendly fire\))?";
                pattern[(int)Patterns.Scorch] = @"^(.+) was scorched by (.*?)( \(Friendly fire\))?$";
                pattern[(int)Patterns.Flame] = @"^(.+) got flamed by (.*?)( \(Friendly fire\))?$";
                pattern[(int)Patterns.Shrapnel] = @"^(.+) was shredded by (.+)'s shrapnel( \(Friendly fire\))?";
                pattern[(int)Patterns.Flatten] = @"^(.+) was flattened by (.*?)( \(Friendly fire\))?$";
                pattern[(int)Patterns.SliceInHalf] = @"^(.+) was sliced in half by (.*?)( \(Friendly fire\))?$";

                pattern[(int)Patterns.Gun] = @"^(.+) was gunned down by (.*?)( \(Friendly fire\))?$";

                pattern[(int)Patterns.Speach] = @"^([^:]+):(.*)$";

                pattern[(int)Patterns.OwnGrenade] = @"^(.+) tripped on his own grenade";
                pattern[(int)Patterns.BlewUp] = @"^(.+) blew"; // blew up, blew himself up
                pattern[(int)Patterns.SelfKill] = @"^(.+) killed himself";
                pattern[(int)Patterns.Fall] = @"^(.+) fell to his death";
                pattern[(int)Patterns.Drown] = @"^(.+) drowned";
                pattern[(int)Patterns.Squash] = @"^(.+) was squished";
                pattern[(int)Patterns.Barbedwire] = @"^(.+) tripped on barbedwire";
                pattern[(int)Patterns.AirstrikeSuicide] = @"^(.+) called an airstrike on himself";
                pattern[(int)Patterns.SelfBurn] = @"^(.+) burned himself";
                pattern[(int)Patterns.WrongPlace] = @"^(.+) was in the wrong place";
                pattern[(int)Patterns.SpawnKill] = @"^(.+) was killed for being in a spawn area";
                pattern[(int)Patterns.TeamChange] = @"^(.+) changed teams";
                pattern[(int)Patterns.Connection] = @"^(.+) connected.$";
                pattern[(int)Patterns.TeamJoin] = @"^(.+) has joined team (.+).$";
                pattern[(int)Patterns.NameChange] = @"^(.+) changed name to (.+)$";
                pattern[(int)Patterns.Destroyed] = @"^(.+) destroyed by (.+) \[(.+)\]$";
                pattern[(int)Patterns.Taken] = @"^(.+) taken by (.+) \[(.+)\]$";
                pattern[(int)Patterns.Observer] = @"^(.+) is now an Observer.$";
                pattern[(int)Patterns.Disconnection] = @"^(.+) disconnected$";
                pattern[(int)Patterns.Overflow] = @"^(.+) overflowed$";
                pattern[(int)Patterns.Timeout] = @"^(.+) timed out$";
                pattern[(int)Patterns.ObserverEnd] = @"^(.+) is no longer an Observer.$";
                pattern[(int)Patterns.Victory] = @"^Team (.+) is";
                pattern[(int)Patterns.SpeedHack] = @"^(.+) is being disconnected for Speed hack..$";
                pattern[(int)Patterns.ModifiedClient] = @"^(.+) is using a modified client.$";
                pattern[(int)Patterns.Kick] = @"^(.+) was kicked$";
                pattern[(int)Patterns.NextGame] = @"^(.+) will be changed for next game.$";

                pattern[(int)Patterns.Ping] = @"^Sending a ping.$";
                pattern[(int)Patterns.Master] = @"^Master server at";
                pattern[(int)Patterns.InitGame] = @"^==== InitGame ====$";
                pattern[(int)Patterns.ShutdownGame] = @"^==== ShutdownGame ====$";
                pattern[(int)Patterns.Rcon] = @"^rcon ";
                pattern[(int)Patterns.Processing] = @"^processing id commands$";
                pattern[(int)Patterns.Q2Admin] = @"^Q2Admin ";
                pattern[(int)Patterns.ConfigStrings] = @"^configstrings not valid";
                pattern[(int)Patterns.UnknownCommand] = @"^Unknown command ";
                pattern[(int)Patterns.NoMap] = @"^Can't find ";
                pattern[(int)Patterns.UnknownItem] = @"^unknown item$";
                pattern[(int)Patterns.NumScorePing] = @"^num score ping name";
                pattern[(int)Patterns.Loading] = @"^------- Loading gamei386.so -------$";

                for (int i = 0; i < Settings.PatternsCount; i++)
                {
                    if (settings.ItemsOpen[i])
                    {
                        max = i;
                    }
                }

                using (StreamReader sr2 = File.OpenText(fileName))
                {
                    while (sr2.ReadLine() != null)
                    {
                        lineCount++;
                    }
                }

                formParser.Invoke(formParser.setProgressBarMaximumDelegate, lineCount);

                using (StreamReader sr = File.OpenText(fileName))
                {
                    string line;
                    int index;
                    int progress = 0;
                    Match m = null;
                    Patterns patternName;
                    int groupsCount;
                    string name1 = "";
                    string name2 = "";
                    string name3 = "";

                    while ((line = sr.ReadLine()) != null)
                    {
                        progress++;
                        tempPercentage = (int)((double)progress / (double)lineCount * 100d);
                        patternName = Patterns.Unknown;
                        index = Settings.PatternsCount - 1;
                        groupsCount = 0;
                        for (int i = 0; i <= max; i++)
                        {
                            m = Regex.Match(line, pattern[i]);
                            if (m.Success)
                            {
                                index = i;
                                patternName = (Patterns)index;
                                groupsCount = m.Groups.Count;
                                break;
                            }
                        }
                        switch (groupsCount)
                        {
                            case 2:
                                name1 = m.Groups[1].Value;
                                break;
                            case 3:
                                name2 = m.Groups[2].Value;
                                goto case 2;
                            case 4:
                                name3 = m.Groups[3].Value;
                                goto case 3;
                        }
                        switch (patternName)
                        {
                            case Patterns.Speach:
                                updatePlayers(map[index], name1);
                                if (settings.Words)
                                {
                                    updateWords(name1, name2);
                                }
                                break;
                            case Patterns.Infantry:
                            case Patterns.Sniper:
                            case Patterns.SMG:
                            case Patterns.LMG:
                            case Patterns.HMG:
                            case Patterns.Bazooka:
                            case Patterns.Flamethrower:
                            case Patterns.Grenade:
                            case Patterns.Pistol:
                            case Patterns.Knife:
                            case Patterns.Airstrike:
                            case Patterns.TNT:
                            case Patterns.Punch:
                            case Patterns.Helmet:
                            case Patterns.Wound:
                            case Patterns.Rocket:
                            case Patterns.Shrapnel:
                            case Patterns.Scorch:
                            case Patterns.Flame:
                            case Patterns.Gun:
                            case Patterns.SliceInHalf:
                            case Patterns.Flatten:
                            case Patterns.Airstrike2:
                                if (name3 == "")
                                {
                                    updatePlayers(map[index] + Settings.KillsCount, name1);
                                    updatePlayers(map[index], name2);
                                }
                                else
                                {
                                    updatePlayers((int)Stats.TeamKills, name2);
                                }
                                break;
                            case Patterns.OwnGrenade:
                            case Patterns.BlewUp:
                            case Patterns.SelfKill:
                            case Patterns.Fall:
                            case Patterns.Drown:
                            case Patterns.Squash:
                            case Patterns.Barbedwire:
                            case Patterns.AirstrikeSuicide:
                            case Patterns.SelfBurn:
                            case Patterns.WrongPlace:
                            case Patterns.SpawnKill:
                            case Patterns.Connection:
                            case Patterns.TeamJoin:
                            case Patterns.NameChange:
                            case Patterns.Observer:
                                //case Patterns.Disconnection:
                                //case Patterns.Overflow:
                                //case Patterns.Timeout:
                                //case Patterns.ObserverEnd:
                                //case Patterns.Victory:
                                //case Patterns.SpeedHack:
                                //case Patterns.ModifiedClient:
                                //case Patterns.Kick:
                                //case Patterns.NextGame:
                                updatePlayers(map[index], name1);
                                break;
                            case Patterns.Destroyed:
                            case Patterns.Taken:
                                updatePlayers(map[index], name2);
                                break;
                            case Patterns.TeamChange:
                                updatePlayers(map[index], name1);
                                if ((line = sr.ReadLine()) != null)
                                {
                                    progress++;
                                }
                                break;
                            case Patterns.InitGame:
                            case Patterns.ShutdownGame:
                                while ((line = sr.ReadLine()) != null)
                                {
                                    progress++;
                                    if (line == "-------------------------------------")
                                    {
                                        break;
                                    }
                                }

                                break;
                            case Patterns.NumScorePing:
                                if ((line = sr.ReadLine()) != null)
                                {
                                    progress++;
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        progress++;
                                        if (line == "")
                                        {
                                            break;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        if (percentage != tempPercentage)
                        {
                            percentage = tempPercentage;
                            formParser.Invoke(formParser.setProgressDelegate, progress, percentage);
                        }
                    }
                    players.Remove("NET_GetPacket");
                    players.Remove("SZ_GetSpace");
                    players.Remove("Netchan_Transmit");
                    players.Remove("WARNING");
                    players.Remove("NET_SendPacket ERROR");
                    Int32[] a = null;
                    List<string> removeList = new List<string>();
                    foreach (KeyValuePair<string, Int32[]> kvp in players)
                    {
                        a = kvp.Value;
                        if (settings.Connections)
                        {
                            if (a[(int)Stats.Connections] == 0)
                            {
                                removeList.Add(kvp.Key);
                                continue;
                            }
                        }

                        a[(int)Stats.Kills] += a[(int)Stats.InfantryKills];
                        a[(int)Stats.Kills] += a[(int)Stats.SniperKills];
                        a[(int)Stats.Kills] += a[(int)Stats.SMGKills];
                        a[(int)Stats.Kills] += a[(int)Stats.LMGKills];
                        a[(int)Stats.Kills] += a[(int)Stats.HMGKills];
                        a[(int)Stats.Kills] += a[(int)Stats.BazookaKills];
                        a[(int)Stats.Kills] += a[(int)Stats.FlamethrowerKills];
                        a[(int)Stats.Kills] += a[(int)Stats.GrenadeKills];
                        a[(int)Stats.Kills] += a[(int)Stats.PistolKills];
                        a[(int)Stats.Kills] += a[(int)Stats.KnifeKills];
                        a[(int)Stats.Kills] += a[(int)Stats.AirstrikeKills];
                        a[(int)Stats.Kills] += a[(int)Stats.TNTKills];
                        a[(int)Stats.Kills] += a[(int)Stats.PunchKills];
                        a[(int)Stats.Kills] += a[(int)Stats.HelmetKills];
                        a[(int)Stats.Kills] += a[(int)Stats.WoundKills];
                        a[(int)Stats.Kills] += a[(int)Stats.FlattenKills];
                        a[(int)Stats.Kills] += a[(int)Stats.GunKills];
                        a[(int)Stats.Kills] += a[(int)Stats.SliceInHalfKills];

                        if (settings.Kills)
                        {
                            if (a[(int)Stats.Kills] < settings.KillsMin)
                            {
                                removeList.Add(kvp.Key);
                                continue;
                            }
                        }

                        a[(int)Stats.Deaths] += a[(int)Stats.InfantryDeaths];
                        a[(int)Stats.Deaths] += a[(int)Stats.SniperDeaths];
                        a[(int)Stats.Deaths] += a[(int)Stats.SMGDeaths];
                        a[(int)Stats.Deaths] += a[(int)Stats.LMGDeaths];
                        a[(int)Stats.Deaths] += a[(int)Stats.HMGDeaths];
                        a[(int)Stats.Deaths] += a[(int)Stats.BazookaDeaths];
                        a[(int)Stats.Deaths] += a[(int)Stats.FlamethrowerDeaths];
                        a[(int)Stats.Deaths] += a[(int)Stats.GrenadeDeaths];
                        a[(int)Stats.Deaths] += a[(int)Stats.PistolDeaths];
                        a[(int)Stats.Deaths] += a[(int)Stats.KnifeDeaths];
                        a[(int)Stats.Deaths] += a[(int)Stats.AirstrikeDeaths];
                        a[(int)Stats.Deaths] += a[(int)Stats.TNTDeaths];
                        a[(int)Stats.Deaths] += a[(int)Stats.PunchDeaths];
                        a[(int)Stats.Deaths] += a[(int)Stats.HelmetDeaths];
                        a[(int)Stats.Deaths] += a[(int)Stats.WoundDeaths];
                        a[(int)Stats.Deaths] += a[(int)Stats.FlattenDeaths];
                        a[(int)Stats.Deaths] += a[(int)Stats.GunDeaths];
                        a[(int)Stats.Deaths] += a[(int)Stats.SliceInHalfDeaths];

                        a[(int)Stats.Suicides] += a[(int)Stats.GrenadeSuicides];
                        a[(int)Stats.Suicides] += a[(int)Stats.BlewUpSuicides];
                        a[(int)Stats.Suicides] += a[(int)Stats.SelfKillSuicides];
                        a[(int)Stats.Suicides] += a[(int)Stats.FallSuicides];
                        a[(int)Stats.Suicides] += a[(int)Stats.DrownSuicides];
                        a[(int)Stats.Suicides] += a[(int)Stats.SquashSuicides];
                        a[(int)Stats.Suicides] += a[(int)Stats.BarbedwireSuicides];
                        a[(int)Stats.Suicides] += a[(int)Stats.AirstrikeSuicides];
                        a[(int)Stats.Suicides] += a[(int)Stats.SelfBurnSuicides];
                        a[(int)Stats.Suicides] += a[(int)Stats.WrongPlaceSuicides];
                        a[(int)Stats.Suicides] += a[(int)Stats.SpawnKillSuicides];

                        double b = (double)(a[(int)Stats.Kills] + a[(int)Stats.Deaths] + a[(int)Stats.Suicides] + a[(int)Stats.TeamKills]);
                        if (settings.PlusMinus)
                        {
                            if (b == 0)
                            {
                                removeList.Add(kvp.Key);
                                continue;
                            }
                        }
                        double percentage = ((double)a[(int)Stats.Kills] / b) * 100d;
                        a[(int)Stats.Percentage] = (int)percentage;

                        a[(int)Stats.PlusMinus] = a[(int)Stats.Kills] - a[(int)Stats.Deaths] - a[(int)Stats.Suicides] - a[(int)Stats.TeamKills];

                        a[(int)Stats.Points] += a[(int)Stats.Destroyed];
                        a[(int)Stats.Points] += a[(int)Stats.Taken];

                        a[(int)Stats.Words] += a[(int)Stats.Word0];
                        a[(int)Stats.Words] += a[(int)Stats.Word1];
                        a[(int)Stats.Words] += a[(int)Stats.Word2];
                        a[(int)Stats.Words] += a[(int)Stats.Word3];
                        a[(int)Stats.Words] += a[(int)Stats.Word4];
                        a[(int)Stats.Words] += a[(int)Stats.Word5];
                        a[(int)Stats.Words] += a[(int)Stats.Word6];
                        a[(int)Stats.Words] += a[(int)Stats.Word7];
                        a[(int)Stats.Words] += a[(int)Stats.Word8];
                        a[(int)Stats.Words] += a[(int)Stats.Word9];
                        a[(int)Stats.Words] += a[(int)Stats.Word10];
                        a[(int)Stats.Words] += a[(int)Stats.Word11];
                        a[(int)Stats.Words] += a[(int)Stats.Word12];
                        a[(int)Stats.Words] += a[(int)Stats.Word13];
                        a[(int)Stats.Words] += a[(int)Stats.Word14];
                        a[(int)Stats.Words] += a[(int)Stats.Word15];
                        a[(int)Stats.Words] += a[(int)Stats.Word16];
                        a[(int)Stats.Words] += a[(int)Stats.Word17];
                        a[(int)Stats.Words] += a[(int)Stats.Word18];
                        a[(int)Stats.Words] += a[(int)Stats.Word19];
                    }
                    foreach (string s in removeList)
                    {
                        players.Remove(s);
                    }
                }
                formParser.Invoke(formParser.finish);
            }
            catch (ThreadAbortException)
            {
            }
        }

        #endregion

        #region private methods

        /// <summary>
        /// Increases some stat of some player.
        /// </summary>
        /// <param name="index">Player's name.</param>
        /// <param name="name">Stat index to the stats array.</param>
        private void updatePlayers(int index, string name)
        {
            if (!players.ContainsKey(name))
            {
                players.Add(name, new Int32[Settings.StatsCount]);
            }
            players[name][index]++;
        }

        /// <summary>
        /// Increases stat of some word of some player.
        /// </summary>
        /// <param name="name1">Player's name.</param>
        /// <param name="name2">Name of the word.</param>
        private void updateWords(string name1, string name2)
        {
            for (int i = 0; i < Settings.WordsCount; i++)
            {
                if (name2.Contains(words[i]))
                {
                    players[name1][(int)Stats.Word0 + i]++;
                }
            }
        }

        #endregion
    }
}
