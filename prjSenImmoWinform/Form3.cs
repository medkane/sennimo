using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace prjSenImmoWinform
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            Series series1 = new Series();
            Series series2 = new Series();

            series1.Points.Add(38);
            series1.Points.Add(26);
            series1.Points.Add(32);
            series1.Points.Add(28);
            series1.Points.Add(45);
            ;
            series2.Points.Add(12);
            series2.Points.Add(43);
            series2.Points.Add(24);
            series2.Points.Add(12);
            series2.Points.Add(28);
            series1.ShadowColor = Color.LightGray;
            series1.ShadowOffset = 5;
            series2.ShadowColor = Color.LightGray;
            series2.ShadowOffset = 5;

            //'On indique d'afficher ces Series sur le ChartArea1
            series1.ChartArea = "ChartArea1";
            series1.ChartArea = "ChartArea1";
            chart1.Series.Add(series1);
            chart1.Series.Add(series2);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Series.Add("Equipe2");

            var p1 = new DataPoint();
            p1.XValue = 1;
            p1.YValues =new double []{ 5};
            p1.Label = "Lulu";
            chart1.Series["Equipe2"].Points.Add(p1);
            var p2 = new DataPoint();
            p2.XValue = 2;
             p2.YValues = new double[] { 1};
            p2.Label = "moi";
            chart1.Series["Equipe2"].Points.Add(p2);
            var p3 = new DataPoint();
            p3.XValue = 3;
            p3.YValues = new double[] { 4};
            p3.Label = "Toto";
            chart1.Series["Equipe2"].Points.Add(p3);
            chart1.Series["Equipe2"].ChartArea = "ChartArea1";
            //Mettre un ToolTip sur le premier point
            chart1.Series["Equipe2"].Points[0].ToolTip = "Premier point";
            //'***Modification de l'aspect du troisième point
            //'Couleur 3eme colonne
            chart1.Series["Equipe2"].Points[2].Color = Color.BlueViolet;
             //'Mettre un bord
             chart1.Series["Equipe2"].Points[2].BorderColor = Color.Chocolate;
            //'Mettre un marker
            chart1.Series["Equipe2"].Points[1].MarkerColor = Color.Cyan;
            chart1.Series["Equipe2"].Points[1].MarkerStyle = MarkerStyle.Star6;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Series.Add("Equipe2");
            chart1.Series["Equipe2"].Points.AddXY("Pierre", 34);
            chart1.Series["Equipe2"].Points.AddXY("Paul", 24);
            chart1.Series["Equipe2"].Points.AddXY("Louis", 32);
            chart1.Series["Equipe2"].ChartArea = "ChartArea1";


            //Creer une 'callout' annotation
            //Dessine un lien graphique entre le point et l'annotation
            var MyAnnotation = new CalloutAnnotation();
            // **Attributes: texte , couleur...
            //Sur quel ChartArea afficher l'annotation ?
            MyAnnotation.ClipToChartArea = "Default";

            //Sur quel point mettre l'annotation
            MyAnnotation.AnchorDataPoint = chart1.Series["Equipe2"].Points[2];
            MyAnnotation.Text = "Annotation sur un Point du graph";
            MyAnnotation.BackColor = Color.FromArgb(255, 255, 128);

            // Permet le déplacement de l'annotation ou sa selection.
            MyAnnotation.AllowMoving = true;
            MyAnnotation.AllowAnchorMoving = true;
            MyAnnotation.AllowSelecting = true;
            //Ajoute l' annotation à la collection Annotations
            chart1.Annotations.Add(MyAnnotation);


        }

        private void button4_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Series.Add("Equipe2");
            chart1.Series["Equipe2"].IsXValueIndexed = true;
            chart1.Series["Equipe2"].ChartArea = "ChartArea1";
            chart1.Series["Equipe2"].Points.AddXY(1, 34);
            chart1.Series["Equipe2"].Points.AddXY(2, 44);
            chart1.Series["Equipe2"].Points.AddXY(6, 24);
            chart1.Series["Equipe2"].Points.AddXY(3, 55);
            //'Affichons les valeurs Y au dessus de chaque colonne
            chart1.Series["Equipe2"].IsValueShownAsLabel = true;

            chart1.ChartAreas[0].AxisX.Title = "Equipe";
             chart1.ChartAreas[0].AxisY.Title = "Nombre de buts";

            chart1.ChartAreas[0].AxisX.ScaleView.Zoom(1, 3);
            chart1.ChartAreas[0].AxisY.ScaleView.Zoom(0, 10);
            //' Enable range selection and zooming end user interface
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            // 'Chart1.ChartAreas(0).CursorX.IsUserSelectionEnabled = True
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            //' Chart1.ChartAreas(0).CursorY.IsUserSelectionEnabled = True
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;


        }

        private void button5_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Series.Add("Equipe2");
            chart1.Series["Equipe2"].IsXValueIndexed = true;
            chart1.Series["Equipe2"].ChartArea = "ChartArea1";
            chart1.Series["Equipe2"].Points.AddXY(1, 34);
            chart1.Series["Equipe2"].Points.AddXY(2, 44);
            chart1.Series["Equipe2"].Points.AddXY(6, 24);
            chart1.Series["Equipe2"].Points.AddXY(3, 55);
            //'Affichons les valeurs Y au dessus de chaque colonne
            chart1.Series["Equipe2"].IsValueShownAsLabel = true;

            chart1.ChartAreas[0].AxisX.Title = "Equipe";

            chart1.ChartAreas[0].AxisY.Title = "Nombre de buts";

            //' Back Color est la couleur autour de chaque ChartArea
            chart1.BackColor = Color.Blue;
            //           ' Seconde couleur (pour le dégradé)
            chart1.BackSecondaryColor = Color.Yellow;
            //' On pourrait mettre un motif
            // Chart1.BackHatchStyle = ChartHatchStyle.DashedHorizontal
            //Met un Gradient sur le couleur du fond
            chart1.BackGradientStyle = GradientStyle.DiagonalRight;
            // Couleur du bord du chart
            chart1.BorderColor = Color.Blue;
            // Style de la couleur du bord: continu , pointillé...
            chart1.BorderDashStyle = ChartDashStyle.Solid;
            // Epaisseur du bord du graph
            chart1.BorderWidth = 2;
            // Back Color est la couleur de fond du ChartArea
            chart1.ChartAreas[0].BackColor = Color.LightSkyBlue;
            // Seconde couleur (pour le dégradé)
            chart1.ChartAreas[0].BackSecondaryColor = Color.White;
            //On pourrait mettre un motif, on ne le fait pas
            //Chart1.ChartAreas("ChartArea1").BackHatchStyle = ChartHatchStyle.DashedHorizontal
            // Met un Gradient sur le couleur du fond
            chart1.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;

            chart1.Series["Equipe2"].ToolTip = "Pourcentage: #PERCENT";
            // ' Autre exemple ToolTip sur tous les points de la series
            chart1.Series["Equipe2"].ToolTip = "Valeur x ey y: #VALX" + "\n" + "#VALY"; ;
            //' ToolTips sur la legende
            chart1.Series["Equipe2"].LegendToolTip = "Income in #LABEL is #VAL million";
            //ToolTips sur les labels
            chart1.Series["Equipe2"].LabelToolTip = "#PERCENT";
            //ToolTips sur un point unique (le second de la series)
            chart1.Series["Equipe2"].Points[1].ToolTip = "Inconnu";

        }

        private void button6_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            Series series1 = new Series();
            series1.Name = "Myserie";
            series1.XValueType = ChartValueType.DateTime;
            series1.Points.AddXY(DateTime.Parse("01/01/2003"), 34);
            series1.Points.AddXY(DateTime.Parse("01/01/2004"), 24);
            series1.Points.AddXY(DateTime.Parse("01/01/2008"), 55);
            series1.Points.AddXY(DateTime.Parse("01/01/2006"), 32);
            series1.Points.AddXY(DateTime.Parse("01/01/2007"), 28);
            series1.IsXValueIndexed = false;
            chart1.Series.Add(series1);
            chart1.DataManipulator.Sort(PointSortOrder.Ascending, "X", "Myserie");
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //'Chart1 existe déjà
            //' Créer un Chart Area


            //' création d 'une premiere series
            chart1.Series.Add("series1");
            //'On affiche la series dans ChartArea1
            // chart1.Series["series1"].ChartArea = chartArea1.Name;
            //'On affiche des Stacked column
            chart1.Series["series1"].ChartType = SeriesChartType.StackedColumn;


            //'Ajout de 3 points dans la premiere serie
            var p = new DataPoint();
            p.XValue = 1;
            p.YValues = new double[] { 2 };
            chart1.Series["series1"].Points.Add(p);


            var p1 = new DataPoint();
            p1.XValue = 2;
            p1.YValues = new double[] { 6 };
            chart1.Series["series1"].Points.Add(p1);

            var p2 = new DataPoint();
            p2.XValue = 3;
            p2.YValues = new double[] { 7 };
            chart1.Series["series1"].Points.Add(p2);

            //'On met des cylindres pour faire joli!!
            chart1.Series["series1"].CustomProperties = "DrawingStyle=cylinder";

            //'Second series
            chart1.Series.Add("series2");
            //'On affiche la series dans ChartArea1
            // chart1.Series["series2"].ChartArea = chartArea1.Name;
            //'On affiche en StackedColumn
            chart1.Series["series2"].ChartType = SeriesChartType.StackedColumn;


            //'Ajout de 3 points dans la seconde series 
            var p3 = new DataPoint();
            p3.XValue = 1;
            p3.YValues = new double[] { 5 };
            chart1.Series["series2"].Points.Add(p3);


            var p4 = new DataPoint();
            p4.XValue = 2;
            p4.YValues = new double[] { 4 };
            chart1.Series["series2"].Points.Add(p4);

            var p5 = new DataPoint();
            p5.XValue = 3;
            p5.YValues = new double[] { 3 };
            chart1.Series["series2"].Points.Add(p5);

            chart1.Series["series2"].CustomProperties = "DrawingStyle=cylinder";

            //'On met les 2 series dans le même StackedGroup
            //'(Peut importe le nom du group)
            chart1.Series["series1"]["StackedGroupName"] = "Group1";
            chart1.Series["series2"]["StackedGroupName"] = "Group1";
            chart1.Series["series1"].IsValueShownAsLabel = true;
            chart1.Series["series2"].IsValueShownAsLabel = true;
        }
    }
}
