using ApiTicketsAzure.Models;
using ApiTicketsAzure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiTicketsAzure.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private ReposiroryTickets repo;

        public EmpresaController(ReposiroryTickets repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Authorize]
        [Route("[action]")]
        //[Route("{idusuario}")]
        public ActionResult<Usuario> GetUsuario()
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();

            string jsonUsuario = claims.SingleOrDefault(z => z.Type == "UserData").Value;

            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(jsonUsuario);

            return usuario;
        }

        [HttpGet]
        [Authorize]
        //[Route("[action]/{idusuario}")]
        [Route("[action]")]
        //[Route("{idusuario}")]
        public ActionResult<List<Ticket>> TicketsUsuario()
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();

            string jsonUsuario = claims.SingleOrDefault(z => z.Type == "UserData").Value;

            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(jsonUsuario);

            return this.repo.GetTicketsUsuario(usuario.IdUsuario);
        }
        
        [HttpGet]
        [Authorize]
        [Route("[action]/{id}")]
        public ActionResult<Ticket> FindTicket(int id)
        {
            return this.repo.FindTicket(id);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult CreateTicket(Ticket ticket)
        {
            this.repo.CreateTicket(ticket);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult ProcessTicket(Ticket ticket)
        {
            return Ok();
        }
    }
}
