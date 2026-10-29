using System;
using System.Collections.Generic;
using System.Text;

namespace dag3
{     
    //Opgave 2
    public class Node
    {
        public byte Værdi;       // which byte (only meaningful for leaves)
        public int Frekvens;     // how often (inner node: sum of children)
        public int Id;           // Til at afgøre uafgjort: blade = byte-værdien, indre noder = 256, 257, ...
        public Node? Venstre;   // left child, null if none
        public Node? Højre;     // right child, null if none

        public bool ErBlad
        {
            get { return Venstre == null && Højre == null; }
        }

        // Konstruktør til blade
        public Node(byte værdi, int frekvens)
        {
            Værdi = værdi;
            Frekvens = frekvens;
            Id = værdi;
        }

        // Konstruktør til indre noder
        public Node(Node venstre, Node højre, int id)
        {
            Venstre = venstre;
            Højre = højre;
            // Constructor for an inner node: frequency = sum of the two children
            Frekvens = venstre.Frekvens + højre.Frekvens;
            Id = id;
        }

        // lav evt en ToString
    }
}

