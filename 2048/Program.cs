using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NombreAleatoire();
            AfficheTable();
            DetectionFleche();
        }

        //Creation de de la variable random
        static Random random = new Random();

        //Creation du tableau
        static int[,] table = new int[4, 4];

        static bool CheckForWin()
        {
            // Parcourir le plateau pour vérifier si une tuile de valeur 2048 est présente
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (table[x, y] >= 2048)
                    {
                        return true; // La victoire est atteinte
                    }
                }
            }
            return false; // Aucune tuile de valeur 2048 trouvée
        }

        //Affiche l'affichage de la console
        static void AffichageConsole()
        {
            //Clear la Console pour ne pas que l'affichage se repete
            Console.Clear();

            //Affiche le nom du jeu
            Console.WriteLine("####### 2048 GAME #######\n");

            //Affiche le tableau
            for (int row = 0; row < 4; row++)
            {
                Console.WriteLine();

                for (int col = 0; col < 4; col++)
                {
                    // Choisir une couleur en fonction de la valeur
                    ConsoleColor color = CouleurTuiles(table[row, col]);

                    //Changer la couleur de la case
                    Console.ForegroundColor = color;

                    // Afficher la valeur de la case
                    Console.Write(table[row, col] + "\t");

                    //Rétablir la couleur par défaut
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nScore : " + score);

            if (CheckForWin())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nTu as gagné, tu peux continuer à jouer !!!");
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
            //Nombre aléatoire pour la rangé du tableau donc de 0 à 4
            int randomLine = random.Next(0, 4);
            int randomLine2 = random.Next(0, 4);

            //Nombre aléatoire qui s'affichera dans la console soit 2 soit 4
            int randomNumber2 = (random.Next(10) == 0) ? 4 : 2;

            do
            {
                //Nombre aléatoire pour la rangée du tableau donc de 0 à 4
                randomLine = random.Next(0, 4);
                randomLine2 = random.Next(0, 4);
            } while (table[randomLine, randomLine2] != 0);


            //Remplace une valeur dans le tableau aléatoirement en y mettant un nombre aléatoire soit 2 soit 4
            table[randomLine, randomLine2] = randomNumber2;
        }
        //affiche le tableau ainsi que les nombres aléatoire
        static void AfficheTable()
        {
            Console.ForegroundColor = ConsoleColor.White;

            //Si la tuile random est 0 alors il ajoute un 2 ou un 4
            if (checkValues())
            {
                NombreAleatoire();
                AffichageConsole();

            }
            //S'il n'y a plus de 0 disponnible, un message s'affiche
            else
            {
                //Affiche le tableau
                AffichageConsole();

                if (Defaite() == false)
                {
                    //Affiche le tableau
                    AffichageConsole();
                    //Affiche un message quand la personne perd
                    Console.WriteLine("\nFin de la partie.\n\nTape C pour quitter.");
                }
            }
        }

        //Gère la défaite
        static bool Defaite()
        {
            bool defaite = true;

            for (int i = 1; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    if ((i - 1 >= 0 && table[i, j] == table[i - 1, j]) ||
                        (i + 1 <= 3 && table[i, j] == table[i + 1, j]) ||
                        (j - 1 >= 0 && table[i, j] == table[i, j - 1]) ||
                        (j + 1 <= 3 && table[i, j] == table[i, j + 1]))
                    {
                        defaite = false;
                    }
                }
            }

            return defaite;
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

                int[] table1D;

                //Fait un mouvement dans le tableau en fonction de la flêche choisi ou quitte le programme
                switch (key)
                {
                    //Flêche du haut
                    case ConsoleKey.UpArrow:
                        for (int i = 0; i < 4; i++)
                        {
                            table1D = ChangeOrder(table[0, i], table[1, i], table[2, i], table[3, i]);
                            table[0, i] = table1D[0];
                            table[1, i] = table1D[1];
                            table[2, i] = table1D[2];
                            table[3, i] = table1D[3];
                        }
                        break;

                    //Flêche du bas
                    case ConsoleKey.DownArrow:
                        for (int i = 0; i < 4; i++)
                        {
                            table1D = ChangeOrder(table[3, i], table[2, i], table[1, i], table[0, i]);
                            table[0, i] = table1D[3];
                            table[1, i] = table1D[2];
                            table[2, i] = table1D[1];
                            table[3, i] = table1D[0];
                        }
                        break;

                    //Flêche de gauche
                    case ConsoleKey.LeftArrow:
                        for (int i = 0; i < 4; i++)
                        {
                            table1D = ChangeOrder(table[i, 0], table[i, 1], table[i, 2], table[i, 3]);
                            table[i, 0] = table1D[0];
                            table[i, 1] = table1D[1];
                            table[i, 2] = table1D[2];
                            table[i, 3] = table1D[3];
                        }
                        break;

                    //Flêche de droite
                    case ConsoleKey.RightArrow:
                        for (int i = 0; i < 4; i++)
                        {
                            table1D = ChangeOrder(table[i, 3], table[i, 2], table[i, 1], table[i, 0]);
                            table[i, 0] = table1D[3];
                            table[i, 1] = table1D[2];
                            table[i, 2] = table1D[1];
                            table[i, 3] = table1D[0];
                        }
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
                AfficheTable();
            }
        }

        //Gère le mouvement et la fusion des tuiles
        static int[] ChangeOrder(int nb0, int nb1, int nb2, int nb3)
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

        /*
        //Mouvement fleche du haut
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
                    }
                }
            }
        }

        //Mouvement fleche du bas
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
                    }
                }
            }
        }

        //Mouvement fleche de gauche
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
                    }
                }
            }
        }

        //Mouvement fleche de droite
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
                    }
                }
            }
        }

        //Fusion des tuiles vers le haut
        static void FusionHaut()
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

                        //Fusionne les tuiles si elles ont la même valeur
                        if (row > 0 && table[row - 1, y] == table[row, y])
                        {
                            table[row - 1, y] *= 2;
                            table[row, y] = 0;
                            score += table[row - 1, y];

                            //Gère une partie de la victoire
                            if (table[row - 1, y] == 2048)
                            {
                                Index += 1;
                            }
                        }
                    }
                }
            }
        }

        //Fusion des tuiles vers le bas
        static void FusionBas()
        {
            int size = table.GetLength(0);

            for (int y = 0; y < size; y++)
            {
                for (int x = size - 2; x >= 0; x--)
                {
                    if (table[x, y] != 0)
                    {
                        int row = x;

                        //Fusionne les tuiles si elles ont la même valeur
                        if (row < size - 1 && table[row + 1, y] == table[row, y])
                        {
                            table[row + 1, y] *= 2;
                            table[row, y] = 0;
                            score += table[row + 1, y];

                            //Gère une partie de la victoire
                            if (table[row + 1, y] == 2048)
                            {
                                Index += 1;
                            }
                        }
                    }
                }
            }

        }

        //Fusion des tuiles vers la gauche
        static void FusionGauche()
        {
            int size = table.GetLength(0);

            for (int x = 0; x < size; x++)
            {
                for (int y = 1; y < size; y++)
                {
                    if (table[x, y] != 0)
                    {
                        int col = y;

                        //Fusionne les tuiles si elles ont la même valeur
                        if (col > 0 && table[x, col - 1] == table[x, col])
                        {
                            table[x, col - 1] *= 2;
                            table[x, col] = 0;
                            score += table[x, col - 1];

                            //Gère une partie de la victoire
                            if (table[x, col - y] == 2048)
                            {
                                Index += 1;
                            }
                        }
                    }
                }
            }

        }

        //Fusion des tuiles vers la droite
        static void FusionDroite()
        {
            int size = table.GetLength(0);

            for (int x = 0; x < size; x++)
            {
                for (int y = size - 2; y >= 0; y--)
                {
                    if (table[x, y] != 0)
                    {
                        int col = y;

                        //Fusionne les tuiles si elles ont la même valeur
                        if (col < size - 1 && table[x, col + 1] == table[x, col])
                        {
                            table[x, col + 1] *= 2;
                            table[x, col] = 0;
                            score += table[x, col + 1];

                            //Gère une partie de la victoire
                            if (table[x, col + 1] == 2048)
                            {
                                Index += 1;
                            }
                        }
                    }
                }
            }

        }
        */
        //Incrémentation du score
        static int score = 0;

        //Mettre des couleurs par nombre
        static ConsoleColor CouleurTuiles(int value)
        {
            switch (value)
            {
                case 0:
                    return ConsoleColor.DarkGray;
                case 2:
                    return ConsoleColor.Red;
                case 4:
                    return ConsoleColor.Blue;
                case 8:
                    return ConsoleColor.Yellow;
                case 16:
                    return ConsoleColor.Green;
                case 32:
                    return ConsoleColor.Cyan;
                case 64:
                    return ConsoleColor.Magenta;
                case 128:
                    return ConsoleColor.DarkRed;
                case 256:
                    return ConsoleColor.DarkBlue;
                case 512:
                    return ConsoleColor.DarkYellow;
                case 1024:
                    return ConsoleColor.DarkGreen;
                case 2048:
                    return ConsoleColor.DarkCyan;
                default:
                    return ConsoleColor.White; // Couleur par défaut
            }
        }
    }
}