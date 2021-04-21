using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TesteProvaApisul.Pages.classes;

namespace TesteProvaApisul.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public Elevador.GestaoElevadores gestao { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            FuncoesIniciais();
        }

        private void FuncoesIniciais()
        {
            Elevador elevador = new Elevador();
            List<Elevador.elevadores> elevadores = new List<Elevador.elevadores>();
            elevadores = elevador.Buscador();
            
            this.gestao = new Elevador.GestaoElevadores();
            this.gestao = elevador.BuscarDadosElevadores();
        }
    }
}
