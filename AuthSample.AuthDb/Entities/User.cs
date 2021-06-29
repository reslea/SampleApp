namespace AuthSample.AuthDb.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public int? RefreshTokenId { get; set; }

        public RefreshToken RefreshToken { get; set; }
    }
}