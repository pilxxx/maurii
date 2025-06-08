using System;

namespace VehiculosCarrera
{
    // interfaz para los vehículos (todos tienen q moverse y saber su posición)
    interface IVehiculo
    {
        void Mover(int tiempoEnSegundos);
        int PosicionActual();
        void Resetear();
    }

    // Bici: va lento pero constante
    class Bicicleta : IVehiculo
    {
        private int distanciaRecorrida = 0;
        private const int velocidad = 10; // en m/s

        public void Mover(int segundos)
        {
            distanciaRecorrida += velocidad * segundos;
        }

        public int PosicionActual()
        {
            return distanciaRecorrida;
        }

        public void Resetear()
        {
            distanciaRecorrida = 0;
        }
    }

    // camión: más rápido que una bici, pero no tanto como un auto
    class Camion : IVehiculo
    {
        private int metros = 0;
        private const int rapidez = 30; // m/s

        public void Mover(int tiempo)
        {
            metros += rapidez * tiempo;
        }
        public int PosicionActual()
        {
            return metros;
        }

        public void Resetear()
        {
            metros = 0;
        }
    }

    // Auto: el más rápido, pero se puede personalizar su velocidad
    class Auto : IVehiculo
   {
        private int pos = 0;
        private int vel;
        public Auto(int velocidad = 40) // por defecto 40 m/s
        {
            vel = velocidad;
        }
        public void Mover(int s)
        {
            pos += vel * s;
        }
        public int PosicionActual()
        {
            return pos;
        }
        public void Resetear()
        {
            pos = 0;
        }
    }

    // imula una carrera entre dos vehículos
    class Carrera
    {
        private IVehiculo vehiculo1;
        private IVehiculo vehiculo2;
        public Carrera(IVehiculo v1, IVehiculo v2)
        {
            vehiculo1 = v1;
            vehiculo2 = v2;
        }

        public void Iniciar(int segundosDeCarrera)
        {
            // reinicio posiciones
            vehiculo1.Resetear();
            vehiculo2.Resetear();

            // Los hago mover
            vehiculo1.Mover(segundosDeCarrera);
            vehiculo2.Mover(segundosDeCarrera);

            // muestro resultados
            Console.WriteLine($"Vehículo 1: {vehiculo1.PosicionActual()} metros.");
            Console.WriteLine($"vehículo 2: {vehiculo2.PosicionActual()} metros.");

            // who won?
            if (vehiculo1.PosicionActual() > vehiculo2.PosicionActual())
                Console.WriteLine("El Vehículo 1 gana! lol 🏆");
            else if (vehiculo2.PosicionActual() > vehiculo1.PosicionActual())
                Console.WriteLine("El Vehículo 2 gana! muejejej 🎉");
            else
                Console.WriteLine("Empate brooo 🤝 toomaaaa");
        }
    }

    // programa de prueba
    class Program
    {
        static void Main(string[] args)
        {
            // Creamos algunos vehículos
            Auto miAuto = new Auto(45);  // Auto a 45 m/s
            Bicicleta bici = new Bicicleta();
            Camion camionVolador = new Camion(); // n camión normalito

            // pruebo mover la bici un rato
            bici.Mover(20);
            Console.WriteLine($"Bici avanzó: {bici.PosicionActual()} metros");

            bici.Mover(10); // Otra vuelta
            Console.WriteLine($"Bici ahora está en: {bici.PosicionActual()} metros");

            // carrera 1: Auto vs Camión (10 segundos)
            Carrera carrera1 = new Carrera(miAuto, camionVolador);
            Console.WriteLine("\n🏁 Carrera 1: Auto vs Camión (10s) 🏁");
            carrera1.Iniciar(10);

            // carrera 2: Bici vs Camión (15 segundos)
            Carrera carrera2 = new Carrera(bici, camionVolador);
            Console.WriteLine("\n🚴‍♂️ vs 🚛: Bici vs Camión (15s)");
            carrera2.Iniciar(15);
        }
    }
}
