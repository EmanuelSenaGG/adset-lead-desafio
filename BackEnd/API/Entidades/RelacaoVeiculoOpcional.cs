using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entidades;

[Table("RelacaoVeiculoOpcional")]
public  class RelacaoVeiculoOpcional
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("VeiculoID")]
    public int VeiculoId { get; set; }

    [Column("OpcionalID")]
    public int OpcionalId { get; set; }

    [ForeignKey("OpcionalId")]
    [InverseProperty("RelacaoVeiculoOpcional")]
    public virtual Opcional Opcional { get; set; } = null!;

    [ForeignKey("VeiculoId")]
    [InverseProperty("RelacaoVeiculoOpcional")]
    public virtual Veiculo Veiculo { get; set; } = null!;
}
