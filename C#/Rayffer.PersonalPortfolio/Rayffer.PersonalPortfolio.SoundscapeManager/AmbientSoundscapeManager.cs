using NAudio.Wave;
using Ookii.Dialogs.WinForms;
using Rayffer.PersonalPortfolio.SoundscapeManager.Controls;
using Rayffer.PersonalPortfolio.SoundscapeManager.DTO;
using Rayffer.PersonalPortfolio.SoundscapeManager.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Rayffer.PersonalPortfolio.SoundscapeManager
{
    public partial class AmbientSoundscapeManager : Form
    {
        public List<string> supportedAudioExtensions;
        public string soundscapeExtension;
        public string soundCollectionExtension;
        public WaveOutEvent generalVolumeControl;

        public AmbientSoundscapeManager()
        {
            InitializeComponent();
            soundscapeExtension = ".jsonsoundscape";
            soundCollectionExtension = ".jsonsoundcollection";
            currentDirectoryLabel.Text = ConfigurationManager.AppSettings["AmbientSounds"];
            generalVolumeControl = new WaveOutEvent();
            supportedAudioExtensions = ConfigurationManager.AppSettings["SupportedAudioExtensions"].Split(';').ToList();
            LoadSounds(ConfigurationManager.AppSettings["AmbientSounds"]);
            savedSoundscapesDirectoryLabel.Text = ConfigurationManager.AppSettings["SoundscapeConfigurationLocation"];
            LoadSoundscapes(ConfigurationManager.AppSettings["SoundscapeConfigurationLocation"]);
            soundCollectionsDirectoryLabel.Text = ConfigurationManager.AppSettings["SoundscapeCollectionLocation"];
            LoadSoundCollections(ConfigurationManager.AppSettings["SoundscapeCollectionLocation"]);
            listBoxSavedSoundScapes.SelectedIndexChanged += listBoxSavedSoundScapes_SelectedIndexChanged;
        }

        ~AmbientSoundscapeManager()
        {
            listBoxSavedSoundScapes.SelectedIndexChanged -= listBoxSavedSoundScapes_SelectedIndexChanged;
        }

        private void selectDirectoryButton_Click(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog folderBrowserDialog = new VistaFolderBrowserDialog();
            folderBrowserDialog.SelectedPath = Environment.CurrentDirectory;
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                LoadSounds(folderBrowserDialog.SelectedPath);
        }

        private void LoadSounds(string selectedPath)
        {
            ClearLayoutControlsSafe();

            currentDirectoryLabel.Text = selectedPath;

            if (!System.IO.Directory.Exists(selectedPath))
                System.IO.Directory.CreateDirectory(selectedPath);

            var ambientSoundEffectsPlayers =
                Directory
                .GetFiles(selectedPath)?
                .Where(file => supportedAudioExtensions.Any(ext => ext.Equals(Path.GetExtension(file))))?
                .Select(ambientSoundEffect => new AmbientSoundEffectPlayer(ambientSoundEffect))?
                .ToArray();

            soundsLayoutPanel.Controls.AddRange(ambientSoundEffectsPlayers);
        }

        private void LoadSoundscapes(string selectedPath)
        {
            soundscapeInformationBindingSource.DataSource = new List<SoundscapeInformation>();

            savedSoundscapesDirectoryLabel.Text = selectedPath;

            if (!System.IO.Directory.Exists(selectedPath))
                System.IO.Directory.CreateDirectory(selectedPath);

            var ambientSoundEffectsPlayers =
                Directory
                .GetFiles(selectedPath)?
                .Where(file => Path.GetExtension(file).Equals(soundscapeExtension))?
                .Select(ambientSoundEffect => JsonTools.ReadFromJsonFile<SoundscapeInformation>(ambientSoundEffect))?
                .ToList();

            soundscapeInformationBindingSource.DataSource = ambientSoundEffectsPlayers;
            soundscapeInformationBindingSource.ResetBindings(false);
        }

        private void ClearLayoutControlsSafe()
        {
            foreach (Control controlToClear in soundsLayoutPanel.Controls)
            {
                ClearControlsSafeRecursive((AmbientSoundEffectPlayer)controlToClear);
            }
            soundsLayoutPanel.Controls.Clear();
        }

        private void ClearControlsSafeRecursive(AmbientSoundEffectPlayer controlToClear)
        {
            controlToClear.Dispose();
        }

        private void AmbientSoundscapeManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClearLayoutControlsSafe();
            GC.Collect();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            generalVolumeControl.Volume = trackBar1.Value / (float)trackBar1.Maximum;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FilterSounds();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            soundsLayoutPanel.SuspendLayout();
            soundScapeFilterTextBox.Text = string.Empty;
            soundCollectionsCombobox.SelectedIndexChanged -= soundCollectionsCombobox_SelectedIndexChanged;
            soundCollectionsCombobox.SelectedIndex = 0;
            soundCollectionsCombobox.SelectedIndexChanged += soundCollectionsCombobox_SelectedIndexChanged;
            foreach (Control controlToFilter in soundsLayoutPanel.Controls)
            {
                controlToFilter.Visible = true;
            }
            soundsLayoutPanel.ResumeLayout();
        }

        private void saveSoundscapeButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(soundscapeNameTextBox.Text))
            {
                MessageBox.Show("Please, specify a name for the soundscape to save", "Error while saving soundscape", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            SoundscapeInformation soundscapeInformation = new SoundscapeInformation();
            soundscapeInformation.Name = $"{soundscapeNameTextBox.Text}";
            foreach (AmbientSoundEffectPlayer soundInformation in soundsLayoutPanel.Controls)
            {
                if (soundInformation.loopAudioCheckBox.Checked)
                {
                    soundscapeInformation.SoundscapeSounds.Add(new SoundscapeSound()
                    {
                        IntervalSeconds = soundInformation.loopIntervalComboBox.SelectedIndex,
                        IsLooping = soundInformation.loopAudioCheckBox.Checked,
                        Name = soundInformation.Name,
                        Volume = soundInformation.volumeTrackBar.Value
                    });
                }
            }
            JsonTools.WriteToJsonFile($"{savedSoundscapesDirectoryLabel.Text}/{soundscapeInformation.Name}{soundscapeExtension}", soundscapeInformation, false);
            LoadSoundscapes(savedSoundscapesDirectoryLabel.Text);

            soundscapeNameTextBox.Text = string.Empty;

            MessageBox.Show("Soundscape saved succesfully", "Soundscape saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void listBoxSavedSoundScapes_SelectedIndexChanged(object sender, EventArgs e)
        {
            SoundscapeInformation soundscapeInformation = listBoxSavedSoundScapes.SelectedItem as SoundscapeInformation;
            if (soundscapeInformation != null)
            {
                foreach (AmbientSoundEffectPlayer ambientSoundEffectPlayer in soundsLayoutPanel.Controls)
                {
                    ambientSoundEffectPlayer.ExternalStop();
                }
                soundscapeInformation.SoundscapeSounds.ForEach(sound =>
                {
                    AmbientSoundEffectPlayer player = (AmbientSoundEffectPlayer)soundsLayoutPanel.Controls.Find(sound.Name, false).FirstOrDefault();

                    if (player != null)
                    {
                        player.volumeTrackBar.Value = sound.Volume;
                        player.loopAudioCheckBox.Checked = sound.IsLooping;
                        player.loopIntervalComboBox.SelectedIndex = sound.IntervalSeconds;
                        if (sound.IsLooping)
                            player.ExternalStart();
                        else
                            player.ExternalStop();
                    }
                });
            }
        }

        public class MyPanel : FlowLayoutPanel
        {
            public MyPanel()
            {
                this.SetStyle(
                    System.Windows.Forms.ControlStyles.UserPaint |
                    System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                    System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
                    true);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (AmbientSoundEffectPlayer soundInformation in soundsLayoutPanel.Controls)
            {
                soundInformation.ExternalStop();
            }
        }

        private void selectSavedSoundscapesButton_Click(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog folderBrowserDialog = new VistaFolderBrowserDialog();
            folderBrowserDialog.SelectedPath = Environment.CurrentDirectory;
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                LoadSoundscapes(folderBrowserDialog.SelectedPath);
        }

        private void searchSoundCollectionDirectoryButton_Click(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog folderBrowserDialog = new VistaFolderBrowserDialog();
            folderBrowserDialog.SelectedPath = Environment.CurrentDirectory;
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                LoadSoundCollections(folderBrowserDialog.SelectedPath);
        }

        private void LoadSoundCollections(string selectedPath)
        {
            List<SoundCollection> soundCollections = new List<SoundCollection>();
            soundCollections.Add(
                new SoundCollection()
                {
                    Name = "None"
                });

            soundCollectionBindingSource.DataSource = new List<SoundCollection>();

            soundCollectionsDirectoryLabel.Text = selectedPath;

            if (!System.IO.Directory.Exists(selectedPath))
                System.IO.Directory.CreateDirectory(selectedPath);

            var savedSoundCollections =
                Directory
                .GetFiles(selectedPath)?
                .Where(file => Path.GetExtension(file).Equals(soundCollectionExtension))?
                .Select(ambientSoundEffect => JsonTools.ReadFromJsonFile<SoundCollection>(ambientSoundEffect))?
                .ToList();

            soundCollections.AddRange(savedSoundCollections);
            soundCollectionBindingSource.DataSource = soundCollections;
            soundCollectionBindingSource.ResetBindings(false);
        }

        private void saveCurrentSoundCollectionButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(soundCollectionNameTextBox.Text))
            {
                MessageBox.Show("Please, specify a name for the sound collection to save", "Error while saving the sound collection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            SoundCollection soundCollection = new SoundCollection();
            soundCollection.Name = $"{soundCollectionNameTextBox.Text}";
            foreach (AmbientSoundEffectPlayer soundInformation in soundsLayoutPanel.Controls)
            {
                if (soundInformation.Selected)
                    soundCollection.SoundNames.Add(soundInformation.Name);
            }
            if (soundCollection.SoundNames.Any())
            {
                JsonTools.WriteToJsonFile($"{soundCollectionsDirectoryLabel.Text}/{soundCollection.Name}{soundCollectionExtension}", soundCollection, false);
                LoadSoundCollections(soundCollectionsDirectoryLabel.Text);

                soundscapeNameTextBox.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Please, select sounds to save in the collection", "Unable to save the sound collection");
            }
            foreach (AmbientSoundEffectPlayer soundInformation in soundsLayoutPanel.Controls)
            {
                soundInformation.Selected = false;
            }
            MessageBox.Show("Soundscape saved succesfully", "Soundscape saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void soundCollectionsCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterSounds();
        }

        private void FilterSounds()
        {
            soundsLayoutPanel.SuspendLayout();

            if (soundCollectionsCombobox.SelectedIndex == 0)
            {
                foreach (Control controlToFilter in soundsLayoutPanel.Controls)
                {
                    controlToFilter.Visible = controlToFilter.Name.Contains(soundScapeFilterTextBox.Text);
                }
            }
            else
            {
                SoundCollection soundCollectionInformation = soundCollectionsCombobox.SelectedItem as SoundCollection;

                if (soundCollectionInformation != null)
                {
                    foreach (Control controlToFilter in soundsLayoutPanel.Controls)
                    {
                        controlToFilter.Visible = controlToFilter.Name.Contains(soundScapeFilterTextBox.Text) &&
                                soundCollectionInformation.SoundNames.Contains(controlToFilter.Name);
                    }
                }
            }
            soundsLayoutPanel.ResumeLayout();
        }

        private void deleteSoundScapeButton_Click(object sender, EventArgs e)
        {
            if (listBoxSavedSoundScapes.SelectedItem is SoundscapeInformation soundscapeInformation)
            {
                DialogResult deleteResult = MessageBox.Show("Are you sure you want to delete this soundscape?", "Confirm soundscape deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (deleteResult == DialogResult.Yes)
                {
                    System.IO.File.Delete($"{savedSoundscapesDirectoryLabel.Text}/{soundscapeInformation.Name}.jsonsoundscape");
                    (soundscapeInformationBindingSource.DataSource as List<SoundscapeInformation>).Remove(soundscapeInformation);
                    soundscapeInformationBindingSource.ResetBindings(false);
                    foreach (AmbientSoundEffectPlayer soundInformation in soundsLayoutPanel.Controls)
                    {
                        soundInformation.ExternalStop();
                    }
                }
            }
            else
            {
                MessageBox.Show("The selected item to delete is not a soundscape", "Can't delete selected item");
            }
        }
    }
}