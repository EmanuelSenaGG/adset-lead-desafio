using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entidades;

[Table("Opcional")]
public  class Opcional
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string Descricao { get; set; } = null!;

    [InverseProperty("Opcional")]
    public virtual ICollection<RelacaoVeiculoOpcional> RelacaoVeiculoOpcional { get; set; } = new List<RelacaoVeiculoOpcional>();
}
