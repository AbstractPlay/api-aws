﻿using System;
using System.Collections.Generic;

namespace abstractplay.DB
{
    public partial class Owners
    {
        public Owners()
        {
            Challenges = new HashSet<Challenges>();
            ChallengesPlayers = new HashSet<ChallengesPlayers>();
            Consoles = new HashSet<Consoles>();
            ConsolesVotes = new HashSet<ConsolesVotes>();
            DmsReceiver = new HashSet<Dms>();
            DmsSender = new HashSet<Dms>();
            GamesDataChats = new HashSet<GamesDataChats>();
            GamesDataClocks = new HashSet<GamesDataClocks>();
            GamesDataPlayers = new HashSet<GamesDataPlayers>();
            GamesDataWhoseturn = new HashSet<GamesDataWhoseturn>();
            GamesMetaRanks = new HashSet<GamesMetaRanks>();
            GamesMetaTags = new HashSet<GamesMetaTags>();
            OwnersNames = new HashSet<OwnersNames>();
        }

        public byte[] OwnerId { get; set; }
        public byte[] CognitoId { get; set; }
        public byte[] PlayerId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ConsentDate { get; set; }
        public string Country { get; set; }
        public bool Anonymous { get; set; }
        public string Tagline { get; set; }

        public virtual ICollection<Challenges> Challenges { get; set; }
        public virtual ICollection<ChallengesPlayers> ChallengesPlayers { get; set; }
        public virtual ICollection<Consoles> Consoles { get; set; }
        public virtual ICollection<ConsolesVotes> ConsolesVotes { get; set; }
        public virtual ICollection<Dms> DmsReceiver { get; set; }
        public virtual ICollection<Dms> DmsSender { get; set; }
        public virtual ICollection<GamesDataChats> GamesDataChats { get; set; }
        public virtual ICollection<GamesDataClocks> GamesDataClocks { get; set; }
        public virtual ICollection<GamesDataPlayers> GamesDataPlayers { get; set; }
        public virtual ICollection<GamesDataWhoseturn> GamesDataWhoseturn { get; set; }
        public virtual ICollection<GamesMetaRanks> GamesMetaRanks { get; set; }
        public virtual ICollection<GamesMetaTags> GamesMetaTags { get; set; }
        public virtual ICollection<OwnersNames> OwnersNames { get; set; }
    }
}
