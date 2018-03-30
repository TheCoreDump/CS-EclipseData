using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OxyPlot.Pdf;
using System;
using System.Windows.Forms;

namespace SunrisePlot
{
    public partial class Form1 : Form
    {
        protected PlotView plot1;

        public Form1()
        {
            InitializeComponent();


            this.SuspendLayout();

            plot1 = new PlotView()
            {
                Name = "plot1",
                Dock = DockStyle.Fill,
            };

            this.Controls.Add(this.plot1);

            this.ResumeLayout(false);


            var myModel = new PlotModel { Title = "Circle" };

            myModel.Series.Add(new FunctionSeries((t) => Math.Cos(t) * 12450D, (t) => Math.Sin(t) * 12450D, 0, 2D * Math.PI, Math.PI / 90D));

            // Add the annotations
            myModel.Annotations.Add(new PointAnnotation()
            {
                X = 6225,
                Y = 0,
                Selectable = false,
                Shape = MarkerType.Circle,
                Fill = OxyColor.FromRgb(0xff, 0xff, 0x00)
            });

            this.plot1.Model = myModel;

            OxyPlot.Pdf.PdfExporter exporter = new OxyPlot.Pdf.PdfExporter();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
