using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entidades;

public partial class Opcional
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string Descricao { get; set; } = null!;

    [JsonIgnore]
    [InverseProperty("Opcional")]
    public virtual ICollection<RelacaoVeiculoOpcional> RelacaoVeiculoOpcional { get; set; } = new List<RelacaoVeiculoOpcional>();
}
