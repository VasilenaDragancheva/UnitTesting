namespace HotelBookingSystem.Data
{
    using Interfaces;

    using Models;

    public class HotelBookingSystemData : IHotelBookingSystemData
    {
        public HotelBookingSystemData()
        {
            this.Users = new UserRepository();
            this.Venues = new Repository<Venue>();
            this.Rooms = new Repository<Room>();
            this.Bookings = new Repository<Booking>();
        }

        public HotelBookingSystemData(IRepository<Room> rooms)
        {
            this.Users = new UserRepository();
            this.Venues = new Repository<Venue>();
            this.Rooms = rooms;
            this.Bookings = new Repository<Booking>();
        }

        public IUserRepository Users { get; private set; }

        public IRepository<Venue> Venues { get; set; }

        public IRepository<Room> Rooms { get; set; }

        public IRepository<Booking> Bookings { get; set; }
    }
}