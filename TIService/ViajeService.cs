using System.ComponentModel.DataAnnotations.Schema;
using TIData;
using TIEntities;
using EntitiesDTO;

namespace TIService
{
    public class ViajeService
    {
        public Result AgregarViaje(ViajeDTO viajeDTO)
        {
           
            Result result = ManejoErores(viajeDTO.FechaDesde, viajeDTO.FechaHasta);
            if (!result.Success)
            {
                return result;
            }

            AsignarViajes(viajeDTO);

            return new Result { Success = true, Message = "El viaje se cargo correctamente" };
        }
        private Result ManejoErores(DateTime desde, DateTime hasta)
        {
            if (desde < DateTime.Now)
            {
                return new Result { Message = "La fecha de inicio es menor a la actual", Status = 400 };
            }
            if (hasta > (desde.AddDays(7)))
            {
                return new Result { Message = "La fecha de entrega es mayor a 7 dias de la fecha de salida", Status = 400 };
            }
            if (VerificarSolapamientoViajes(desde, hasta))
            {
                return new Result { Message = "Ya existe un viaje entre esas fechas", Status = 400 };
            }
            List<Compra> compras = ObtenerCompras(desde, hasta);
            if (compras.Count() == 0)
            {
                return new Result { Message = "No hay ninguna compra ingresada en este rango de fechas", Status = 404 };
            }

            return new Result { Success = true };
        }

        private bool VerificarSolapamientoViajes(DateTime fechaInicioNuevo, DateTime fechaFinNuevo)
        {
            List<Viaje> viajes = ViajeFiles.LeerViajeAJson();
            foreach (var viaje in viajes)
            {
                if (!(fechaFinNuevo < viaje.FechaDesde || fechaInicioNuevo > viaje.FechaHasta))
                {
                    return true; // Si se solapa
                }
            }
            return false; // Si no se solapan con ningún viaje
        }

        private void AsignarViajes(ViajeDTO viajeDTO)
        {
            List<Compra> compras = ObtenerCompras(viajeDTO.FechaDesde, viajeDTO.FechaHasta);
            List<Viaje> viajes = ViajeFiles.LeerViajeAJson();
            List<Camioneta> camionetas = CamionetaFiles.LeerCamionetaAJson();

            List<Viaje> viajesAsignados = new List<Viaje>()
            {
                new Viaje() { FechaDesde = viajeDTO.FechaDesde, FechaHasta = viajeDTO.FechaHasta, PatenteCamionetaAsignada = camionetas[0].Patente },
                new Viaje() { FechaDesde = viajeDTO.FechaDesde, FechaHasta = viajeDTO.FechaHasta, PatenteCamionetaAsignada = camionetas[1].Patente },
                new Viaje() { FechaDesde = viajeDTO.FechaDesde, FechaHasta = viajeDTO.FechaHasta, PatenteCamionetaAsignada = camionetas[2].Patente }
            };

            double[] capacidades = new double[camionetas.Count];

            foreach (Compra compra in compras.Where(x => x.Estado == EnumEstadoCompra.OPEN))
            {
                double espacio = (ProductoFiles.LeerProductosAJson().FirstOrDefault(x => x.Codigo == compra.CodigoProducto).PasarACentimetrosCubicos()) * compra.CantidadComprada;
                double Distancia = compra.CalcularDistancia();
                for (int i = 0; i < camionetas.Count; i++)
                {
                    if ((capacidades[i] + espacio) <= camionetas[i].TamañoDeCargaEnCm3 &&
                        Distancia <= camionetas[i].DistanciaMaximaEnKm)
                    {
                        capacidades[i] += espacio;                      
                        compra.Estado = EnumEstadoCompra.READY_TO_DISPATCH;
                        CompraFiles.EscribirCompraAJson(compra);
                        viajesAsignados[i].Compras.Add(compra.Codigo);
                        viajesAsignados[i].PorcentajeOcupacionCarga = (capacidades[i] * 100) / camionetas[i].TamañoDeCargaEnCm3;
                        break;
                    }
                }

                if (compra.Estado == EnumEstadoCompra.OPEN)
                {
                    compra.FechaEntregaEstimada.AddDays(14);
                }
            }

            foreach (Viaje viaje in viajesAsignados)
            {
                if (viaje.Compras.Count() != 0)
                {
                    ViajeFiles.EscribirViajeAJson(viaje);
                }
            }
        }

        private List<Compra> ObtenerCompras(DateTime desde, DateTime hasta)
        {
            return CompraFiles.LeerCompraAJson().Where(x => x.FechaEntregaEstimada >= desde &&
                                                            x.FechaEntregaEstimada <= hasta).ToList();
        }
    }
}