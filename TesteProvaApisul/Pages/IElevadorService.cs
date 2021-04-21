using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TesteProvaApisul.Pages.classes;

namespace TesteProvaApisul.Pages
{
    interface IElevadorService
    {

        public List<Elevador.elevadores> Buscador();

        public Elevador.GestaoElevadores BuscarDadosElevadores();
            //static Elevador a(Elevador[] args)
            //{
            //    using (StreamReader r = new StreamReader("../input.json"))
            //    {

            //        var json = r.ReadToEnd();
            //        Elevador items = JsonConvert.DeserializeObject<Elevador>(json);
            //        Console.WriteLine(items.andar);
            //        Console.ReadKey();
            //        return items;
            //    }

            //}
        
            

    }


}
