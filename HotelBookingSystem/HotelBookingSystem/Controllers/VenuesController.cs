namespace HotelBookingSystem.Controllers
{
    using System;

    using Infrastructure;

    using Interfaces;

    using Models;

    public class VenuesController : Controller
    {
        public VenuesController(IHotelBookingSystemData data, User user)
            : base(data, user)
        {
        }

        public IView Add(string name, string address, string description)
        {
            var newVenue = new Venue(name, address, description, this.CurrentUser);
            this.Data.Venues.Add(newVenue);
            return this.View(newVenue);
        }

        public IView All()
        {
            var venues = this.Data.Venues.GetAll();
            return this.View(venues);
        }

        public IView Details(int id)
        {
            this.Authorize(Roles.User, Roles.VenueAdmin);
            var venue = this.Data.Venues.Get(id);
            if (venue == null)
            {
                return this.NotFound(string.Format("The venue with ID {0} does not exist.", id));
            }

            return this.View(venue);
        }

        public IView Rooms(int id)
        {
            // TODO: Implement me
            throw new NotImplementedException();
        }
    }
}