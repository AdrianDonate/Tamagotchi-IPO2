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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MicroTamagochi
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer t1;
        double decremento = 1.5;
        int puntuacion = 0;
        private string nombreTamagotchi;
        public MainWindow()
        {
            InitializeComponent();
            t1 = new DispatcherTimer();
            t1.Interval = TimeSpan.FromMilliseconds(1000.0);
            t1.Tick += new EventHandler(reloj);
            t1.Start();
            Bienvenido ventanaInicial = new Bienvenido(this);
            ventanaInicial.ShowDialog();
        }
        private void reloj(object sender, EventArgs e)
        {
            this.pbComida.Value -= decremento;
            this.pbDescanso.Value -= decremento;
            this.pbDiversion.Value -= decremento;
            //decremento += 1;
            puntuacion += 1;
            lblPuntuacion.Content = puntuacion;
            if (pbComida.Value <= 0 || pbDiversion.Value <= 0 || pbDescanso.Value <= 0)
            {
                t1.Stop();
                lblGameOver.Visibility = Visibility.Visible;
                btnGuardarPuntuacion.Visibility = Visibility.Visible;
                deshabilitarBotones(false);

            }
            if (pbComida.Value <= 15)
            {
                Storyboard sbHambre = (Storyboard)this.Resources["Hambre"];
                sbHambre.Begin();
            }
            if (pbDiversion.Value <= 15)
            {
                Storyboard sbAburrido = (Storyboard)this.Resources["Aburrido"];
                sbAburrido.Begin();
            }
            if (pbDescanso.Value <= 15)
            {

                Storyboard sbCansado = (Storyboard)this.Resources["Cansado"];
                sbCansado.Begin();

            }
            if (pbDescanso.Value >=90)
            {
                logro3.Visibility = Visibility.Visible;
            }
            if (pbDiversion.Value >= 90)
            {
                logro2.Visibility = Visibility.Visible;
            }
            if (pbComida.Value >= 90)
            {
                logro1.Visibility = Visibility.Visible;
            }
            if (puntuacion >= 50)
            {
                logro4.Visibility = Visibility.Visible;
            }
            if (puntuacion >= 40)
            {
                imEstrella.Visibility = Visibility.Visible;
            }
            if (puntuacion >= 30)
            {
                imFondo3.Visibility = Visibility.Visible;
            }

        }
        public void setNombre(string nombre)
        {
            this.nombreTamagotchi = nombre;
            this.tbMensajes.Text = "Bienvenido " + nombreTamagotchi;
        }

        private void btnComer_Click(object sender, RoutedEventArgs e)
        {
            this.pbComida.Value += 5;
            comer();
        }

        private void btnJugar_Click(object sender, RoutedEventArgs e)
        {
            this.pbDiversion.Value += 5;
            jugar();
        }

        private void btnDescansar_Click(object sender, RoutedEventArgs e)
        {
            this.pbDescanso.Value += 5;

            dormir();
        }




        private void jugar()
        {
            Storyboard sbJugar = (Storyboard)this.Resources["Jugar"];
            sbJugar.Completed += new EventHandler(finAnimacion);
            sbJugar.Begin();
            deshabilitarBotones(false);
        }
        private void comer()
        {

            Storyboard sbComer = (Storyboard)this.Resources["Comer"];
            sbComer.Completed += new EventHandler(finAnimacion);
            sbComer.Begin();
            deshabilitarBotones(false);


        }
        private void dormir()
        {
            Storyboard sbCansado = (Storyboard)this.Resources["Cansado"];
            sbCansado.Completed += new EventHandler(finAnimacion);
            sbCansado.Begin();
            deshabilitarBotones(false);
        }
        private void finAnimacion(object sender, EventArgs e)
        {
            btnComer.IsEnabled = true;
            btnDescansar.IsEnabled = true;
            btnJugar.IsEnabled = true;
        }
        private void deshabilitarBotones(Boolean v)
        {
            btnComer.IsEnabled = v;
            btnDescansar.IsEnabled = v;
            btnJugar.IsEnabled = v;
        }

        private void cambiarFondo(object sender, MouseButtonEventArgs e)
        {
            this.imFondo.Source = ((Image)sender).Source;
        }

        
        private void cerrar(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
          "Práctica realizada por:\nAdrián Donate\n\n ¿Desea Salir?", "IPO2 - Tamagotchi",
          MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void inicioArrastrar(object sender, MouseButtonEventArgs e)
        {
            DataObject dataO = new DataObject(((Image)sender));
            DragDrop.DoDragDrop((Image)sender, dataO, DragDropEffects.Move);

        }

        private void aniadirObjeto(object sender, DragEventArgs e)
        {
            Image aux = (Image)e.Data.GetData(typeof(Image));
            switch (aux.Name)
            {
                case "imGorroMini":
                    imGorro.Visibility = Visibility.Visible;
                    break;
                case "imEstrella":
                    imEstrella1.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void btnGuardarPuntuacion_Click(object sender, RoutedEventArgs e)
        {
      
         
            lstPuntos.Items.Add(new
            {
                Puntuacion = this.nombreTamagotchi + " " + lblPuntuacion.Content

            });
            btnGuardarPuntuacion.IsEnabled = false;
            btnGuardarPuntuacion.Visibility = Visibility.Hidden;
            btnJugardenuevo.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            puntuacion = 0;
            this.pbComida.Value = 75;
            this.pbDescanso.Value = 75;
            this.pbDiversion.Value = 75;
            lblGameOver.Visibility = Visibility.Hidden;
            btnGuardarPuntuacion.Visibility = Visibility.Hidden;
            btnGuardarPuntuacion.IsEnabled = true;
            deshabilitarBotones(true);
            InitializeComponent();
            t1 = new DispatcherTimer();
            t1.Interval = TimeSpan.FromMilliseconds(1000.0);
            t1.Tick += new EventHandler(reloj);
            t1.Start();
            Bienvenido ventanaInicial = new Bienvenido(this);
            ventanaInicial.ShowDialog();
            btnJugardenuevo.Visibility = Visibility.Hidden;
            imFondo3.Visibility = Visibility.Hidden;
            imEstrella1.Visibility = Visibility.Hidden;
            imEstrella.Visibility = Visibility.Hidden;
            imGorro.Visibility = Visibility.Hidden;
        }
    }
}
