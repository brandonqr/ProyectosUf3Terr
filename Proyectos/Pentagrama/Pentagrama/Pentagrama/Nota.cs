using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentagrama
{
    class Nota
    {
        public const int TIPUS_NEGRA = 1;
        public const int TIPUS_BLANCA = 2;
        public const int TIPUS_CORXERA = 3;
        public const int TIPUS_SEMI = 4;

        public enum TNOTES
        {
            DO = 10,
            RE = 9,
            MI = 8,
            FA = 7,
            SOL = 6,
            LA = 5,
            SI = 4,
            DO2 = 3
        }

        TNOTES nota;

        internal TNOTES nNota
        {
            get { return nota; }
            set { nota = value; }
        }
        int tipus;

        public int Tipus
        {
            get { return tipus; }
            set { tipus = value; }
        }

        public Nota(TNOTES nota, int tipus)
        {
            this.nNota = nota;
            this.tipus = tipus;
        }

        int numNota = 0;

        public int NumNota
        {
            get { return numNota; }
            set { numNota = value; }
        }
        int posicio = 0;

        public int Posicio
        {
            get { return posicio; }
            set { posicio = value; }
        }
    }
}
