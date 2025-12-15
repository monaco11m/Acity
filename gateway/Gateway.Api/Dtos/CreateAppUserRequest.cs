namespace Gateway.Api.Dtos
{
    public class CreateAppUserRequest
    {
        public AppUserDto User { get; set; }
        public string password { get; set; }
    }
}
