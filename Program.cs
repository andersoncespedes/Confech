public class UniCount{
    public string Nombre {get; set;}
    public int Aceptado {get; set;}
    public int Rechazado {get; set;}
    public int Nulo {get; set;}
    public int Blanco {get; set;}
}
public static class Program
{
    public static Dictionary<string, List<string>> UniversidadesVotar = new ();
    public static void MenuVotos()
    {
        string voto = "m";
        List<string> votos = new();
        Console.Write("Ingrese el Nombre de la universidad -> ");
        string  NombreUni = Console.ReadLine();
        UniversidadesVotar.Keys.Append(NombreUni);
        while (voto.ToUpper() != "X")
        {
            Console.Clear();
            Console.WriteLine(@$"Ingrese Su Voto Para la Universidad {NombreUni}
A:Aceptar
R:Rechazar
N:Nulo
B:Blanco");
            Console.Write("-> ");
            voto = Console.ReadLine();
            votos.Add(new string[]{"B", "A", "R"}.Any(e => e == voto.ToUpper()) ? voto.ToUpper() : "N");
            
        }
        UniversidadesVotar[NombreUni] = votos;
    }
    public static IEnumerable<UniCount> ConteoVotos(){
        List<UniCount> Contador = new();
        foreach(var i in UniversidadesVotar.Keys){
            UniCount unidades = new UniCount();
            unidades.Nombre = i;
            unidades.Aceptado = UniversidadesVotar[i].Where(e => e == "A").Count();
            unidades.Rechazado = UniversidadesVotar[i].Where(e => e == "R").Count();
            unidades.Nulo = UniversidadesVotar[i].Where(e => e == "B").Count();
            Contador.Add(unidades);
        }
        return Contador;
    }
    public static void PanelContador(){
         foreach(var x in ConteoVotos()){
            Console.WriteLine("=========================");
            Console.WriteLine($"Universidad {x.Nombre}");
            Console.WriteLine($"Aceptados {x.Aceptado}");
            Console.WriteLine($"Rechazados {x.Rechazado}");
            Console.WriteLine($"Nulos {x.Nulo}");
            Console.WriteLine($"Blanco {x.Blanco}");
        }
    }
    public static void PanelFinal(){
            int aceptados = ConteoVotos().Where(e => e.Aceptado > e.Rechazado).Count();
            int rechazados = ConteoVotos().Where(e => e.Rechazado > e.Aceptado).Count();
            int empatados = ConteoVotos().Where(e => e.Rechazado == e.Aceptado).Count();
            Console.WriteLine("=========================");
            Console.WriteLine("Resultados : ");
            Console.WriteLine($"Universidades que Aceptan {aceptados}");
            Console.WriteLine($"Universidades que Rechazan {rechazados}");
            Console.WriteLine($"Universidades en Empate {empatados}");

    }
    public static void Main(string[] args)
    {
        int Universidades;
        Console.Write("Ingrese el numero de universidades -> ");
        int.TryParse(Console.ReadLine(), out Universidades);
        for (int i = 0; i < Universidades; i++)
        {
            try{
                MenuVotos();
            }catch(Exception err){
                Console.WriteLine(err.Message);
            }
            
        }
        PanelContador();
       PanelFinal();
    }
}