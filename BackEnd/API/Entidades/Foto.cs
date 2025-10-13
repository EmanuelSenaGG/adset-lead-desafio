using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entidades;

public partial class Foto
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("VeiculoID")]
    public int VeiculoId { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Arquivo { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string Path { get; set; } = null!;

    [ForeignKey("VeiculoId")]
    [InverseProperty("Foto")]
    [JsonIgnore]
    public virtual Veiculo Veiculo { get; set; } = null!;
}
