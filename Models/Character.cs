using System;
using System.Collections.Generic;

namespace ASPAPI.Models;

public partial class Character
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int HitPoints { get; set; }

    public int Strength { get; set; }

    public int Defense { get; set; }

    public int Intelligence { get; set; }

    public int Roles { get; set; }
}
