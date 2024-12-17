using static CodeClash.Core.Constants.Constants;
using CSharpFunctionalExtensions;
namespace CodeClash.Core.Models;

public class Room
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public TimeOnly Time { get; private set; }
    public Issue Issue { get; private set;}
    public List<User> Participants { get; private set; }
    
    private Room(Guid id, string name, TimeOnly time, Issue issue)
    {
        Id = id;
        Name = name;
        Issue = issue;
        Time = time;
        Participants = [];
    }

    public static Result<Room> Create(Guid id, string name, TimeOnly time, Issue issue)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > MaxNameLength)
            return Result.Failure<Room>("Incorrect name length.");
        var room = new Room(id, name, time, issue);
        return Result.Success(room);
    }

    public void AddParticipant(User participant) => Participants.Add(participant);
    
}