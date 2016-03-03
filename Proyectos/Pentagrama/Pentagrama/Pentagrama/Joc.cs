using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SdlDotNet.Graphics;//Permet crear pantalla
using SdlDotNet.Core;
using SdlDotNet.Graphics.Primitives;//permet pintar en la pantalla creada
using System.Drawing;//color
using SdlDotNet.Input;
using System.Collections.Generic;
using SdlDotNet.Graphics.Sprites;

namespace Pentagrama
{
    class Joc
    {
        public const int PENTA_INICI_X = 40;
        public const int PENTA_INICI_Y = 40;
        public const int PENTA_LONG = 320;
        public const int PENTA_ENTRE_LINIA = 40;
        public const int RADI_NOTA = 20;
        Surface sPentagrama;
        List<Nota> llNotes;
        TextSprite reprodueix;
        TextSprite parar;
        TextSprite borrar;
        TextSprite borrarTot;
        SdlDotNet.Graphics.Font font_Text = new SdlDotNet.Graphics.Font("../../fonts/fuente1.ttf", 18);
        int cont = 0;
        Reproductor reproductor;
        bool condiReproduir = false;
        public Joc()
        {
            Video.SetVideoMode(400, 500);
            Video.WindowCaption = "Escala musical";

            //inicialitzem el llistat de notes
            llNotes = new List<Nota>();

            Point puntRepr = new Point(50, 300);
            reprodueix = new TextSprite("REPRODUEIX", font_Text, Color.Red, puntRepr);
            reprodueix.Width = 100;
            reprodueix.Height = 50;
            Point puntParar = new Point(250, 300);
            parar = new TextSprite("PARAR", font_Text, Color.Red, puntParar);
            parar.Width = 100;
            parar.Height = 50;
            Point puntBorrar = new Point(50, 400);
            borrar = new TextSprite("BORRAR", font_Text, Color.Red, puntBorrar);
            borrar.Width = 100;
            borrar.Height = 50;
            Point puntBorrar2 = new Point(250, 400);
            borrarTot = new TextSprite("BORRA TOT", font_Text, Color.Red, puntBorrar2);
            borrarTot.Width = 100;
            borrarTot.Height = 50;

            //Dibuixar el pentagrama
            //Point p1 = new Point(PENTA_INICI_X, PENTA_INICI_Y);
            //Point p2 = new Point(PENTA_INICI_X + PENTA_LONG, PENTA_INICI_Y);
            sPentagrama = new Surface(new Size(Video.Screen.Width, PENTA_ENTRE_LINIA * 10));
            //Dibuixar el pentagrama
            Point p1 = new Point(PENTA_INICI_X, PENTA_INICI_Y);
            Point p2 = new Point(PENTA_INICI_X + PENTA_LONG, PENTA_INICI_Y);
            for (int i = 0; i < 5; i++)
            {
                Line liniaPentagrama = new Line(p1, p2);
                //Line liniaPentagrama = new Line(PENTA_INICI_X, (short)(PENTA_INICI_Y + (PENTA_ENTRE_LINIA * i)), 
                //    PENTA_INICI_X + PENTA_LONG, (short)(PENTA_INICI_Y + (PENTA_ENTRE_LINIA * i)));
                //Video.Screen.Draw(liniaPentagrama, Color.Red, false, true);//pinta linia sobre la pantalla
                sPentagrama.Draw(liniaPentagrama, Color.Red, false, true);
                p1.Y += PENTA_ENTRE_LINIA;
                p2.Y += PENTA_ENTRE_LINIA;
            }
            Events.Tick += Events_Tick_Reprodueix;
            Events.MouseButtonDown += Events_MouseButtonDown;
            Events.Tick += Events_Tick;
            Events.Run();

        }
        void Events_MouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Position.X >= 50 && e.Position.X < 151 && e.Position.Y >= 300 && e.Position.Y < 351)
            {
                if (llNotes != null)
                {
                    reproductor = new Reproductor(llNotes);
                    condiReproduir = true;
                }
                Console.WriteLine("Has polsat el boto");
            }
            else if (e.Position.X >= 250 && e.Position.X < 301 && e.Position.Y >= 300 && e.Position.Y < 351)
            {
                Console.WriteLine("Has polsat parar");
                condiReproduir = false;
            }

            else if (e.Position.X >= 250 && e.Position.X < 301 && e.Position.Y >= 400 && e.Position.Y < 451)
            {
                Console.Clear();
                llNotes.Clear();
                condiReproduir = false;
            }
            //Calculem el num de la nota
            int nNota = CalcularNumNota(e.Position.Y);

            string nom = "";
            //si esta entre els valors valids fem
            if (nNota >= 3 && nNota < 11)
            {
                Nota.TNOTES to = Nota.TNOTES.DO;
                switch (nNota)//swich per saber quin to te la nota en la que estem
                {
                    case 10:
                        to = Nota.TNOTES.DO;
                        nom = "DO";
                        break;
                    case 9:
                        to = Nota.TNOTES.RE;
                        nom = "RE";
                        break;
                    case 8:
                        to = Nota.TNOTES.MI;
                        nom = "MI";
                        break;
                    case 7:
                        to = Nota.TNOTES.FA;
                        nom = "FA";
                        break;
                    case 6:
                        to = Nota.TNOTES.SOL;
                        nom = "SOL";
                        break;
                    case 5:
                        to = Nota.TNOTES.LA;
                        nom = "LA";
                        break;
                    case 4:
                        to = Nota.TNOTES.SI;
                        nom = "SI";
                        break;
                    case 3:
                        to = Nota.TNOTES.DO2;
                        nom = "DO ALT";
                        break;
                    default:
                        break;
                }//una vegada obtingut el to afegim la nota a la llista
                llNotes.Add(new Nota(to, Nota.TIPUS_NEGRA));
                Console.WriteLine("Nota {0} guardada. So " + nom, cont);
                cont += 1;
            }
            else
            {
                Console.WriteLine("Fora del rang de notes");
            }
            //llNotes.Add(new Nota())
            //Nota n = new Nota();
            //n.NumNota = CalcularNumNota(e.Position.Y);
            //n.Posicio = e.Position.X;
            //llNotes.Add(n);
        }

        private void Events_Tick_Reprodueix(object sender, TickEventArgs e)
        {
            if (llNotes.Count != 0 && condiReproduir == true)
            {
                reproductor.reprodueixSeguentNota();
            }
            else condiReproduir = false;
        }
        void Events_Tick(object sender, TickEventArgs e)
        {//farem una surface per recuperar el pentagrama. Ara el punt de la nota pinta tota la pantalla
            Video.Screen.Blit(sPentagrama);
            Video.Screen.Blit(reprodueix);
            Video.Screen.Blit(parar);
            Video.Screen.Blit(borrar);
            Video.Screen.Blit(borrarTot);
           Circle nota = new Circle(Mouse.MousePosition, PENTA_ENTRE_LINIA / 3);
          //  dterminar el numero de notes
            int nNota = CalcularNumNota(Mouse.MousePosition.Y);
            //Console.WriteLine("NumNota = " + nNota);
         //   Circle nota = new Circle(new Point(Mouse.MousePosition.X, PENTA_INICI_Y + nNota * RADI_NOTA), PENTA_ENTRE_LINIA / 2);
            Video.Screen.Draw(nota, Color.Blue, false, true);
            Video.Screen.Update();
        }
        public void Run()
        {
            Events.Run();
        }
        public static int CalcularNumNota(int y)
        {
            int nNota = (y - (PENTA_INICI_Y)) / RADI_NOTA;
            return nNota;
        }
    }
}
