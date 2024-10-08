using System.ComponentModel.DataAnnotations.Schema;
using TIData;
using TIEntities;
using EntitiesDTO;

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
                (viaje.FechaDesde, "Falta agregar la FechaPosibleEntregaDesde"),
                (viaje.FechaHasta, "Falta agregar la FechaPosibleEntregaHasta"),
                (viaje.PorcentajeOcupacionCarga, "Falta agregar el porcentaje de la ocupacion de la carga"),
            };

            foreach (var (valor, mensaje) in validaciones)
            {
                if (valor == null ||
                    (valor is string str && string.IsNullOrEmpty(str)) ||
                    (valor is int num && num <= 0))
                {
                    return new Result { Message = mensaje };
                }
            }

            return new Result { Success = true };
        }

        public Result AgregarViaje(ViajeDTO viajeDTO)
        {
            Viaje viaje = PasarDtoAEntity(viajeDTO);

            var resultado = ValidarCompletitudViaje(viaje);
            if (!resultado.Success)
            {
                return new Result { Message = resultado.Message, Status = 400 };
            }

            Result result = AsignarViaje(viaje.FechaDesde, viaje.FechaHasta);
            if (!resultado.Success)
            {
                return result;
            }

            ViajeFiles.EscribirViajeAJson(viaje);
            return new Result { Success = true, Message = "El viaje se cargo correctamente" };
        }

        public List<Viaje> ObtenerViajes()
        {
            return ViajeFiles.LeerViajeAJson().ToList();
        }

        public Result AsignarViaje(DateOnly desde, DateOnly hasta)
        {
            if (desde < DateOnly.FromDateTime(DateTime.Now))
            {
                return new Result { Message = "La fecha de inicio es menor a la actual", Status = 400 };
            }
            if (hasta > (desde.AddDays(7)))
            {
                return new Result { Message = "La fecha de entrega es mayor a 7 dias de la fecha de salida", Status = 400 };
            }
            List<Viaje> Viajes = ViajeFiles.LeerViajeAJson();
            if (VerificarSolapamientoViajes(desde, hasta, Viajes))
            {
                return new Result { Message = "Ya existe un viaje entre esas fechas", Status = 400 };
            }

            //List<Camioneta> camionetas = CamionetaFiles.LeerCamionetaAJson();
            //List<Compra> Compras = CompraFiles.LeerCompraAJson();

            ////foreach (var Compra in Compras)
            ////{
            ////    if (Compra.Estado == EnumEstadoCompra.OPEN)
            ////    {
            ////        double Distancia = Compra.CalcularDistancia();
            ////        List<Producto> productos = ProductoFiles.LeerProductosAJson().Where(x => x.Codigo == Compra.Codigo).ToList();

            ////        Camioneta camioneta = camionetas.FirstOrDefault(x => x.DistanciaMaximaEnKm >= Distancia || );

            ////        Viaje viaje = new Viaje();

            ////    }
            ////}

            return new Result { Success = true, Message = "El viaje se aasigno correctamente" };
        }

        private bool VerificarSolapamientoViajes(DateOnly fechaInicioNuevo, DateOnly fechaFinNuevo, List<Viaje> viajesExistentes)
        {
            foreach (var viaje in viajesExistentes)
            {
                if (ExisteSolapamiento(fechaInicioNuevo, fechaFinNuevo, viaje.FechaDesde, viaje.FechaHasta))
                {
                    return true; // Si se solapa
                }
            }
            return false; // Si no se solapan con ningún viaje
        }

        private bool ExisteSolapamiento(DateOnly fechaInicioA, DateOnly fechaFinA, DateOnly fechaInicioB, DateOnly fechaFinB)
        {
            // Si no se solapan, entonces el final de A es antes del inicio de B, o el inicio de A es después del final de B.
            return !(fechaFinA < fechaInicioB || fechaInicioA > fechaFinB);
        }

        public static Viaje PasarDtoAEntity(ViajeDTO viajeDTO)
        {
            return new Viaje
            {
                FechaDesde = viajeDTO.FechaDesde,
                FechaHasta = viajeDTO.FechaHasta
            };
        }
    }
}