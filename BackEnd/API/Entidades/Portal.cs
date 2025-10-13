using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace API.Entidades;

public partial class Portal
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [JsonIgnore]
    [InverseProperty("Portal")]
    public virtual ICollection<Pacote> Pacote { get; set; } = new List<Pacote>();

    [JsonIgnore]
    [InverseProperty("Portal")]
    public virtual ICollection<RelacaoVeiculoPacotePortal> RelacaoVeiculoPacotePortal { get; set; } = new List<RelacaoVeiculoPacotePortal>();
}
