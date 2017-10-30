﻿using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using processo_seletivo_glaicon_peixer.Data;
using processo_seletivo_glaicon_peixer.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace processo_seletivo_glaicon_peixer.Controllers
{
    [Route("api/usuarios")]
    public class UsuarioControllercs : Controller
    {
        private readonly IUsuarioRepository repositorio;

        public UsuarioControllercs(IUsuarioRepository repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpPost]
        public Usuario Post([FromBody]Usuario usuario)
        {
            return repositorio.Add(usuario);
        }

        [HttpGet("xlsx")]
        public HttpResponseMessage Xlsx()
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            ////create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            ////create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;

            //here, we must insert at least one sheet to the workbook. otherwise, Excel will say 'data lost in file'
            //So we insert three sheet just like what Excel does
            hssfworkbook.CreateSheet("Sheet1");
            hssfworkbook.CreateSheet("Sheet2");
            hssfworkbook.CreateSheet("Sheet3");
            hssfworkbook.CreateSheet("Sheet4");

            ((HSSFSheet)hssfworkbook.GetSheetAt(0)).AlternativeFormula = false;
            ((HSSFSheet)hssfworkbook.GetSheetAt(0)).AlternativeExpression = false;

            //Write the stream data of workbook to the root directory
            FileStream file = new FileStream(@"test.xls", FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();

            var response =  new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(file)
            };

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "usuarios.xlsx" };

            return response;
        }
    }
}