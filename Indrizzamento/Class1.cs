using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Indrizzamento
{
    class Indirizzi
    {
        //Trasformo i numeri interi in stringhe di bit

        internal static string ToBit(int numero)
        {
            string bit = "";
            bit = Convert.ToString(numero, 2).PadLeft(8, '0');
            return bit;
        }



        //Creo un array contenente i numeri dell'indirizzo in bit 
        internal static void ConvertiInBytes(int[] numeri,string[] bytes)
        {
            string bits;
            for(int i = 0; i < 4; i++)
            {
                //bits = ToBit(numeri[i]);
                bits = ToBit(numeri[i]);
                bytes[i] = bits;
            }
        }

        //Controllo che l'indirizzo ip sia valido
        internal static bool ControlloIndirizzo(string indirizzo)
        {
            string[] bytes = new string[4];
            bytes = indirizzo.Split(".");//tolgo i punti dalla stringa dell'indirizzo e inserisco le singole stringhe all'interno dell'array bytes
            for(int i = 0; i < 4; i++)
            {
                //Console.WriteLine($"{bytes[i]}");
                if (Convert.ToInt32(bytes[i]) < 0 || Convert.ToInt32(bytes[i]) > 255)
                {
                    return false;
                }
            }
            return true;
        }

        //Controllo CIDR
        internal static bool ControllaCIDR(int mask)
        {
            if (mask < 8 || mask > 32)
            {
                return false;
            }
            return true;
        }

        //Risolvo la CIDR in bit
        internal static int[] RisolviCIDR(int mask)
        {
            int[] maskbits = new int[32];
            for(int i = 0; i < 32; i++)
            {
                if (i < mask)
                {
                    maskbits[i] = 1;
                }
                else
                {
                    maskbits[i] = 0;
                }
                
            }
            return maskbits;
        }

        //Controllo classe appartenenza
        internal static void ControlloClasse(int mask)
        {
            if (mask >= 8 && mask < 16)
            {
                Console.WriteLine("Classe A");
            }else if (mask >= 16 && mask < 24)
            {
                Console.WriteLine("Classe B");
            }
            else
            {
                Console.WriteLine("Classe C");
            }
        }

        //Metto la maschera in byte
        internal static string[] RisolviMaschera(int[] bytesmask)
        {
            string result="";
            for (int i = 0; i < 32; i++)
            {
                if (i == 7 || i == 15 || i == 23)
                {
                    result += bytesmask[i].ToString()+".";
                }
                else
                {
                    result += bytesmask[i].ToString();
                }
                
            }
            string[] bytes = new string[4];
            bytes = result.Split(".");
            return bytes;
        }

        //Creo un array di int contenenti i numeri dell'indirizzo
        internal static void Scrivinumeri(string indirizzo,int[] numeri)
        {
            string[] bytes = new string[4];
            bytes = indirizzo.Split(".");
            for(int i = 0; i < 4; i++)
            {
                numeri[i] = Convert.ToInt32(bytes[i]);
            }

        }

        //Trasfroma i byte in interi
        internal static int ByteToNumber(string bytes)
        {
            int numero=0;
            
            for(int i = 0; i < 8;)
            {
                //1 convertito da stringa a int è 49
                if (i==0 && Convert.ToInt32(bytes[i])==49)
                {
                    numero += 128;
                }
                else if(i==1 && Convert.ToInt32(bytes[i])==49)
                {
                    numero += 64;
                }
                else if (i == 2 && Convert.ToInt32(bytes[i]) == 49)
                {
                    numero += 32;
                }
                else if (i == 3 && Convert.ToInt32(bytes[i]) == 49)
                {
                    numero += 16;
                }
                else if (i == 4 && Convert.ToInt32(bytes[i]) == 49)
                {
                    numero += 8;
                }
                else if (i == 5 && Convert.ToInt32(bytes[i]) == 49)
                {
                    numero += 4;
                }
                else if (i == 6 && Convert.ToInt32(bytes[i]) == 49)
                {
                    numero += 2;
                }
                else if (i == 7 && Convert.ToInt32(bytes[i]) == 49)
                {
                    numero += 1;
                }
                i++;
            }
            //Console.WriteLine($"Il numero è:{numero}");
            return numero;
        }

        //Scrivo la maschera indirizzi in interi

        internal static string ScriviIndirizzo(string[] fonte,string obiettivo)
        {
            int numero;
            for(int i = 0; i < 4; i++)
            {
                numero = ByteToNumber(fonte[i]);
                if (i == 3)
                {
                    obiettivo += Convert.ToString(numero);
                }
                else
                {
                    obiettivo += Convert.ToString(numero) + ".";
                }
            }
            return obiettivo;
        }

        //Trovo net id in bit
        internal static string[] CalcolaNetwrok(string[] bytes,string[] maschera)
        {
            string[] network = new string[4];
            for(int i = 0; i < 4; i++)
            {
                for(int j=0; j < 8; j++)
                {
                    if (Convert.ToInt32(bytes[i][j]) == Convert.ToInt32(maschera[i][j]) && Convert.ToInt32(bytes[i][j])==49)
                    {
                        network[i] += "1";
                    }
                    else
                    {
                        network[i] += "0";
                    }
                }
            }
            return network;
        }

        //RANGE indirizzi in bit
        //Iniziale
        internal static string[] AddressIniziale(string[] network)
        {
            string[] iniziale = new string[4];
            for (int i= 0; i < 4; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if (i == 3 && j == 7)//ultimo bit a 1
                    {
                        iniziale[i] += "1";
                    }
                    else
                    {
                        iniziale[i] +=network[i][j];
                    }
                }
            }

            return iniziale;
        }
        //finale
        internal static string[] AddressFinale(string[] starteraddress,int cidr)
        {
            string[] finaladdress = new string[4];
            int counter = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    counter++;
                    if (counter <= cidr)
                    {
                        finaladdress[i] += starteraddress[i][j];
                    }
                    else
                    {
                        if(i==3 && j == 7)
                        {
                            finaladdress[i] +="0";
                        }
                        else
                        {
                            finaladdress[i] += "1";
                        }
                    }
                }
            }
            return finaladdress;
        }

        //scrivo indirizzo broadcast in bit
        internal static string[] Broadcast(string[] finalestring)
        {
            string[] broadcastbyte = new string[4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if(i==3 && j == 7)
                    {
                        broadcastbyte[i] +="1";
                    }
                    else
                    {
                        broadcastbyte[i] += finalestring[i][j];
                    }
                }
            }
            return broadcastbyte;
        }

    }
}
