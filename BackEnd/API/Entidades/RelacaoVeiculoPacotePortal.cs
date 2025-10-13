using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace API.Entidades;

[Index("VeiculoId", "PortalId", Name = "UQ_Veiculo_Portal", IsUnique = true)]
public partial class RelacaoVeiculoPacotePortal
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("VeiculoID")]
    public int VeiculoId { get; set; }

    [Column("PacoteID")]
    public int PacoteId { get; set; }

    [Column("PortalID")]
    public int PortalId { get; set; }

    [ForeignKey("PacoteId")]
    [InverseProperty("RelacaoVeiculoPacotePortal")]
    public virtual Pacote Pacote { get; set; } = null!;

    [JsonIgnore]
    [ForeignKey("PortalId")]
    [InverseProperty("RelacaoVeiculoPacotePortal")]
    public virtual Portal Portal { get; set; } = null!;

    [JsonIgnore]
    [ForeignKey("VeiculoId")]
    [InverseProperty("RelacaoVeiculoPacotePortal")]
    public virtual Veiculo Veiculo { get; set; } = null!;
}
