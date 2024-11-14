using CodeClash.Core.Models;

namespace CodeClash.Core.Entities;

public class Room(Guid id, List<Problem> problems, Room.RoomStatus status)
{
    public Guid Id { get; } = id;
    public List<Problem> Problems { get; } = problems;
    private RoomStatus Status { get; set; } = status;
    private List<User> Participants { get; } = [];
    private Competition _competition = new();
    
    public void AddParticipant(User participant)
    {
        if (Status is RoomStatus.WaitingForParticipants)
            Participants.Add(participant);
        else
            throw new InvalidOperationException("Cannot add participant. Competition in progress!");
    }
    
    public void StartCompetition()
    {
        if (Status is RoomStatus.WaitingForParticipants)
        {
            Status = RoomStatus.CompetitionInProgress;
            _competition.Start();
        }
        else 
            throw new InvalidOperationException("Competition is already started!");
    }
    
    public void FinishCompetition()
    {
        if (Status is RoomStatus.CompetitionInProgress)
            Status = RoomStatus.WaitingForParticipants;
        else
            throw new InvalidOperationException("Competition is not started!");
    }
    
    public enum RoomStatus
    {
        WaitingForParticipants = 0,
        CompetitionInProgress = 1
    }
}