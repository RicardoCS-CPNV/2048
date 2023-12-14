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

        //Creation du tableau
        static int[,] table = new int[4, 4];

        //Creation de de la variable random
        static Random random = new Random();

        //Incrémentation du score
        static int score = 0;

        //Affiche l'affichage de la console
        static void AffichageConsole()
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

            if (Victoire())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nTu as gagné, tu peux continuer à jouer !!!");
            }
        }

        //affiche le tableau ainsi que les nombres aléatoire
        static void AfficheTable()
        {
            //Clear la Console pour ne pas que l'affichage se repete
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            //Si la tuile random est 0 alors il ajoute un 2 ou un 4
            if (CheckValues())
            {
                NombreAleatoire();
                AffichageConsole();
            }
            //S'il n'y a plus de 0 disponnible, un message s'affiche
            else
            {
                //Affiche le tableau
                AffichageConsole();

                // Ajout de la vérification de défaite ici
                if (!Defaite())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nVous avez perdu. Appuyez sur 'C' pour quitter.");

                    bool pressC = true;

                    while (pressC)
                    {

                        //Attent que l'utilisateur tape une flêche
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        ConsoleKey keyC = keyInfo.Key;

                        switch (keyC)
                        {
                            //C quitte le programme
                            case ConsoleKey.C:
                                pressC = false;
                                break;

                            //Affiche un message d'erreur et demande d'appuyer sur une flêche
                            default:
                                break;
                        }
                    }
                }
            }
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

        static int[,] tableTest = new int[4, 4];

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
                            table1D = MouvementFusion(table[0, i], table[1, i], table[2, i], table[3, i]);
                            table[0, i] = table1D[0];
                            table[1, i] = table1D[1];
                            table[2, i] = table1D[2];
                            table[3, i] = table1D[3];
                        }

                        //TEST
                        /*Array.Copy(table, tableTest, table.Length);
                        for (int i = 0; i < 4; i++)
                        {


                            table1D = MouvementFusion(tableTest[0, i], tableTest[1, i], tableTest[2, i], tableTest[3, i]);
                            tableTest[0, 0] = table1D[0];
                            tableTest[1, i] = table1D[1];
                            tableTest[2, i] = table1D[2];
                            tableTest[3, i] = table1D[3];
                        }
                        ArraysEqual(tableTest, table);*/
                        AfficheTable();

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
                        AfficheTable();

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
                        AfficheTable();

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

        //Check si le tableau contient 2048 ou plus
        static bool Victoire()
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

        //Regarde si la valeur est de 0
        static bool CheckValues()
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

        //Gère la défaite
        static bool Defaite()
        {
            // Vérifie si toutes les cases sont remplies
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (table[i, j] == 0)
                    {
                        return false; // Il reste des cases vides, la partie continue
                    }
                }
            }

            int[,] tableTemp = new int[4, 4];
            Array.Copy(table, tableTemp, table.Length);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    //Verification vers le haut
                    if (i - 1 >= 0)
                    {
                        tableTemp[i, j] = table[i - 1, j];
                        tableTemp[i - 1, j] = table[i, j];
                        if (!ArraysEqual(table, tableTemp))
                        {
                            return false; // Un mouvement possible est trouvé, la partie continue
                        }
                        // Rétablir la copie temporaire
                        Array.Copy(table, tableTemp, table.Length);
                    }

                    //Verification vers le bas
                    if (i + 1 < 4)
                    {
                        tableTemp[i, j] = table[i + 1, j];
                        tableTemp[i + 1, j] = table[i, j];
                        if (!ArraysEqual(table, tableTemp))
                        {
                            return false; // Un mouvement possible est trouvé, la partie continue
                        }
                        // Rétablir la copie temporaire
                        Array.Copy(table, tableTemp, table.Length);
                    }

                    //Verification vers le gauche
                    if (j - 1 >= 0)
                    {
                        tableTemp[i, j] = table[i, j - 1];
                        tableTemp[i, j - 1] = table[i, j];
                        if (!ArraysEqual(table, tableTemp))
                        {
                            return false; // Un mouvement possible est trouvé, la partie continue
                        }
                        // Rétablir la copie temporaire
                        Array.Copy(table, tableTemp, table.Length);
                    }

                    //Verification vers le droite
                    if (j + 1 < 4)
                    {
                        tableTemp[i, j] = table[i, j + 1];
                        tableTemp[i, j + 1] = table[i, j];
                        if (!ArraysEqual(table, tableTemp))
                        {
                            return false; // Un mouvement possible est trouvé, la partie continue
                        }
                        // Rétablir la copie temporaire
                        Array.Copy(table, tableTemp, table.Length);
                    }

                }
            }

            return true;
        }

        static bool ArraysEqual(int[,] array1, int[,] array2)
        {
            for (int i = 0; i < array1.GetLength(0); i++)
            {
                for (int j = 0; j < array1.GetLength(1); j++)
                {
                    if (array1[i, j] != array2[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //Mettre des couleurs pour chaque nombre
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