using ComplexityCalculator.Services;
using System.Windows;

namespace ComplexityCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ComplexityAnalyzerService _analyzer;
        
        public MainWindow()
        {
            InitializeComponent();

            _analyzer = new ComplexityAnalyzerService();
        }

        private void Analyze_Click(object sender, RoutedEventArgs e)
        {
            var code = CodeInput.Text;

            if (string.IsNullOrWhiteSpace(code))
            {
                ResultOutput.Text = "Insira algum código C# para analisar.";
               
                return;
            }

            var resultado = _analyzer.Analyze(code);
            
            ResultOutput.Text = $"Complexidade estimada: {resultado}";
        }
    }
}