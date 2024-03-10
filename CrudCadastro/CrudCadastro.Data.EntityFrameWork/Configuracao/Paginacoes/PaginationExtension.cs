using Microsoft.EntityFrameworkCore;

namespace CrudCadastro.Data.EntityFrameWork.Configuracao.Paginacoes;

public static class PaginationExtension
{
    public static async Task<PageList<T>> CreatePagenationAsync<T>(
        IQueryable<T> source, int pageNumber, int itensBypage)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * itensBypage).Take(itensBypage).ToListAsync();
           
        return new PageList<T>(items, pageNumber, itensBypage,count);
    }
}