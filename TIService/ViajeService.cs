using System.ComponentModel.DataAnnotations.Schema;
using TIData;
using TIEntities;
using EntitiesDTO;

namespace TIService
{
    public class ViajeService
    {
        private Result ValidarCompletitudViaje(ViajeDTO viaje)
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
            var resultado = ValidarCompletitudViaje(viajeDTO);
            if (!resultado.Success)
            {
                return new Result { Message = resultado.Message, Status = 400 };
            }

            Result result = ManejoErores(viajeDTO.FechaDesde, viajeDTO.FechaHasta);
            if (!resultado.Success)
            {
                return result;
            }

            AsignarViajes(viajeDTO);

            return new Result { Success = true, Message = "El viaje se cargo correctamente" };
        }

        public Result ManejoErores(DateOnly desde, DateOnly hasta)
        {
            if (desde < DateOnly.FromDateTime(DateTime.Now))
            {
                return new Result { Message = "La fecha de inicio es menor a la actual", Status = 400 };
            }
            if (hasta > (desde.AddDays(7)))
            {
                return new Result { Message = "La fecha de entrega es mayor a 7 dias de la fecha de salida", Status = 400 };
            }
            List<Viaje> viajes = ObtenerViajes();
            if (VerificarSolapamientoViajes(desde, hasta, viajes))
            {
                return new Result { Message = "Ya existe un viaje entre esas fechas", Status = 400 };
            }
            List<Compra> compras = ObtenerCompras(desde, hasta);
            if (compras == null)
            {
                return new Result { Message = "No hay ninguna compra ingresada en este rango de fechas", Status = 404 };
            }

            return new Result { Success = true };
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

        public void AsignarViajes(ViajeDTO viajeDTO)
        {
            List<Compra> compras = ObtenerCompras(viajeDTO.FechaDesde, viajeDTO.FechaHasta);
            List<Viaje> viajes = ObtenerViajes();
            List<Camioneta> camionetas = CamionetaFiles.LeerCamionetaAJson();

            Viaje viajeC1 = new Viaje() { FechaDesde = viajeDTO.FechaDesde, FechaHasta = viajeDTO.FechaHasta, PatenteCamionetaAsignada = camionetas[0].Patente };
            Viaje viajeC2 = new Viaje() { FechaDesde = viajeDTO.FechaDesde, FechaHasta = viajeDTO.FechaHasta, PatenteCamionetaAsignada = camionetas[1].Patente };
            Viaje viajeC3 = new Viaje() { FechaDesde = viajeDTO.FechaDesde, FechaHasta = viajeDTO.FechaHasta, PatenteCamionetaAsignada = camionetas[2].Patente };

            double capacidadC1 = 0; double capacidadC2 = 0; double capacidadC3 = 0;

            foreach (var compra in compras)
            {
                if (compra.Estado == EnumEstadoCompra.OPEN)
                {
                    double espacio = (ProductoFiles.LeerProductosAJson().FirstOrDefault(x => x.Codigo == compra.CodigoProducto).PasarACentimetrosCubicos()) * compra.CantidadComprada;

                    if ((capacidadC1 + espacio) <= camionetas[0].TamañoDeCargaEnCm3 &&
                        compra.CalcularDistancia() <= camionetas[0].DistanciaMaximaEnKm)
                    {
                        capacidadC1 += espacio;
                        compra.Estado = EnumEstadoCompra.READY_TO_DISPATCH;
                        viajeC1.Compras.Add(compra.Codigo);
                    }
                    else if ((capacidadC2 + espacio) <= camionetas[1].TamañoDeCargaEnCm3 &&
                        compra.CalcularDistancia() <= camionetas[1].DistanciaMaximaEnKm)
                    {
                        capacidadC2 += espacio;
                        compra.Estado = EnumEstadoCompra.READY_TO_DISPATCH;
                        viajeC2.Compras.Add(compra.Codigo);
                    }
                    else if ((capacidadC3 + espacio) <= camionetas[2].TamañoDeCargaEnCm3 &&
                       compra.CalcularDistancia() <= camionetas[2].DistanciaMaximaEnKm)
                    {
                        capacidadC3 += espacio;
                        compra.Estado = EnumEstadoCompra.READY_TO_DISPATCH;
                        viajeC3.Compras.Add(compra.Codigo);
                    }
                    else
                    {
                        compra.FechaEntregaEstimada.AddDays(14);
                    }
                }
            }

            if (viajeC1.Compras.Count() != 0)
            {
                ViajeFiles.EscribirViajeAJson(viajeC1);
            }
            if (viajeC2.Compras.Count() != 0)
            {
                ViajeFiles.EscribirViajeAJson(viajeC2);
            }
            if (viajeC3.Compras.Count() != 0)
            {
                ViajeFiles.EscribirViajeAJson(viajeC3);
            }
        }

        public List<Viaje> ObtenerViajes()
        {
            return ViajeFiles.LeerViajeAJson().ToList();
        }

        public List<Compra> ObtenerCompras(DateOnly desde, DateOnly hasta)
        {
            return CompraFiles.LeerCompraAJson().Where(x => DateOnly.FromDateTime(x.FechaEntregaSolicitada) >= desde &&
                                                                       DateOnly.FromDateTime(x.FechaEntregaSolicitada) <= hasta).ToList();
        }
    }
}