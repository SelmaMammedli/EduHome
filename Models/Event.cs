﻿using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Models
{
    public class Event
    {
        //public Event()
        //{
        //    EventSpeakers = new();
        //}
        public int Id { get; set; }
        public string Title { get; set; }
        public string Venue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public string? ShortDesc => Description.Length > 10 ? Description.Substring(0, 10) + "..." : Description;
        public List<EventSpeaker> EventSpeakers { get; set; }
        [NotMapped]
        public List<int>SpeakerIds { get; set; }
    }
}
