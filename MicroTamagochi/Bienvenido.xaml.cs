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
using System.Windows.Shapes;

namespace MicroTamagochi
{
    /// <summary>
    /// Lógica de interacción para Bienvenido.xaml
    /// </summary>
    public partial class Bienvenido : Window
    {
        private MainWindow padre;

       

        public Bienvenido(MainWindow padre)
        {
            InitializeComponent();
            this.padre = padre;
        }

     

        private void eventoEmpezar(object sender, RoutedEventArgs e)
        {
            padre.setNombre(this.tbNombre.Text);
            this.Close();
        }
    }
}
