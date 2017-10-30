using System;
using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using processo_seletivo_glaicon_peixer.Data;
using processo_seletivo_glaicon_peixer.Model;
using Simplexcel;

namespace processo_seletivo_glaicon_peixer.Controllers
{
    [Route("api/usuarios")]
    public class UsuarioControllercs : Controller
    {
        private readonly IUsuarioRepository repositorio;
        private readonly IMapper mapper;

        public UsuarioControllercs(IUsuarioRepository repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                repositorio.Delete(id);
                return new OkResult();
            }
            catch (Exception e)
            {
                return BadRequest("Não foi possível excluir usuário");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]UsuarioDto usuarioDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioMapper = mapper.Map<UsuarioDto, Usuario>(usuarioDto);

            var usuario = repositorio.Add(usuarioMapper);

            var usuarioResult = mapper.Map<Usuario, UsuarioDto>(usuario);

            return new OkObjectResult(usuarioResult);
        }

        [HttpGet]
        public IEnumerable<UsuarioSimpleDto> Get()
        {
            var usuarios = repositorio.GetAll();

            return mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioSimpleDto>>(usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var usuario = repositorio.GetSingle(id);

            if (usuario == null)
                return NotFound("Usuário não encontrado");

            var usuarioResult = mapper.Map<Usuario, UsuarioDto>(usuario);

            return new OkObjectResult(usuarioResult);
        }

        [HttpGet("xlsx")]
        public void Xlsx()
        {
            var sheet = new Worksheet("Relatório Usuários");
            var usuarios = repositorio.GetAll();
            var usuariosSimple =  mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioSimpleDto>>(usuarios);
            
            sheet.Cells[0,0] = "Nome";
            sheet.Cells[0,1] = "Email";

            var index = 1;
            foreach (var usu in usuariosSimple)
            {
                sheet.Cells[index, 0] = usu.Nome;
                sheet.Cells[index, 1] = usu.Email;
                index++;
            }
            
            var workbook = new Workbook();
            workbook.Add(sheet);

            HttpContext.Response.ContentType = "application/octet-stream";
            HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            HttpContext.Response.Headers.Add("attachment", "usuarios.xlsx");

            workbook.Save(HttpContext.Response.Body);
        }
    }
}
