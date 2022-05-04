using MEM2.Data.MEM2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MEM2.Data
{
    public interface IEventoService
    {
        Task<List<Evento>> GetEventos();
        Task<Evento> GetEvento(int id);
    }
}
