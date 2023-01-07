using System;

namespace Indrizzamento
{

    class Program
    {

        static int Main(string[] args)
        {
            string indirizzo;
            int cidr;
            Console.WriteLine("Insersci un indirizzo grazie:");
            indirizzo = Console.ReadLine();
            Console.WriteLine("Insersci la notazione CIDR:");
            cidr = Convert.ToInt32(Console.ReadLine());
            if (Indirizzi.ControlloIndirizzo(indirizzo) == false)//Controllo validità indirizzo
            {
                Console.WriteLine("Indirizzo non valido!");
                return 0;
            }
            if (Indirizzi.ControllaCIDR(cidr) == false)//Controllo validità indirizzo
            {
                Console.WriteLine("CIDR non valida");
                return 0;
            }
            int[] numeri = new int[4];
            Indirizzi.Scrivinumeri(indirizzo, numeri);
            string[] bytes = new string[4];//array con indirizzo in byte
            Indirizzi.ConvertiInBytes(numeri, bytes);
            /*for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"{bytes[i]}");
            }*/
            Indirizzi.ControlloClasse(cidr);
            int[] bytemaschera = Indirizzi.RisolviCIDR(cidr);
            string[] mascherabyte = Indirizzi.RisolviMaschera(bytemaschera);//array di byte della maschera in stringhe
            string maschera="";
            maschera = Indirizzi.ScriviIndirizzo(mascherabyte, maschera);
            Console.WriteLine($"Questa è la sua maschera:{maschera}");
            string[] networkbyte = Indirizzi.CalcolaNetwrok(bytes,mascherabyte);
            string networkaddress = "";
            networkaddress = Indirizzi.ScriviIndirizzo(networkbyte, networkaddress);
            Console.WriteLine($"Questa è la sua network address:{networkaddress}");
            string[] startaddressbyte = Indirizzi.AddressIniziale(networkbyte);
            string starteraddress = "";
            starteraddress = Indirizzi.ScriviIndirizzo(startaddressbyte, starteraddress);
            string[] finaleaddressbyte = Indirizzi.AddressFinale(startaddressbyte, cidr);
            string finaladdress = "";
            finaladdress = Indirizzi.ScriviIndirizzo(finaleaddressbyte, finaladdress);
            Console.WriteLine($"Questa è il range di indirizzi disponibili:{starteraddress}-{finaladdress}");
            string[] broadcastbyte = Indirizzi.Broadcast(finaleaddressbyte);
            string broadcast = "";
            broadcast = Indirizzi.ScriviIndirizzo(broadcastbyte, broadcast);
            Console.WriteLine($"Questo è il broadcast:{broadcast}");
            Console.ReadKey();
            return 0;
        }
    }
}

/*for(int i = 0; i < 4; i++)
{
    Console.WriteLine($"{network[i]}");
}*/

