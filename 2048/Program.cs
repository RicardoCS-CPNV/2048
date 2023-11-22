using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NombreAleatoire();
            AfficheTabAle();
            DetectionFleche();
        }
        static int[,] table = new int[4, 4];
        static void AfficheTableau()
        {
            //Affiche le nom du jeu
            Console.WriteLine("####### 2048 GAME #######\n");
            //Affiche le tableau
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    Console.Write(table[row, col] + "\t");
                }
                Console.WriteLine();
            }
        }
        //Regarde si la valeur est de 0
        static bool checkValues()
        {
            //Check si la valeur est 0
            foreach (int i in table)
            {
                if (i == 0)
                {
                    return true;
                }
            }
            return false;
        }
        //Créer un nombre aléatoire et selectionne une tuile aléatoire
        static void NombreAleatoire()
        {
            //Creation de nombre aléatoire
            Random random = new Random();
            //Nombre aléatoire pour la rangé du tableau donc de 0 à 4
            int randomLine = random.Next(0, 4);
            int randomLine2 = random.Next(0, 4);

            //Nombre aléatoire qui s'affichera dans la console de 0 à 9
            int randomNumber2 = (random.Next(10) == 0) ? 4 : 2;

            do
            {
                //Nombre aléatoire pour la rangée du tableau donc de 0 à 4
                randomLine = random.Next(0, 4);
                randomLine2 = random.Next(0, 4);
            } while (table[randomLine, randomLine2] != 0);


            //Remplace une valeur dans le tableau aléatoirement en y mettant un nombre aléatoire entre 0 et 9
            table[randomLine, randomLine2] = randomNumber2;
        }
        //affiche le tableau ainsi que les nombres aléatoire
        static void AfficheTabAle()
        {
            //Clear la Console pour ne pas que l'affichage se repete
            Console.Clear();

            //Si la tuile random est 0 alors il ajoute un 2 ou un 4
            if (checkValues())
            {
                NombreAleatoire();
                AfficheTableau();

            }
            //S'il n'y a plus de 0 disponnible, un message s'affiche
            else
            {
                //Affiche le tableau
                AfficheTableau();
                //Affiche un message quand la personne perd
                Console.WriteLine("\nYou failed !\nPress C to leave.");
            }

        }
        //Detecte la flêche selectionnée et quitte si l'utilisateur tape C
        static void DetectionFleche()
        {
            bool arrow = true;

            //Boucle permettant de rester dans la DetectionFleche jusqu'a ce que l'utilisateur tape C
            while (arrow)
            {
                //Attent que l'utilisateur tape une flêche
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                ConsoleKey key = keyInfo.Key;

                //Fait un mouvement dans le tableau en fonction de la flêche choisi ou quitte le programme
                switch (key)
                {
                    //Flêche du haut
                    case ConsoleKey.UpArrow:
                        MouvementHaut();
                        AfficheTabAle();
                        break;

                    //Flêche du bas
                    case ConsoleKey.DownArrow:
                        MouvementBas();
                        AfficheTabAle();
                        break;

                    //Flêche de gauche
                    case ConsoleKey.LeftArrow:
                        MouvementGauche();
                        AfficheTabAle();
                        break;

                    //Flêche de droite
                    case ConsoleKey.RightArrow:
                        MouvementDroite();
                        AfficheTabAle();
                        break;

                    //C quitte le programme
                    case ConsoleKey.C:
                        arrow = false;
                        break;

                    //Affiche un message d'erreur et demande d'appuyer sur une flêche
                    default:
                        Console.WriteLine("Veuillez taper une touche valide.");
                        break;

                }
            }
        }
        //Mouvement et fusion fleche du haut
        static void MouvementHaut()
        {
            int size = table.GetLength(0);

            //Parcourir chaque colonne de gauche à droite
            for (int y = 0; y < size; y++)
            {
                for (int x = 1; x < size; x++)
                {
                    if (table[x, y] != 0)
                    {
                        int row = x;

                        //Déplace la tuile vers le haut
                        while (row > 0 && table[row - 1, y] == 0)
                        {
                            table[row - 1, y] = table[row, y];
                            table[row, y] = 0;
                            row--;
                        }

                        //Fusionne les tuiles si elles ont la même valeur
                        if (row > 0 && table[row - 1, y] == table[row, y])
                        {
                            table[row - 1, y] *= 2;
                            table[row, y] = 0;
                        }
                    }
                }
            }
        }
        //Mouvement et fusion fleche du bas
        static void MouvementBas()
        {
            int size = table.GetLength(0);

            for (int y = 0; y < size; y++)
            {
                for (int x = size - 2; x >= 0; x--)
                {
                    if (table[x, y] != 0)
                    {
                        int row = x;

                        //Déplace la tuile vers le bas
                        while (row < size - 1 && table[row + 1, y] == 0)
                        {
                            table[row + 1, y] = table[row, y];
                            table[row, y] = 0;
                            row++;
                        }

                        //Fusionne les tuiles si elles ont la même valeur
                        if (row < size - 1 && table[row + 1, y] == table[row, y])
                        {
                            table[row + 1, y] *= 2;
                            table[row, y] = 0;
                        }
                    }
                }
            }
        }
        //Mouvement et fusion fleche de gauche
        static void MouvementGauche()
        {
            int size = table.GetLength(0);

            for (int x = 0; x < size; x++)
            {
                for (int y = 1; y < size; y++)
                {
                    if (table[x, y] != 0)
                    {
                        int col = y;

                        //Déplace la tuile vers la gauche
                        while (col > 0 && table[x, col - 1] == 0)
                        {
                            table[x, col - 1] = table[x, col];
                            table[x, col] = 0;
                            col--;
                        }

                        //Fusionne les tuiles si elles ont la même valeur
                        if (col > 0 && table[x, col - 1] == table[x, col])
                        {
                            table[x, col - 1] *= 2;
                            table[x, col] = 0;
                        }
                    }
                }
            }
        }
        //test
        //Mouvement et fusion fleche de droite
        static void MouvementDroite()
        {
            int size = table.GetLength(0);

            for (int x = 0; x < size; x++)
            {
                for (int y = size - 2; y >= 0; y--)
                {
                    if (table[x, y] != 0)
                    {
                        int col = y;

                        //Déplace la tuile vers la droite
                        while (col < size - 1 && table[x, col + 1] == 0)
                        {
                            table[x, col + 1] = table[x, col];
                            table[x, col] = 0;
                            col++;
                        }

                        //Fusionne les tuiles si elles ont la même valeur
                        if (col < size - 1 && table[x, col + 1] == table[x, col])
                        {
                            table[x, col + 1] *= 2;
                            table[x, col] = 0;
                        }
                    }
                }
            }
        }
    }
}