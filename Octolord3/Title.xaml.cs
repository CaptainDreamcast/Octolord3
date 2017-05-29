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

namespace EyeOfTheMedusa3
{
    /// <summary>
    /// Interaction logic for Title.xaml
    /// </summary>
    public partial class Title : Page
    {
        private Action callGameCB;

        public Title(Action callGameCB)
        {
            InitializeComponent();
            this.callGameCB = callGameCB;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            callGameCB();
        }
    }
}
