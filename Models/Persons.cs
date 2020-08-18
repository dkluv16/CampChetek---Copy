using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CampChetek.Models
{
    public class Persons : DbContext
    {
        public Persons(DbContextOptions<Persons> options)
            : base(options)
        { }

        public DbSet<Events> events { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Rooms> Room { get; set; }
        public DbSet<Bedding> beddings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reason> Reasons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Events>().HasData(
                new Events {event_id = 1,  event_start = DateTime.Now, event_end = DateTime.Today, FirstName = "Randy", LastName = "Tanis", Email = "pr@campchetek.org", Address = "730 Lakeview Drive", City = "Chetek", State = "WI", Zip = 54728, Phone = "715 924-3236", BeddingId = "A", NumberGuest = 2, Message = "", RoomsId = 30, UserId = 1, all_day = false, title = "Test ", description = "test ", sendEmail = false, ReasonId = "J"},
                new Events {event_id = 2,  event_start = DateTime.Now, event_end = DateTime.Today, FirstName = "Noah", LastName = "Rost", Email = "noah@campchetek.org", Address = "730 Lakeview Drive", City = "Chetek", State = "WI", Zip = 54728, Phone = "715 924-3236", BeddingId = "B", NumberGuest = 1, Message = "", RoomsId = 30, UserId = 2, all_day = false, title = "Test2 ", description = "test2 ", sendEmail = false, ReasonId = "J"}
            );
            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = "C", Name = "Clean" },
                new Status { StatusId = "D", Name = "Dirty"},
                new Status { StatusId = "O", Name = "Occupied"},
                new Status { StatusId = "R", Name = "Ready"}
            );
            modelBuilder.Entity<Rooms>().HasData(
                new Rooms { RoomsId = 1, Name = "Schnick 1", StatusId = "D"},
                new Rooms { RoomsId = 2, Name = "Schnick 2", StatusId = "D"},
                new Rooms { RoomsId = 3, Name = "Schnick 3", StatusId = "D"},
                new Rooms { RoomsId = 4, Name = "Schnick 4", StatusId = "D"},
                new Rooms { RoomsId = 5, Name = "Tomah 1", StatusId = "D"},
                new Rooms { RoomsId = 6, Name = "Tomah 2", StatusId = "D"},
                new Rooms { RoomsId = 7, Name = "Tomah 3", StatusId = "D" },
                new Rooms { RoomsId = 8, Name = "Tomah 4", StatusId = "D" },
                new Rooms { RoomsId = 9, Name = "New Lisbon 1", StatusId = "D" },
                new Rooms { RoomsId = 10, Name = "New Lisbon 2", StatusId = "D" },
                new Rooms { RoomsId = 11, Name = "Chetek", StatusId = "D" },
                new Rooms { RoomsId = 12, Name = "RV Park", StatusId = "D" },
                new Rooms { RoomsId = 13, Name = "Sanasac 1", StatusId = "D" },
                new Rooms { RoomsId = 14, Name = "Sanasac 2", StatusId = "D" },
                new Rooms { RoomsId = 15, Name = "Sanasac 3", StatusId = "D" },
                new Rooms { RoomsId = 16, Name = "Sanasac 4", StatusId = "D" },
                new Rooms { RoomsId = 17, Name = "Dining Hall 1", StatusId = "D" },
                new Rooms { RoomsId = 18, Name = "Dining Hall 2", StatusId = "D" },
                new Rooms { RoomsId = 19, Name = "Dining Hall 3", StatusId = "D" },
                new Rooms { RoomsId = 20, Name = "Dining Hall 4", StatusId = "D" },
                new Rooms { RoomsId = 21, Name = "Dining Hall 5", StatusId = "D" },
                new Rooms { RoomsId = 22, Name = "Hotel 1", StatusId = "D" },
                new Rooms { RoomsId = 23, Name = "Hotel 2", StatusId = "D" },
                new Rooms { RoomsId = 24, Name = "Hotel 3", StatusId = "D" },
                new Rooms { RoomsId = 25, Name = "Hotel 4", StatusId = "D" },
                new Rooms { RoomsId = 26, Name = "Hotel 5", StatusId = "D" },
                new Rooms { RoomsId = 27, Name = "Hotel 6", StatusId = "D" },
                new Rooms { RoomsId = 28, Name = "Hotel 7", StatusId = "D" },
                new Rooms { RoomsId = 29, Name = "Hotel 8", StatusId = "D" },
                new Rooms { RoomsId = 30, Name = "None", StatusId = "D" }
                );
            modelBuilder.Entity<User>().HasData(
                new User {userId = 1, FirstName = "Dan", LastName = "Kluver", Email = "danael@campchetek.org", Password = "cccc1944ad"},
                new User {userId = 2, FirstName = "Marikka", LastName = "Kartman", Email = "marikkakay@yahoo.com", Password = "Marikka0410"},
                new User {userId = 3, FirstName = "System", LastName = "1", Email = "system@campchetek.org", Password = "12Ers45Bd5sS@"},
                new User {userId = 4, FirstName = "Randy", LastName = "Tanis", Email = "pr@campchetek.org", Password = "Pronly3236"},
                new User {userId = 5, FirstName = "First", LastName = "Last", Email = "email@goeshere.org", Password = "X234Veeetw123"}
                );
            modelBuilder.Entity<Bedding>().HasData(
                new Bedding { BeddingId = "A", Name = "1 Queen"},
                new Bedding { BeddingId = "B", Name = "1 Twin"},
                new Bedding { BeddingId = "C", Name = "1 Queen, 1 Twin"},
                new Bedding { BeddingId = "D", Name = "1 Queen, 2 Twin"},
                new Bedding { BeddingId = "E", Name = "1 Queen, 3 Twin"},
                new Bedding { BeddingId = "F", Name = "1 Queen, 4 Twin"},
                new Bedding { BeddingId = "G", Name = "1 Queen, 5 Twin"},
                new Bedding { BeddingId = "H", Name = "1 Queen, 6 Twin"}
                );
            modelBuilder.Entity<Reason>().HasData(
                new Reason { ReasonId = "A", Name = "Pastor"},
                new Reason { ReasonId = "B", Name = "Missionary"},
                new Reason { ReasonId = "C", Name = "Evanglist"},
                new Reason { ReasonId = "D", Name = "Former Staff"},
                new Reason { ReasonId = "E", Name = "Former Camper"},
                new Reason { ReasonId = "F", Name = "Rental"},
                new Reason { ReasonId = "G", Name = "Family Of Staff"},
                new Reason { ReasonId = "H", Name = "Sponsor"},
                new Reason { ReasonId = "I", Name = "Attending An Event"},
                new Reason { ReasonId = "J", Name = "Other"}

                );
            
        }
       }
}
