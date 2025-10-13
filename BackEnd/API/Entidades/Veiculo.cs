
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entidades;

public partial class Veiculo
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Marca { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string Modelo { get; set; } = null!;

    public int Ano { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Placa { get; set; } = null!;

    public int? Km { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Cor { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Preco { get; set; }

    [InverseProperty("Veiculo")]
    public virtual ICollection<Foto> Foto { get; set; } = new List<Foto>();

    [InverseProperty("Veiculo")]
    public virtual ICollection<RelacaoVeiculoOpcional> RelacaoVeiculoOpcional { get; set; } = new List<RelacaoVeiculoOpcional>();

    [InverseProperty("Veiculo")]
    public virtual ICollection<RelacaoVeiculoPacotePortal> RelacaoVeiculoPacotePortal { get; set; } = new List<RelacaoVeiculoPacotePortal>();




}
