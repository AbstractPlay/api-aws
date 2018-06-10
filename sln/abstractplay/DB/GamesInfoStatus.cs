﻿using System;
using System.Collections.Generic;

namespace abstractplay.DB
{
    public partial class GamesInfoStatus
    {
        public byte[] StatusId { get; set; }
        public byte[] GameId { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsUp { get; set; }
        public string Message { get; set; }

        public GamesInfo Game { get; set; }
    }
}
