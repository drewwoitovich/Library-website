using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace c_final_capstone_v2.Models
{
    public class PollViewModel
    {
        public CreatePollModel CreatePoll { get; set; }
        public List<PollResultsModel> PollResults { get; set; } = new List<PollResultsModel>();
    }
}