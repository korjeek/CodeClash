namespace CodeClash.Core.Models.DTOs;

public class RoomDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public TimeOnly Time { get; set; }
    public string IssueName { get; set; }
    public List<UserDTO>? Users { get; set; }
    public int ParticipantsCount { get; set; }
}