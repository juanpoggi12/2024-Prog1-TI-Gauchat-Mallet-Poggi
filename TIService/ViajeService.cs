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
                (viaje.FechaDesde, "Falta agregar la FechaPosibleEntregaDesde"),
                (viaje.FechaHasta, "Falta agregar la FechaPosibleEntregaHasta"),
            };

            foreach (var (valor, mensaje) in validaciones)
            {
                if (valor == null ||
                    (valor is DateTime dt && dt == default))
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
            List<Compra> compras = CompraFiles.LeerCompraAJson();
            if (compras == null)
            {
                return new Result { Message = "No hay ninguna compra ingresada", Status = 404 };
            }
            return new Result { Success = true };
            //List<Camioneta> camionetas = CamionetaFiles.LeerCamionetaAJson();
            //int sumaC1 = 0; int sumaC2 = 0; int sumaC3 = 0;
            //double capacidadC1 = 0; double capacidadC2 = 0; double capacidadC3 = 0;

            //foreach (var compra in compras)
            //{
            //    if (compra.Estado == EnumEstadoCompra.OPEN)
            //    {
            //        if (capacidadC1 < camionetas[0].TamañoDeCargaEnCm3 && compra.CalcularDistancia() <= camionetas[0].DistanciaMaximaEnKm)
            //        {
            //            capacidadC1 += ProductoFiles.LeerProductosAJson().FirstOrDefault(x => x.Codigo == compra.CodigoProducto).PasarACentimetrosCubicos();
            //            if (capacidadC1 < camionetas[0].TamañoDeCargaEnCm3)
            //            {
            //                sumaC1++;
            //            }
            //        }
            //        double Distancia = compra.CalcularDistancia();
            //        List<Producto> productos = ProductoFiles.LeerProductosAJson().Where(x => x.Codigo == compra.Codigo).ToList();
            //        Camioneta camioneta = camionetas.FirstOrDefault(x => x.DistanciaMaximaEnKm >= Distancia || );
            //        Viaje viaje = new Viaje();
            //    }
            //}
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