using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPAPI.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Julian";
        public int HitPoints { get; set; } = 95;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Roles { get; set; } = RpgClass.Paladin;
  }
}