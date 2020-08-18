using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace CampChetek.Models
{
    public class Events
    {
        [Key]
        public int event_id { get; set; }

        [Required(ErrorMessage = "Please enter a time of Arrival.")]
        public DateTime event_start { get; set; }

        [Required(ErrorMessage = "Please enter a time of Departure.")]
        public DateTime event_end { get; set; }

        [Required(ErrorMessage = "Please enter your First Name.")]
        [RegularExpression("(?i)^[a-z0-9 ]+$",
            ErrorMessage = "First Name may not contain special characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your Last Name.")]
        [RegularExpression("(?i)^[a-z0-9 ]+$",
            ErrorMessage = "Last Name may not contain special characters.")]
        public string LastName { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        [Required(ErrorMessage = "Please enter a Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter an Address.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter a City.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a State.")]
        public string State { get; set; }

        public int Zip { get; set; }

        [Required(ErrorMessage = "Please enter a Phone.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter a number of Guest.")]
        public int NumberGuest { get; set; }

        public string Message { get; set; }

        public int RoomsId { get; set; }
        public Rooms Rooms { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string BeddingId { get; set; }
        public Bedding Bedding { get; set; }

        public string DateUpdate { get; set; }

        public bool all_day { get; set; }

        public bool sendEmail { get; set; }

        public string ReasonId { get; set; }
        public Reason Reason { get; set; }

    }
}
