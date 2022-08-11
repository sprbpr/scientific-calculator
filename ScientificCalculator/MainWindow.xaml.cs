using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ScientificCalculator
{
    public partial class MainWindow : Window
    {
        CoordinatePlane Graph;
        Clock Clk;
        SolveEquations Equations;
        TaylorSeries Taylor;

        public MainWindow()
        {
            InitializeComponent();

            Clk = new Clock(Clock_Grid, Clock.Frame.Circle, Clock.Face.NoNumber);

            Graph = new CoordinatePlane(Graph_Grid, Brushes.White, Brushes.Black);
            Graph.DrawCoordinatePlane();

            Taylor = new TaylorSeries(Taylor_Graph_Grid, Brushes.White, Brushes.Red, Brushes.Blue);
        }

        private void Draw_Graph_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Graph.GetFunctionInformation(Function_Text.Text, double.Parse(Min_X_Text.Text), double.Parse(Min_Y_Text.Text),
                                                                 double.Parse(Max_X_Text.Text), double.Parse(Max_Y_Text.Text));
                Graph.DrawCoordinatePlane();
                Graph.DrawGraph();
            }
            catch
            {
                string messageBoxText = "Invalid input. Please try again.";
                MessageBox.Show(messageBoxText);
                Clear_Graph_Button_Click(new object(), new RoutedEventArgs());
            }
        }

        private void Clear_Graph_Button_Click(object sender, RoutedEventArgs e)
        {
            Graph.DrawCoordinatePlane();
            Function_Text.Text = "";
            Min_X_Text.Text = "";
            Min_Y_Text.Text = "";
            Max_X_Text.Text = "";
            Max_Y_Text.Text = "";
        }

        private void Calculate_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Equations = new SolveEquations(Equations_Text.Text);
                Solutions_Label.Content = Equations.WriteSolutions();
            }
            catch
            {
                string messageBoxText = "Invalid input. Please try again.";
                MessageBox.Show(messageBoxText);
                Clear_Equations_Button_Click(new object(), new RoutedEventArgs());
            }
        }

        private void Clear_Equations_Button_Click(object sender, RoutedEventArgs e)
        {
            Equations_Text.Text = "";
            Solutions_Label.Content = "";
        }

        private void Draw_Taylor_Graph_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Sin_Item.IsSelected)
                    Taylor.GetTaylorInformation("sin(x)", int.Parse(N_Text.Text), double.Parse(X0_Text.Text));
                if (Cos_Item.IsSelected)
                    Taylor.GetTaylorInformation("cos(x)", int.Parse(N_Text.Text), double.Parse(X0_Text.Text));
                if (Exp_Item.IsSelected)
                    Taylor.GetTaylorInformation("exp(x)", int.Parse(N_Text.Text), double.Parse(X0_Text.Text));
                //Taylor.GetTaylorInformation(Taylor_Function_Text.Text, int.Parse(N_Text.Text), double.Parse(X0_Text.Text));
                Taylor.DrawTaylorFunctionGraph();
                Taylor.DrawTaylorSeriesGraph();
            }
            catch
            {
                string messageBoxText = "Invalid input. Please try again.";
                MessageBox.Show(messageBoxText);
                Clear_Taylor_Graph_Button_Click(new object(), new RoutedEventArgs());
            }
        }

        private void Clear_Taylor_Graph_Button_Click(object sender, RoutedEventArgs e)
        {
            Taylor.Clear();
            //Taylor_Function_Text.Text = "";
            N_Text.Text = "";
            X0_Text.Text = "";
        }

        private void Controller(object sender, KeyEventArgs e)
        {
            if (Draw_Graph_Tab.IsSelected && Graph.UpdateView(e.Key))
            {
                Graph.DrawCoordinatePlane();
                if (Graph.Function != null)
                    Graph.DrawGraph();
            }

            if (Taylor_Series_Tab.IsSelected && Taylor.Plane.UpdateView(e.Key))
            {
                Taylor.Taylor.UpdateView(e.Key);
                Taylor.Plane.DrawCoordinatePlane();
                Taylor.DrawTaylorFunctionGraph();
                Taylor.DrawTaylorSeriesGraph();
            }
        }

        private void Show_Clock_Checked(object sender, RoutedEventArgs e)
        {
            if (Clk != null)
            {
                Clk.Timer.Start();
                Clk.DrawFrame();
                Clk.DrawFace();
            }

            Frame_Label.Visibility = Visibility.Visible;
            Frame_ComboBox.Visibility = Visibility.Visible;
            Numbers_Label.Visibility = Visibility.Visible;
            Numbers_ComboBox.Visibility = Visibility.Visible;
            Background_Color_Label.Visibility = Visibility.Visible;
            Background_Color_ComboBox.Visibility = Visibility.Visible;
            Frame_Color_Label.Visibility = Visibility.Visible;
            Frame_Color_ComboBox.Visibility = Visibility.Visible;
            Numbers_Color_Label.Visibility = Visibility.Visible;
            Numbers_Color_ComboBox.Visibility = Visibility.Visible;
            Sec_Hand_Color_Label.Visibility = Visibility.Visible;
            Sec_Hand_Color_ComboBox.Visibility = Visibility.Visible;
            Min_Hand_Color_Label.Visibility = Visibility.Visible;
            Min_Hand_Color_ComboBox.Visibility = Visibility.Visible;
            H_Hand_Color_Label.Visibility = Visibility.Visible;
            H_Hand_Color_ComboBox.Visibility = Visibility.Visible;
            Clock_Apply_Button.Visibility = Visibility.Visible;
            Clock_Default_Button.Visibility = Visibility.Visible;
        }

        private void Show_Clock_Unchecked(object sender, RoutedEventArgs e)
        {
            Frame_Label.Visibility = Visibility.Hidden;
            Frame_ComboBox.Visibility = Visibility.Hidden;
            Numbers_Label.Visibility = Visibility.Hidden;
            Numbers_ComboBox.Visibility = Visibility.Hidden;
            Background_Color_Label.Visibility = Visibility.Hidden;
            Background_Color_ComboBox.Visibility = Visibility.Hidden;
            Frame_Color_Label.Visibility = Visibility.Hidden;
            Frame_Color_ComboBox.Visibility = Visibility.Hidden;
            Numbers_Color_Label.Visibility = Visibility.Hidden;
            Numbers_Color_ComboBox.Visibility = Visibility.Hidden;
            Sec_Hand_Color_Label.Visibility = Visibility.Hidden;
            Sec_Hand_Color_ComboBox.Visibility = Visibility.Hidden;
            Min_Hand_Color_Label.Visibility = Visibility.Hidden;
            Min_Hand_Color_ComboBox.Visibility = Visibility.Hidden;
            H_Hand_Color_Label.Visibility = Visibility.Hidden;
            H_Hand_Color_ComboBox.Visibility = Visibility.Hidden;
            Clock_Apply_Button.Visibility = Visibility.Hidden;
            Clock_Default_Button.Visibility = Visibility.Hidden;

            Clk.Timer.Stop();
            Clk.Clear();
        }

        private void Clock_Apply_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Circle_Item.IsSelected)
                Clk.ClockFrame = Clock.Frame.Circle;
            if (Square_Item.IsSelected)
                Clk.ClockFrame = Clock.Frame.Square;

            if (No_Number_Item.IsSelected)
                Clk.ClockFace = Clock.Face.NoNumber;
            if (Eng_Number_Item.IsSelected)
                Clk.ClockFace = Clock.Face.EngNumber;
            if (Roman_Number_Item.IsSelected)
                Clk.ClockFace = Clock.Face.RomanNumber;

            if (White_Clock_Background_Color.IsSelected)
                Clk.BackgroundColor = Brushes.White;
            if (Black_Clock_Background_Color.IsSelected)
                Clk.BackgroundColor = Brushes.Black;
            if (Blue_Clock_Background_Color.IsSelected)
                Clk.BackgroundColor = Brushes.Blue;
            if (Green_Clock_Background_Color.IsSelected)
                Clk.BackgroundColor = Brushes.Green;
            if (Red_Clock_Background_Color.IsSelected)
                Clk.BackgroundColor = Brushes.Red;

            if (White_Frame_Color.IsSelected)
                Clk.FrameColor = Brushes.White;
            if (Black_Frame_Color.IsSelected)
                Clk.FrameColor = Brushes.Black;
            if (Blue_Frame_Color.IsSelected)
                Clk.FrameColor = Brushes.Blue;
            if (Green_Frame_Color.IsSelected)
                Clk.FrameColor = Brushes.Green;
            if (Red_Frame_Color.IsSelected)
                Clk.FrameColor = Brushes.Red;

            if (White_Number_Color.IsSelected)
                Clk.NumbersColor = Brushes.White;
            if (Black_Number_Color.IsSelected)
                Clk.NumbersColor = Brushes.Black;
            if (Blue_Number_Color.IsSelected)
                Clk.NumbersColor = Brushes.Blue;
            if (Green_Number_Color.IsSelected)
                Clk.NumbersColor = Brushes.Green;
            if (Red_Number_Color.IsSelected)
                Clk.NumbersColor = Brushes.Red;

            if (White_Second_Color.IsSelected)
                Clk.SecondHandColor = Brushes.White;
            if (Black_Second_Color.IsSelected)
                Clk.SecondHandColor = Brushes.Black;
            if (Blue_Second_Color.IsSelected)
                Clk.SecondHandColor = Brushes.Blue;
            if (Green_Second_Color.IsSelected)
                Clk.SecondHandColor = Brushes.Green;
            if (Red_Second_Color.IsSelected)
                Clk.SecondHandColor = Brushes.Red;

            if (White_Minute_Color.IsSelected)
                Clk.MinuteHandColor = Brushes.White;
            if (Black_Minute_Color.IsSelected)
                Clk.MinuteHandColor = Brushes.Black;
            if (Blue_Minute_Color.IsSelected)
                Clk.MinuteHandColor = Brushes.Blue;
            if (Green_Minute_Color.IsSelected)
                Clk.MinuteHandColor = Brushes.Green;
            if (Red_Minute_Color.IsSelected)
                Clk.MinuteHandColor = Brushes.Red;

            if (White_Hour_Color.IsSelected)
                Clk.HourHandColor = Brushes.White;
            if (Black_Hour_Color.IsSelected)
                Clk.HourHandColor = Brushes.Black;
            if (Blue_Hour_Color.IsSelected)
                Clk.HourHandColor = Brushes.Blue;
            if (Green_Hour_Color.IsSelected)
                Clk.HourHandColor = Brushes.Green;
            if (Red_Hour_Color.IsSelected)
                Clk.HourHandColor = Brushes.Red;

            Clk.Clear();
            Clk.Start(new object(), new EventArgs());

            Clk.DrawFrame();
            Clk.DrawFace();
        }

        private void Clock_Defult_Button_Click(object sender, RoutedEventArgs e)
        {
            Clk.Clear();
            Clk.Start(new object(), new EventArgs());
            Clk.Timer.Start();
            Frame_ComboBox.SelectedItem = Circle_Item;
            Numbers_ComboBox.SelectedItem = No_Number_Item;
            Clk.ClockFrame = Clock.Frame.Circle;
            Clk.ClockFace = Clock.Face.NoNumber;
            Clk.DrawFrame();
            Clk.DrawFace();
        }

        private void Graph_Apply_Button_Click(object sender, RoutedEventArgs e)
        {
            Graph.MinX = double.Parse(Default_Min_X_Text.Text);
            Graph.MinY = double.Parse(Default_Min_Y_Text.Text);
            Graph.MaxX = double.Parse(Default_Max_X_Text.Text);
            Graph.MaxY = double.Parse(Default_Max_Y_Text.Text);
            Graph.AxisThickness = double.Parse(Axis_Thickness_Text.Text);
            Graph.SubAxisThickness = double.Parse(Sub_Axis_Thickness_Text.Text);
            Graph.GraphThickness = double.Parse(Graph_Thickness_Text.Text);
            Graph.Accuracy = double.Parse(Graph_Accuracy_Text.Text);

            if (White_Graph_Background_Color.IsSelected)
                Graph.PlaneColor = Brushes.White;
            if (Gray_Graph_Background_Color.IsSelected)
                Graph.PlaneColor = Brushes.Gray;
            if (Blue_Graph_Background_Color.IsSelected)
                Graph.PlaneColor = Brushes.Blue;
            if (Green_Graph_Background_Color.IsSelected)
                Graph.PlaneColor = Brushes.Green;
            if (Red_Graph_Background_Color.IsSelected)
                Graph.PlaneColor = Brushes.Red;

            if (Black_Graph_Color.IsSelected)
                Graph.GraphColor = Brushes.Black;
            if (Gray_Graph_Color.IsSelected)
                Graph.GraphColor = Brushes.Gray;
            if (Blue_Graph_Color.IsSelected)
                Graph.GraphColor = Brushes.Blue;
            if (Green_Graph_Color.IsSelected)
                Graph.GraphColor = Brushes.Green;
            if (Red_Graph_Color.IsSelected)
                Graph.GraphColor = Brushes.Red;

            if (Black_Axis_Color.IsSelected)
                Graph.AxisColor = Brushes.Black;
            if (Gray_Axis_Color.IsSelected)
                Graph.AxisColor = Brushes.Gray;
            if (Blue_Axis_Color.IsSelected)
                Graph.AxisColor = Brushes.Blue;
            if (Green_Axis_Color.IsSelected)
                Graph.AxisColor = Brushes.Green;
            if (Red_Axis_Color.IsSelected)
                Graph.AxisColor = Brushes.Red;

            if (Gray_Sub_Axis_Color.IsSelected)
                Graph.SubAxisColor = Brushes.Gray;
            if (Black_Sub_Axis_Color.IsSelected)
                Graph.SubAxisColor = Brushes.Black;
            if (Blue_Sub_Axis_Color.IsSelected)
                Graph.SubAxisColor = Brushes.Blue;
            if (Green_Sub_Axis_Color.IsSelected)
                Graph.SubAxisColor = Brushes.Green;
            if (Red_Sub_Axis_Color.IsSelected)
                Graph.SubAxisColor = Brushes.Red;

            if (Black_XY_Color.IsSelected)
                Graph.XYColor = Brushes.Black;
            if (Gray_XY_Color.IsSelected)
                Graph.XYColor = Brushes.Gray;
            if (Blue_XY_Color.IsSelected)
                Graph.XYColor = Brushes.Blue;
            if (Green_XY_Color.IsSelected)
                Graph.XYColor = Brushes.Green;
            if (Red_XY_Color.IsSelected)
                Graph.XYColor = Brushes.Red;

            if (Black_Numbers_Color.IsSelected)
                Graph.XYColor = Brushes.Black;
            if (Gray_Numbers_Color.IsSelected)
                Graph.XYColor = Brushes.Gray;
            if (Blue_Numbers_Color.IsSelected)
                Graph.XYColor = Brushes.Blue;
            if (Green_Numbers_Color.IsSelected)
                Graph.XYColor = Brushes.Green;
            if (Red_Numbers_Color.IsSelected)
                Graph.XYColor = Brushes.Red;

            Graph.DrawCoordinatePlane();
        }

        private void Graph_Defult_Button_Click(object sender, RoutedEventArgs e)
        {
            Graph.MinX = -6.5;
            Graph.MinY = -4.5;
            Graph.MaxX = 6.5;
            Graph.MaxY = 4.5;
            Graph.AxisThickness = 3;
            Graph.SubAxisThickness = 1;
            Graph.GraphThickness = 2;
            Graph.Accuracy = 0.001;
            Graph.PlaneColor = Brushes.White;
            Graph.GraphColor = Brushes.Black;
            Graph.AxisColor = Brushes.Black;
            Graph.SubAxisColor = Brushes.Gray;
            Graph.XYColor = Brushes.Black;
            Graph.XYColor = Brushes.Black;
        }

        public class Clock
        {
            const double HoursLength = 10;
            const double HoursThickness = 3;
            const double MinutesLength = 5;
            const double MinutesThickness = 1;
            const double SecondHandThickness = 1;
            double SecondHandLength;
            const double MinuteHandThickness = 3;
            double MinuteHandLength;
            const double HourHandThickness = 5;
            double HourHandLength;

            public Frame ClockFrame;
            public Face ClockFace;

            public Brush BackgroundColor;
            public Brush FrameColor;
            public Brush NumbersColor;
            public Brush HourHandColor;
            public Brush MinuteHandColor;
            public Brush SecondHandColor;
            public Draw Draw;

            public enum Frame
            {
                Circle,
                Square,
            }
            public enum Face
            {
                NoNumber,
                EngNumber,
                RomanNumber
            }

            private Grid Grid;
            private Point Center;
            public DispatcherTimer Timer;


            public Clock(Grid grid, Frame frame, Face face)
            {
                Grid = grid;
                Center = new Point(0, 0);
                SecondHandLength = Grid.Width / 2 - HoursLength - 10;
                MinuteHandLength = SecondHandLength * 3 / 4;
                HourHandLength = SecondHandLength / 2;

                ClockFrame = frame;
                ClockFace = face;

                BackgroundColor = Brushes.White;
                FrameColor = Brushes.Black;
                NumbersColor = Brushes.Black;
                HourHandColor = Brushes.Black;
                MinuteHandColor = Brushes.Black;
                SecondHandColor = Brushes.Black;

                Draw = new Draw(grid);

                DrawFrame();
                DrawFace();

                Timer = new DispatcherTimer();
                Timer.Tick += new EventHandler(Start);
                Timer.Interval = new TimeSpan(0, 0, 1);
                Timer.Start();
            }

            public void Start(object sender, EventArgs e)
            {
                DrawSeconHand(DateTime.Now);
                DrawMinuteHand(DateTime.Now);
                DrawHourHand(DateTime.Now);
                DrawCenter();
            }

            public void DrawSeconHand(DateTime time)
            {
                //ClockDraw.Line(0, SecondHandLength, -(time.Second - 1) * 6 + 90, Brushes.White, SecondHandThickness + 1);
                Draw.FilledCircle(new Point(0, 0), SecondHandLength, BackgroundColor);
                Draw.Line(0, SecondHandLength, -time.Second * 6 + 90, SecondHandColor, SecondHandThickness);
            }

            public void DrawMinuteHand(DateTime time)
            {
                Draw.Line(0, MinuteHandLength, -(time.Minute * 60 + time.Second - 1) / 10 + 90, BackgroundColor, MinuteHandThickness + 1);
                Draw.Line(0, MinuteHandLength, -(time.Minute * 60 + time.Second) / 10 + 90, MinuteHandColor, MinuteHandThickness);
            }

            public void DrawHourHand(DateTime time)
            {
                if (time.Hour >= 12)
                {
                    Draw.Line(0, HourHandLength, -((time.Hour - 12) * 3600 + time.Minute * 60 + time.Second - 1) / 120 + 90, BackgroundColor, HourHandThickness + 1);
                    Draw.Line(0, HourHandLength, -((time.Hour - 12) * 3600 + time.Minute * 60 + time.Second) / 120 + 90, HourHandColor, HourHandThickness);
                }
                else
                {
                    Draw.Line(0, HourHandLength, -(time.Hour * 3600 + time.Minute * 60 + time.Second - 1) / 120 + 90, BackgroundColor, HourHandThickness + 1);
                    Draw.Line(0, HourHandLength, -(time.Hour * 3600 + time.Minute * 60 + time.Second) / 120 + 90, HourHandColor, HourHandThickness);
                }
            }

            public void DrawFrame()
            {
                if (ClockFrame == Frame.Circle)
                {
                    Draw.FilledCircle(Center, Grid.Width / 2, BackgroundColor);
                    Draw.UnFilledCircle(Center, Grid.Width / 2, FrameColor);
                }
                if (ClockFrame == Frame.Square)
                {
                    Draw.FilledRectangle(Center, Grid.Width, Grid.Height, BackgroundColor);
                    Draw.UnfilledRectangle(Center, Grid.Width, Grid.Height, FrameColor);
                }
            }

            public void DrawFace()
            {
                DrawHours();
                DrawMinute();
            }

            public void DrawHours()
            {
                if (ClockFace == Face.NoNumber)
                    for (int i = 0; i < 12; i++)
                        Draw.Line(Grid.Width / 2 - HoursLength, Grid.Width / 2, 30 * i, NumbersColor, HoursThickness);

                if (ClockFace == Face.EngNumber)
                    for (int i = 1; i <= 12; i++)
                        Draw.String(i.ToString(), Grid.Width - 20, -(30 * i) + 90, NumbersColor);

                if (ClockFace == Face.RomanNumber)
                    for (int i = 1; i <= 12; i++)
                        switch (i)
                        {
                            case 1:
                                Draw.String("I", Grid.Width - 20, -(30 * i) + 90, NumbersColor);
                                break;
                            case 2:
                                Draw.String("II", Grid.Width - 20, -(30 * i) + 90, NumbersColor);
                                break;
                            case 3:
                                Draw.String("III", Grid.Width - 20, -(30 * i) + 90, NumbersColor);
                                break;
                            case 4:
                                Draw.String("IV", Grid.Width - 20, -(30 * i) + 90, NumbersColor);
                                break;
                            case 5:
                                Draw.String("V", Grid.Width - 20, -(30 * i) + 90, NumbersColor);
                                break;
                            case 6:
                                Draw.String("VI", Grid.Width - 20, -(30 * i) + 90, NumbersColor);
                                break;
                            case 7:
                                Draw.String("VII", Grid.Width - 20, -(30 * i) + 90, NumbersColor);
                                break;
                            case 8:
                                Draw.String("VIII", Grid.Width - 20, -(30 * i) + 90, NumbersColor);
                                break;
                            case 9:
                                Draw.String("IX", Grid.Width - 20, -(30 * i) + 90, NumbersColor);
                                break;
                            case 10:
                                Draw.String("X", Grid.Width - 20, -(30 * i) + 90, NumbersColor);
                                break;
                            case 11:
                                Draw.String("XI", Grid.Width - 20, -(30 * i) + 90, NumbersColor);
                                break;
                            case 12:
                                Draw.String("XII", Grid.Width - 20, -(30 * i) + 90, NumbersColor);
                                break;
                        }
            }

            public void DrawMinute()
            {
                if (ClockFace == Face.NoNumber)
                    for (int i = 0; i < 60; i++)
                        Draw.Line(Grid.Width / 2 - MinutesLength, Grid.Width / 2, 6 * i, Brushes.Black, MinutesThickness);
            }

            public void DrawCenter()
            {
                Draw.FilledCircle(new Point(0, 0), 4, Brushes.Black);
            }

            public void Clear()
            {
                Draw.FilledRectangle(Center, Grid.Width, Grid.Height, Brushes.White);
            }
        }

        public class CoordinatePlane
        {
            const double DefaultAxisThickness = 3;
            const double DefaultSubAxisThickness = 1;
            const double DefaultGraphThickness = 2;
            const double XYMargin = 10;
            const double NumbersMargin = 15;
            const double DefaultMinX = -6.5;
            const double DefaultMaxX = 6.5;
            const double DefaultMinY = -4.5;
            const double DefaultMaxY = 4.5;
            const double DefaultAccuracy = 0.001;
            const double Speed = 0.5;

            private Grid Grid;
            private Point Center;
            private Point Origin;
            public Function Function;
            public double MinX;
            public double MinY;
            public double MaxX;
            public double MaxY;
            public double Accuracy;
            public Brush GraphColor;
            public Brush PlaneColor;
            public Brush AxisColor;
            public Brush SubAxisColor;
            public Brush XYColor;
            public Brush NumbersColor;
            public double AxisThickness;
            public double SubAxisThickness;
            public double GraphThickness;

            public Draw Draw;

            public CoordinatePlane(Grid grid, Brush planeColor, Brush graphColor, string functionString = null,
                                   double minX = DefaultMinX, double minY = DefaultMinY,
                                   double maxX = DefaultMaxX, double maxY = DefaultMaxY,
                                   double shift = 0, double accuracy = DefaultAccuracy)
            {
                Grid = grid;
                MinX = minX;
                MinY = minY;
                MaxX = maxX;
                MaxY = maxY;
                AxisThickness = DefaultAxisThickness;
                SubAxisThickness = DefaultSubAxisThickness;
                GraphThickness = DefaultGraphThickness;
                Center = new Point(0, 0);
                //Origin = Center;
                Origin = ConvertPoint(new Point(0, 0));
                Draw = new Draw(grid);
                Accuracy = accuracy;
                GraphColor = graphColor;
                PlaneColor = planeColor;
                AxisColor = Brushes.Black;
                SubAxisColor = Brushes.Gray;
                XYColor = Brushes.Black;
                NumbersColor = Brushes.Black;
            }

            public void GetFunctionInformation(string functionString, double minX, double minY, double maxX, double maxY, double shift = 0)
            {
                MinX = minX;
                MinY = minY;
                MaxX = maxX;
                MaxY = maxY;
                Origin = ConvertPoint(new Point(0, 0));
                Function = new Function(functionString, minX, maxX, DefaultAccuracy, shift);
            }

            public bool UpdateView(Key key)
            {
                if (key == Key.Down)
                {
                    MinY -= Speed;
                    MaxY -= Speed;
                    Origin = ConvertPoint(new Point(0, 0));
                    return true;
                }

                if (key == Key.Up)
                {
                    MinY += Speed;
                    MaxY += Speed;
                    Origin = ConvertPoint(new Point(0, 0));
                    return true;
                }

                if (key == Key.Left)
                {
                    MaxX -= Speed;
                    MaxX -= Speed;
                    Origin = ConvertPoint(new Point(0, 0));
                    return true;
                }

                if (key == Key.Right)
                {
                    MaxX += Speed;
                    MaxX += Speed;
                    Origin = ConvertPoint(new Point(0, 0));
                    return true;
                }

                if (key == Key.F1)
                {
                    MaxX -= Speed;
                    MaxX -= Speed;
                    MinY -= Speed;
                    MaxY -= Speed;
                    Origin = ConvertPoint(new Point(0, 0));
                    return true;
                }

                if (key == Key.F2)
                {
                    MaxX += Speed;
                    MaxX += Speed;
                    MinY += Speed;
                    MaxY += Speed;
                    Origin = ConvertPoint(new Point(0, 0));
                    return true;
                }

                return false;
            }

            private void DrawMainAxis()
            {
                Draw.Line(new Point(Origin.X, -Grid.Height / 2), new Point(Origin.X, Grid.Height / 2), AxisColor, AxisThickness);
                Draw.String("X", new Point(Grid.Width / 2 - XYMargin, XYMargin + Origin.Y), XYColor);
                Draw.Line(new Point(-Grid.Width / 2, Origin.Y), new Point(Grid.Width / 2, Origin.Y), AxisColor, AxisThickness);
                Draw.String("Y", new Point(XYMargin + Origin.X, Grid.Height / 2 - XYMargin), XYColor);
            }

            private void DrawSubAxis()
            {
                for (int i = (int)Math.Ceiling(MinX); i <= (int)Math.Floor(MaxX); i++)
                {
                    double x = Grid.Width * (i - MinX) / (MaxX - MinX);
                    Draw.Line(
                        new Point(-Grid.Width / 2 + x, -Grid.Height / 2),
                        new Point(-Grid.Width / 2 + x, Grid.Height / 2),
                        SubAxisColor,
                        DefaultSubAxisThickness
                        );
                    if (i != MinX && i != MaxX)
                        Draw.String(i.ToString(), new Point(-Grid.Width / 2 + x, -NumbersMargin + Origin.Y), NumbersColor);
                }

                for (int i = (int)Math.Ceiling(MinY); i <= (int)Math.Floor(MaxY); i++)
                {
                    double y = Grid.Height * (i - MinY) / (MaxY - MinY);
                    Draw.Line(
                        new Point(-Grid.Width / 2, -Grid.Height / 2 + y),
                        new Point(Grid.Width / 2, -Grid.Height / 2 + y),
                        SubAxisColor,
                        DefaultSubAxisThickness
                        );
                    if (i != MinY && i != MaxY)
                        Draw.String(i.ToString(), new Point(-NumbersMargin + Origin.X, -Grid.Height / 2 + y), NumbersColor);
                }
            }

            public void DrawGraph()
            {
                for (int i = 0; i < Function.Points.Length - 1; i++)
                {
                    if (Function.Points[i].Y >= MinY && Function.Points[i].Y <= MaxY &&
                       Function.Points[i + 1].Y >= MinY && Function.Points[i + 1].Y <= MaxY)
                        Draw.Line(ConvertPoint(Function.Points[i]),
                                  ConvertPoint(Function.Points[i + 1]),
                                  GraphColor, GraphThickness);
                }
            }

            public void DrawCoordinatePlane()
            {
                //Draw.FilledRectangle(new Point(-Grid.Width / 2, Grid.Height / 2), Grid.Width, Grid.Height, Brushes.Blue);
                Draw.FilledRectangle(new Point(0, 0), Grid.Width, Grid.Height, PlaneColor);
                Origin = ConvertPoint(new Point(0, 0));
                DrawMainAxis();
                DrawSubAxis();
            }

            private Point ConvertPoint(Point point)
            {
                return new Point(-Grid.Width / 2 + Grid.Width * (point.X - MinX) / (MaxX - MinX),
                                 -Grid.Height / 2 + Grid.Height * (point.Y - MinY) / (MaxY - MinY));
            }
        }

        public class SolveEquations
        {
            public string[] Equations;
            public char[] Variable;
            public Matrix Coefficients;
            public double[] Constants;
            public double[] Solutions;
            public bool NoSolution;

            public SolveEquations(string equations)
            {
                NoSolution = false;
                Equations = equations.Split(',');
                Coefficients = GetCoefficient();
                Constants = GetConstants();
                Variable = GetVariable();
                Solutions = Solve();
            }

            private char[] GetVariable()
            {
                List<char> result = new List<char>();
                foreach (char c in Equations[0])
                    if (char.IsLetter(c))
                        result.Add(c);
                return result.ToArray();
            }

            private Matrix GetCoefficient()
            {
                List<List<double>> result = new List<List<double>>();

                foreach (string equation in Equations)
                {
                    List<double> coefficients = new List<double>();
                    string[] subEqu = equation.Split('+');
                    foreach (string sub in subEqu)
                    {
                        string[] num = sub.Split('*');
                        coefficients.Add(double.Parse(num[0]));
                    }
                    result.Add(coefficients);
                }
                return ConvertListToMatrix(result);
            }

            private double[] GetConstants()
            {
                List<double> result = new List<double>();
                foreach (string equation in Equations)
                    result.Add(double.Parse(equation.Split('=')[1]));
                return result.ToArray();
            }

            private double[] Solve()
            {
                List<double> result = new List<double>();
                for (int i = 0; i < Equations.Length; i++)
                {
                    Matrix adjMat = GetAdjMat(i);
                    result.Add(Math.Round(adjMat.Determinant / Coefficients.Determinant, 3));
                }
                return result.ToArray();
            }

            private Matrix GetAdjMat(int n)
            {
                double[,] r = new double[Coefficients.Size, Coefficients.Size];
                //Matrix result = new Matrix(Coefficients.Mat);
                for (int i = 0; i < Coefficients.Size; i++)
                    for (int j = 0; j < Coefficients.Size; j++)
                        r[i, j] = Coefficients.Mat[i, j];
                Matrix result = new Matrix(r);
                for (int i = 0; i < Equations.Length; i++)
                    result.Mat[i, n] = Constants[i];
                return result;
            }

            private Matrix ConvertListToMatrix(List<List<double>> list)
            {
                double[,] result = new double[list.Count(), list.Count()];
                for (int i = 0; i < list.Count(); i++)
                    for (int j = 0; j < list.Count(); j++)
                        result[i, j] = list[i][j];
                return new Matrix(result);
            }

            public string WriteSolutions()
            {
                if (double.IsNaN(Solutions[0]))
                    return "Many Solutions.";
                if (double.IsInfinity(Solutions[0]))
                    return "No Solution.";

                List<string> solutions = new List<string>();

                //for (int i = 0; i < Variable.Length; i++)
                //    solutions.Add($"{Variable[i]} = {Solutions[i]}");
                //return string.Join(",", solutions);

                for (int i = 0; i < Variable.Length; i++)
                    solutions.Add($"{Variable[i]} = {Solutions[i]}");

                using (StreamWriter sw = File.AppendText(@"..\..\History.txt"))
                {
                    sw.WriteLine($"Date: {DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}    Time:{DateTime.Now.Hour}:{DateTime.Now.Minute} \n");
                    sw.WriteLine($"Equations:");
                    foreach (string equation in Equations)
                        sw.WriteLine(equation);
                    sw.WriteLine("");
                    sw.WriteLine($"Solutions:");
                    foreach (string solution in solutions)
                        sw.WriteLine(solution);
                    sw.WriteLine("");
                    sw.WriteLine("===============================================");
                    sw.WriteLine("");
                }

                return string.Join("\n", solutions);
            }
        }

        public class TaylorSeries
        {
            public CoordinatePlane Plane;
            public CoordinatePlane Taylor;
            public string TaylorFunction;
            public int N;
            public double X0;

            public TaylorSeries(Grid grid, Brush planeColor, Brush taylorFunctionColor, Brush taylorSeriesColor)
            {
                Plane = new CoordinatePlane(grid, planeColor, taylorFunctionColor);
                Plane.DrawCoordinatePlane();
                Taylor = new CoordinatePlane(grid, planeColor, taylorSeriesColor, MakeTaylorSeries());
            }

            public void GetTaylorInformation(string taylorFunction, int n, double x0)
            {
                TaylorFunction = taylorFunction;
                N = n;
                X0 = x0;
                Plane.GetFunctionInformation(taylorFunction, x0 - 20, -2, x0 + 20, 2);
                //Plane.DrawCoordinatePlane();
                Taylor.GetFunctionInformation(MakeTaylorSeries(), x0 - 20, -2, x0 + 20, 2, x0);
                Plane.GetFunctionInformation(taylorFunction, x0 - FindRange(), -2, x0 + FindRange(), 2);
                Taylor.GetFunctionInformation(MakeTaylorSeries(), x0 - FindRange(), -2, x0 + FindRange(), 2, x0);
                Plane.DrawCoordinatePlane();
            }

            public void DrawTaylorFunctionGraph()
            {
                Plane.DrawGraph();
            }

            public void DrawTaylorSeriesGraph()
            {
                Taylor.DrawGraph();
            }

            public void Clear()
            {
                Plane.DrawCoordinatePlane();
            }

            public string MakeTaylorSeries()
            {
                List<string> phrases = new List<string>();
                if (TaylorFunction == "sin(x)")
                    for (int i = 0; i < N; i++)
                    {
                        phrases.Add($"{Math.Sin(X0 + i * Math.PI / 2) / Factorial(i)}*x^{i}");
                    }
                if (TaylorFunction == "cos(x)")
                    for (int i = 0; i < N; i++)
                    {
                        phrases.Add($"{Math.Cos(X0 + i * Math.PI / 2) / Factorial(i)}*x^{i}");
                    }
                if (TaylorFunction == "exp(x)")
                    for (int i = 0; i < N; i++)
                    {
                        phrases.Add($"{Math.Exp(X0) / Factorial(i)}*x^{i}");
                    }
                return string.Join("+", phrases);
            }

            private double FindRange()
            {
                double result = X0;
                while (Math.Abs(Plane.Function.F(result) - Taylor.Function.F(result)) < 0.01)
                    result += Plane.Accuracy;
                return 2 * result;
            }

            private int Factorial(int n)
            {
                if (n == 0 || n == 1)
                    return 1;
                else
                    return n * Factorial(n - 1);
            }
        }

        public class Matrix
        {
            public double[,] Mat;
            public int Size;
            public double Determinant
            {
                get
                {
                    return GetDeterminant(Mat, Size);
                }
            }

            public Matrix(double[,] mat)
            {
                Mat = mat;
                Size = (int)Math.Sqrt(mat.Length);
            }

            static void GetCofactor(double[,] matrix, double[,] subMatrix, int factorRow, int factorCol, int size)
            {
                int i = 0;
                int j = 0;

                for (int row = 0; row < size; row++)
                {
                    for (int col = 0; col < size; col++)
                    {
                        if (row != factorRow && col != factorCol)
                        {
                            subMatrix[i, j++] = matrix[row, col];
                            if (j == size - 1)
                            {
                                j = 0;
                                i++;
                            }
                        }
                    }
                }
            }

            static double GetDeterminant(double[,] matrix, int size)
            {
                double D = 0;
                int sign = 1;

                if (size == 1)
                    return matrix[0, 0];

                double[,] temp = new double[size - 1, size - 1];

                for (int f = 0; f < size; f++)
                {
                    GetCofactor(matrix, temp, 0, f, size);
                    D += sign * matrix[0, f] * GetDeterminant(temp, size - 1);
                    sign = -sign;
                }
                return D;
            }
        }

        public class Function
        {
            private string FunctionString;
            public Point[] Points;
            private double Shift;

            public Function(string functionString, double minX, double maxX, double accuracy, double shift = 0)
            {
                FunctionString = functionString;
                Shift = shift;
                List<Point> points = new List<Point>();
                for (int i = (int)(minX / accuracy); i < (int)(maxX / accuracy); i++)
                    points.Add(new Point(i * accuracy, F(i * accuracy)));
                Points = points.ToArray();
            }

            private List<Variable> RecognizeFunction(string functionString)
            {
                List<Variable> result = new List<Variable>();
                //for(int i = 0; i < functionString.Length; i++)
                //    if(functionString[i] == 'x')
                //        result.Add(new Variable(double.Parse(functionString[i - 2].ToString()),
                //                                double.Parse(functionString[i + 2].ToString())));
                string[] subFunc = functionString.Split('+');
                foreach (string sub in subFunc)
                {
                    string[] num = sub.Split('*', '^');
                    result.Add(new Variable(double.Parse(num[0]), double.Parse(num[2])));
                }
                return result;
            }

            public double F(double x)
            {
                if (FunctionString == "sin(x)")
                    return Math.Sin(x - Shift);
                if (FunctionString == "cos(x)")
                    return Math.Cos(x - Shift);
                if (FunctionString == "exp(x)")
                    return Math.Exp(x - Shift);

                double Y = 0;
                foreach (Variable variable in RecognizeFunction(FunctionString))
                    Y += variable.Coefficient * Math.Pow(x - Shift, variable.Power);
                return Y;
            }
        }

        public class Variable
        {
            public double Coefficient;
            public double Power;

            public Variable(double coefficient, double power)
            {
                Coefficient = coefficient;
                Power = power;
            }
        }

        public class Draw
        {
            public Grid Grid;
            public Point Center;

            public Draw(Grid grid)
            {
                Grid = grid;
                Center = new Point(Grid.Width / 2, Grid.Height / 2);
            }

            public void UnFilledCircle(Point Center, double r, Brush brush)
            {

                Ellipse circle = new Ellipse();
                circle.Stroke = brush;
                circle.Width = 2 * r;
                circle.Height = 2 * r;
                circle.Margin = new Thickness(Center.X, -Center.Y, 0, 0);
                Grid.Children.Add(circle);
            }

            public void FilledCircle(Point Center, double r, Brush brush)
            {

                Ellipse circle = new Ellipse();
                circle.Stroke = brush;
                circle.Fill = brush;
                circle.Width = 2 * r;
                circle.Height = 2 * r;
                circle.Margin = new Thickness(Center.X, -Center.Y, 0, 0);
                Grid.Children.Add(circle);
            }

            public void UnfilledRectangle(Point Center, double w, double h, Brush brush)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Stroke = brush;
                rectangle.Width = w;
                rectangle.Height = h;
                rectangle.Margin = new Thickness(Center.X, -Center.Y, 0, 0);
                Grid.Children.Add(rectangle);
            }

            public void FilledRectangle(Point Center, double w, double h, Brush brush)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Stroke = brush;
                rectangle.Fill = brush;
                rectangle.Width = w;
                rectangle.Height = h;
                rectangle.Margin = new Thickness(Center.X, -Center.Y, 0, 0);
                Grid.Children.Add(rectangle);
            }

            public void Line(Point start, Point end, Brush brush, double thickness)
            {
                Line line = new Line();
                line.Stroke = brush;
                line.X1 = Grid.Width / 2 + start.X;
                line.Y1 = Grid.Height / 2 - start.Y;
                line.X2 = Grid.Width / 2 + end.X;
                line.Y2 = Grid.Height / 2 - end.Y;
                line.StrokeThickness = thickness;
                Grid.Children.Add(line);
            }

            public void Line(double startRadius, double endRadius, double teta, Brush brush, double thickness)
            {
                Line line = new Line();
                line.Stroke = brush;
                line.X1 = Grid.Width / 2 + startRadius * Math.Cos(Math.PI * teta / 180);
                line.Y1 = Grid.Height / 2 - startRadius * Math.Sin(Math.PI * teta / 180);
                line.X2 = Grid.Width / 2 + endRadius * Math.Cos(Math.PI * teta / 180);
                line.Y2 = Grid.Height / 2 - endRadius * Math.Sin(Math.PI * teta / 180);
                line.StrokeThickness = thickness;
                Grid.Children.Add(line);
            }

            public void String(string str, Point Center, Brush brush)
            {
                Label label = new Label();
                label.Content = str;
                label.Width = 25;
                label.Height = 25;
                label.Margin = new Thickness(2 * Center.X, -2 * Center.Y, 0, 0);
                label.Foreground = brush;
                //label.FontSize = 5;
                label.FontWeight = FontWeights.Bold;
                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                label.VerticalContentAlignment = VerticalAlignment.Center;
                //label.Background = Brushes.Green;
                //label.Margin = new Thickness(Center.X, -Center.Y, 0, 0);
                Grid.Children.Add(label);
            }

            public void String(string str, double Radius, double teta, Brush brush)
            {
                Label label = new Label();
                label.Content = str;
                label.Width = 25;
                label.Height = 25;
                label.Margin = new Thickness(Radius * Math.Cos(Math.PI * teta / 180), -Radius * Math.Sin(Math.PI * teta / 180), 0, 0);
                label.Foreground = brush;
                //label.FontSize = 5;
                label.FontWeight = FontWeights.Bold;
                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                label.VerticalContentAlignment = VerticalAlignment.Center;
                //label.Background = Brushes.Green;
                //label.Margin = new Thickness(Center.X, -Center.Y, 0, 0);
                Grid.Children.Add(label);
            }
        }
    }
}
