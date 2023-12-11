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
            Victoire();

            /*if (CheckForWin())
            {
                Console.WriteLine("Félicitations ! Vous avez atteint 2048 !");
            }*/

        }
        static int[,] table = new int[4, 4];

        static bool CheckForWin()
        {
            // Parcourir le plateau pour vérifier si une tuile de valeur 2048 est présente
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (table[x, y] == 2048)
                    {
                        return true; // La victoire est atteinte
                    }
                }
            }

            return false; // Aucune tuile de valeur 2048 trouvée
        }

        static void AfficheTableau()
        {
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
        static void AfficheTable()
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
                        FusionHaut();
                        MouvementHaut();
                        AfficheTable();
                        break;

                    //Flêche du bas
                    case ConsoleKey.DownArrow:
                        MouvementBas();
                        FusionBas();
                        MouvementBas();
                        AfficheTable();
                        break;

                    //Flêche de gauche
                    case ConsoleKey.LeftArrow:
                        MouvementGauche();
                        FusionGauche();
                        MouvementGauche();
                        AfficheTable();
                        break;

                    //Flêche de droite
                    case ConsoleKey.RightArrow:
                        MouvementDroite();
                        FusionDroite();
                        MouvementDroite();
                        AfficheTable();
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

                        }
                    }
                }
            }

        }

        //Incrémentation du score
        static int score = 0;

        //Mettre des couleurs par nombre
        static ConsoleColor CouleurTuiles(int value)
        {
            switch (value)
            {
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

        //Gerer la victoire
        static void Victoire()
        {
            int size = table.GetLength(0);

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (table[x, y] == 2048)
                    {
                        Console.WriteLine("Tu as gagné, Bravo !!!\nTu peux continuer à jouer.");
                    }
                }
            }
        }
    }
}