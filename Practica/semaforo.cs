using System;

public class Semaforo
{
    private string colorActual;
    private int tiempoTranscurrido;
    private bool intermitente;

    private readonly (string color, int duracion)[] secuencia = new[]
    {
        ("Rojo", 30),
        ("Rojo-Amarillo", 2),
        ("Verde", 20),
        ("Amarillo", 2)
    };

    private int indiceSecuencia;

    public Semaforo(string colorInicial)
    {
        // Validar color inicial
        if (colorInicial != "Rojo" && colorInicial != "Verde" && colorInicial != "Amarillo")
        {
            throw new ArgumentException("Color inicial debe ser Rojo, Verde o Amarillo");
        }

        // Encontrar el índice de la secuencia correspondiente al color inicial
        for (int i = 0; i < secuencia.Length; i++)
        {
            if (secuencia[i].color.StartsWith(colorInicial))
            {
                indiceSecuencia = i;
                break;
            }
        }

        colorActual = secuencia[indiceSecuencia].color;
        tiempoTranscurrido = 0;
        intermitente = false;
    }

    public void PasoDelTiempo(int segundos)
    {
        if (intermitente)
        {
            // En modo intermitente, el semáforo parpadea amarillo/apagado cada segundo
            // No avanzamos en la secuencia normal
            return;
        }

        tiempoTranscurrido += segundos;

        while (tiempoTranscurrido >= secuencia[indiceSecuencia].duracion)
        {
            tiempoTranscurrido -= secuencia[indiceSecuencia].duracion;
            indiceSecuencia = (indiceSecuencia + 1) % secuencia.Length;
            colorActual = secuencia[indiceSecuencia].color;
        }
    }

    public string MostrarColor()
    {
        if (intermitente)
        {
            // En modo intermitente, alternamos entre amarillo y apagado cada segundo
            return (tiempoTranscurrido % 2 == 0) ? "Amarillo" : "Apagado";
        }

        return colorActual;
    }

    public void PonerEnIntermitente()
    {
        intermitente = true;
        tiempoTranscurrido = 0;
    }

    public void SacarDeIntermitente()
    {
        intermitente = false;
        tiempoTranscurrido = 0;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Simulador de Semáforo");

        // Crear semáforo (puedes cambiar el color inicial)
        Semaforo semaforo = new Semaforo("Verde");

        while (true)
        {
            Console.WriteLine("\nOpciones:");
            Console.WriteLine("1. Avanzar tiempo");
            Console.WriteLine("2. Mostrar color actual");
            Console.WriteLine("3. Poner en modo intermitente");
            Console.WriteLine("4. Sacar de modo intermitente");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese segundos a avanzar: ");
                    if (int.TryParse(Console.ReadLine(), out int segundos))
                    {
                        semaforo.PasoDelTiempo(segundos);
                        Console.WriteLine($"Tiempo avanzado {segundos} segundos.");
                    }
                    else
                    {
                        Console.WriteLine("Entrada inválida.");
                    }
                    break;

                case "2":
                    Console.WriteLine($"Color actual: {semaforo.MostrarColor()}");
                    break;

                case "3":
                    semaforo.PonerEnIntermitente();
                    Console.WriteLine("Semáforo en modo intermitente.");
                    break;

                case "4":
                    semaforo.SacarDeIntermitente();
                    Console.WriteLine("Semáforo en modo normal.");
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }
}
