public class Enums
{
    public enum CardState
    {
        Hidden,
        Visible,
        Matched
    }
    
    public enum CardEventType
    {
        CardFlipped
    }
    public enum GridEventType
    {
        CardFailed,
        CardMatched,
        AllCardsMatched

    }
    public enum GameLevel
    {
        VeryEasy,
        Easy,
        Medium,
        Hard,
        VeryHard
    }
}
