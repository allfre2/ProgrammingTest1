using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Data.Interfaces;
using Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        readonly IContactsUnitOfWork db;

        public ContactController(IContactsUnitOfWork db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Contact>> Get()
        {
            return await db.Contacts.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var permission = await db.Contacts.Get(id);
            if (permission == null) return NotFound();
            else return Ok(permission);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Contact contact)
        {
            var EmailRegex = new Regex(@"^\w[0-9A-Za-z]@\w[0-9A-Za-z]+\.\w[0-9A-Za-z].*");

            if (String.IsNullOrEmpty(contact.Name) || String.IsNullOrEmpty(contact.LastName)) 
            {
                return BadRequest(new
                {
                    Message = "El nombre y apellido del contacto son requeridos"
                });
            }

            try
            {
                new MailAddress(contact.Email);
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    Message = "El email no está en un formato correcto"
                });
            }

            try
            {
                await db.Contacts.Add(contact);
                await db.Complete();
                return Created(HttpContext.Request.Path, contact);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = "No se ha podido agregar el contacto",
                    Exception = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Contact newValues)
        {
            try
            {
                var contact = await db.Contacts.Get(id);
                if (contact == null) return NotFound();
                contact.Name = newValues.Name;
                contact.LastName = newValues.LastName;
                contact.Email = newValues.Email;
                await db.Complete();
                return Created(HttpContext.Request.Path, contact);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = $"No se ha podido modificar el contacto con id: {id}",
                    Exception = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await db.Contacts.Remove(id);
                await db.Complete();
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = $"No se ha podido eliminar el contacto con id: {id}",
                    Exception = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
    }
}
