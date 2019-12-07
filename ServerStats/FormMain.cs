using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Xml.Serialization;
using System.Reflection;
using System.Web;
using System.Net;

namespace Stats
{
    /// <summary>
    /// Main window containing menu bar, tool bar, status bar, list box of the players and the stats tab.
    /// </summary>
    public partial class FormMain : Form
    {
        #region private constants

        public const string HtmlHeader =
@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""cs"" lang=""cs"">
<head>
<title>D-Day: Normandy Server Stats</title>
<meta http-equiv=""content-type"" content=""text/html; charset=windows-1250""  />
<meta http-equiv=""content-language"" content=""cs"" />
<script type=""text/javascript"">
<!--
function rowClick(element)
{
if (element.className == 'a')
  element.className = 'a1';
else if (element.className == 'b')
  element.className = 'b1';
else if (element.className == 'a1')
  element.className = 'a';
else if (element.className == 'b1')
  element.className = 'b';
}
//-->
</script>
<style type=""text/css"">
<!--
body {
margin: 0px;
padding: 0px;
border: none;
font-family: Verdana, Arial, sans-serif;
font-size: 11px;
line-height: 16px;
}
a {
color: Navy;
font-weight: bold;
}
a:link {
color: Navy;
text-decoration: none;
}
a:visited {
color: Navy;
text-decoration: none;
}
a:hover {
color: #BB0000;
text-decoration: none;
}
tr.a {
background-color: #DDDDDD;
}
tr.b {
background-color: #CCCCCC;
}
tr.a1 {
background-color: #F09050;
}
tr.b1 {
background-color: #F09050;
}
th {
background-color: #D3DCE3;
padding: 0px 4px 0px 4px;
}
td {
padding: 0px 4px 0px 4px;
}
.navy {
font-weight: bold;
color: Navy;
}
//-->
</style>
</head>";
        #endregion

        #region private members

        private SortedDictionary<string, Int32[]> players;
        private FormManual formHelp = null;
        private Settings settings;
        private TextBox[] textBoxes;
        private Label[] labels;
        private string[] names;
        private int[] map;

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the FormMain class.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            Assembly asm = Assembly.GetExecutingAssembly();
            this.Text += string.Format(" {0}", asm.GetName().Version.ToString());
            labels = new Label[Settings.WordsCount];
            textBoxes = new TextBox[Settings.StatsCount];
            names = Enum.GetNames(typeof(Stats));
            try
            {
                using (StreamReader sr = new StreamReader("settings.xml"))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Settings));
                    settings = (Settings)xs.Deserialize(sr);
                    map = settings.MapPatternsToStats;
                }
            }
            catch
            {
                settings = new Settings();
                map = settings.MapPatternsToStats;
                for (int i = 0; i < Settings.StatsCount; i++)
                {
                    settings.ItemsExport[i] = true;
                }
                int max = (int)Patterns.Flatten;
                for (int i = 0; i <= max; i++)
                {
                    settings.ItemsOpen[i] = true;
                    if (map[i] != -1)
                    {
                        settings.ItemsVisible[map[i]] = true;
                    }
                }
                for (int i = 0; i < Settings.Total - 1; i++)
                {
                    settings.ItemsVisible[i] = true;
                }
                for (int i = Settings.Total; i < Settings.Total + Settings.KillsCount; i++)
                {
                    if (settings.ItemsVisible[i])
                    {
                        settings.ItemsVisible[i + Settings.KillsCount] = true;
                    }
                }

                settings.WordNames[0] = "shit";
                settings.WordNames[1] = "fuck";
                settings.WordNames[2] = "wtf";
                settings.WordNames[3] = "omfg";
                settings.WordNames[4] = "stfu";
                settings.WordNames[5] = "asshole";
                settings.WordNames[6] = "bitch";
                settings.WordNames[7] = "arse";
                settings.WordNames[8] = "lamer";
                settings.WordNames[9] = "idiot";
                settings.WordNames[10] = "kua";
                settings.WordNames[11] = "kurva";
                settings.WordNames[12] = "kunda";
                settings.WordNames[13] = "prdel";
                settings.WordNames[14] = "curak";
                settings.WordNames[15] = "hovno";
                settings.WordNames[16] = "dement";
                settings.WordNames[17] = "mrd";
                settings.WordNames[18] = "pica";
                settings.WordNames[19] = "pici";
                settings.Words = true;
                if (settings.Words)
                {
                    for (int i = 0; i < Settings.WordsCount; i++)
                    {
                        settings.ItemsVisible[i + Settings.Stop] = true;
                    }
                    settings.ItemsVisible[Settings.Total - 1] = true;
                }

                settings.Connections = true;
                settings.PlusMinus = true;
                settings.Kills = true;
                settings.KillsMin = 200;
                settings.Url = "http://ddaystats.xf.cz/updatestats.php";
            }
            if (Thread.CurrentThread.CurrentUICulture.Name == "cs-CZ")
            {
                czechToolStripMenuItem.Checked = true;
            }
            else
            {
                englishToolStripMenuItem.Checked = true;
            }
            mappingLabelsAndTextBoxes();
            updateLabelsWords();
        }

        #endregion

        #region file menu event handlers

        /// <summary>
        /// Opens and parses log file and writes stats to textboxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.InitialDirectory = ".";
            openDialog.Filter = "Log files (*.log)|*.log|All files (*.*)|*.*";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                FormParser formParser = new FormParser(settings, openDialog.FileName);
                formParser.ShowDialog();
                if (formParser.Parser != null)
                {
                    SortedDictionary<string, Int32[]> tmpPlayers = formParser.Parser.Players;
                    if (tmpPlayers.Count != 0)
                    {
                        players = tmpPlayers;
                        updateLabelsWords();
                        updateListBoxPlayer();
                        updateToolStripMenuItem.Enabled = true;
                        toolStripButton8.Enabled = true;
                    }
                }
                formParser.Dispose();
            }
        }

        /// <summary>
        /// Close the forms and exit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit(object sender, EventArgs e)
        {
            if (formHelp != null)
            {
                formHelp.Close();
            }
            Close();
        }

        #endregion

        #region export menu event handlers

        /// <summary>
        /// Exports the stats of all players to a txt file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportTxt(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.InitialDirectory = ".";
            saveDialog.Filter = "Text files (*.txt)|*.txt";
            saveDialog.DefaultExt = "txt";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = File.CreateText(saveDialog.FileName))
                {
                    foreach (KeyValuePair<string, Int32[]> kvp in players)
                    {
                        sw.WriteLine("Name: {0}", kvp.Key);
                        for (int i = 0; i < Settings.Stop; i++)
                        {
                            if (settings.ItemsExport[i])
                            {
                                sw.Write("{0}: ", names[i]);
                                if (settings.ItemsVisible[i])
                                {
                                    sw.WriteLine(kvp.Value[i]);
                                }
                                else
                                {
                                    sw.WriteLine("N/A");
                                }
                            }
                        }
                        for (int i = 0; i < Settings.WordsCount; i++)
                        {
                            if (settings.ItemsExport[i + Settings.Stop])
                            {
                                sw.Write("{0}: ", settings.WordNames[i]);
                                if (settings.ItemsVisible[i])
                                {
                                    sw.WriteLine(kvp.Value[i + Settings.Stop]);
                                }
                                else
                                {
                                    sw.WriteLine("N/A");
                                }
                            }
                        }
                        sw.WriteLine("------------------------------");
                    }
                }
            }
        }

        /// <summary>
        /// Exports the stats of all players to a html file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportHtml(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.InitialDirectory = ".";
            saveDialog.Filter = "Html files (*.html)|*.html";
            saveDialog.DefaultExt = "html";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = File.CreateText(saveDialog.FileName))
                {
                    sw.WriteLine(HtmlHeader);
                    sw.WriteLine(@"<body>");
                    sw.WriteLine(@"<table>");
                    bool even = true;
                    int lineCounter = 0;
                    foreach (KeyValuePair<string, Int32[]> kvp in players)
                    {
                        if ((lineCounter++ % 25) == 0)
                        {
                            sw.Write(@"<tr>");
                            sw.Write(@"<th>");
                            sw.Write(@"&nbsp;");
                            sw.Write(@"</th>");
                            for (int i = 0; i < Settings.Stop; i++)
                            {
                                if (settings.ItemsExport[i])
                                {
                                    string name = names[i].Replace("Kills", " Kills");
                                    name = name.Replace("Deaths", " Deaths");
                                    name = name.Replace("Suicides", " Suicides");
                                    sw.Write(@"<th class=""navy"">");
                                    sw.Write(name);
                                    sw.Write(@"</th>");
                                }
                            }
                            for (int i = 0; i < Settings.WordsCount; i++)
                            {
                                if (settings.ItemsExport[i + Settings.Stop])
                                {
                                    sw.Write(@"<th class=""navy"">");
                                    sw.Write(htmlSpecialChars(settings.WordNames[i]));
                                    sw.Write(@"</th>");
                                }
                            }
                            sw.WriteLine(@"</tr>");
                        }
                        if (even)
                        {
                            sw.Write(@"<tr class=""a"" onClick=""rowClick(this);"">");
                        }
                        else
                        {
                            sw.Write(@"<tr class=""b"" onClick=""rowClick(this);"">");
                        }
                        even = !even;
                        sw.Write(@"<td>");
                        sw.Write(htmlSpecialChars(kvp.Key));
                        sw.Write(@"</td>");
                        for (int i = 0; i < Settings.StatsCount; i++)
                        {
                            if (settings.ItemsExport[i])
                            {
                                sw.Write(@"<td>");
                                if (settings.ItemsVisible[i])
                                {
                                    sw.Write(kvp.Value[i]);
                                }
                                else
                                {
                                    sw.Write("N/A");
                                }
                                sw.Write(@"</td>");
                            }
                        }
                        sw.WriteLine(@"</tr>");
                    }
                    sw.WriteLine(@"</table>");
                    sw.WriteLine(@"</body>");
                    sw.WriteLine(@"</html>");
                }
            }
        }

        /// <summary>
        /// Exports the stats of all players to a sql file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportSql(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.InitialDirectory = ".";
            saveDialog.Filter = "Sql files (*.sql)|*.sql";
            saveDialog.DefaultExt = "sql";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = File.CreateText(saveDialog.FileName))
                {
                    sw.Write(CreateSql());
                }
            }
        }

        #endregion

        #region options menu event handlers

        /// <summary>
        /// Changes the language to english.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void English(object sender, EventArgs e)
        {
            if (englishToolStripMenuItem.Checked == false)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US", false);
                rebuildForm();
                englishToolStripMenuItem.Checked = true;
            }
        }

        /// <summary>
        /// Changes the language to czech.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Czech(object sender, EventArgs e)
        {
            if (czechToolStripMenuItem.Checked == false)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("cs-CZ", false);
                rebuildForm();
                czechToolStripMenuItem.Checked = true;
            }
        }

        /// <summary>
        /// Shows FormPreferences form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsHandler(object sender, EventArgs e)
        {
            FormOptions formPreferences = new FormOptions(settings);
            formPreferences.ShowDialog();
            formPreferences.Dispose();
        }

        #endregion

        #region database menu event handlers

        private void Update(object sender, EventArgs e)
        {
            FormUpdate formUpdate = new FormUpdate(settings, CreateContent());
            formUpdate.ShowDialog();
            formUpdate.Dispose();
        }

        #endregion

        #region help menu event handlers

        /// <summary>
        /// Shows FormHelp form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Manual(object sender, EventArgs e)
        {
            if (formHelp == null)
            {
                formHelp = new FormManual();
            }
            formHelp.Show();
        }

        /// <summary>
        /// Shows FormAbout form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void About(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.ShowDialog();
            formAbout.Dispose();
        }

        #endregion

        #region other event handlers

        /// <summary>
        /// Closes the form and saves the settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("settings.xml"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Settings));
                xs.Serialize(sw, settings);
            }
        }

        /// <summary>
        /// Updates textboxes to the right values when the listbox's index is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxPlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Settings.StatsCount; i++)
            {
                if (settings.ItemsVisible[i])
                {
                    textBoxes[i].Text = players[listBoxPlayer.Text][i].ToString();
                }
                else
                {
                    textBoxes[i].Text = "";
                }
            }
        }

        #endregion

        #region private methods

        private string CreateContent()
        {
            string query = HttpUtility.UrlEncode(CreateSql());
            int sbLength = query.Length + 70;
            StringBuilder sb = new StringBuilder(sbLength);
            sb.Append(string.Format("host={0}&", settings.Host));
            sb.Append(string.Format("user={0}&", settings.User));
            sb.Append(string.Format("password={0}&", settings.Password));
            sb.Append(string.Format("database={0}&", settings.Database));
            sb.Append(string.Format("query={0}&", query));
            sb.Append("action=Update");
            return sb.ToString();
        }

        private string CreateSql()
        {
            int sbLength = 400 * players.Count + 4000;
            StringBuilder sb = new StringBuilder(sbLength);
            sb.Append(string.Format("DROP TABLE IF EXISTS `{0}`;\n", settings.Table));
            sb.Append(string.Format("CREATE TABLE `{0}` (\n", settings.Table));
            sb.Append("`ID` int(11) NOT NULL default '0',\n");
            sb.Append("`Name` varchar(30) NOT NULL default '',\n");
            for (int i = 0; i < Settings.Stop; i++)
            {
                if (settings.ItemsExport[i])
                {
                    sb.Append(string.Format("`{0}` int(11) default NULL,\n", names[i]));
                }
            }
            for (int i = 0; i < Settings.WordsCount; i++)
            {
                if (settings.ItemsExport[i + Settings.Stop])
                {
                    sb.Append(string.Format("`{0}` int(11) default NULL,\n", addSlashes(settings.WordNames[i])));
                }
            }
            sb.Append("PRIMARY KEY (`ID`)\n");
            sb.Append(") ENGINE=MyISAM;\n");
            int id = 1;
            foreach (KeyValuePair<string, Int32[]> kvp in players)
            {
                sb.Append(string.Format("INSERT INTO `{0}` VALUES ({1}, '{2}'", settings.Table, id++, addSlashes(kvp.Key)));
                for (int i = 0; i < Settings.StatsCount; i++)
                {
                    if (settings.ItemsExport[i])
                    {
                        if (settings.ItemsVisible[i])
                        {
                            sb.Append(string.Format(", {0}", kvp.Value[i]));
                        }
                        else
                        {
                            sb.Append(string.Format(", NULL"));
                        }
                    }
                }
                sb.Append(");\n");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Creates arrays of references to word labels and stat textboxes.
        /// </summary>
        private void mappingLabelsAndTextBoxes()
        {
            labels[0] = labelWord0;
            labels[1] = labelWord1;
            labels[2] = labelWord2;
            labels[3] = labelWord3;
            labels[4] = labelWord4;
            labels[5] = labelWord5;
            labels[6] = labelWord6;
            labels[7] = labelWord7;
            labels[8] = labelWord8;
            labels[9] = labelWord9;
            labels[10] = labelWord10;
            labels[11] = labelWord11;
            labels[12] = labelWord12;
            labels[13] = labelWord13;
            labels[14] = labelWord14;
            labels[15] = labelWord15;
            labels[16] = labelWord16;
            labels[17] = labelWord17;
            labels[18] = labelWord18;
            labels[19] = labelWord19;
            textBoxes[(int)Stats.InfantryKills] = textBoxInfantryKill;
            textBoxes[(int)Stats.SniperKills] = textBoxSniperKill;
            textBoxes[(int)Stats.SMGKills] = textBoxSMGKill;
            textBoxes[(int)Stats.LMGKills] = textBoxLMGKill;
            textBoxes[(int)Stats.HMGKills] = textBoxHMGKill;
            textBoxes[(int)Stats.BazookaKills] = textBoxBazookaKill;
            textBoxes[(int)Stats.FlamethrowerKills] = textBoxFlamethrowerKill;
            textBoxes[(int)Stats.GrenadeKills] = textBoxGrenadeKill;
            textBoxes[(int)Stats.PistolKills] = textBoxPistolKill;
            textBoxes[(int)Stats.KnifeKills] = textBoxKnifeKill;
            textBoxes[(int)Stats.AirstrikeKills] = textBoxAirstrikeKill;
            textBoxes[(int)Stats.TNTKills] = textBoxTNTKill;
            textBoxes[(int)Stats.PunchKills] = textBoxPunchKill;
            textBoxes[(int)Stats.HelmetKills] = textBoxHelmetKill;
            textBoxes[(int)Stats.WoundKills] = textBoxWoundKill;
            textBoxes[(int)Stats.FlattenKills] = textBoxFlattenKill;
            textBoxes[(int)Stats.GunKills] = textBoxGunKill;
            textBoxes[(int)Stats.SliceInHalfKills] = textBoxSliceInHalfKill;
            textBoxes[(int)Stats.InfantryDeaths] = textBoxInfantryDeath;
            textBoxes[(int)Stats.SniperDeaths] = textBoxSniperDeath;
            textBoxes[(int)Stats.SMGDeaths] = textBoxSMGDeath;
            textBoxes[(int)Stats.LMGDeaths] = textBoxLMGDeath;
            textBoxes[(int)Stats.HMGDeaths] = textBoxHMGDeath;
            textBoxes[(int)Stats.BazookaDeaths] = textBoxBazookaDeath;
            textBoxes[(int)Stats.FlamethrowerDeaths] = textBoxFlamethrowerDeath;
            textBoxes[(int)Stats.GrenadeDeaths] = textBoxGrenadeDeath;
            textBoxes[(int)Stats.PistolDeaths] = textBoxPistolDeath;
            textBoxes[(int)Stats.KnifeDeaths] = textBoxKnifeDeath;
            textBoxes[(int)Stats.AirstrikeDeaths] = textBoxAirstrikeDeath;
            textBoxes[(int)Stats.TNTDeaths] = textBoxTNTDeath;
            textBoxes[(int)Stats.PunchDeaths] = textBoxPunchDeath;
            textBoxes[(int)Stats.HelmetDeaths] = textBoxHelmetDeath;
            textBoxes[(int)Stats.WoundDeaths] = textBoxWoundDeath;
            textBoxes[(int)Stats.FlattenDeaths] = textBoxFlattenDeath;
            textBoxes[(int)Stats.GunDeaths] = textBoxGunDeath;
            textBoxes[(int)Stats.SliceInHalfDeaths] = textBoxSliceInHalfDeath;
            textBoxes[(int)Stats.GrenadeSuicides] = textBoxOwnGrenade;
            textBoxes[(int)Stats.BlewUpSuicides] = textBoxBlewUp;
            textBoxes[(int)Stats.SelfKillSuicides] = textBoxSelfKill;
            textBoxes[(int)Stats.FallSuicides] = textBoxFall;
            textBoxes[(int)Stats.DrownSuicides] = textBoxDrown;
            textBoxes[(int)Stats.SquashSuicides] = textBoxSquash;
            textBoxes[(int)Stats.BarbedwireSuicides] = textBoxBarbedwire;
            textBoxes[(int)Stats.AirstrikeSuicides] = textBoxAirstrikeSuicide;
            textBoxes[(int)Stats.SelfBurnSuicides] = textBoxSelfBurnSuicides;
            textBoxes[(int)Stats.WrongPlaceSuicides] = textBoxWrongPlaceSuicides;
            textBoxes[(int)Stats.SpawnKillSuicides] = textBoxSpawnKillSuicides;
            textBoxes[(int)Stats.Connections] = textBoxConnections;
            textBoxes[(int)Stats.TeamJoins] = textBoxTeamJoins;
            textBoxes[(int)Stats.NameChanges] = textBoxNameChanges;
            textBoxes[(int)Stats.TeamChanges] = textBoxTeamSwitches;
            textBoxes[(int)Stats.Destroyed] = textBoxDestroyed;
            textBoxes[(int)Stats.Taken] = textBoxTaken;
            textBoxes[(int)Stats.Speaches] = textBoxSpeaches;
            textBoxes[(int)Stats.Percentage] = textBoxPercentage;
            textBoxes[(int)Stats.PlusMinus] = textBoxPlusMinus;
            textBoxes[(int)Stats.Kills] = textBoxKills;
            textBoxes[(int)Stats.Deaths] = textBoxDeaths;
            textBoxes[(int)Stats.Suicides] = textBoxSuicides;
            textBoxes[(int)Stats.TeamKills] = textBoxTeamKills;
            textBoxes[(int)Stats.Points] = textBoxPoints;
            textBoxes[(int)Stats.Words] = textBoxWords;
            textBoxes[(int)Stats.Observer] = textBoxObserver;
            textBoxes[(int)Stats.Word0] = textBoxWord0;
            textBoxes[(int)Stats.Word1] = textBoxWord1;
            textBoxes[(int)Stats.Word2] = textBoxWord2;
            textBoxes[(int)Stats.Word3] = textBoxWord3;
            textBoxes[(int)Stats.Word4] = textBoxWord4;
            textBoxes[(int)Stats.Word5] = textBoxWord5;
            textBoxes[(int)Stats.Word6] = textBoxWord6;
            textBoxes[(int)Stats.Word7] = textBoxWord7;
            textBoxes[(int)Stats.Word8] = textBoxWord8;
            textBoxes[(int)Stats.Word9] = textBoxWord9;
            textBoxes[(int)Stats.Word10] = textBoxWord10;
            textBoxes[(int)Stats.Word11] = textBoxWord11;
            textBoxes[(int)Stats.Word12] = textBoxWord12;
            textBoxes[(int)Stats.Word13] = textBoxWord13;
            textBoxes[(int)Stats.Word14] = textBoxWord14;
            textBoxes[(int)Stats.Word15] = textBoxWord15;
            textBoxes[(int)Stats.Word16] = textBoxWord16;
            textBoxes[(int)Stats.Word17] = textBoxWord17;
            textBoxes[(int)Stats.Word18] = textBoxWord18;
            textBoxes[(int)Stats.Word19] = textBoxWord19;
        }

        /// <summary>
        /// Updates the labels to the right values.
        /// </summary>
        private void updateLabelsWords()
        {
            for (int i = 0; i < Settings.WordsCount; i++)
            {
                labels[i].Text = settings.WordNames[i];
            }
        }

        /// <summary>
        /// Updates listbox of the players to the right player names.
        /// </summary>
        private void updateListBoxPlayer()
        {
            listBoxPlayer.Items.Clear();
            clearTextBoxes();
            foreach (object o in players.Keys)
            {
                listBoxPlayer.Items.Add(o);
            }
            if (txtToolStripMenuItem.Enabled == false)
            {
                txtToolStripMenuItem.Enabled = true;
                htmlToolStripMenuItem.Enabled = true;
                sqlToolStripMenuItem.Enabled = true;
                toolStripButton2.Enabled = true;
                toolStripButton3.Enabled = true;
                toolStripButton4.Enabled = true;
            }
        }

        /// <summary>
        /// Sets all textboxes to the empty value.
        /// </summary>
        private void clearTextBoxes()
        {
            for (int i = 0; i < Settings.StatsCount; i++)
            {
                textBoxes[i].Text = "";
            }
        }

        /// <summary>
        /// Does the same as appropriate php function.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string htmlSpecialChars(string s)
        {
            s = s.Replace("<", "&lt;");
            s = s.Replace(">", "&gt;");
            return s;
        }

        /// <summary>
        /// Does the same as appropriate php function.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string addSlashes(string s)
        {
            return s.Replace("'", @"\'");
        }

        /// <summary>
        /// Disposes all forms and recreates them after language is switched.
        /// </summary>
        private void rebuildForm()
        {
            int listIndex = listBoxPlayer.SelectedIndex;
            int tabIndex = tabControl1.SelectedIndex;
            if (formHelp != null)
            {
                formHelp.Dispose();
            }
            statusStrip1.Dispose();
            splitContainer1.Dispose();
            toolStrip1.Dispose();
            menuStrip1.Dispose();
            InitializeComponent();
            mappingLabelsAndTextBoxes();
            updateLabelsWords();
            formHelp = null;
            tabControl1.SelectedIndex = tabIndex;
            if (players != null)
            {
                if (players.Count != 0)
                {
                    updateListBoxPlayer();
                    if (listIndex != -1)
                    {
                        listBoxPlayer.SelectedIndex = listIndex;
                    }
                }
            }
            listBoxPlayer.Focus();
        }

        #endregion
    }
}
