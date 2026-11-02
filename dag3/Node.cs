using System;
using System.Collections.Generic;
using System.Text;

namespace dag3
{     
    //Opgave 2
    public class Node
    {
        public byte Værdi;      // Hvilken byte-værdi (kun relevant for blade)
        public int Frekvens;    // Hvor mange gange byte-værdien forekommer (indre node: summen af børnenes frekvenser)
        public int Id;          //Et unikt nummer for noden, som bruges til sortering: blade = byte-værdien, indre noder = 256, 257, ...
        public Node? Venstre;   // Venstre barn, null hvis der ikke er et.
        public Node? Højre;     // Højre barn, null hvis der ikke er et.


        //Kontrollerer, om den aktuelle Node er en bladnode
        public bool ErBlad  //Denne kode returnerer automatisk true eller false
        {
            get { return Venstre == null && Højre == null; }
        }

        // Konstruktør til blade (Opretter en bladnode.)
        public Node(byte værdi, int frekvens)
        {
            Værdi = værdi;
            Frekvens = frekvens;
            Id = værdi;
        }

        // Konstruktør til indre noder
        public Node(Node venstre, Node højre, int id)
        {
            //Modtager en venstre node
            Venstre = venstre;
            //Modtager en højre node
            Højre = højre;
            // Frekvensen er summen af de to børns frekvenser
            Frekvens = venstre.Frekvens + højre.Frekvens;
            Id = id; //Et unikt nummer for noden, som bruges til sortering
        }

      
    }
}

