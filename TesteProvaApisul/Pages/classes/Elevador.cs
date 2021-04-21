using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TesteProvaApisul.Pages.classes
{
    public class Elevador : IElevadorService
    {
        public class elevadores
        {
            public int andar { get; set; }
            public string elevador { get; set; }
            public string turno { get; set; }
        }

        List<elevadores> elevador { get; set; }

        public List<elevadores> Buscador()
        {

            using (StreamReader r = new StreamReader("../TesteProvaApisul/Pages/input.json"))
            {

                var json = r.ReadToEnd();
                elevador = JsonConvert.DeserializeObject<List<elevadores>>(json);
                Console.WriteLine(elevador[0].andar);
                Console.WriteLine(elevador[0].elevador);

                return elevador;
            }


        }


        public class GestaoElevadores
        {
            public int andarMenosUtilizado { get; set; }
            public string elevadorMaisFrequentado { get; set; }
            public string elevadorMenosFrequentado { get; set; }
            public string periodoMaiorConjuntoElevadores { get; set; }
            public List<Percentual> percentual { get; set; }
            public string periodoMaiorFluxo { get; set; }
            public string periodoMenorFluxo { get; set; }
        }

        public GestaoElevadores BuscarDadosElevadores()
        {
            List<elevadores> listaElevador = Buscador();
            List<int> listaAndares = new List<int>();
            for (int i = 0; i < 16; i++)
            {
                listaAndares.Add(0);
            }
            listaAndares = ListaAndares(listaAndares, listaElevador);

            GestaoElevadores gestaoElevadores = new GestaoElevadores();

            gestaoElevadores.andarMenosUtilizado = VerificaMenorAndarUtilizado(listaAndares, listaElevador);
            List<string> elevadoresFrequentados = new List<string>();
            //Busca os elevadores mais e menos frequentados;
            elevadoresFrequentados = ElevadoresFrequentados(listaElevador);
            gestaoElevadores.elevadorMaisFrequentado = elevadoresFrequentados[0];
            gestaoElevadores.elevadorMenosFrequentado = elevadoresFrequentados[1];
            List<string> fluxoPorPeriodo = new List<string>();
            fluxoPorPeriodo = FluxoPorPeriodo(listaElevador);

            gestaoElevadores.periodoMaiorFluxo = fluxoPorPeriodo[0];
            gestaoElevadores.periodoMenorFluxo = fluxoPorPeriodo[1];
            gestaoElevadores.periodoMaiorConjuntoElevadores = fluxoPorPeriodo[2];

            gestaoElevadores.percentual = CalcularPercentual(listaAndares, listaElevador);

            return gestaoElevadores;

        }


        public List<int> ListaAndares(List<int> listaAndares, List<elevadores> listaElevador)
        {
            for (int i = 0; i < listaElevador.Count; i++)
            {
                listaAndares[listaElevador[i].andar] = listaAndares[listaElevador[i].andar] + 1;

            }

            return listaAndares;

        }


        public int VerificaMenorAndarUtilizado(List<int> listaAndares, List<elevadores> listaElevador)
        {

            int menorAndar;
            List<int> listaAndarFinal = new List<int>();
            listaAndarFinal = listaAndares.ToList();

            //descobre qual o andar menos utilizado
            for (int i = 0; i < listaAndares.Count; i++)
            {
                //List<int> removerAndar = new List<int>();

                if (listaAndarFinal.Count > 1)
                {
                    menorAndar = listaAndarFinal.Find(x => x < listaAndarFinal[i]);
                    listaAndarFinal.RemoveAll(x => x > menorAndar);
                }

            }

            //Busca e retorna o andar que foi menos utilizado
            return listaAndares.IndexOf(listaAndarFinal[0]);
        }

        public List<string> ElevadoresFrequentados(List<elevadores> listaElevador)
        {
            List<int> listaQuantidadeElevador = new List<int>();
            while (listaQuantidadeElevador.Count < 5)
            {
                listaQuantidadeElevador.Add(0);
            }


            for (int i = 0; i < listaElevador.Count; i++)
            {
                switch (listaElevador[i].elevador)
                {
                    case "A":
                        listaQuantidadeElevador[0] = listaQuantidadeElevador[0] + 1;
                        break;
                    case "B":
                        listaQuantidadeElevador[1] = listaQuantidadeElevador[1] + 1;
                        break;
                    case "C":
                        listaQuantidadeElevador[2] = listaQuantidadeElevador[2] + 1;
                        break;
                    case "D":
                        listaQuantidadeElevador[3] = listaQuantidadeElevador[3] + 1;
                        break;
                    case "E":
                        listaQuantidadeElevador[4] = listaQuantidadeElevador[4] + 1;
                        break;
                }


            }

            int maisUtilizado;
            List<int> listaElevadorFinal = new List<int>();
            listaElevadorFinal = listaQuantidadeElevador.ToList();
            for (int i = 0; i < listaQuantidadeElevador.Count; i++)
            {
                //List<int> removerAndar = new List<int>();

                if (listaElevadorFinal.Count > 1)
                {
                    maisUtilizado = listaElevadorFinal.Find(x => x > listaElevadorFinal[i]);
                    listaElevadorFinal.RemoveAll(x => x < maisUtilizado);
                }

            }

            int posicaoElevadorMaisUtilizado = listaQuantidadeElevador.IndexOf(listaElevadorFinal[0]);


            int menosUtilizado = -1;
            listaElevadorFinal = new List<int>();
            listaElevadorFinal = listaQuantidadeElevador.ToList();
            for (int i = 0; i < listaQuantidadeElevador.Count; i++)
            {
                //List<int> removerAndar = new List<int>();

                //if (listaElevadorFinal.Count > 1)
                //{
                menosUtilizado = listaElevadorFinal.Find(x => x <= listaElevadorFinal[i]);
                //listaElevadorFinal.RemoveAll(x => x > menosUtilizado);
                //}

            }

            int posicaoElevadorMenosUtilizado = listaQuantidadeElevador.IndexOf(menosUtilizado);

            List<string> MaiorEMenorElevadorUtilizado = new List<string>();

            //Atribuindo a letra do mais utilizado
            switch (posicaoElevadorMaisUtilizado)
            {
                case 0:
                    MaiorEMenorElevadorUtilizado.Add("A");
                    break;
                case 1:
                    MaiorEMenorElevadorUtilizado.Add("B");
                    break;
                case 2:
                    MaiorEMenorElevadorUtilizado.Add("C");
                    break;
                case 3:
                    MaiorEMenorElevadorUtilizado.Add("D");
                    break;
                case 4:
                    MaiorEMenorElevadorUtilizado.Add("E");
                    break;
            }

            //Atribuindo a letra do menos utilizado
            switch (posicaoElevadorMenosUtilizado)
            {
                case 0:
                    MaiorEMenorElevadorUtilizado.Add("A");
                    break;
                case 1:
                    MaiorEMenorElevadorUtilizado.Add("B");
                    break;
                case 2:
                    MaiorEMenorElevadorUtilizado.Add("C");
                    break;
                case 3:
                    MaiorEMenorElevadorUtilizado.Add("D");
                    break;
                case 4:
                    MaiorEMenorElevadorUtilizado.Add("E");
                    break;
            }


            return MaiorEMenorElevadorUtilizado;

        }


        public List<string> FluxoPorPeriodo(List<elevadores> listaElevador)
        {
            List<int> listaQuantidadePorPeriodo = new List<int>();
            List<Periodo> periodoPorConjuntoElevadores = new List<Periodo>();

            for (int i = 0; i < 3; i++)
            {
                listaQuantidadePorPeriodo.Add(0);
                Periodo periodo = new Periodo();
                switch (i)
                {
                    case 0:
                        periodo.turno = "M";
                        periodo.elevadores = new List<string>();
                        break;
                    case 1:
                        periodo.turno = "V";
                        periodo.elevadores = new List<string>();
                        break;
                    case 2:
                        periodo.turno = "N";
                        periodo.elevadores = new List<string>();
                        break;
                }

                periodoPorConjuntoElevadores.Add(periodo);
            }


            for (int i = 0; i < listaElevador.Count; i++)
            {
                switch (listaElevador[i].turno)
                {
                    case "M":
                        listaQuantidadePorPeriodo[0] = listaQuantidadePorPeriodo[0] + 1;

                        if (periodoPorConjuntoElevadores[0].elevadores.Count == 0)
                        {
                            //Adiciono o elevador que seja diferente para saber qual periodo usa mais elevadores sem se repetir
                            periodoPorConjuntoElevadores[0].elevadores.Add(listaElevador[i].elevador);
                        }
                        else
                        {
                            //Busco se o elevador atual ja existe dentro da lista
                            string letraElevador = periodoPorConjuntoElevadores[0].elevadores.Find(x => x == listaElevador[i].elevador);
                            if (letraElevador == null)
                            {
                                //Adiciono o elevador que seja diferente para saber qual periodo usa mais elevadores sem se repetir
                                periodoPorConjuntoElevadores[0].elevadores.Add(listaElevador[i].elevador);
                            }
                        }
                        break;
                    case "V":
                        listaQuantidadePorPeriodo[1] = listaQuantidadePorPeriodo[1] + 1;

                        if (periodoPorConjuntoElevadores[1].elevadores.Count == 0)
                        {
                            //Adiciono o elevador que seja diferente para saber qual periodo usa mais elevadores sem se repetir
                            periodoPorConjuntoElevadores[1].elevadores.Add(listaElevador[i].elevador);
                        }
                        else
                        {
                            //Busco se o elevador atual ja existe dentro da lista
                            string letraElevador = periodoPorConjuntoElevadores[1].elevadores.Find(x => x == listaElevador[i].elevador);
                            if (letraElevador == null)
                            {
                                //Adiciono o elevador que seja diferente para saber qual periodo usa mais elevadores sem se repetir
                                periodoPorConjuntoElevadores[1].elevadores.Add(listaElevador[i].elevador);
                            }
                        }
                        break;
                    case "N":
                        listaQuantidadePorPeriodo[2] = listaQuantidadePorPeriodo[2] + 1;

                        if (periodoPorConjuntoElevadores[2].elevadores.Count == 0)
                        {
                            //Adiciono o elevador que seja diferente para saber qual periodo usa mais elevadores sem se repetir
                            periodoPorConjuntoElevadores[2].elevadores.Add(listaElevador[i].elevador);
                        }
                        else
                        {
                            //Busco se o elevador atual ja existe dentro da lista
                            string letraElevador = periodoPorConjuntoElevadores[2].elevadores.Find(x => x == listaElevador[i].elevador);
                            if (letraElevador == null)
                            {
                                //Adiciono o elevador que seja diferente para saber qual periodo usa mais elevadores sem se repetir
                                periodoPorConjuntoElevadores[2].elevadores.Add(listaElevador[i].elevador);
                            }
                        }
                        break;

                }
            }

            string maiorPeriodo = "";
            string menorPeriodo = "";

            if (listaQuantidadePorPeriodo[0] > listaQuantidadePorPeriodo[1] && listaQuantidadePorPeriodo[0] > listaQuantidadePorPeriodo[2])
            {
                maiorPeriodo = "M";
                if (listaQuantidadePorPeriodo[1] < listaQuantidadePorPeriodo[2])
                {
                    menorPeriodo = "V";
                }
                else
                {
                    menorPeriodo = "N";
                }
            }
            else
            {
                if (listaQuantidadePorPeriodo[1] > listaQuantidadePorPeriodo[0] && listaQuantidadePorPeriodo[1] > listaQuantidadePorPeriodo[2])
                {
                    maiorPeriodo = "V";

                    if (listaQuantidadePorPeriodo[0] < listaQuantidadePorPeriodo[2])
                    {
                        menorPeriodo = "M";
                    }
                    else
                    {
                        menorPeriodo = "N";
                    }
                }
                else
                {
                    if (listaQuantidadePorPeriodo[2] > listaQuantidadePorPeriodo[0] && listaQuantidadePorPeriodo[2] > listaQuantidadePorPeriodo[1])
                    {
                        maiorPeriodo = "N";
                        if (listaQuantidadePorPeriodo[0] < listaQuantidadePorPeriodo[1])
                        {
                            menorPeriodo = "M";
                        }
                        else
                        {
                            menorPeriodo = "V";
                        }
                    }
                }
            }


            string maiorPeriodoConjunto;
            if (periodoPorConjuntoElevadores[0].elevadores.Count > periodoPorConjuntoElevadores[1].elevadores.Count && periodoPorConjuntoElevadores[0].elevadores.Count > periodoPorConjuntoElevadores[2].elevadores.Count)
            {
                maiorPeriodoConjunto = periodoPorConjuntoElevadores[0].turno;
            }
            else
            {
                if (periodoPorConjuntoElevadores[1].elevadores.Count > periodoPorConjuntoElevadores[0].elevadores.Count && periodoPorConjuntoElevadores[1].elevadores.Count > periodoPorConjuntoElevadores[2].elevadores.Count)
                {
                    maiorPeriodoConjunto = periodoPorConjuntoElevadores[1].turno;
                }
                else
                {
                    maiorPeriodoConjunto = periodoPorConjuntoElevadores[2].turno;
                }
            }


            List<string> retornoMaiorEMenorPeriodo = new List<string>();
            retornoMaiorEMenorPeriodo.Add(maiorPeriodo);
            retornoMaiorEMenorPeriodo.Add(menorPeriodo);
            retornoMaiorEMenorPeriodo.Add(maiorPeriodoConjunto);
            return retornoMaiorEMenorPeriodo;


        }


        public List<Percentual> CalcularPercentual(List<int> listaAndares, List<elevadores> listaElevador)
        {
            List<int> listaQuantidadeElevador = new List<int>();
            List<int> listaQuantidadePorPeriodo = new List<int>();

            while (listaQuantidadeElevador.Count < 5)
            {
                listaQuantidadeElevador.Add(0);
                if (listaQuantidadePorPeriodo.Count < 3)
                {
                    listaQuantidadePorPeriodo.Add(0);
                }

            }


            for (int i = 0; i < listaElevador.Count; i++)
            {


                switch (listaElevador[i].elevador)
                {
                    case "A":
                        listaQuantidadeElevador[0] = listaQuantidadeElevador[0] + 1;
                        break;
                    case "B":
                        listaQuantidadeElevador[1] = listaQuantidadeElevador[1] + 1;
                        break;
                    case "C":
                        listaQuantidadeElevador[2] = listaQuantidadeElevador[2] + 1;
                        break;
                    case "D":
                        listaQuantidadeElevador[3] = listaQuantidadeElevador[3] + 1;
                        break;
                    case "E":
                        listaQuantidadeElevador[4] = listaQuantidadeElevador[4] + 1;
                        break;
                }



                ///%%%%%%%%%
                switch (listaElevador[i].turno)
                {
                    case "M":
                        listaQuantidadePorPeriodo[0] = listaQuantidadePorPeriodo[0] + 1;


                        break;
                    case "V":
                        listaQuantidadePorPeriodo[1] = listaQuantidadePorPeriodo[1] + 1;


                        break;
                    case "N":
                        listaQuantidadePorPeriodo[2] = listaQuantidadePorPeriodo[2] + 1;

                        break;

                }
            }

            List<Percentual> percentuais = new List<Percentual>();

            //Percentual Andar
            Percentual perAndar = new Percentual();
            perAndar.porcentagem = new List<double>();
            perAndar.nome = new List<string>();

            //double total = 0;
            for (int i = 0; i < listaAndares.Count; i++)
            {
                double porcentagem = ((double) listaAndares[i] / (double)listaElevador.Count) * 100;
                //total += Math.Round(porcentagem);
                perAndar.porcentagem.Add(Math.Round(porcentagem));
                perAndar.nome.Add(i.ToString());
            }

            percentuais.Add(perAndar);

            //Percentual Elevador
            Percentual perElevador = new Percentual();
            perElevador.porcentagem = new List<double>();
            perElevador.nome = new List<string>();
            string[] nomeElevador = new string[5];
            nomeElevador[0] = "A";
            nomeElevador[1] = "B";
            nomeElevador[2] = "C";
            nomeElevador[3] = "D";
            nomeElevador[4] = "E";
            for (int i = 0; i < listaQuantidadeElevador.Count; i++)
            {
                double porcentagem = ((double)listaQuantidadeElevador[i] / (double)listaElevador.Count) * 100;
                //total += Math.Round(porcentagem);
                perElevador.porcentagem.Add(Math.Round(porcentagem));
                perElevador.nome.Add(nomeElevador[i]);
            }
            percentuais.Add(perElevador);


            //Percentual Por Periodo
            Percentual perPeriodo = new Percentual();
            perPeriodo.porcentagem = new List<double>();
            perPeriodo.nome = new List<string>();
            string[] nomePeriodo = new string[3];
            nomePeriodo[0] = "M";
            nomePeriodo[1] = "V";
            nomePeriodo[2] = "N";
            for (int i = 0; i < listaQuantidadePorPeriodo.Count; i++)
            {
                double porcentagem = ((double)listaQuantidadePorPeriodo[i] / (double)listaElevador.Count) * 100;
                //total += Math.Round(porcentagem);
                perPeriodo.porcentagem.Add(Math.Round(porcentagem));
                perPeriodo.nome.Add(nomePeriodo[i]);
            }
            percentuais.Add(perPeriodo);

            return percentuais;
        }

    }

}
