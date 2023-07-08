namespace ClothingStore.Entities.Dtos
{
    public class ChangePasswordDto
    {
        public string Username { get; set; }
        public string PasswordOld { get; set; }
        public string PasswordNew { get; set; }
    }
}
