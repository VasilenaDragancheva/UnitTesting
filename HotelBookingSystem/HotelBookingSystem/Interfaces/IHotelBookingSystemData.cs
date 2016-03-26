namespace HotelBookingSystem.Interfaces
{
    using Models;

    public interface IHotelBookingSystemData
    {
        IUserRepository Users { get; }

        IRepository<Venue> Venues { get; }

        IRepository<Room> Rooms { get; }

        IRepository<Booking> Bookings { get; }
    }
}