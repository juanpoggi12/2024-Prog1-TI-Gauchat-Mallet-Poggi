using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIEntities;
using TIData;
//private Result ValidarCompletitudViaje(Viaje viaje)
//{
//    var validaciones = new (object valor, string mensaje)[]
//    {
//        (viaje.Codigo, "Falta agregar codigo"),
//        (viaje.PatenteCamionetaAsignada, "Falta agregar Patente de la Camioneta asignada"),
//        (viaje.FechaPosibleEntregaDesde, "Falta agregar la FechaPosibleEntregaDesde"),
//        (viaje.FechaPosibleEntregaHasta, "Falta agregar la FechaPosibleEntregaHasta"),
//        (viaje.PorcentajeOcupacionCarga, "Falta agregar el porcentaje de la ocupacion de la carga"),
//        (viaje.Compras, "Falta agregar la lista de compras")
//    };

//    foreach (var (valor, mensaje) in validaciones)
//    {
//        if (valor == null ||
//            (valor is string str && string.IsNullOrEmpty(str)) ||
//            (valor is int num && num <= 0))
//        {
//            return new Result { Message = mensaje, Success = false };
//        }
//    }

//    return new Result { Success = true };
//}
//public Result AgregarViaje(Viaje viaje)
//{
//    var resultado = ValidarCompletitudViaje(viaje);
//    if (!resultado.Success)
//    {
//        return new Result { Success = false, Message = resultado.Message };
//    }
//    ViajeFiles.EscribirViajeAJson(viaje);
//    return new Result { Success = true };
//}
//public List<Viaje> ObtenerViajes()
//{
//    return ViajeFiles.LeerViajeAJson().ToList();
//}

//public bool EditarViaje(int codigo, Viaje viajeTemporal)
//{
//    Viaje viaje = ViajeFiles.LeerViajeAJson().FirstOrDefault(x => x.Codigo == viajeTemporal.Codigo);
//    var resultado = ValidarCompletitudViaje(viajeTemporal);

//    if (viaje == null || !resultado.Success)
//    {
//        return false;
//    }
//    viaje.Codigo = viajeTemporal.Codigo;
//    viaje.PatenteCamionetaAsignada = viajeTemporal.PatenteCamionetaAsignada;
//    viaje.FechaPosibleEntregaHasta = viajeTemporal.FechaPosibleEntregaHasta;
//    viaje.PorcentajeOcupacionCarga = viajeTemporal.PorcentajeOcupacionCarga;
//    viaje.Compras = viajeTemporal.Compras;

//    ViajeFiles.EscribirViajeAJson(viaje);

//    return true;
//}

//public bool EliminarViaje(int dni)
//{
//    List<Cliente> clientes = ClienteFiles.LeerClienteAJson();

//    Cliente cliente = clientes.FirstOrDefault(x => x.Dni == dni);

//    if (cliente == null)
//    {
//        return false;
//    }

//    cliente.FechaDeEliminacion = DateTime.Now;

//    ClienteFiles.EscribirClienteAJson(cliente);

//    return true;
//}
namespace TIService
{
    public class ViajeService
    {
        public Result AgregarViaje(Viaje viaje)
        {
            var resultado = ValidarViaje(viaje);
            if (!resultado.success)
            {
                return new result { success = false, message = resultado.message };
            }
            ViajeFiles.EscribirViajeAJson(viaje);
            return new result { success = true };
        }

        public Result ValidarViaje(Viaje viaje)
        {
            if (viaje.FechaPosibleEntregaDesde < DateTime.Now())
            {
                return new Result { success = false, message = "La fecha desde no puede ser menor a la fecha actual" };
            }else if (viaje.FechaPosibleEntregaHasta > viaje.FechaPosibleEntregaDesde.AddDays(7))
            {
                return new Result { success = false, message = "La fecha hasta no puede ser 7 dias mayor a la fecha desde" };
            }

            List<Viaje> viajes = ObtenerViajes();
            foreach (Viaje viajeTemporal in viajes)
            {
                if (viaje.FechaPosibleEntregaDesde >= viajeTemporal.FechaPosibleEntregaDesde &&
                    viaje.FechaPosibleEntregaDesde <= viajeTemporal.FechaPosibleEntregaHasta)
                {
                    return new Result { success = false, message = "La fecha desde no puede ser menor a la fecha actual" };
                }
                else if (viaje.FechaPosibleEntregaHasta >= viajeTemporal.FechaPosibleEntregaDesde &&
                         viaje.FechaPosibleEntregaDesde <= viajeTemporal.FechaPosibleEntregaHasta)
                {
                    return new Result { success = false, message = "La fecha hasta no puede ser 7 dias mayor a la fecha desde" };
                }
            }

            return new Result { Message = "", Success = false };
        }

        public List<Viaje> ObtenerViajes()
        {
            return ViajeFiles.LeerViajeAJson().ToList();
        }

    }
}
