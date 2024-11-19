namespace CodeClash.Core.Models;

public class Room(TimeOnly time, Issue issues)
{
    public Guid Id { get; } = Guid.NewGuid();
    public Issue Problems { get; } = issues;
    public TimeOnly Time { get; } = time;
    private RoomStatus Status { get; set; } = RoomStatus.WaitingForParticipants;
    private List<User> Participants { get; } = [];
    private Competition _competition = new();
    
    public void AddParticipant(User participant)
    {
        if (Status is RoomStatus.WaitingForParticipants)
            Participants.Add(participant);
        else
            throw new InvalidOperationException("Cannot add participant. Competition in progress!");
    }
    
    public void RemoveParticipant(User participant)
    {
        
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