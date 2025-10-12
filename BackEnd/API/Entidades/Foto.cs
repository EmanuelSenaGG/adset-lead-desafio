using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entidades;

[Table("Foto")]
public  class Foto
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
}
