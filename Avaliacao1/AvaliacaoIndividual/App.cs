namespace advocacia;
public class App {
    List<Advogado> advogados;
    List<Cliente> clientes;

    public App() {
        advogados = new();
        clientes = new();

        try {
            insereAdvogado("123456789123", "Advogado 1", "12345678901", "17/07/2001");
            insereAdvogado("123456789323", "Advogado 2", "12345678902", "17/07/2001");
            insereAdvogado("123456789123", "Advogado 1", "12345678911", "17/12/2001");
            insereCliente("Cliente 1", "12345678912", "17/12/2001", "Solteiro", "Empregado");
            insereCliente("Cliente 2", "12345678911", "17/12/2001", "Solteiro", "Empregado");
            listaAdvogados();
            Console.WriteLine($"-----------------");            
            listaClientes();
        } catch (Exceptions.UniqueValueException ex) {
            Console.WriteLine($"{ex.Message}");
            
        }
    }

    public void insereAdvogado(string cna, string nome, string cpf, string nascimento) {
        if(this.advogados.Any(x => x.Cpf == cpf && x.Cna == cna)){
            throw new Exceptions.UniqueValueException("CPF e CNA precisam ser únicos!");
        }

        try {
            DateOnly dataNascimento = DateOnly.Parse(nascimento);
            Advogado advogado = new Advogado(cna, nome, cpf, dataNascimento);
            this.advogados.Add(advogado);
        } catch (Exceptions.EmptyInputException ex) {
            Console.WriteLine($"{ex.Message}");            
        } catch (Exceptions.InvalidCpfException ex) {
            Console.WriteLine($"{ex.Message}");            
        } catch (Exceptions.InvalidCnaException ex) {
            Console.WriteLine($"{ex.Message}");            
        } catch (Exceptions.InvalidDateException ex) {
            Console.WriteLine($"{ex.Message}");            
        } catch (Exception ex) {
            Console.WriteLine($"Ocorreu um erro: {ex.Message}");            
        }
    }

    public void insereCliente(string nome, string cpf, string nascimento, string estadoCivil, string profissao) {
        if(this.clientes.Any(x => x.Cpf == cpf)){
            throw new Exceptions.UniqueValueException("CPF precisa ser único!");
        }

        try {
            DateOnly dataNascimento = DateOnly.Parse(nascimento);
            Cliente cliente = new Cliente(nome, cpf, dataNascimento, estadoCivil, profissao);
            this.clientes.Add(cliente);
        } catch (Exceptions.EmptyInputException ex) {
            Console.WriteLine($"{ex.Message}");            
        } catch (Exceptions.InvalidCpfException ex) {
            Console.WriteLine($"{ex.Message}");            
        } catch (Exceptions.InvalidDateException ex) {
            Console.WriteLine($"{ex.Message}");            
        } catch (Exception ex) {
            Console.WriteLine($"Ocorreu um erro: {ex.Message}");            
        }
    }

    public void listaAdvogados(){
        foreach(Advogado advogado in this.advogados){
            Console.WriteLine($"{advogado.Nome}");            
        }
    }

    public void listaClientes(){
        foreach(Cliente cliente in this.clientes){
            Console.WriteLine($"{cliente.Nome}");            
        }
    }

    public void advogadosEntreIdade(string minIdade, string maxIdade){
        int min, max;
        try {
            min = int.Parse(minIdade);
            max = int.Parse(maxIdade);
        } catch(FormatException) {
            Console.WriteLine($"Insira valores válidos para as idades");
            return;            
        }

        foreach(Advogado advogado in this.advogados.Where(x => x.idade() < min && x.idade() > max).ToList()){
            Console.WriteLine(advogado.ToStr());
        }
    }

    public void clientesEntreIdade(string minIdade, string maxIdade){
        int min, max;
        try {
            min = int.Parse(minIdade);
            max = int.Parse(maxIdade);
        } catch(FormatException) {
            Console.WriteLine($"Insira valores válidos para as idades");
            return;            
        }

        foreach(Cliente cliente in this.clientes.Where(x => x.idade() < min && x.idade() > max).ToList()){
            Console.WriteLine(cliente.ToStr());
        }
    }

    public void clientesPorEstadoCivil(string estadoCivil){
        foreach(Cliente cliente in this.clientes.Where(x => x.EstadoCivil.Equals(estadoCivil)).ToList()){
            Console.WriteLine(cliente.ToStr());
        }
    }

    public void clientesOrdemAlfabetica(){
        foreach(Cliente cliente in this.clientes.OrderBy(x => x.Nome).ToList()){
            Console.WriteLine(cliente.ToStr());
        }
    }

    public void advogadosPorKeyword(string keywords){
        foreach(Cliente cliente in this.clientes.Where(x => x.Profissao.Contains(keywords)).ToList()){
            Console.WriteLine(cliente.ToStr());
        }
    }

    public pessoasAniversariantesDoMes(){
        List<Pessoa> pessoas = this.clientes.Concat(this.advogados);

        foreach(Pessoa pessoa in pessoas){
            Console.WriteLine($"{pessoa.ToStr}");            
        }
    }
}
