using System;
using System.Collections.Generic;
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

using App1;
using System.Threading;



namespace EyeOfTheMedusa3
{
    /// <summary>
    /// Interaction logic for GameScreen.xaml
    /// </summary>
    public partial class GameScreen : Page
    {
        private Level game;
        private Thread drawThread;

        public static bool isDownT = false;
        public static bool isUpT = false;

        private Action callTitleCallback;

        private System.Media.SoundPlayer boing =
 new System.Media.SoundPlayer();
      

        public GameScreen(Action callTitleCallback)
        {
            InitializeComponent();

            this.callTitleCallback = callTitleCallback;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            boing.SoundLocation = @"Assets\boing.wav";
            boing.Load();
            game = new Level(boing);
            drawThread = new Thread(new ThreadStart(drawLoop));
            drawThread.Start();
        }

        private void drawLoop()
        {
            bool isActive = true;
            while (isActive)
            {
                game.update(1 / 60.0f);

                List<DrawElement> de = game.getDrawElements();

                try
                {
                    this.Dispatcher.Invoke(() =>
                    {

                        HashSet<int> vis = new HashSet<int>();

                        foreach (var element in de)
                        {
                            var name = "element" + element.id;


                            bool exists = false;
                            this.Dispatcher.Invoke(() => { exists = grid.FindName(name) == null; });
                            if (exists)
                            {
                                Image i = new Image();
                                BitmapImage src = new BitmapImage();
                                src.BeginInit();
                                src.UriSource = new Uri(element.path, UriKind.Relative);
                                src.CacheOption = BitmapCacheOption.OnLoad;
                                src.EndInit();
                                i.Source = src;
                                i.Stretch = Stretch.Uniform;

                                element.setSize(src.PixelWidth, src.PixelHeight);

                                i.Name = "element" + element.id;


                                grid.Children.Add(i);

                                grid.RegisterName(name, i);
                            }

                            var e = (Image)grid.FindName(name);

                            Canvas.SetLeft(e, element.x - element.width);
                            Canvas.SetTop(e, element.y - element.height);

                            e.Width = element.width;
                            e.Height = element.height;

                            vis.Add(e.GetHashCode());
                        }


                        List<UIElement> rems = new List<UIElement>();

                        foreach (Image ch in grid.Children)
                        {
                            if (ch.Name == "bg") continue;

                            if (!vis.Contains(ch.GetHashCode()))
                            {
                                rems.Add(ch);
                            }
                        }

                        foreach (var ch in rems)
                        {
                            grid.Children.Remove(ch);
                        }


                        isDownT = Keyboard.GetKeyStates(Key.Down) == KeyStates.Down;
                        isUpT = Keyboard.GetKeyStates(Key.Up) == KeyStates.Down;
                        var reset = Keyboard.GetKeyStates(Key.R) == KeyStates.Down;
                        if (reset) callTitleCallback();
                    });
                }
                catch (TaskCanceledException){
                    isActive = false;
                    // TODO: do this the proper way
                }


                Thread.Sleep(17);
            }

        }
    }
}
