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

namespace Recommendation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _movie;
        private Model model;
        private List<string> _recommendedMovies = new List<string>();
        private List<string> noResult = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            noResult.Add("No recommendation was found!");
            model = new Model();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (slctMovie.Text == string.Empty)
                System.Windows.MessageBox.Show("Please enter a movie before searching");
            else
            {
                _movie = slctMovie.Text;
                _recommendedMovies = model.getRecommendation(_movie.ToLower());
                if (_recommendedMovies == null)
                    rcdMovies.ItemsSource = _recommendedMovies;
                rcdHeader.Visibility = Visibility.Visible;
                rcdMovies.Visibility = Visibility.Visible;
            }
        }
    }
}
