/*sanatorio!! data importante: gestionar la programación de las intervenciones quirúrgicas a pacientes. El sanatorio guarda información sobre sus pacientes, sus médicos y sobre las intervenciones quirúrgicas habilitadas por nomenclador (estos datos ya están registrado);De cada médico registra: nombre y apellido, matrícula profesional, especialidad y condición de disponible o no para intervenir.

De cada paciente registra: documento de identidad, nombre y apellido, teléfono y el conjunto de las intervenciones quirúrgicas realizadas. Si el paciente cuenta con obra social además se registra el nombre de la misma y el monto de cobertura sobre el arancel. Las intervenciones quirúrgicas habilitadas por nomenclador pueden ser comunes o de alta complejidad. De cada intervención se registra: código, descripción, especialidad a la que pertenece y arancel (precio) según nomenclador.Si la intervención es de alta complejidad, se registra además el porcentaje adicional que se cobra por uso de equipo especial y que es el mismo para todas las intervenciones de alta complejidad.

Cuando se solicita una intervención para un paciente, debe registrarse la siguiente información: identificador (valor numérico asignado automáticamente por el sistema), fecha de la intervención, intervención a efectuar, médico que interviene y condición de pagado o pendiente de pago. Se debe verificar que el médico interviniente tenga la misma especialidad que la intervención a realizar. Si el paciente no se encuentra registrado en el sistema al momento de realizar la intervención, debe ser dado de alta en ese momento.

Por último el sanatorio desea obtener un reporte de las liquidaciones pendientes de pago de los pacientes. El mismo debe contener la siguiente información: identificador, fecha, descripción, nombre y apellido del paciente, nombre y matrícula del médico interviniente, obra social del paciente (poner un guión medio si no tuviese) y el importe total a pagar.*/

/*Requisitos: a)Dar de alta un nuevo Paciente b)Listar los pacientes. c)Asignarle una nueva intervencion a un Paciente. d)Calcular el costo de las intervenciones de un paciente dado su DNI. e)Realizar el reporte de las liquidaciones pendientes de pago de los pacientes. f)Un menu de opciones(en la clase Program).*/
//P: c^d; G: b^e; R: a^f

//c):
using System;
using System.Collections.Generic;

class Sanatorio
{
    public List<Paciente> pacientes = new List<Paciente>();
    public List<Intervencion> intervenciones = new List<Intervencion>();
    public List<Medico> medicos = new List<Medico>();
    public void AsignarIntervencion(int dniPaciente, string codIntervencion, string matriculaMedico, DateTime fecha)
    {
        Paciente paciente = pacientes.Find(p => p.Dni == dniPaciente);
        Intervencion intervencion = intervenciones.Find(i => i.Codigo == codIntervencion);
        Medico medico = medicos.Find(m => m.Matricula == matriculaMedico);
        if (paciente == null)
        {
            Console.WriteLine("El paciente no fue encontrado, no se puede asignar intervención.");
            return;
        }

        if (intervencion == null)
        {
            Console.WriteLine("No existe la intervencióm");
            return;
        }
      if (medico.Especialidad != intervencion.Especialidad)
        {
            Console.WriteLine("Error: El médico no tiene la misma especialidad que la intervención");
            return;
        }

        IntervencionRealizada nueva = new IntervencionRealizada()
        {
            Fecha = fecha,
            Intervencion = intervencion,
            Medico = medico,
            Pagado = false
        };

        paciente.Intervenciones.Add(nueva);

        Console.WriteLine("Intervención asignada correctamente!!");
    }
