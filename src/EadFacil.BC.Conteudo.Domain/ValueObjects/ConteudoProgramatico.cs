namespace EadFacil.BC.Conteudo.Domain.ValueObjects;

public class ConteudoProgramatico
{
    public string Ementa { get; private set; }
    public string Materiais { get; private set; }
    

    public ConteudoProgramatico(string ementa, string materiais)
    {
        Ementa = ementa;
        Materiais = materiais;
    }
    public void Atualizar(string ementa, string materiais)
    {
        Ementa = ementa;
        Materiais = materiais;
    }
}