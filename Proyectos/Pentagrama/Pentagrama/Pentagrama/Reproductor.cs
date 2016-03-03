using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SdlDotNet.Audio;

namespace Pentagrama
{
    //agregar las referencias de sdlnet
    class Reproductor
    {//tindra una llista per guradar les notes, controlara un canal, i un index que retornara
        List<Nota> notes = null;
        Channel canal = null;
        int indexReproduccio = 0;
        Sound[] sons = new Sound[8];
        float tempsIniciRepr = 0f;
        float tempsUltNota;
        public Reproductor(List<Nota> notes)
        {
            this.notes = notes;

            //Carregar els sons
            sons[0] = new Sound(@"..\..\sons\do.wav");
            sons[1] = new Sound(@"..\..\sons\re.wav");
            sons[2] = new Sound(@"..\..\sons\mi.wav");
            sons[3] = new Sound(@"..\..\sons\fa.wav");
            sons[4] = new Sound(@"..\..\sons\sol.wav");
            sons[5] = new Sound(@"..\..\sons\la.wav");
            sons[6] = new Sound(@"..\..\sons\si.wav");
            sons[7] = new Sound(@"..\..\sons\doalt.wav");

            indexReproduccio = 0;
        }
        public void reprodueixSeguentNota()
        {//reproduir la seguent nota quan hagi passat x temps
            float tempsActual = SdlDotNet.Core.Timer.TicksElapsed;
            float tempsRecorregut = tempsActual - tempsIniciRepr;
            if (tempsRecorregut >= tempsUltNota)
            {
                if (indexReproduccio >= notes.Count)
                {
                    indexReproduccio = 0;
                }
                Nota notaActual = notes[indexReproduccio];
                tempsUltNota = notaActual.Tipus;
                if (notaActual.Tipus == Nota.TIPUS_BLANCA)
                {
                    tempsUltNota = 1000f;
                }
                else if (notaActual.Tipus == Nota.TIPUS_CORXERA)
                {
                    tempsUltNota = 250f;
                }
                else if (notaActual.Tipus == Nota.TIPUS_NEGRA)
                {
                    tempsUltNota = 500f;
                }
                else if (notaActual.Tipus == Nota.TIPUS_SEMI)
                {
                    tempsUltNota = 100f;
                }
                reprodiurNota(notaActual);
                //Anotem el temps de la ultima nota reproduida.
                tempsIniciRepr = SdlDotNet.Core.Timer.TicksElapsed;
                indexReproduccio++;
            }

        }
        private void reprodiurNota(Nota nota)
        {
            Sound so = null;
            switch (nota.nNota)
            {
                case Nota.TNOTES.DO:
                    so = sons[0];
                    break;
                case Nota.TNOTES.RE:
                    so = sons[1];
                    break;
                case Nota.TNOTES.MI:
                    so = sons[2];
                    break;
                case Nota.TNOTES.FA:
                    so = sons[3];
                    break;
                case Nota.TNOTES.SOL:
                    so = sons[4];
                    break;
                case Nota.TNOTES.LA:
                    so = sons[5];
                    break;
                case Nota.TNOTES.SI:
                    so = sons[6];
                    break;
                case Nota.TNOTES.DO2:
                    so = sons[7];
                    break;
                default:
                    break;
            }

            if (canal == null)
            {
                canal = so.Play();
            }
            else
            {
                if (canal.IsPlaying())//si el canal te so(esta tocant)
                {
                    canal.Stop();
                }
                canal = so.Play();
            }
        }
    }
}
