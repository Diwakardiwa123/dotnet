namespace Appointment.WebAPI.Service
{
    public abstract class IUserService
    {
        public string userID { get; private set; }

        public virtual void SetUserID(string id)
        {
            userID = id;
        }

        public virtual string GetUserID()
        {
            return userID;
        }
    }
}
