using System;
using System.IO;

class Program
{
    static int ConsoleIntTryParse()
    {
        int resultado;
        while (!int.TryParse(Console.ReadLine(), out resultado))
        {
            Console.Write("Error. Por favor, ingrese un número válido: ");
        }
        return resultado;
    }

    static void Main()
    {
        int[,] escuela = new int[2, 3];
        bool[,] tieneInternet = new bool[2, 3]; 

        Console.Write("¿Desea cargar los datos a mano? (S/N): ");
        string opcion = Console.ReadLine();

        if (opcion == "N" || opcion == "n")
        {
            escuela[0, 0] = 15; escuela[0, 1] = 25; escuela[0, 2] = 10;
            escuela[1, 0] = 30; escuela[1, 1] = 5; escuela[1, 2] = 22;

            tieneInternet[0, 0] = true; tieneInternet[0, 1] = false; tieneInternet[0, 2] = true;
            tieneInternet[1, 0] = false; tieneInternet[1, 1] = true; tieneInternet[1, 2] = false;

            Console.WriteLine("Datos precargados con éxito.");
        }
        else
        {
            for (int f = 0; f < escuela.GetLength(0); f++)
            {
                for (int c = 0; c < escuela.GetLength(1); c++)
                {
                    Console.WriteLine($"Nos encontramos en: ");
                    Console.Write($"Piso N°:{f} - Laboratorio N°:{c} - Ingrese los alumnos: ");
                    escuela[f, c] = ConsoleIntTryParse();

                    Console.Write("¿Tiene internet? (S/N): ");
                    string r = Console.ReadLine();

                    while (r != "S" && r != "s" && r != "N" && r != "n")
                    {
                        Console.Write("Error. Ingrese S o N: ");
                        r = Console.ReadLine();
                    }

                    if (r == "S" || r == "s")
                    {
                        tieneInternet[f, c] = true;
                    }
                    else
                    {
                        tieneInternet[f, c] = false;
                    }
                }
            }
        }

        Console.Write("¿Qué piso buscás?: ");
        int p = ConsoleIntTryParse();
        Console.Write("¿Qué lab buscás?: ");
        int l = ConsoleIntTryParse();
        bool ubicacion = false;

        while (ubicacion == false)
        {
            if (p >= 0 && p < 2 && l >= 0 && l < 3)
            {
                Console.WriteLine($"Alumnos en el laboratorio N°{l}: {escuela[p, l]}.");
                ubicacion = true;
            }
            else
            {
                Console.WriteLine("Ubicacion inexistente, Ingrese de nuevo.");
                Console.Write("¿Que piso buscas?: ");
                p = ConsoleIntTryParse();
                Console.Write("¿Que lab buscas?: ");
                l = ConsoleIntTryParse();
            }
        }

        int totalLabsConInternet = 0;
        int sumaAlumnosConInternet = 0;

        for (int f = 0; f < 2; f++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (tieneInternet[f, c] == true)
                {
                    totalLabsConInternet = totalLabsConInternet + 1;
                    sumaAlumnosConInternet = sumaAlumnosConInternet + escuela[f, c];
                }
            }
        }

        Console.WriteLine($"Cantidad de laboratorios con internet: {totalLabsConInternet}");

        if (totalLabsConInternet > 0)
        {
            int promedio = sumaAlumnosConInternet / totalLabsConInternet;
            Console.WriteLine($"Promedio de alumnos en labs con internet: {promedio}");
        }

        Console.WriteLine("Laboratorios críticos (Mas de 20 alumnos y SIN internet):");
        for (int f = 0; f < 2; f++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (escuela[f, c] > 20 && tieneInternet[f, c] == false)
                {
                    Console.WriteLine($"ALERTA: Piso {f}, Lab {c} tiene {escuela[f, c]} alumnos y no tiene internet.");
                }
            }
        }

        int suma = 0;
        for (int i = 0; i < 3; i++) suma += escuela[0, i];
        Console.WriteLine($"Total de alumnos en el piso N°0: {suma}");

        string reporte = "REPORTE DE LA ESCUELA \n\n";

        for (int f = 0; f < escuela.GetLength(0); f++)
        {
            for (int c = 0; c < escuela.GetLength(1); c++)
            {
                reporte += $"Piso {f}, Lab {c}: {escuela[f, c]} alumnos. Internet: {tieneInternet[f, c]}\n";
            }
        }

        File.WriteAllText("reporte_escuela.txt", reporte);
        Console.WriteLine("Datos guardados con exito en el disco.");
    }
}
