using ApiTicketsAzure.Data;
using ApiTicketsAzure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTicketsAzure.Repositories
{
    public class ReposiroryTickets
    {
        private TicketsContext context;

        public ReposiroryTickets(TicketsContext context)
        {
            this.context = context;
        }

        public List<Ticket> GetTicketsUsuario(int idusuario)
        {
            var consulta = from datos in this.context.Tickets
                           where datos.IdUsuario == idusuario
                           select datos;
            return consulta.ToList();
        }

        public Ticket FindTicket(int id)
        {
            return this.context.Tickets.FirstOrDefault(x => x.IdTicket == id);
        }

        public void CreateTicket(Ticket ticket)
        {
            this.context.Tickets.Add(ticket);
            this.context.SaveChanges();
        }

        public Usuario ExisteUsuario(string userName, string password)
        {
            var consulta = from datos in this.context.Usuarios
                           where datos.Username == userName && datos.Password == password
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                return consulta.First();
            }
        }
    }
}
