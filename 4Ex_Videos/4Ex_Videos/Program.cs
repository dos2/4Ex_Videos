using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace _4Ex_Videos
{

    class Program
    {
        static List<Usuari> llistaUsuaris = new List<Usuari>();
        static List<Video> llistaVideos = new List<Video>();
        static void Main(string[] args)
        {
            
            mainMenu();
           

        }

        internal static void mainMenu()
        {
            /*No sé si es muy cómodo poner todas las opciones una detrás de la otra. 
            He pensado que se podría usar una constante Array con todas las opciones válidas y que el bucle compruebe contra esa array.
            ¿Existe alguna función que compruebe si un valor está contenido en un Array o debería crear un bucle foreach?
            */
            Console.WriteLine("Menú principal. Escull una opció: \n 1- Crear un nou usuari. \n 2- Login usuari creat. \n 0- Sortir.");
            List<char> opcions = new List<char>() { '1', '2', '0' };
            escullMenu(validarResposta(Console.ReadLine(), opcions));

        }

        internal static void escullMenu(char opcio)
        {
            switch (opcio)
            {
                case '1':
                    creaUsuariMenu();
                    break;
                case '2':
                    loginUsuariMenu();
                    break;
                case '0':
                    Console.WriteLine("El programa finalitzarà.");
                    break;
                default:
                    Console.WriteLine("Aquesta opció no existeix. Es tornarà al menú principal");
                    mainMenu();
                    break;
            }

        }

        internal static void creaUsuariMenu()
        {
            Console.WriteLine("Creació d'usuari.");
            Usuari user1 = new Usuari();
            Boolean control = false;
            string username, nom, cognom;
            char resposta;
            string pwd = "", pwd2 = "-";
            do
            {
                do
                {
                    Console.WriteLine("Introdueix el nom de l'usuari:");
                    username = respostaBuida(Console.ReadLine());
                    control = true;
                    foreach (Usuari usr in llistaUsuaris)
                    {
                        if (username == usr.USERNAME)
                        {
                            control = false;
                            Console.WriteLine($"El nom d'usuari {username} ja està usat. Utilitza'n un altre.");
                        }
                    }
                } while (control==false);
                control = false;
                Console.WriteLine("Introdueix el teu nom:");
                nom = respostaBuida(Console.ReadLine());
                Console.WriteLine("Introdueix el teu cognom:");
                cognom = respostaBuida(Console.ReadLine());
                Console.WriteLine("Introdueix la contrassenya:");
                pwd = respostaBuida(Console.ReadLine());
                Console.WriteLine("Repeteix la contrassenya:");
                pwd2 = respostaBuida(Console.ReadLine());
                while (pwd != pwd2)
                {
                    Console.WriteLine("Les contrassenyes no coincideixen. Torna-ho a intentar");
                    Console.WriteLine("Introdueix la contrassenya:");
                    pwd = respostaBuida(Console.ReadLine());
                    Console.WriteLine("Repeteix la contrassenya:");
                    pwd2 = respostaBuida(Console.ReadLine());
                }

                Console.WriteLine("S'han introduït correctament tots els elements");
                Console.WriteLine($" Usuari: {username} \n Nom i cognom: {nom} {cognom} \n Contrassenya: {pwd}.");
                Console.WriteLine("\n Són correctes les dades? (S/N). 0 per sortir.");
                List<char> opcions = new List<char> { 'S', 'N', '0' };
                resposta = validarResposta(Console.ReadLine(), opcions);
                if (resposta == 'S')
                {
                    user1.USERNAME = username;
                    user1.NOM = nom;
                    user1.COGNOM = cognom;
                    user1.PASSSWORD = pwd;
                    user1.DATA = System.DateTime.Now;
                    llistaUsuaris.Add(user1);
                    control = true;
                    Console.WriteLine($"Usuari desat correctament. \n Hi ha un total de {llistaUsuaris.Count} usuaris registrats.");
                }
                else if (resposta == 'N')
                {
                    Console.WriteLine("Usuari no desat. Es reiniciarà el procés de creació");
                }
                else if (resposta == '0')
                {
                    control = true;
                    mainMenu();
                }
            } while (control == false);
            mainMenu();

        }

        internal static void loginUsuariMenu()
        {
            Console.WriteLine("Menú login");
            bool block = true;
            string nomUsuari;
            Usuari usuariLogin=null;
            
            do
            {
                Console.WriteLine("Introdueix el teu usuari:");
                nomUsuari = respostaBuida(Console.ReadLine());
                foreach (Usuari usr in llistaUsuaris)
                {
                    if (nomUsuari==usr.USERNAME)
                    {
                        block = false;
                        usuariLogin = usr;
                    }
                }
                if (block) Console.WriteLine($"L'usuari {nomUsuari} no existeix. Intenta-ho de nou.");
            } while (block);
            block = true;
            int intents = 0;
            Console.WriteLine("Introdueix la contrassenya.");
            do
            {
                if (respostaBuida(Console.ReadLine()) == usuariLogin.PASSSWORD)
                {
                    block = false;
                    Console.WriteLine($"S'ha iniciat sessió ammb l'usuari {usuariLogin.USERNAME}.");
                }
                else
                {
                    intents++;
                    Console.WriteLine($"Contrassenya incorrecta. Intents fallits: {intents}. Et queden {3 - intents} oportunitats.");
                }
            } while (block && intents<3);
            
            if (block)
            {
                Console.WriteLine("S'ha excedit el nombre màxim d'intents d'inici de sessió. Es tornarà al menú principal.");
                mainMenu();
            }else
                userMenu(usuariLogin);

        }
        internal static void userMenu(Usuari usuari)
        {
            Console.WriteLine($"Benvingut/da {usuari.NOM} {usuari.COGNOM}. Què desitges fer?");
            Console.WriteLine("1- Crear un vídeo nou. \n2- Seleccionar vídeo\n0- Tornar al menú principal.");
            List<char> opcions = new List<char> { '1', '2', '0' };
            char resposta = validarResposta(Console.ReadLine(), opcions);
            switch (resposta)
            {
                case '1':
                    crearVideoMenu(usuari);
                    break;
                case '2':
                    if (llistaVideos.Count == 0)
                    {
                        Console.WriteLine("No tens cap vídeo creat. No es pot mostrar llistat.");
                        userMenu(usuari);
                    }else
                        llistatVideosMenu(usuari);
                    break;
                case '0':
                    Console.WriteLine("Es tornarà al menú Principal.");
                    mainMenu();
                    break;
                default:
                    Console.WriteLine("Aquesta opció no existeix. Es tornarà al menú principal");
                    mainMenu();
                    break;
            }

        }

        internal static void crearVideoMenu (Usuari usuari)
        {
            Console.WriteLine("Introduïu el títol del vídeo.");
            string titolVideo = respostaBuida(Console.ReadLine());
            Video video1 = new Video();
            video1.USERNAME_USUARI = usuari.USERNAME;
            video1.TITOL = titolVideo;
            video1.URL = $"www.videos.com/{usuari.USERNAME}/{video1.TITOL}";
            addTagMenu(video1);
            llistaVideos.Add(video1);
            Console.WriteLine("Vídeo afegit correctament. Es tornarà al menú d'usuari.");
            userMenu(usuari);
        }
        internal static void llistatVideosMenu (Usuari usuari)
        {
            List<int> opcions = new List<int>();
            int numeracio = 0;
            Console.WriteLine($"Aquests són els vídeos de l'usuari {usuari.USERNAME}:");
            foreach(Video v in llistaVideos)
            {
                if (v.USERNAME_USUARI == usuari.USERNAME)
                {
                    numeracio++;
                    Console.WriteLine(numeracio + "- " + v.TITOL);
                    opcions.Add(numeracio);
                }
            }
            Console.WriteLine("Selecciona un vídeo (usa el número de davant).");
            int resposta = int.Parse(validarResposta(Console.ReadLine(), opcions));
            Video seleccionat = llistaVideos[resposta-1];
            Console.WriteLine($"Vídeo seleccionat: {seleccionat.TITOL}.");
            opcionsVideoMenu(seleccionat);
        }

        internal static void opcionsVideoMenu(Video video)
        {
            Console.WriteLine("Què vols fer amb el vídeo? \n1- Reproduïr-lo \n2- Afegir etiquetes\n0- Tornar enrere.");
            List<char> opcions = new List<char> { '1', '2', '0' };
            char resposta = validarResposta(Console.ReadLine(), opcions);
            switch (resposta)
            {
                case '1':
                    playVideoMenu(video);
                    break;
                case '2':
                    addTagMenu(video);
                    opcionsVideoMenu(video);
                    break;
                case '0':
                    llistatVideosMenu(obteUsuariVideo(video));
                    break;

            }
        }

        internal static void addTagMenu (Video video)
        {
            string nouTag;
            bool continuar=true;
            List<char> opcions=new List<char> { 'S','N'};
            Console.WriteLine($"Menú Afegir tags a vídeo {video.TITOL}.");
            do
            {
                Console.WriteLine("Escriu el tag que vols afegir");
                video.addTags(respostaBuida(Console.ReadLine()));
                
                Console.WriteLine("Tag afegit correctament.");
                Console.WriteLine($"Actualment el vídeo {video.TITOL} té els següents tags: ");
                foreach (string t in video.TAGS) { Console.Write(t+"-"); }
                Console.WriteLine("Vols afegir més tags? (S/N)");
                if(validarResposta(respostaBuida(Console.ReadLine()),opcions)=='N') continuar = false;
            } while (continuar);
        }

        enum accionsVideo {Reproduccio, Pausa, Atura}
        internal static void playVideoMenu (Video video)       
        {
            Console.WriteLine($"Es reproduirà el vídeo {video.TITOL}.");
            Console.WriteLine($"URL del vídeo: {video.URL}");
            Console.WriteLine("Escull una opció: \n1-Reprodueix\n2-Pausa\n3-Atura\n0-Surt");
            List<char> opcions = new List<char>(){ '1', '2', '0' };
            char resposta = validarResposta(Console.ReadLine(), opcions);
            accionsVideo accio=accionsVideo.Reproduccio;
            do
            {
                switch (resposta)
                {
                    case '1':
                        accio = accionsVideo.Reproduccio;
                        break;
                    case '2':
                        accio = accionsVideo.Pausa;
                        break;
                    case '3':
                        accio = accionsVideo.Atura;
                        break;
                    default:
                        opcionsVideoMenu(video);
                        break;

                }
                Console.WriteLine($"Acció escollida: {accio}.");
                Console.WriteLine("Com continuem?\n1-Reprodueix\n2-Pausa\n3-Atura\n0-Surt");
                resposta = validarResposta(Console.ReadLine(), opcions);
            } while (resposta != '0');
            
        }
        internal static char validarResposta(string resposta, List<char> opcions)
        {
            char respostaOk='"';
            bool control = false;
            do
            {
                try
                {
                    respostaOk = Convert.ToChar(resposta);
                    foreach (char r in opcions)
                    {
                        if (respostaOk == r) control = true;
                    }
                    if (control == false)
                    {
                        Console.WriteLine("La resposta és incorrecta. Les opcions correctes són: ");
                        foreach (char r in opcions)
                            Console.Write(r + " ");
                        Console.WriteLine();
                        Console.WriteLine("Torna-ho a intentar.");
                        resposta = Console.ReadLine();
                    }
                }
                catch(FormatException e)
                {
                    Console.WriteLine("Només és vàlid un caràcter: ");
                    foreach(char r in opcions)
                        Console.Write(r + " ");
                    Console.WriteLine();
                    resposta = Console.ReadLine();
                    control = false;
                }

            } while (control==false);
            
            return respostaOk;
            
        }
        internal static string validarResposta(string resposta, List<int> opcions)
        {
            bool control = false;
            do
            {
                    foreach (int r in opcions)
                    {
                        if (resposta == r.ToString()) control = true;
                    }
                    if (control == false)
                    {
                        Console.WriteLine("La resposta és incorrecta. Les opcions correctes són: ");
                        foreach (int r in opcions)
                            Console.Write(r + " ");
                        Console.WriteLine();
                        Console.WriteLine("Torna-ho a intentar.");
                        resposta = Console.ReadLine();
                    }

            } while (control == false);

            return resposta;

        }
        internal static string respostaBuida(string resposta)
        {
            while (resposta == "")
            {
                Console.WriteLine("La resposta no pot estar en blanc. Torna-ho a intentar.");
                resposta = Console.ReadLine();
            }
            return resposta;
        }

        internal static Usuari obteUsuariVideo(Video video)
        {
            Usuari selectedUser=null;
            foreach(Usuari u in llistaUsuaris)
            {
                if (u.USERNAME == video.USERNAME_USUARI)
                    selectedUser = u;
            }
            return selectedUser;
        }
    }
}
