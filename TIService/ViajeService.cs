using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIEntities;
using TIData;
namespace TIService
{
    public class ViajeService
    {
        private Result ValidarCompletitudViaje(Viaje viaje)
{
    var validaciones = new (object valor, string mensaje)[]
    {
        (viaje.Codigo, "Falta agregar codigo"),
        (viaje.PatenteCamionetaAsignada, "Falta agregar Patente de la Camioneta asignada"),
        (viaje.FechaPosibleEntregaDesde, "Falta agregar la FechaPosibleEntregaDesde"),
        (viaje.FechaPosibleEntregaHasta, "Falta agregar la FechaPosibleEntregaHasta"),
        (viaje.PorcentajeOcupacionCarga, "Falta agregar el porcentaje de la ocupacion de la carga"),
        (viaje.Compras, "Falta agregar la lista de compras")
    };

            foreach (var (valor, mensaje) in validaciones)
            {
                if (valor == null ||
                    (valor is string str && string.IsNullOrEmpty(str)) ||
                    (valor is int num && num <= 0))
                {
                    return new Result { Message = mensaje, Success = false };
                }
            }

            return new Result { Success = true };
        }
        public Result AgregarViaje(Viaje viaje)
        {
            var resultado = ValidarCompletitudViaje(viaje);
            if (!resultado.Success)
            {
                return new Result { Success = false, Message = resultado.Message };
            }
            ViajeFiles.EscribirViajeAJson(viaje);
            return new Result { Success = true };
        }
        public List<Viaje> ObtenerViajes()
        {
            return ViajeFiles.LeerViajeAJson().ToList();
        }

        public bool EditarViaje(int codigo, Viaje viajeTemporal)
        {
            Viaje viaje = ViajeFiles.LeerViajeAJson().FirstOrDefault(x => x.Codigo == viajeTemporal.Codigo);
            var resultado = ValidarCompletitudViaje(viajeTemporal);

            if (viaje == null || !resultado.Success)
            {
                return false;
            }
            viaje.Codigo = viajeTemporal.Codigo;
            viaje.PatenteCamionetaAsignada = viajeTemporal.PatenteCamionetaAsignada;
            viaje.FechaPosibleEntregaHasta = viajeTemporal.FechaPosibleEntregaHasta;
            viaje.PorcentajeOcupacionCarga = viajeTemporal.PorcentajeOcupacionCarga;
            viaje.Compras = viajeTemporal.Compras;

            ViajeFiles.EscribirViajeAJson(viaje);

            return true;
        }

        public Result EliminarViaje(int dni)
        {
            List<Cliente> clientes = ClienteFiles.LeerClienteAJson();

            Cliente cliente = clientes.FirstOrDefault(x => x.Dni == dni);

            if (cliente == null)
            {
                return new Result { };
            }

            cliente.FechaDeEliminacion = DateTime.Now;

            ClienteFiles.EscribirClienteAJson(cliente);

            return new Result { Success = true};
        }

    }
}
