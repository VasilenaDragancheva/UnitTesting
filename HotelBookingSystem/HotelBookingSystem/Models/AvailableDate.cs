namespace HotelBookingSystem.Models
{
    using System;

    // Egyptian brackets FTW!
    public class AvailableDate
    {
        public AvailableDate(DateTime startDate, DateTime endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
            if (endDate < startDate)
            {
                throw new ArgumentException("The date range is invalid.");
            }
        }

        public DateTime StartDate { get; internal set; }

        public DateTime EndDate { get; internal set; }
    }
}