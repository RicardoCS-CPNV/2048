using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
/*
 * Auteur : Ricardo Costa Santos
 * Date : 16.12.2023
 * Description : Programme qui execute le jeu 2048 en mode console. 
 */
namespace _2048
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NombreAleatoire();
            NombreAleatoire();
            AffichageConsole();

            //Entre dans une boucle
            while (true)
            {
                //Tant que les conditions sont remplies, l'utilisateur reste dans la boucle if
                if (!Defaite)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.LeftArrow ||
                        keyInfo.Key == ConsoleKey.UpArrow ||
                        keyInfo.Key == ConsoleKey.RightArrow ||
                        keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        bool tableChange = DetectionFleche(keyInfo.Key);
                        if (tableChange)
                        {
                            NombreAleatoire();
                        }

                        Console.Clear();
                        AffichageConsole();

                        if (Victoire())
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nTu as gagné, tu peux continuer à jouer !!!");
                        }
                        else if (!TableauRempli())
                        {
                            Console.Clear();
                            AffichageConsole();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Game Lost");
                            Defaite = true;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.C)
                    {
                        return; //Quitte le programme
                    }
                    else
                    {
                        Console.WriteLine("Veuillez taper une touche valide.");
                    }
                }
                else //L'utilisateur vient de perdre la partie
                {
                    Console.Clear();
                    AffichageConsole();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nVous avez perdu. Appuyez sur 'C' pour quitter.");
                    if (Console.ReadKey(true).Key == ConsoleKey.C)
                    {
                        return; //Quitte le programme
                    }
                }
            }
        }

        //Creation du tableau
        static int[,] table = new int[4, 4];

        //Creation d'un bool pour vérifier si l'utilisateur a perdu
        static bool Defaite = false;

        //Incrementation du score
        static int score = 0;

        //Affiche le nom du jeu, le tableau et le score
        static void AffichageConsole()
        {
            Console.WriteLine("####### 2048 GAME #######"); //Affiche le nom du jeu

            for (int row = 0; row < table.GetLength(0); row++)
            {
                Console.WriteLine();
                for (int col = 0; col < table.GetLength(1); col++)
                {
                    CouleurTuiles(table[row, col]); // Choisir une couleur en fonction de la valeur
                    Console.Write(table[row, col] + "\t"); //Affiche la valeur de la tuile
                    Console.ResetColor(); //Rétablie la couleur par défaut
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nScore : " + score); //Affichage du score
        }

        //Créer un nombre aléatoire et selectionne une tuile aléatoire
        static void NombreAleatoire()
        {
            Random random = new Random();
            int randomNumber2 = (random.Next(10) == 0) ? 4 : 2;

            if (!AjoutNombrePossible())
            {
                return;
            }

            bool caseVide = false;
            while (!caseVide)
            {
                int randomLine = random.Next(0, 4);
                int randomLine2 = random.Next(0, 4);

                if (table[randomLine, randomLine2] == 0)
                {
                    table[randomLine, randomLine2] = randomNumber2;
                    caseVide = true;
                }
            }
        }

        //Regarde si la valeur est de 0
        static bool AjoutNombrePossible()
        {
            for (int row = 0; row < table.GetLength(0); row++)
            {
                for (int col = 0; col < table.GetLength(1); col++)
                {
                    if (table[row, col] == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Detecte les touches tapées
        static bool DetectionFleche(ConsoleKey key)
        {
            int[,] tableTest = (int[,])table.Clone(); //Créer une copie du tableau avant de faire le mouvement
            int[] table1D;
            switch (key)
            {
                //Flêche du haut
                case ConsoleKey.UpArrow:
                    for (int i = 0; i < 4; i++)
                    {
                        table1D = MouvementFusion(table[0, i], table[1, i], table[2, i], table[3, i]);
                        table[0, i] = table1D[0];
                        table[1, i] = table1D[1];
                        table[2, i] = table1D[2];
                        table[3, i] = table1D[3];
                    }
                    AffichageConsole();
                    break;

                //Flêche du bas
                case ConsoleKey.DownArrow:
                    for (int i = 0; i < 4; i++)
                    {
                        table1D = MouvementFusion(table[3, i], table[2, i], table[1, i], table[0, i]);
                        table[0, i] = table1D[3];
                        table[1, i] = table1D[2];
                        table[2, i] = table1D[1];
                        table[3, i] = table1D[0];
                    }
                    AffichageConsole();
                    break;

                //Flêche de gauche
                case ConsoleKey.LeftArrow:
                    for (int i = 0; i < 4; i++)
                    {
                        table1D = MouvementFusion(table[i, 0], table[i, 1], table[i, 2], table[i, 3]);
                        table[i, 0] = table1D[0];
                        table[i, 1] = table1D[1];
                        table[i, 2] = table1D[2];
                        table[i, 3] = table1D[3];
                    }
                    AffichageConsole();
                    break;

                //Flêche de droite
                case ConsoleKey.RightArrow:
                    for (int i = 0; i < 4; i++)
                    {
                        table1D = MouvementFusion(table[i, 3], table[i, 2], table[i, 1], table[i, 0]);
                        table[i, 0] = table1D[3];
                        table[i, 1] = table1D[2];
                        table[i, 2] = table1D[1];
                        table[i, 3] = table1D[0];
                    }
                    AffichageConsole();
                    break;

                //C quitte le programme
                case ConsoleKey.C:
                    break;

                //Affiche un message d'erreur et demande d'appuyer sur une flêche
                default:
                    Console.WriteLine("Veuillez taper une touche valide.");
                    break;
            }
            return ChangementTable(tableTest, table); //Verifie si le tableau a été modifié après le mouvement
        }

        //Gère le mouvement et la fusion des tuiles
        static int[] MouvementFusion(int nb0, int nb1, int nb2, int nb3)
        {
            //Gère le mouvement
            if (nb2 == 0 && nb3 > 0)
            {
                nb2 = nb3;
                nb3 = 0;
            }
            if (nb1 == 0 && nb2 > 0)
            {
                nb1 = nb2;
                nb2 = nb3;
                nb3 = 0;
            }
            if (nb0 == 0 && nb1 > 0)
            {
                nb0 = nb1;
                nb1 = nb2;
                nb2 = nb3;
                nb3 = 0;
            }
            //Gère la fusion
            if (nb0 == nb1)
            {
                nb0 += nb1;
                nb1 = nb2;
                nb2 = nb3;
                nb3 = 0;
                score += nb0;
            }
            if (nb1 == nb2)
            {
                nb1 += nb2;
                nb2 = nb3;
                nb3 = 0;
                score += nb1;
            }
            if (nb2 == nb3)
            {
                nb2 += nb3;
                nb3 = 0;
                score += nb2;
            }
            int[] tableau = { nb0, nb1, nb2, nb3 };
            return tableau;
        }
        //Verifie si le tableau est remplie et si des mouvements sont encores possible
        static bool TableauRempli()
        {
            for (int row = 0; row < table.GetLength(0); row++)
            {
                for (int col = 0; col < table.GetLength(1); col++)
                {
                    if (table[row, col] == 0)
                        return true; //Trouve un 0 dans le tableau

                    if (row < table.GetLength(0) - 1 && table[row, col] == table[row + 1, col])
                        return true; //Mouvement Verticale possible

                    if (col < table.GetLength(1) - 1 && table[row, col] == table[row, col + 1])
                        return true; //M ouvement Horizontale possible
                }
            }

            return false;
        }

        //Verifie si le tableau est identique ou non
        static bool ChangementTable(int[,] tableOriginal, int[,] tableCourante)
        {
            for (int row = 0; row < tableOriginal.GetLength(0); row++)
            {
                for (int col = 0; col < tableOriginal.GetLength(1); col++)
                {
                    if (tableOriginal[row, col] != tableCourante[row, col])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Verifie la victoire
        static bool Victoire()
        {
            for (int row = 0; row < table.GetLength(0); row++)
            {
                for (int col = 0; col < table.GetLength(1); col++)
                {
                    if (table[row, col] >= 2048)
                        return true;
                }
            }
            return false;
        }

        //Mettre des couleurs pour chaques cases
        static void CouleurTuiles(int value)
        {
            switch (value)
            {
                case 0: Console.ForegroundColor = ConsoleColor.DarkGray; break;
                case 2: Console.ForegroundColor = ConsoleColor.Red; break;
                case 4: Console.ForegroundColor = ConsoleColor.Blue; break;
                case 8: Console.ForegroundColor = ConsoleColor.Yellow; break;
                case 16: Console.ForegroundColor = ConsoleColor.Green; break;
                case 32: Console.ForegroundColor = ConsoleColor.Cyan; break;
                case 64: Console.ForegroundColor = ConsoleColor.Magenta; break;
                case 128: Console.ForegroundColor = ConsoleColor.DarkRed; break;
                case 256: Console.ForegroundColor = ConsoleColor.DarkBlue; break;
                case 512: Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                case 1024: Console.ForegroundColor = ConsoleColor.DarkGreen; break;
                case 2048: Console.ForegroundColor = ConsoleColor.DarkMagenta; break;
                default: Console.ForegroundColor = ConsoleColor.White; break; //Couleur par défaut
            }
        }
    }
}