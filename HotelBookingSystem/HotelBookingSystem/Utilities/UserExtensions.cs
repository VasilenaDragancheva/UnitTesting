namespace HotelBookingSystem.Utilities
{
    using Models;

    public static class UserExtensions
    {
        public static bool IsInRole(this User user, Roles role)
        {
            return user.Role == role;
        }
    }
}