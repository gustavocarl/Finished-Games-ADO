namespace FinishedGamesADO.Models;

public class Game : Base
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Platform { get; set; } = string.Empty;

    public string Genre { get; set; } = string.Empty;

    public string Grade { get; set; } = string.Empty;
}