using System;
using System.Collections.Generic;
using System.Text;

namespace Stats
{
    /// <summary>
    /// Contains settings of the program and important constants.
    /// </summary>
    public class Settings
    {
        #region consts

        /// <summary>
        /// Represents number of words of which is created the stats.
        /// </summary>
        public const int WordsCount = 20;
        /// <summary>
        /// Represents number of input pattern items.
        /// </summary>
        public const int PatternsCount = 65;
        /// <summary>
        /// Represents number of output stats items.
        /// </summary>
        public const int StatsCount = 83;
        /// <summary>
        /// Represents difference between number of output stats items and number of output stats items.
        /// </summary>
        public const int Stop = StatsCount - WordsCount;
        /// <summary>
        /// Represents number of kill pattern items.
        /// </summary>
        public const int KillsCount = 18;
        /// <summary>
        /// 
        /// </summary>
        public const int Total = 8;

        #endregion

        #region private members

        private bool words;
        private bool connections;
        private bool plusMinus;
        private bool kills;
        private int killsMin;
        private string host;
        private string user;
        private string password;
        private string database;
        private string table;
        private string url;

        private bool[] itemsOpen;
        private bool[] itemsExport;
        private bool[] itemsVisible;
        private string[] wordNames;

        private int[] mapPatternsToStats;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets a value indicating whether stats of words is created.
        /// </summary>
        public bool Words
        {
            get
            {
                return words;
            }
            set
            {
                words = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether players with no connections are deleted.
        /// </summary>
        public bool Connections
        {
            get
            {
                return connections;
            }
            set
            {
                connections = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether players with no kills and deaths and suicides and team kills are deleted.
        /// </summary>
        public bool PlusMinus
        {
            get
            {
                return plusMinus;
            }
            set
            {
                plusMinus = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether players with low kills are deleted.
        /// </summary>
        public bool Kills
        {
            get
            {
                return kills;
            }
            set
            {
                kills = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating number of minimum kills for players which is not deleted.
        /// </summary>
        public int KillsMin
        {
            get
            {
                return killsMin;
            }
            set
            {
                killsMin = value;
            }
        }

        public string Host
        {
            get
            {
                return host;
            }
            set
            {
                host = value;
            }
        }

        public string User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public string Database
        {
            get
            {
                return database;
            }
            set
            {
                database = value;
            }
        }

        public string Table
        {
            get
            {
                return table;
            }
            set
            {
                table = value;
            }
        }

        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
            }
        }

        /// <summary>
        /// Gets a bool array indicating which input pattern items is parsed from a log file.
        /// </summary>
        public bool[] ItemsOpen
        {
            get
            {
                return itemsOpen;
            }
            set
            {
                itemsOpen = value;
            }
        }

        /// <summary>
        /// Gets a bool array indicating which output stat items is exported to a file.
        /// </summary>
        public bool[] ItemsExport
        {
            get
            {
                return itemsExport;
            }
            set
            {
                itemsExport = value;
            }
        }

        /// <summary>
        /// Gets a bool array indicating which output stat items is visible on the screen.
        /// </summary>
        public bool[] ItemsVisible
        {
            get
            {
                return itemsVisible;
            }
            set
            {
                itemsVisible = value;
            }
        }

        /// <summary>
        /// Gets a string array of word names that is parsed form a log file and from which is created the stats.
        /// </summary>
        public string[] WordNames
        {
            get
            {
                return wordNames;
            }
            set
            {
                wordNames = value;
            }
        }

        /// <summary>
        /// Get a map representing which input pattern item belongs to output stat item.
        /// </summary>
        public int[] MapPatternsToStats
        {
            get
            {
                return mapPatternsToStats;
            }
        }

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the Settings class.
        /// </summary>        
        public Settings()
        {
            itemsOpen = new bool[PatternsCount];
            itemsExport = new bool[StatsCount];
            itemsVisible = new bool[StatsCount];
            wordNames = new string[WordsCount];
            mapPatternsToStats = new int[PatternsCount];
            mappingPatternsToStats();
        }

        #endregion

        #region private methods

        private void mappingPatternsToStats()
        {
            mapPatternsToStats[(int)Patterns.Speach] = (int)Stats.Speaches;
            mapPatternsToStats[(int)Patterns.Infantry] = (int)Stats.InfantryKills;
            mapPatternsToStats[(int)Patterns.Sniper] = (int)Stats.SniperKills;
            mapPatternsToStats[(int)Patterns.Grenade] = (int)Stats.GrenadeKills;
            mapPatternsToStats[(int)Patterns.Connection] = (int)Stats.Connections;
            mapPatternsToStats[(int)Patterns.TeamJoin] = (int)Stats.TeamJoins;
            mapPatternsToStats[(int)Patterns.LMG] = (int)Stats.LMGKills;
            mapPatternsToStats[(int)Patterns.HMG] = (int)Stats.HMGKills;
            mapPatternsToStats[(int)Patterns.Disconnection] = -1;
            mapPatternsToStats[(int)Patterns.SMG] = (int)Stats.SMGKills;
            mapPatternsToStats[(int)Patterns.Taken] = (int)Stats.Taken;
            mapPatternsToStats[(int)Patterns.Bazooka] = (int)Stats.BazookaKills;
            mapPatternsToStats[(int)Patterns.Pistol] = (int)Stats.PistolKills;
            mapPatternsToStats[(int)Patterns.OwnGrenade] = (int)Stats.GrenadeSuicides;
            mapPatternsToStats[(int)Patterns.Knife] = (int)Stats.KnifeKills;
            mapPatternsToStats[(int)Patterns.Processing] = -1;
            mapPatternsToStats[(int)Patterns.Destroyed] = (int)Stats.Destroyed;
            mapPatternsToStats[(int)Patterns.ShutdownGame] = -1;
            mapPatternsToStats[(int)Patterns.Fall] = (int)Stats.FallSuicides;
            mapPatternsToStats[(int)Patterns.Rcon] = -1;
            mapPatternsToStats[(int)Patterns.Overflow] = -1;
            mapPatternsToStats[(int)Patterns.Wound] = (int)Stats.WoundKills;
            mapPatternsToStats[(int)Patterns.Victory] = -1;
            mapPatternsToStats[(int)Patterns.Airstrike] = (int)Stats.AirstrikeKills;
            mapPatternsToStats[(int)Patterns.NameChange] = (int)Stats.NameChanges;
            mapPatternsToStats[(int)Patterns.Rocket] = (int)Stats.BazookaKills;
            mapPatternsToStats[(int)Patterns.TeamChange] = (int)Stats.TeamChanges;
            mapPatternsToStats[(int)Patterns.BlewUp] = (int)Stats.BlewUpSuicides;
            mapPatternsToStats[(int)Patterns.TNT] = (int)Stats.TNTKills;
            mapPatternsToStats[(int)Patterns.Flamethrower] = (int)Stats.FlamethrowerKills;
            mapPatternsToStats[(int)Patterns.SelfKill] = (int)Stats.SelfKillSuicides;
            mapPatternsToStats[(int)Patterns.Scorch] = (int)Stats.FlamethrowerKills;
            mapPatternsToStats[(int)Patterns.Kick] = -1;
            mapPatternsToStats[(int)Patterns.SpeedHack] = -1;
            mapPatternsToStats[(int)Patterns.ModifiedClient] = -1;
            mapPatternsToStats[(int)Patterns.Shrapnel] = (int)Stats.GrenadeKills;
            mapPatternsToStats[(int)Patterns.Timeout] = -1;
            mapPatternsToStats[(int)Patterns.Punch] = (int)Stats.PunchKills;
            mapPatternsToStats[(int)Patterns.Helmet] = (int)Stats.HelmetKills;
            mapPatternsToStats[(int)Patterns.WrongPlace] = (int)Stats.WrongPlaceSuicides;
            mapPatternsToStats[(int)Patterns.Drown] = (int)Stats.DrownSuicides;
            mapPatternsToStats[(int)Patterns.SelfBurn] = (int)Stats.SelfBurnSuicides;
            mapPatternsToStats[(int)Patterns.AirstrikeSuicide] = (int)Stats.AirstrikeSuicides;
            mapPatternsToStats[(int)Patterns.Squash] = (int)Stats.SquashSuicides;
            mapPatternsToStats[(int)Patterns.Barbedwire] = (int)Stats.BarbedwireSuicides;
            mapPatternsToStats[(int)Patterns.Gun] = (int)Stats.GunKills;
            mapPatternsToStats[(int)Patterns.Ping] = -1;
            mapPatternsToStats[(int)Patterns.SliceInHalf] = (int)Stats.SliceInHalfKills;
            mapPatternsToStats[(int)Patterns.Flame] = (int)Stats.FlamethrowerKills;
            mapPatternsToStats[(int)Patterns.Airstrike2] = (int)Stats.AirstrikeKills;
            mapPatternsToStats[(int)Patterns.Q2Admin] = -1;
            mapPatternsToStats[(int)Patterns.NoMap] = -1;
            mapPatternsToStats[(int)Patterns.ConfigStrings] = -1;
            mapPatternsToStats[(int)Patterns.InitGame] = -1;
            mapPatternsToStats[(int)Patterns.SpawnKill] = (int)Stats.SpawnKillSuicides;
            mapPatternsToStats[(int)Patterns.Loading] = -1;
            mapPatternsToStats[(int)Patterns.Observer] = (int)Stats.Observer;
            mapPatternsToStats[(int)Patterns.NumScorePing] = -1;
            mapPatternsToStats[(int)Patterns.Flatten] = (int)Stats.FlattenKills;
            mapPatternsToStats[(int)Patterns.ObserverEnd] = -1;
            mapPatternsToStats[(int)Patterns.UnknownCommand] = -1;
            mapPatternsToStats[(int)Patterns.Master] = -1;
            mapPatternsToStats[(int)Patterns.UnknownItem] = -1;
            mapPatternsToStats[(int)Patterns.NextGame] = -1;
            mapPatternsToStats[(int)Patterns.Unknown] = -1;
        }

        #endregion
    }

    #region enum patterns

    /// <summary>
    /// Input pattern items.
    /// </summary>
    public enum Patterns
    {
        /// <summary>
        /// 
        /// </summary>
        Speach,
        /// <summary>
        /// 
        /// </summary>
        Infantry,
        /// <summary>
        /// 
        /// </summary>
        Sniper,
        /// <summary>
        /// 
        /// </summary>
        Grenade,
        /// <summary>
        /// 
        /// </summary>
        Connection,
        /// <summary>
        /// 
        /// </summary>
        TeamJoin,
        /// <summary>
        /// 
        /// </summary>
        LMG,
        /// <summary>
        /// 
        /// </summary>
        HMG,
        /// <summary>
        /// 
        /// </summary>
        Disconnection,
        /// <summary>
        /// 
        /// </summary>
        SMG,
        /// <summary>
        /// 
        /// </summary>
        Taken,
        /// <summary>
        /// 
        /// </summary>
        Bazooka,
        /// <summary>
        /// 
        /// </summary>
        Pistol,
        /// <summary>
        /// 
        /// </summary>
        OwnGrenade,
        /// <summary>
        /// 
        /// </summary>
        Knife,
        /// <summary>
        /// 
        /// </summary>
        Processing,
        /// <summary>
        /// 
        /// </summary>
        Destroyed,
        /// <summary>
        /// 
        /// </summary>
        ShutdownGame,
        /// <summary>
        /// 
        /// </summary>
        Fall,
        /// <summary>
        /// 
        /// </summary>
        Rcon,
        /// <summary>
        /// 
        /// </summary>
        Overflow,
        /// <summary>
        /// 
        /// </summary>
        Wound,
        /// <summary>
        /// 
        /// </summary>
        Victory,
        /// <summary>
        /// 
        /// </summary>
        Airstrike,
        /// <summary>
        /// 
        /// </summary>
        NameChange,
        /// <summary>
        /// 
        /// </summary>
        Rocket,
        /// <summary>
        /// 
        /// </summary>
        TeamChange,
        /// <summary>
        /// 
        /// </summary>
        BlewUp,
        /// <summary>
        /// 
        /// </summary>
        TNT,
        /// <summary>
        /// 
        /// </summary>
        Flamethrower,
        /// <summary>
        /// 
        /// </summary>
        SelfKill, // konec2
        /// <summary>
        /// 
        /// </summary>
        Scorch,
        /// <summary>
        /// 
        /// </summary>
        Kick,
        /// <summary>
        /// 
        /// </summary>
        SpeedHack,
        /// <summary>
        /// 
        /// </summary>
        ModifiedClient,
        /// <summary>
        /// 
        /// </summary>
        Shrapnel,
        /// <summary>
        /// 
        /// </summary>
        Timeout,
        /// <summary>
        /// 
        /// </summary>
        Punch,
        /// <summary>
        /// 
        /// </summary>
        Helmet, // konec
        /// <summary>
        /// 
        /// </summary>
        WrongPlace,
        /// <summary>
        /// 
        /// </summary>
        Drown,
        /// <summary>
        /// 
        /// </summary>
        SelfBurn,
        /// <summary>
        /// 
        /// </summary>
        AirstrikeSuicide,
        /// <summary>
        /// 
        /// </summary>
        Squash,
        /// <summary>
        /// 
        /// </summary>
        Barbedwire,
        /// <summary>
        /// 
        /// </summary>
        Gun,
        /// <summary>
        /// 
        /// </summary>
        Ping,
        /// <summary>
        /// 
        /// </summary>
        SliceInHalf,
        /// <summary>
        /// 
        /// </summary>
        Flame,
        /// <summary>
        /// 
        /// </summary>
        Airstrike2,
        /// <summary>
        /// 
        /// </summary>
        Q2Admin,
        /// <summary>
        /// 
        /// </summary>
        NoMap,
        /// <summary>
        /// 
        /// </summary>
        ConfigStrings,
        /// <summary>
        /// 
        /// </summary>
        InitGame,
        /// <summary>
        /// 
        /// </summary>
        SpawnKill,
        /// <summary>
        /// 
        /// </summary>
        Loading,
        /// <summary>
        /// 
        /// </summary>
        Observer,
        /// <summary>
        /// 
        /// </summary>
        NumScorePing,
        /// <summary>
        /// 
        /// </summary>
        Flatten,
        /// <summary>
        /// 
        /// </summary>
        ObserverEnd,
        /// <summary>
        /// 
        /// </summary>
        UnknownCommand,
        /// <summary>
        /// 
        /// </summary>
        Master,
        /// <summary>
        /// 
        /// </summary>
        UnknownItem,
        /// <summary>
        /// 
        /// </summary>
        NextGame,
        /// <summary>
        /// 
        /// </summary>
        Unknown
    }

    #endregion

    #region enum stats

    /// <summary>
    /// Output stat items.
    /// </summary>
    public enum Stats
    {
        /// <summary>
        /// 
        /// </summary>
        Percentage,
        /// <summary>
        /// 
        /// </summary>
        PlusMinus,
        /// <summary>
        /// 
        /// </summary>
        Kills,
        /// <summary>
        /// 
        /// </summary>
        Deaths,
        /// <summary>
        /// 
        /// </summary>
        Suicides,
        /// <summary>
        /// 
        /// </summary>
        TeamKills,
        /// <summary>
        /// 
        /// </summary>
        Points,
        /// <summary>
        /// 
        /// </summary>
        Words,
        /// <summary>
        /// 
        /// </summary>
        InfantryKills,
        /// <summary>
        /// 
        /// </summary>
        SniperKills,
        /// <summary>
        /// 
        /// </summary>
        SMGKills,
        /// <summary>
        /// 
        /// </summary>
        LMGKills,
        /// <summary>
        /// 
        /// </summary>
        HMGKills,
        /// <summary>
        /// 
        /// </summary>
        BazookaKills,
        /// <summary>
        /// 
        /// </summary>
        FlamethrowerKills,
        /// <summary>
        /// 
        /// </summary>
        GrenadeKills,
        /// <summary>
        /// 
        /// </summary>
        PistolKills,
        /// <summary>
        /// 
        /// </summary>
        KnifeKills,
        /// <summary>
        /// 
        /// </summary>
        AirstrikeKills,
        /// <summary>
        /// 
        /// </summary>
        TNTKills,
        /// <summary>
        /// 
        /// </summary>
        PunchKills,
        /// <summary>
        /// 
        /// </summary>
        HelmetKills,
        /// <summary>
        /// 
        /// </summary>
        WoundKills,
        /// <summary>
        /// 
        /// </summary>
        FlattenKills,
        /// <summary>
        /// 
        /// </summary>
        GunKills,
        /// <summary>
        /// 
        /// </summary>
        SliceInHalfKills,
        /// <summary>
        /// 
        /// </summary>
        InfantryDeaths,
        /// <summary>
        /// 
        /// </summary>
        SniperDeaths,
        /// <summary>
        /// 
        /// </summary>
        SMGDeaths,
        /// <summary>
        /// 
        /// </summary>
        LMGDeaths,
        /// <summary>
        /// 
        /// </summary>
        HMGDeaths,
        /// <summary>
        /// 
        /// </summary>
        BazookaDeaths,
        /// <summary>
        /// 
        /// </summary>
        FlamethrowerDeaths,
        /// <summary>
        /// 
        /// </summary>
        GrenadeDeaths,
        /// <summary>
        /// 
        /// </summary>
        PistolDeaths,
        /// <summary>
        /// 
        /// </summary>
        KnifeDeaths,
        /// <summary>
        /// 
        /// </summary>
        AirstrikeDeaths,
        /// <summary>
        /// 
        /// </summary>
        TNTDeaths,
        /// <summary>
        /// 
        /// </summary>
        PunchDeaths,
        /// <summary>
        /// 
        /// </summary>
        HelmetDeaths,
        /// <summary>
        /// 
        /// </summary>
        WoundDeaths,
        /// <summary>
        /// 
        /// </summary>
        FlattenDeaths,
        /// <summary>
        /// 
        /// </summary>
        GunDeaths,
        /// <summary>
        /// 
        /// </summary>
        SliceInHalfDeaths,
        /// <summary>
        /// 
        /// </summary>
        GrenadeSuicides,
        /// <summary>
        /// 
        /// </summary>
        BlewUpSuicides,
        /// <summary>
        /// 
        /// </summary>
        SelfKillSuicides,
        /// <summary>
        /// 
        /// </summary>
        FallSuicides,
        /// <summary>
        /// 
        /// </summary>
        DrownSuicides,
        /// <summary>
        /// 
        /// </summary>
        SquashSuicides,
        /// <summary>
        /// 
        /// </summary>
        BarbedwireSuicides,
        /// <summary>
        /// 
        /// </summary>
        AirstrikeSuicides,
        /// <summary>
        /// 
        /// </summary>
        SelfBurnSuicides,
        /// <summary>
        /// 
        /// </summary>
        WrongPlaceSuicides,
        /// <summary>
        /// 
        /// </summary>
        SpawnKillSuicides,
        /// <summary>
        /// 
        /// </summary>
        Connections,
        /// <summary>
        /// 
        /// </summary>
        TeamJoins,
        /// <summary>
        /// 
        /// </summary>
        Speaches,
        /// <summary>
        /// 
        /// </summary>
        TeamChanges,
        /// <summary>
        /// 
        /// </summary>
        NameChanges,
        /// <summary>
        /// 
        /// </summary>
        Observer,
        /// <summary>
        /// 
        /// </summary>
        Destroyed,
        /// <summary>
        /// 
        /// </summary>
        Taken,
        /// <summary>
        /// 
        /// </summary>
        Word0,
        /// <summary>
        /// 
        /// </summary>
        Word1,
        /// <summary>
        /// 
        /// </summary>
        Word2,
        /// <summary>
        /// 
        /// </summary>
        Word3,
        /// <summary>
        /// 
        /// </summary>
        Word4,
        /// <summary>
        /// 
        /// </summary>
        Word5,
        /// <summary>
        /// 
        /// </summary>
        Word6,
        /// <summary>
        /// 
        /// </summary>
        Word7,
        /// <summary>
        /// 
        /// </summary>
        Word8,
        /// <summary>
        /// 
        /// </summary>
        Word9,
        /// <summary>
        /// 
        /// </summary>
        Word10,
        /// <summary>
        /// 
        /// </summary>
        Word11,
        /// <summary>
        /// 
        /// </summary>
        Word12,
        /// <summary>
        /// 
        /// </summary>
        Word13,
        /// <summary>
        /// 
        /// </summary>
        Word14,
        /// <summary>
        /// 
        /// </summary>
        Word15,
        /// <summary>
        /// 
        /// </summary>
        Word16,
        /// <summary>
        /// 
        /// </summary>
        Word17,
        /// <summary>
        /// 
        /// </summary>
        Word18,
        /// <summary>
        /// 
        /// </summary>
        Word19
    }

    #endregion
}