namespace CrudCadastro.Data.EntityFrameWork.Configuracao.Paginacoes;

public class PageList<T> : List<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int ItensByPage { get; set; }
    public int TotalItens { get; set; }

    public PageList(IEnumerable<T> items, int pageNumber, int itensbyPage, int count)
    {
        CurrentPage = pageNumber;
        TotalPages = (int) Math.Ceiling(count/ (double) itensbyPage);
        ItensByPage = itensbyPage;
        TotalItens = count;
        
        AddRange(items);
    }
}