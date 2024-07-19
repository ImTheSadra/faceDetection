using Emgu.CV;
using Emgu.CV.Structure;

namespace face;

public partial class Form1 : Form
{
    private VideoCapture capture;
    private CascadeClassifier cascade;
    private PictureBox picture = new();
    public Form1()
    {
        InitializeComponent();
        this.capture = new();
        this.cascade = new("face.xml");

        this.picture.Width = this.Width;
        this.picture.Height = this.Height;

        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.MinimizeBox = false;

        this.Controls.Add(this.picture);
        Application.Idle += this.Application_Idle;
    }

    private void Application_Idle(object sender, EventArgs e)
    {
        using (Image<Bgr, byte> frame = capture.QueryFrame().ToImage<Bgr, byte>())
        {
            if (frame != null)
            {
                Image<Gray, byte> grayFrame = frame.Convert<Gray, byte>(); // Convert the frame to grayscale

                Rectangle[] faces = this.cascade.DetectMultiScale(grayFrame, 1.1, 3, Size.Empty); // Detect faces in the frame

                foreach (Rectangle face in faces)
                {
                    frame.Draw(face, new Bgr(Color.Red), 2); // Draw a rectangle around each detected face
                }

                this.picture.Image = frame.ToBitmap(); // Display the frame with detected faces in a PictureBox
            }
        }
    }
}
