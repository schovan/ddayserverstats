using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Stats
{
    /// <summary>
    /// Form allowing user to change the program settings.
    /// </summary>
    public partial class FormOptions : Form
    {
        #region private members

        private const int mapLength = 32;
        private Settings settings;
        private string[] namesOpen;
        private string[] namesExport;
        private int[] map;
        private int[] mapPatternsToStats;

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the FormPreferences class. Loads settings and shows them in the form's tabs.
        /// </summary>
        /// <param name="settings">Instance of the settings class containing program settings.</param>
        public FormOptions(Settings settings)
        {
            InitializeComponent();
            this.settings = settings;
            namesOpen = Enum.GetNames(typeof(Patterns));
            namesExport = Enum.GetNames(typeof(Stats));
            mapPatternsToStats = settings.MapPatternsToStats;

            map = new int[mapLength];
            map[0] = (int)Patterns.Infantry;
            map[1] = (int)Patterns.Sniper;
            map[2] = (int)Patterns.Grenade;
            map[3] = (int)Patterns.LMG;
            map[4] = (int)Patterns.HMG;
            map[5] = (int)Patterns.SMG;
            map[6] = (int)Patterns.Pistol;
            map[7] = (int)Patterns.Knife;
            map[8] = (int)Patterns.Bazooka;
            map[9] = (int)Patterns.Flamethrower;
            map[10] = (int)Patterns.Wound;
            map[11] = (int)Patterns.Airstrike;
            map[12] = (int)Patterns.TNT;
            map[13] = (int)Patterns.Punch;
            map[14] = (int)Patterns.Helmet;
            map[15] = (int)Patterns.Flatten;
            map[16] = (int)Patterns.Gun;
            map[17] = (int)Patterns.SliceInHalf;
            map[18] = (int)Patterns.OwnGrenade;
            map[19] = (int)Patterns.BlewUp;
            map[20] = (int)Patterns.SelfKill;
            map[21] = (int)Patterns.Fall;
            map[22] = (int)Patterns.Drown;
            map[23] = (int)Patterns.Squash;
            map[24] = (int)Patterns.AirstrikeSuicide;
            map[25] = (int)Patterns.SelfBurn;
            map[26] = (int)Patterns.WrongPlace;
            map[27] = (int)Patterns.Barbedwire;
            map[28] = (int)Patterns.SpawnKill;
            map[29] = (int)Patterns.Destroyed;
            map[30] = (int)Patterns.Taken;
            map[31] = (int)Patterns.Observer;

            for (int i = 0; i < mapLength; i++)
            {
                listViewOpen.Items.Add(namesOpen[map[i]]);
                listViewOpen.Items[i].Checked = settings.ItemsOpen[map[i]];
            }
            for (int i = 0; i < Settings.Stop; i++)
            {
                listViewExport.Items.Add(namesExport[i]);
                listViewExport.Items[i].Checked = settings.ItemsExport[i];
            }
            for (int i = 0; i < Settings.WordsCount; i++)
            {
                listViewWords.Items.Add(settings.WordNames[i]);
                listViewWords.Items[i].Checked = settings.ItemsExport[i + Settings.Stop];
            }
            checkBoxWords.Checked = settings.Words;
            checkBoxConnections.Checked = settings.Connections;
            checkBoxPlusMinus.Checked = settings.PlusMinus;
            checkBoxKills.Checked = settings.Kills;
            textBoxKills.Text = settings.KillsMin.ToString();
            textBoxHost.Text = settings.Host;
            textBoxUser.Text = settings.User;
            textBoxPassword.Text = settings.Password;
            textBoxDatabase.Text = settings.Database;
            textBoxTable.Text = settings.Table;
            textBoxUrl.Text = settings.Url;
        }

        #endregion

        #region event handlers

        /// <summary>
        /// If the settings was changed, it is stored to the Settings instance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>   
        private void Ok(object sender, EventArgs e)
        {
            settings.Words = checkBoxWords.Checked;
            settings.Connections = checkBoxConnections.Checked;
            settings.PlusMinus = checkBoxPlusMinus.Checked;
            settings.Kills = checkBoxKills.Checked;
            for (int i = 0; i < Settings.KillsCount; i++)
            {
                settings.ItemsOpen[map[i]] = listViewOpen.Items[i].Checked;
                settings.ItemsVisible[mapPatternsToStats[map[i]]] = listViewOpen.Items[i].Checked;
                settings.ItemsVisible[mapPatternsToStats[map[i]] + Settings.KillsCount] = listViewOpen.Items[i].Checked;
            }
            for (int i = Settings.KillsCount; i < mapLength; i++)
            {
                settings.ItemsOpen[map[i]] = listViewOpen.Items[i].Checked;
                settings.ItemsVisible[mapPatternsToStats[map[i]]] = listViewOpen.Items[i].Checked;
            }
            for (int i = 0; i < Settings.Stop; i++)
            {
                settings.ItemsExport[i] = listViewExport.Items[i].Checked;
            }
            for (int i = 0; i < Settings.WordsCount; i++)
            {
                settings.WordNames[i] = listViewWords.Items[i].Text;
                settings.ItemsExport[i + Settings.Stop] = listViewWords.Items[i].Checked;
                if (settings.Words)
                {
                    settings.ItemsVisible[i + Settings.StatsCount - Settings.WordsCount] = listViewWords.Items[i].Checked;
                }
                else
                {
                    settings.ItemsVisible[i + Settings.StatsCount - Settings.WordsCount] = false;
                }
            }
            try
            {
                if (checkBoxKills.Checked)
                {
                    int killsNum = int.Parse(textBoxKills.Text);
                    settings.KillsMin = killsNum;
                }
                Close();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            settings.Host = textBoxHost.Text;
            settings.User = textBoxUser.Text;
            settings.Password = textBoxPassword.Text;
            settings.Database = textBoxDatabase.Text;
            settings.Table = textBoxTable.Text;
            settings.Url = textBoxUrl.Text;
        }

        /// <summary>
        /// Closes the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}