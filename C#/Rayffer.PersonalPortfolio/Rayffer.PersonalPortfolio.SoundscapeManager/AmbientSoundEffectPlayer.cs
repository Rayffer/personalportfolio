using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Rayffer.PersonalPortfolio.SoundscapeManager.NAudioStreams;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rayffer.PersonalPortfolio.SoundscapeManager.Controls
{
    public partial class AmbientSoundEffectPlayer : UserControl
    {
        private static List<Types.LoopIntervals> loopIntervals;
        private WaveOutEvent waveOutEvent;
        private AudioFileReader ambientSoundEffect;
        private LoopStream loopStream;
        private string filepath;
        private static Image playButtonImage;
        private static Image stopButtonImage;
        private static Image disabledStopButtonImage;
        private bool selected;

        public bool Selected
        {
            get => selected;
            set
            {
                selected = value;
                UpdateControl();
            }
        }

        public AmbientSoundEffectPlayer(string filepath)
        {
            this.SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
                true);
            InitializeComponent();

            this.Name = System.IO.Path.GetFileNameWithoutExtension(filepath);
            if (loopIntervals == null)
            {
                loopIntervals = Enum.GetValues(typeof(Types.LoopIntervals)).Cast<Types.LoopIntervals>().ToList();
            }
            loopIntervalComboBox.DataSource = loopIntervals;

            waveOutEvent = new WaveOutEvent();
            waveOutEvent.PlaybackStopped += WaveOutEvent_PlaybackStopped;

            label1.Text = System.IO.Path.GetFileNameWithoutExtension(filepath);
            this.filepath = filepath;
            ambientSoundPlayerButton.Padding = new Padding(10);
            loopIntervalComboBox.SelectedIndex = 0;

            if (playButtonImage == null)
            {
                playButtonImage =
                    Rayffer.PersonalPortfolio.SoundscapeManager.Properties.Resources.playerPlayArrow.GetThumbnailImage(
                    ambientSoundPlayerButton.Width - (ambientSoundPlayerButton.Padding.Left + ambientSoundPlayerButton.Padding.Right),
                    ambientSoundPlayerButton.Height - (ambientSoundPlayerButton.Padding.Top + ambientSoundPlayerButton.Padding.Bottom),
                    () => false,
                    IntPtr.Zero);
            }
            if (stopButtonImage == null)
            {
                stopButtonImage =
                    Rayffer.PersonalPortfolio.SoundscapeManager.Properties.Resources.playerStop.GetThumbnailImage(
                    ambientSoundPlayerButton.Width - (ambientSoundPlayerButton.Padding.Left + ambientSoundPlayerButton.Padding.Right),
                    ambientSoundPlayerButton.Height - (ambientSoundPlayerButton.Padding.Top + ambientSoundPlayerButton.Padding.Bottom),
                    () => false,
                    IntPtr.Zero);
            }
            if (disabledStopButtonImage == null)
            {
                disabledStopButtonImage =
                    Rayffer.PersonalPortfolio.SoundscapeManager.Properties.Resources.playerStop.GetThumbnailImage(
                    ambientSoundPlayerButton.Width - (ambientSoundPlayerButton.Padding.Left + ambientSoundPlayerButton.Padding.Right),
                    ambientSoundPlayerButton.Height - (ambientSoundPlayerButton.Padding.Top + ambientSoundPlayerButton.Padding.Bottom),
                    () => false,
                    IntPtr.Zero);
            }

            CalculateButtonImage();
        }

        private void InitSound(string filepath)
        {
            if (filepath.EndsWith(".ogg", StringComparison.OrdinalIgnoreCase))
            {
                string destPath = filepath.Replace("\\", "/").Replace(".ogg", string.Empty) + ".wav";
                if (!System.IO.File.Exists(destPath))
                {
                    using (var vorbis = new NVorbis.VorbisReader(filepath.Replace("\\", "/")))
                    using (var outFile = System.IO.File.Create(destPath))
                    using (var writer = new System.IO.BinaryWriter(outFile))
                    {
                        writer.Write(Encoding.ASCII.GetBytes("RIFF"));
                        writer.Write(0);
                        writer.Write(Encoding.ASCII.GetBytes("WAVE"));
                        writer.Write(Encoding.ASCII.GetBytes("fmt "));
                        writer.Write(18);
                        writer.Write((short)1); // PCM format
                        writer.Write((short)vorbis.Channels);
                        writer.Write(vorbis.SampleRate);
                        writer.Write(vorbis.SampleRate * vorbis.Channels * 2);  // avg bytes per second
                        writer.Write((short)(2 * vorbis.Channels)); // block align
                        writer.Write((short)16); // bits per sample
                        writer.Write((short)0); // extra size

                        writer.Write(Encoding.ASCII.GetBytes("data"));
                        writer.Flush();
                        var dataPos = outFile.Position;
                        writer.Write(0);

                        var buf = new float[vorbis.SampleRate / 10 * vorbis.Channels];
                        int count;
                        while ((count = vorbis.ReadSamples(buf, 0, buf.Length)) > 0)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                var temp = (int)(32767f * buf[i]);
                                if (temp > 32767)
                                {
                                    temp = 32767;
                                }
                                else if (temp < -32768)
                                {
                                    temp = -32768;
                                }
                                writer.Write((short)temp);
                            }
                        }
                        writer.Flush();

                        writer.Seek(4, System.IO.SeekOrigin.Begin);
                        writer.Write((int)(outFile.Length - 8L));

                        writer.Seek((int)dataPos, System.IO.SeekOrigin.Begin);
                        writer.Write((int)(outFile.Length - dataPos - 4L));

                        writer.Flush();

                        this.filepath = destPath;
                    }
                    System.IO.File.Delete(filepath);
                }
                else
                    this.filepath = destPath;
            }

            if (waveOutEvent.PlaybackState != PlaybackState.Stopped)
            {
                waveOutEvent.PlaybackStopped -= WaveOutEvent_PlaybackStopped;
                waveOutEvent.Stop();
                waveOutEvent.PlaybackStopped += WaveOutEvent_PlaybackStopped;
            }

            if (ambientSoundEffect == null)
                ambientSoundEffect = new AudioFileReader(this.filepath);
            else
                ambientSoundEffect.CurrentTime = new System.TimeSpan(0);

            ambientSoundEffect.Volume = volumeTrackBar.Value / (float)volumeTrackBar.Maximum;
            OffsetSampleProvider offsetSampleProvider = new OffsetSampleProvider(ambientSoundEffect);

            if (loopStream == null)
                loopStream = new LoopStream(ambientSoundEffect);

            if (loopAudioCheckBox.Checked)
            {
                switch ((Types.LoopIntervals)loopIntervalComboBox.SelectedItem)
                {
                    case Types.LoopIntervals.NoInterval:
                        waveOutEvent.Init(loopStream);
                        return;

                    case Types.LoopIntervals._5_Seconds:
                        offsetSampleProvider.DelayBy = TimeSpan.FromSeconds(5);
                        break;

                    case Types.LoopIntervals._10_Seconds:
                        offsetSampleProvider.DelayBy = TimeSpan.FromSeconds(10);
                        break;

                    case Types.LoopIntervals._15_Seconds:
                        offsetSampleProvider.DelayBy = TimeSpan.FromSeconds(15);
                        break;

                    case Types.LoopIntervals._20_Seconds:
                        offsetSampleProvider.DelayBy = TimeSpan.FromSeconds(20);
                        break;

                    case Types.LoopIntervals._25_Seconds:
                        offsetSampleProvider.DelayBy = TimeSpan.FromSeconds(25);
                        break;

                    case Types.LoopIntervals._30_Seconds:
                        offsetSampleProvider.DelayBy = TimeSpan.FromSeconds(30);
                        break;

                    case Types.LoopIntervals._45_Seconds:
                        offsetSampleProvider.DelayBy = TimeSpan.FromSeconds(45);
                        break;

                    case Types.LoopIntervals._1_Minute:
                        offsetSampleProvider.DelayBy = TimeSpan.FromMinutes(1);
                        break;

                    case Types.LoopIntervals._2_Minutes:
                        offsetSampleProvider.DelayBy = TimeSpan.FromSeconds(2);
                        break;

                    case Types.LoopIntervals._3_Minutes:
                        offsetSampleProvider.DelayBy = TimeSpan.FromSeconds(3);
                        break;
                }
            }
            waveOutEvent.Init(offsetSampleProvider);
        }

        private void WaveOutEvent_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (!loopAudioCheckBox.Checked)
            {
                CalculateButtonImage();
                return;
            }

            InitSound(filepath);
            waveOutEvent.Play();
            CalculateButtonImage();
        }

        private void loopAudioCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!loopAudioCheckBox.Checked)
            {
                waveOutEvent.Stop();
                ambientSoundPlayerButton.Enabled = true;
            }
            else if (waveOutEvent.PlaybackState == PlaybackState.Playing && loopAudioCheckBox.Checked)
            {
                ambientSoundPlayerButton.Enabled = false;
            }
            CalculateButtonImage();
        }

        private void ambientSoundPlayerButton_Click(object sender, EventArgs e)
        {
            if (waveOutEvent.PlaybackState == PlaybackState.Playing)
            {
                waveOutEvent.Stop();
            }
            else if (waveOutEvent.PlaybackState == PlaybackState.Stopped)
            {
                InitSound(filepath);
                waveOutEvent.Play();
                if (loopAudioCheckBox.Checked)
                {
                    ambientSoundPlayerButton.Enabled = false;
                }
            }
            CalculateButtonImage();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (ambientSoundEffect != null)
                ambientSoundEffect.Volume = volumeTrackBar.Value / (float)volumeTrackBar.Maximum;
        }

        private void CalculateButtonImage()
        {
            if (waveOutEvent.PlaybackState == PlaybackState.Stopped)
            {
                ambientSoundPlayerButton.Image = playButtonImage;
            }
            else
            {
                if (loopAudioCheckBox.Checked)
                {
                    ambientSoundPlayerButton.Image = stopButtonImage;
                }
                else
                {
                    ambientSoundPlayerButton.Image = disabledStopButtonImage;
                }
            }
        }

        public void ExternalStart()
        {
            InitSound(filepath);
            waveOutEvent.Play();
            if (loopAudioCheckBox.Checked)
                ambientSoundPlayerButton.Enabled = false;
            if (ambientSoundEffect != null)
                ambientSoundEffect.Volume = volumeTrackBar.Value / (float)volumeTrackBar.Maximum;
            CalculateButtonImage();
        }

        public void ExternalStop()
        {
            // desmarcar el checkbox para la reproducción
            loopAudioCheckBox.Checked = false;
        }

        public new void Dispose()
        {
            waveOutEvent.PlaybackStopped -= WaveOutEvent_PlaybackStopped;
            waveOutEvent.Stop();

            waveOutEvent.Dispose();
            waveOutEvent = null;
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            Selected = !Selected;
        }

        private void UpdateControl()
        {
            if (selected)
            {
                this.BackColor = Color.DarkRed;
            }
            else
            {
                this.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
            }
        }
    }
}