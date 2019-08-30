using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Rayffer.PersonalPortfolio.ColorManipulatorForm
{
    public partial class ColorManipulationForm : Form
    {
        private Bitmap imageBitmap;

        public ColorManipulationForm()
        {
            InitializeComponent();
        }

        private void transformedPictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (imageBitmap != null)
            {
                var redRotation = (sin: Math.Sin((double)redRotationNumericUpDown.Value), cos: Math.Cos((double)redRotationNumericUpDown.Value));
                var greenRotation = (sin: Math.Sin((double)greenRotationNumericUpDown.Value), cos: Math.Cos((double)greenRotationNumericUpDown.Value));
                var blueRotation = (sin: Math.Sin((double)blueRotationNumericUpDown.Value), cos: Math.Cos((double)blueRotationNumericUpDown.Value));

                float[][] colorMatrixElements =
                {
                    new float[]  // red transformations
                    {
                        (float)redFactorNumericUpDown.Value * (float)(greenRotation.cos * blueRotation.cos),
                        (float)blueRotation.sin,
                        (float)-greenRotation.sin,
                        0,
                        0
                    },
                    new float[]  // green transformations
                    {
                        (float)-blueRotation.sin,
                        (float)greenFactorNumericUpdown.Value * (float)(redRotation.cos * blueRotation.cos),
                        (float)redRotation.sin,
                        0,
                        0
                    },
                    new float[]  // blue transformations
                    {
                        (float)greenRotation.sin,
                        (float)-redRotation.sin,
                        (float)blueFactorNumericUpdown.Value * (float)(redRotation.cos * greenRotation.cos),
                        0,
                        0
                    },
                    new float[]  // alpha transformations
                    {
                        0,
                        0,
                        0,
                        (float)alphaFactorNumericUpDown.Value,
                        0
                    },
                    new float[]  // offset transformations
                    {
                        (float)redOffsetNumericUpDown.Value,
                        (float)greenOffsetNumericUpDown.Value,
                        (float)blueOffsetNumericUpDown.Value,
                        (float)alphaOffsetNumericUpDown.Value,
                        1
                    }
                };

                using (ImageAttributes imageAttributes = new ImageAttributes())
                using (Image imageToPaint = (Image)imageBitmap.Clone())
                {
                    imageAttributes.SetColorMatrix(
                       new ColorMatrix(colorMatrixElements),
                       ColorMatrixFlag.Default,
                       ColorAdjustType.Bitmap);

                    e.Graphics.Clear(SystemColors.Control);

                    e.Graphics.DrawImage(
                        imageToPaint,
                        new Rectangle
                        (
                            transformedPictureBox.Width / 2 - imageToPaint.Width / 2,
                            transformedPictureBox.Height / 2 - imageToPaint.Height / 2,
                            imageToPaint.Width,
                            imageToPaint.Height
                        ),  // destination rectangle
                        0,
                        0,        // upper-left corner of source rectangle
                        imageToPaint.Width,       // width of source rectangle
                        imageToPaint.Height,      // height of source rectangle
                        GraphicsUnit.Pixel,
                        imageAttributes);
                }
            }
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            transformedPictureBox.Invalidate();
        }

        private void loadImageButton_Click(object sender, EventArgs e)
        {
            if (originalPictureBox.Image != null)
            {
                originalPictureBox.Image.Dispose();
            }
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (System.IO.File.Exists(openFileDialog1.FileName))
                {
                    try
                    {
                        imageBitmap = new Bitmap(openFileDialog1.FileName);
                        imagePathLabel.Text = openFileDialog1.FileName;
                        originalPictureBox.Image = (Image)imageBitmap.Clone();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("The image selected is not valid, please select another image");
                    }

                }
            }
        }
    }
}