using CodeClash.Core.Models;

namespace CodeClash.Core.Entities;

public class Competition
{
    private Issue issue;
    private bool _inProgress;
    // private string _usersCode;
    
    public void Start()
    {
        _inProgress = true;
        UpdateIncomingSolution();
        //...
    }
    
    private void UpdateIncomingSolution()
    {
        while (_inProgress)
        {
            
        }
    }
    
    // private string IncomingSolution()
    // {
    //     
    // }
}