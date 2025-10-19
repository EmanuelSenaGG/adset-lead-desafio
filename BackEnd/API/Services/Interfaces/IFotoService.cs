using API.Dtos.Foto;
using API.Entidades;

namespace API.Services.Interfaces
{
    public interface IFotoService
    {
        Task<FotoDto> EditarFotoVeiculoAsync(int idFoto, IFormFile foto);
        Task DeletarFotoAsync(int idFoto);
    }
}
