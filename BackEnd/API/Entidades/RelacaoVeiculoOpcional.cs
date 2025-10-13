using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entidades;

public partial class RelacaoVeiculoOpcional
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

    [JsonIgnore]
    [ForeignKey("VeiculoId")]
    [InverseProperty("RelacaoVeiculoOpcional")]
    public virtual Veiculo Veiculo { get; set; } = null!;
}
