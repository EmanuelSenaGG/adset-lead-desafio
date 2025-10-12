using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entidades;

[Table("Pacote")]
public  class Pacote
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [Column("PortalID")]
    public int PortalId { get; set; }

    [ForeignKey("PortalId")]
    [InverseProperty("Pacote")]
    public virtual Portal Portal { get; set; } = null!;

    [InverseProperty("Pacote")]
    public virtual ICollection<RelacaoVeiculoPacotePortal> RelacaoVeiculoPacotePortal { get; set; } = new List<RelacaoVeiculoPacotePortal>();
}
