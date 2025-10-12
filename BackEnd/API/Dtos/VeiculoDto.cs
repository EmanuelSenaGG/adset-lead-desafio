namespace API.Dtos
{
    public record VeiculoDto(
     int Id,
     string Marca,
     string Modelo,
     int Ano,
     string Placa,
     int? Km,
     string Cor,
     decimal Preco
 );

}
