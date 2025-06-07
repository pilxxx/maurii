using System;

namespace JugadorCansado
{
    // interfaz para los jugadores
    interface IJugador
    {
        bool Correr(int mins);
        bool Cansado();
        void Descansar(int mins);
    }

    // jugador amateur
    class Amateur : IJugador
    {
        private int totalCorrido = 0;
        private const int maximo = 20; // amateur aguanta 20 mins
        
        public bool Correr(int mins)
        {
            // Si ya está cansado no puede correr
            if (Cansado())
            {
                Console.WriteLine("No puedo correr más, estoy cansado!");
                return false;
            }
            
            totalCorrido += mins;
            
            // Verificar si después de correr se cansó
            if (totalCorrido >= maximo)
            {
                Console.WriteLine($"Corrí {mins} minutos y ahora estoy cansado!");
            }
            else
            {
                Console.WriteLine($"Corrí {mins} minutos, todo bien.");
            }
            
            return true;
        }
        
        public bool Cansado()
        {
            return totalCorrido >= maximo;
        }
        
        public void Descansar(int mins)
        {
            totalCorrido = Math.Max(0, totalCorrido - mins);
            Console.WriteLine($"Descansé {mins} mins. Ahora tengo {totalCorrido} mins corridos.");
        }
    }

    // Jugador PROO
    class Profesional : IJugador
    {
        private int tiempoCorriendo = 0;
        private const int limite = 40; // los pro aguantan 40
        
        public bool Correr(int mins)
        {
            if (Cansado())
            {
                Console.WriteLine("Estoy demasiado cansado para correr ahora");
                return false;
            }
            
            tiempoCorriendo += mins;
            
            if (tiempoCorriendo > limite)
            {
                Console.WriteLine($"Corrí {mins} mins... necesito descanso!");
            }
            else
            {
                Console.WriteLine($"Corrí {mins} mins sin problemas.");
            }
            
            return true;
        }
        
        public bool Cansado()
        {
            return tiempoCorriendo >= limite;
        }
        
        public void Descansar(int mins)
        {
            tiempoCorriendo -= mins;
            if (tiempoCorriendo < 0) tiempoCorriendo = 0;
            Console.WriteLine($"Descansando {mins} minutos... mejor!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Probando el amateur
            IJugador amateur = new Amateur();
            Console.WriteLine("--- AMATEUR ---");
            amateur.Correr(8);
            amateur.Correr(7);
            amateur.Correr(10); // debería cansarse
            amateur.Correr(1); // no debería poder
            amateur.Descansar(5);
            amateur.Correr(4); // ahora sí
            
            // Probando el pro
            IJugador pro = new Profesional();
            Console.WriteLine("\n--- PROFESIONAL ---");
            pro.Correr(25);
            pro.Correr(20); // justo en el límite
            pro.Correr(1); // ya no puede
            pro.Descansar(30);
            pro.Correr(15); // recuperado
        }
    }
}
