using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPAPI.Models;

public partial class TblRefreshToken
{
    [Key]
    [Column("userid")]
    [StringLength(50)]
    [Unicode(false)]
    public string UserId { get; set; } = null!;

    [Column("tokenid")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TokenId { get; set; }

    [Column("refreshtoken")]
    [Unicode(false)]
    public string? RefreshToken { get; set; }
}
