namespace CodeClash.Core.Models.DTOs;

public class RoomDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<UserDTO> Users { get; set; }
}