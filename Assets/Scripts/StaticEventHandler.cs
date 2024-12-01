using System;

public static class StaticEventHandler
{

    public static event Action<GameOverArgs> OnGameOver;
    public static event Action<GameWonArgs> OnGameWon;
    public static event Action<RerollArgs> OnReroll;
    public static event Action<DiceUsedArgs> OnDiceUsed;

    public static void CallGameOverEvent(string title, string body)
    {
        OnGameOver?.Invoke(new GameOverArgs() { titleText = title, bodyText = body });
    }
    public static void CallGameWonEvent(string title, string body)
    {
        OnGameWon?.Invoke(new GameWonArgs() { titleText = title, bodyText = body });
    }
    public static void CallOnReroll()
    {
        OnReroll?.Invoke(new RerollArgs() { });
    }
    public static void CallOnDiceUsed()
    {
        OnDiceUsed?.Invoke(new DiceUsedArgs() { });
    }


}


public class GameOverArgs : EventArgs
{
    public string titleText;
    public string bodyText;
}
public class GameWonArgs : EventArgs
{
    public string titleText;
    public string bodyText;
}
public class RerollArgs : EventArgs
{
}

public class DiceUsedArgs : EventArgs
{

}
