using System;
using System.Collections.Generic;
using VeloTours.Models.Extensions;

namespace VeloTours.Models
{
    public partial class Segment
    {
        public bool IsClimb
        {
            get
            {
                return this.Info.ClimbCategory != "NC";
            }
        }
    }
}
