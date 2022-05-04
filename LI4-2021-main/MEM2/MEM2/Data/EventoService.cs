using MEM2.Data.MEM2;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MEM2.Data
{
    public class EventoService
    {
        private readonly MEMContext _context;
        public EventoService(MEMContext context)
        {
            _context = context;
        }

        public async Task<List<Evento>>GetEventosAsync(){
            return await _context.Evento
                 .AsNoTracking().ToListAsync();
        }


        public async Task<List<Evento>> GetEventosAsyncFilter(string termo)
        {
            return await _context.Evento
                .Where( x => (x.Titulo == termo || x.Categoria == termo))
                 .AsNoTracking().ToListAsync();
        }


        public async  Task<Evento> GetEvento(int eventoID)
        {
            return await _context.Evento
                .FirstOrDefaultAsync(x => x.Id == eventoID);
        }

        public async void SetSeguido(string idMail,int idEvento, Boolean On )
        {
            List<Seguidos> ListSeguidos =  await _context.Seguidos.Where(x => x.SeguidosId >= 0).ToListAsync();
            List<Notificacao> ListNotificacao = await _context.Notificacao.Where(x => x.Id >= 0).ToListAsync();
            

            List<AspNetUsers> User = await _context.AspNetUsers.Where(x => x.Email == idMail).ToListAsync();

            String idUser = User.First().Id;

            Seguidos Seguidos = new Seguidos();
            Notificacao Notificacao = new Notificacao();
            List<int> IdsSeguidos = new List<int>();
            List<int> IdsNotif = new List<int>();

            Evento Evento = _context.Evento.Find(idEvento);
            Notificacao.FkEventoId = idEvento;
            Notificacao.FkUtilizadorId = idUser;
            Notificacao.Hora = Evento.Inicio;

  
            Seguidos.FkEventoId = idEvento;
            Seguidos.FkUtilizadorId = idUser;

            Boolean addSeguidos = true;
            Boolean addNotificacao = true;

            int removeIdSeguidos = 0;
            int removeIdNotif= 0;

            if (ListSeguidos.Count == 0) {
                addSeguidos = true;
            }
            else
            {
                foreach (var Seguido in ListSeguidos)
                {
                    IdsSeguidos.Add(Seguido.SeguidosId);

                    if (Seguido.FkUtilizadorId == Seguidos.FkUtilizadorId && Seguido.FkEventoId == Seguidos.FkEventoId)
                    {
                        removeIdSeguidos = Seguido.SeguidosId;
                        addSeguidos = addSeguidos && false;
                    }
                    else
                    {
                        addSeguidos = addSeguidos && true;
                    }
                }
            }

            int properIdSeguidos = 0;


            if (ListNotificacao.Count == 0)
            {
                addNotificacao = true;
            }
            else
            {
                foreach (var Notif in ListNotificacao)
                {
                    IdsNotif.Add(Notif.Id);

                    if (Notif.FkUtilizadorId == Seguidos.FkUtilizadorId && Notif.FkEventoId == Seguidos.FkEventoId)
                    {
                        removeIdNotif = Notif.Id;
                        addNotificacao = addNotificacao && false;
                        
                    }
                    else
                    {
                        addNotificacao = addNotificacao && true;
                    }
                }
            }

            int properIdNotif = 0;


            while (IdsSeguidos.Contains(properIdSeguidos))
            {
                properIdSeguidos = properIdSeguidos + 1;
            }

            if (!On) { Seguidos.SeguidosId = properIdSeguidos; } else { Seguidos.SeguidosId = removeIdSeguidos; }
            

            while (IdsNotif.Contains(properIdNotif))
            {
                properIdNotif = properIdNotif + 1;
            }

            if (!On) { Notificacao.Id = properIdNotif; } else { Notificacao.Id = removeIdNotif; }
            


            //Debug.WriteLine(properId);
            //Debug.WriteLine(Seguidos.SeguidosId);

            if (addSeguidos && !On) {
                _context.Seguidos.Add(Seguidos);
                await _context.SaveChangesAsync();

            }

            if (addNotificacao && !On)
            {
                _context.Notificacao.Add(Notificacao);
                await _context.SaveChangesAsync();
            }

            if (On)
            {
                var removedSeguido = await _context.Seguidos.FirstOrDefaultAsync(x => x.SeguidosId == removeIdSeguidos);
                if (removedSeguido != null) { 
                    
                _context.Seguidos.Remove(removedSeguido);
                await _context.SaveChangesAsync();
                }
            }


            if (On)
            {
                var removedNotif = await _context.Notificacao.FirstOrDefaultAsync(x => x.Id == removeIdNotif);
                if (removedNotif != null)
                {
                _context.Notificacao.Remove(removedNotif);
                await _context.SaveChangesAsync();
                }
            }

        }

        public async Task<List<Evento>> GetListIdSeguido(string idMail)
        {

            List<AspNetUsers> User = await _context.AspNetUsers.Where(x => x.Email == idMail).ToListAsync();
            String idUser = User.First().Id;

            List<Seguidos> ListSeguidos = await _context.Seguidos.Where(x => x.FkUtilizadorId == idUser).ToListAsync();

            List<int> IdsEvento = new List<int>();

            foreach (var Seguidos in ListSeguidos) {
                IdsEvento.Add(Seguidos.FkEventoId);
            }

            return await _context.Evento
                .Where(x =>  IdsEvento.Contains(x.Id) )
                 .AsNoTracking().ToListAsync();

            
        }

        public Boolean IsFollowing(string idMail, int idEvento)
        {
            List<AspNetUsers> User = _context.AspNetUsers.Where(x => x.Email == idMail).ToList();

            String idUser = User.First().Id;

            List<Notificacao> Notificacao =  _context.Notificacao.Where(x => x.FkUtilizadorId == idUser).ToList();

            bool on = false;

            

            foreach ( var notificacao in Notificacao)
            {
                if (notificacao.FkEventoId == idEvento)
                {
                     on = on || true;
                }
                else
                {
                     on = on || false;
                }
            }

            if (on) { return true; } else { return false; }
            

        }


        public async Task<Boolean> ScheduleAction(string idMail)
        {
            List<AspNetUsers> User = _context.AspNetUsers.Where(x => x.Email == idMail).ToList();

            String idUser = User.First().Id;

            List<Notificacao> Notificacao = _context.Notificacao.Where(x => x.FkUtilizadorId == idUser).ToList();


            foreach(var Notif in Notificacao)
            {

                Debug.WriteLine((int)Notif.Hora.Subtract(DateTime.Now).TotalMilliseconds);
                await Task.Delay((int)Notif.Hora.Subtract(DateTime.Now).TotalMilliseconds);
                return true;
            }

            return false;


        }

    }
}
